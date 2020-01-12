﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float movespeed = 0.2f;
    public float dashDist = 0.5f;
    public float hitMultiplier = 1.5f;

    public Vector2 minMaxPos;

    float input_hor;
    bool input_dash;

    public bool grabbed = false;
    public float pauseTime = 0.1f;
    private float pauseTimer = -1f;
    private Vector2 throwspeed;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        input_hor = Input.GetAxisRaw("Horizontal");
        input_dash = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl);
    }


    void FixedUpdate() {

        if(grabbed) {
            if( Time.time > pauseTimer + pauseTime) {
                Ball.activeBall.rb.isKinematic = false;
                Ball.activeBall.rb.velocity = throwspeed;
                grabbed = false;
                Camera.main.GetComponent<ScreenShake>().Shake(throwspeed.magnitude);
            }
        }

        //Vector3 newpos = transform.position;
        //newpos.x = Mathf.Max(Mathf.Min(minMaxPos.y, newpos.x + , minMaxPos.x);
        //transform.position = newpos;
        rb.velocity = new Vector2(input_hor * (input_dash ? dashDist : movespeed),0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject go = collision.gameObject;
        if(grabbed)
            return;
        if(go.layer == LayerMask.NameToLayer("Ball")) {
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            Ball b = go.GetComponent<Ball>();

            throwspeed = (rb.velocity.magnitude * new Vector2((transform.position - go.transform.position).normalized.x * -1f, 1)) * hitMultiplier;
            
            
            
            if(b.type != Ball.Type.Heal) {
                b.playerHit = true;
                //TODO make heal ball actually heal
            }




            b.rb.isKinematic = true;
            pauseTimer = Time.time;
            grabbed = true;
            
            
            
            //else if(!b.playerHit) {
            //    rb.velocity = rb.velocity.normalized * b.minMaxSpeed.x;
            //}


        }
    }

}
