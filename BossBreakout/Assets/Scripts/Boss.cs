using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public static Boss activeBoss;
    public Hand hand1;
    public Hand hand2;
    public Block[] blocks;



    void Start() {
        activeBoss = this;
    }

    // Update is called once per frame
    void Update() {

    }

    public void LostBlock() {

    }

    public void LostTube() {
        ResetBlocks();
    }

    private void ResetBlocks() {
        foreach(Block b in blocks) {
            bool r = Random.value > 0.5f;
            b.gameObject.SetActive(r);
        }
    }
}
