using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public float movespeed = 0.05f;
    private float currentInput;
    public Vector2 minMaxPos;

    private float waitTimer = -10f;
    public float waitTime = 2f;
    private bool grabbed = false;
    private int throwdir;
    private float currentballspeed;
    public float speedMultiplier = 2f;

    void Start() {

    }

    void FixedUpdate() {
        if(grabbed) {
            if(waitTimer + waitTime < Time.time) {
                Ball.activeBall.rb.simulated = true;
                Vector2 dir = new Vector2(0, -Ball.activeBall.minMaxSpeed.x);
                dir += (throwdir - 1) * new Vector2(Ball.activeBall.minMaxSpeed.x, 0);

                grabbed = false;
                Ball.activeBall.rb.velocity = dir.normalized * currentballspeed * speedMultiplier;
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

                rb.velocity = Vector2.zero;
                grabbed = true;
                Ball.activeBall.playerHit = false;

                int type = Random.Range(0, 3);
                Ball.activeBall.type = (Ball.Type)type;
                throwdir = Random.Range(0, 3);
                waitTimer = Time.time;

                ThrowIndicator.activeIndicator.SetIndicator(throwdir, type);



                Ball.activeBall.transform.position = transform.position - new Vector3(0, 0.5f);
                Ball.activeBall.rb.simulated = false;
            } else {
                gameObject.SetActive(false);
            }
        }
    }
}
