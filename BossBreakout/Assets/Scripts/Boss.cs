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



    void Start() {
        activeBoss = this;
        ResetBlocks();
    }



    public void LostBlock() {
        Debug.Log("OOF LOST A BLOCK");
    }

    public void LostTube(bool leftside) {
        stage++;

        Debug.Log("OOF LOST A 'Tube");
        if(leftside) { 
            hand1.gameObject.SetActive(true);
            hand1.transform.Translate(new Vector3(0,-handMoveDist));
        } else { 
            hand2.gameObject.SetActive(true);
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
