using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public float movespeed = 0.05f;
    public Vector2 minMaxPos;
    public bool leftside;

    public float waitTime = 2f;
    public float speedMultiplier = 2f;
    public float grabDist = -0.5f;


    [HideInInspector] public float waitTimer = -10f;
    [HideInInspector] public bool grabbed = false;


    private float currentballspeed;
    private int lastType;
    private int throwdir;
    private Collider2D coll;

    [HideInInspector] public bool broken = false;

    public float respawnTime = 5f;
    private float respawnTimer = 5f;

    private float currentInput;

    void Start() {
        coll = GetComponent<Collider2D>();
    }

    void FixedUpdate() {


        if(broken) {
            if(Time.time > respawnTimer + respawnTime) {
                GetComponent<Renderer>().enabled = true;
            }
        }





        if(grabbed) {
            if(waitTimer + waitTime < Time.time) {
                Ball.activeBall.rb.simulated = true;
                Vector2 dir = new Vector2(0, -Ball.activeBall.minMaxSpeed.x);
                dir += (throwdir - 1) * new Vector2(Ball.activeBall.minMaxSpeed.x, 0);

                grabbed = false;
                Ball.activeBall.rb.velocity = dir.normalized * currentballspeed * speedMultiplier;
                transform.parent.rotation = Quaternion.identity;
            }
            
        }
        if(Time.time > waitTimer + waitTime + 0.3f) {
            coll.enabled = true;
        }


        if(!grabbed && !broken) {

            float relativepos = Ball.activeBall.transform.position.x - transform.parent.parent.position.x;
            float targetDir = Mathf.Clamp(relativepos, -1, 1);
            currentInput = targetDir;

            Vector3 newpos = transform.parent.parent.position;
            newpos.x = Mathf.Max(Mathf.Min(minMaxPos.y, newpos.x + currentInput * movespeed), minMaxPos.x);
            transform.parent.parent.position = newpos;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject go = collision.gameObject;
        if(go.layer == LayerMask.NameToLayer("Ball")) {
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            Ball b = go.GetComponent<Ball>();

            currentballspeed = rb.velocity.magnitude;

            if(currentballspeed != b.minMaxSpeed.y) {

                waitTimer = Time.time;
                grabbed = true;
                coll.enabled = false;
                Ball.activeBall.playerHit = false;
                Ball.activeBall.rb.simulated = false;

                //Select Type
                lastType = (int) Ball.activeBall.type;
                int type = RandomType();
                Ball.activeBall.type = (Ball.Type)(type);

                ThrowIndicator.activeIndicator.SetIndicator(throwdir, type);

                //Select Dir
                throwdir = Random.Range(0, 3);
                transform.parent.rotation = Quaternion.Euler(0,0,45*(throwdir-1));

                Ball.activeBall.transform.position = transform.position - new Vector3(-(throwdir - 1),1,0).normalized * grabDist;//Add rotation


            } else {
                Ball.activeBall.type = Ball.Type.Normal;

                broken = true;
                respawnTimer = Time.time;
                GetComponent<Renderer>().enabled = false;

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
