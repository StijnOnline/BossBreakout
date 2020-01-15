using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss : Block {



    public static Boss activeBoss;
    public bool startRandomTiles = false;

    public Hand hand1;
    public Hand hand2;
    public Transform rails1;
    public Transform rails2;
    public GameObject forceField;
    public Block[] blocks;

    public int stage = 0;
    public float handMoveDist = 1f;

    public float handRespawnTime = 5f;
    private float hand1RespawnTimer = 5f;
    private float hand2RespawnTimer = 5f;
    public int stages = 2;

    public GameObject explosionPrefab;
    public GameObject victoryCanvas;

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


    public void LostBlock() {
        Debug.Log("OOF LOST A BLOCK");
    }

    public void LostTube(bool leftside) {
        
        

        Debug.Log("OOF LOST A 'Tube");
        StartCoroutine(ChangePhase(leftside));
    }

    public IEnumerator ChangePhase(bool leftside) {

        
        Ball.activeBall.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        //Animation

        Vector3 move = new Vector3(0, -handMoveDist);
        if(leftside) {
            hand1.transform.parent.parent.Translate(move);
            rails1.Translate(move);
            Ball.activeBall.transform.position = hand1.transform.position;
        } else {
            hand2.transform.parent.parent.Translate(move);
            rails2.Translate(move);
            Ball.activeBall.transform.position = hand2.transform.position;
        }
        Ball.activeBall.gameObject.SetActive(true);
        Ball.activeBall.rb.velocity = Vector2.one * Ball.activeBall.minMaxSpeed.x;

        stage++;
        if(stage == stages) {
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

        victoryCanvas.SetActive(true);
        StartCoroutine(Victory());
    }

    public IEnumerator Victory() {
        for(int i = 0; i < 30; i++) {
            Destroy(Instantiate(explosionPrefab, transform.position + new Vector3(Random.Range(-4f,4f), Random.Range(-4f, 4f),0), Quaternion.identity), 10f);
            yield return 0;
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
