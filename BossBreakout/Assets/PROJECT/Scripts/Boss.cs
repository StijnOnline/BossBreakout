﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Block {



    public static Boss activeBoss;
    public bool startRandomTiles = false;

    public Hand hand1;
    public Hand hand2;
    public GameObject forceField;
    public Block[] blocks;

    public int stage = 0;
    public float handMoveDist = 1f;

    public float handRespawnTime = 5f;
    private float hand1RespawnTimer = 5f;
    private float hand2RespawnTimer = 5f;



    void Start() {
        activeBoss = this;
        invurnerable = true;
        if(startRandomTiles) ResetBlocks();
        forceField.SetActive(true);
    }

    public void Update() {

        //if(!hand1.gameObject.activeSelf) {
        //    if(Time.time > hand1RespawnTimer + handRespawnTime ) {
        //        //hand1.gameObject.SetActive(true);

        //        hand1.broken = false;

        //        //Ball.activeBall.rb.velocity = Ball.activeBall.minMaxSpeed.x * Vector2.one;
        //        //Ball.activeBall.rb.simulated = false;
        //        //Ball.activeBall.transform.position = hand1.transform.position - new Vector3(0, 0.5f);
        //        //hand1.grabbed = true;
        //        //hand1.waitTimer = Time.time;
        //    }
        //}
        //if(!hand2.gameObject.activeSelf) {
        //    if(Time.time > hand2RespawnTimer + handRespawnTime) {
        //        hand2.gameObject.SetActive(true);
                
                
        //        hand2.broken = false;


        //        //Ball.activeBall.rb.velocity = Ball.activeBall.minMaxSpeed.x * Vector2.one;
        //        //Ball.activeBall.rb.simulated = false;
        //        //Ball.activeBall.transform.position = hand2.transform.position - new Vector3(0, 0.5f);
        //        //hand2.grabbed = true;
        //        //hand2.waitTimer = Time.time;
        //    }
        //}
        
    }


    public void DeactivateHand(bool leftside) {
        if(leftside) {

            hand1.gameObject.SetActive(false);
            hand1RespawnTimer = Time.time;
        } else {
            hand2.gameObject.SetActive(false);
            hand2RespawnTimer = Time.time;
        }

    }


    public void LostBlock() {
        Debug.Log("OOF LOST A BLOCK");
    }

    public void LostTube(bool leftside) {
        stage++;

        Debug.Log("OOF LOST A 'Tube");
        if(leftside) {
            hand1.transform.Translate(new Vector3(0, -handMoveDist));
        } else {
            hand2.transform.Translate(new Vector3(0, -handMoveDist));
        }

        if(stage == 4) {
            invurnerable = false;
            forceField.SetActive(false);
        }

        ResetBlocks();
    }

    public void ResetBlocks() {
        foreach(Block b in blocks) {
            bool r = Random.value > 0.35f + (stage * 0.65f / 4f);
            b.gameObject.SetActive(r);

        }
    }

    public override void Destroyed() {
        gameObject.SetActive(false);

    }
}
