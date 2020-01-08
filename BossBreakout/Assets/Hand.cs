using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    [Range(0, 1)]
    public float velocityInfluence;

    public float movespeed = 0.05f;
    private float currentInput;
    public Vector2 minMaxPos;

    private float waitTimer;
    public float waitTime = 2f;
    public bool grabbed = false;
    public int throwdir;

    void Start() {

    }

    void FixedUpdate() {

        if(grabbed) {


            if(waitTimer + waitTime < Time.time) { //start of grab
                Ball.activeBall.transform.position = transform.position - new Vector3(0, 0.5f);

                int r = Random.Range(0, 3);
                Ball.activeBall.type = (Ball.Type)r;
                throwdir = Random.Range(0, 3);
                waitTimer = Time.time; 

                //visuals
            }



            if(waitTimer + waitTime > Time.time) {
                Vector2 dir = new Vector2(0, Ball.activeBall.minMaxSpeed.x);
                dir += (throwdir - 1) * new Vector2(Ball.activeBall.minMaxSpeed.x,0);

                grabbed = false;
                Ball.activeBall.rb.velocity = dir;
            }
        } 
        
        
        
        if(!grabbed) {

            float relativepos = Ball.activeBall.transform.position.x - transform.position.x;
            float ballspeed = Ball.activeBall.rb.velocity.x;
            float targetDir = Mathf.Clamp(relativepos - ballspeed * velocityInfluence, -1, 1);
            currentInput = Mathf.Lerp(currentInput, targetDir, 0.1f);

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


            rb.velocity = Vector2.zero;
            grabbed = true;
        }
    }
}
