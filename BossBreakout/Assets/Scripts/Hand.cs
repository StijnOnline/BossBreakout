using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public float movespeed = 0.05f;
    private float currentInput;
    public Vector2 minMaxPos;

    [HideInInspector] public float waitTimer = -10f;
    public float waitTime = 2f;
    [HideInInspector] public bool grabbed = false;
    private int throwdir;
    private float currentballspeed;
    public float speedMultiplier = 2f;
    public bool leftside;
    public Vector3 grabpos = new Vector3(0,-0.5f);

    private int lastType;
    private Collider2D coll;

    void Start() {
        coll = GetComponent<Collider2D>();
    }

    void FixedUpdate() {
        if(grabbed) {
            if(waitTimer + waitTime < Time.time) {
                Ball.activeBall.rb.simulated = true;
                Vector2 dir = new Vector2(0, -Ball.activeBall.minMaxSpeed.x);
                dir += (throwdir - 1) * new Vector2(Ball.activeBall.minMaxSpeed.x, 0);

                grabbed = false;
                Ball.activeBall.rb.velocity = dir.normalized * currentballspeed * speedMultiplier;
                coll.enabled = true;
            }
            if(waitTimer + waitTime + 0.5f < Time.time) {

            }
        }



        if(!grabbed) {

            float relativepos = Ball.activeBall.transform.position.x - transform.position.x;
            float targetDir = Mathf.Clamp(relativepos, -1, 1);
            currentInput = targetDir;

            Vector3 newpos = transform.localPosition;
            newpos.x = Mathf.Max(Mathf.Min(minMaxPos.y, newpos.x + currentInput * movespeed), minMaxPos.x);
            transform.localPosition = newpos;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject go = collision.gameObject;
        if(go.layer == LayerMask.NameToLayer("Ball")) {
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            Ball b = go.GetComponent<Ball>();

            currentballspeed = rb.velocity.magnitude;

            if(currentballspeed != b.minMaxSpeed.y) {

                grabbed = true;
                Ball.activeBall.playerHit = false;


                lastType = (int) Ball.activeBall.type;
                //int type = Random.Range(0, 4);
                //int type = 2;
                int type = RandomType();


                Ball.activeBall.type = (Ball.Type)(type);
                throwdir = Random.Range(0, 3);
                waitTimer = Time.time;

                ThrowIndicator.activeIndicator.SetIndicator(throwdir, type);



                Ball.activeBall.transform.position = transform.position - grabpos;
                Ball.activeBall.rb.simulated = false;

                coll.enabled = false;

            } else {
                Ball.activeBall.type = Ball.Type.Normal;

                Boss.activeBoss.DeactivateHand(leftside);
            }
        }
    }

    public int RandomType() {
        float r = Random.Range(0f, 1f);
        if(r < 0.6f) {
            return 0;
        }else if(lastType == 2 && r > 0.8) {
            return 1;
        } else {
            return 2;
        }
    }
}
