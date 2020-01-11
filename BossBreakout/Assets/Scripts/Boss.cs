using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public static Boss activeBoss;
    public Hand hand1;
    public Hand hand2;
    public Block[] blocks;

    public int stage = 0;



    void Start() {
        activeBoss = this;
        ResetBlocks();
    }



    public void LostBlock() {
        Debug.Log("OOF LOST A BLOCK");
    }

    public void LostTube(bool leftside) {
        Debug.Log("OOF LOST A 'Tube");
        if(leftside) { hand1.gameObject.SetActive(true); } else { hand2.gameObject.SetActive(true); }

        stage++;
        ResetBlocks();
    }

    private void ResetBlocks() {
        foreach(Block b in blocks) {
            bool r = Random.value > 0.35f + (stage * 0.65f / 4f);
            b.gameObject.SetActive(r);

        }
    }
}
