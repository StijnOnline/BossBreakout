using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public static Boss activeBoss;


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
        ResetBlocks();
    }

    public void Update() {

        if(!hand1.gameObject.activeSelf) {
            if(Time.time > hand1RespawnTimer + handRespawnTime ) {
                hand1.gameObject.SetActive(true);
                //Ball.activeBall.rb.velocity = Ball.activeBall.minMaxSpeed.x * Vector2.one;
                //Ball.activeBall.rb.simulated = false;
                //Ball.activeBall.transform.position = hand1.transform.position - new Vector3(0, 0.5f);
                //hand1.grabbed = true;
                //hand1.waitTimer = Time.time;
            }
        }
        if(!hand2.gameObject.activeSelf) {
            if(Time.time > hand2RespawnTimer + handRespawnTime) {
                hand2.gameObject.SetActive(true);
                //Ball.activeBall.rb.velocity = Ball.activeBall.minMaxSpeed.x * Vector2.one;
                //Ball.activeBall.rb.simulated = false;
                //Ball.activeBall.transform.position = hand2.transform.position - new Vector3(0, 0.5f);
                //hand2.grabbed = true;
                //hand2.waitTimer = Time.time;
            }
        }
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


        ResetBlocks();
    }

    private void ResetBlocks() {
        foreach(Block b in blocks) {
            bool r = Random.value > 0.35f + (stage * 0.65f / 4f);
            b.gameObject.SetActive(r);

        }
    }
}
