using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float movespeed = 0.05f;

    public float hitTime = 0.5f;
    private float hitTimer = 0;
    public float hitMultiplier = 1.5f;

    public Vector2 minMaxPos;

    private Renderer r;

    float input_hor;

    void Start() {
        //TODO remove temp:
        r = GetComponent<Renderer>();
    }

    void Update() {
        input_hor = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Z)) {
            hitTimer = Time.time;
        }
    }

    //TODO dash
    void FixedUpdate() {
        if(hitTimer + hitTime > Time.time) {
            //TODO remove temp:
            r.material.color = Color.blue;
        } else {
            //TODO remove temp:
            r.material.color = Color.white;
        }

        Vector3 newpos = transform.position;
        newpos.x = Mathf.Max(Mathf.Min(minMaxPos.y, newpos.x + input_hor * movespeed), minMaxPos.x);
        transform.position = newpos;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject go = collision.gameObject;
        if(go.layer == LayerMask.NameToLayer("Ball")) {
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            Ball b = go.GetComponent<Ball>();
            rb.velocity = (rb.velocity.magnitude * (transform.position - go.transform.position) * -1f);
            
            
            
            if(hitTimer + hitTime > Time.time && b.type != Ball.Type.Spiked) {
                b.playerHit = true;
                rb.velocity = rb.velocity * hitMultiplier;
            } else if(!b.playerHit) {
                //TODO remove temp:
                r.material.color = Color.red;
                rb.velocity = rb.velocity.normalized * b.minMaxSpeed.x;
            }


        }
    }

}
