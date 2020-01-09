using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss activeBoss;
    public Hand hand1;
    public Hand hand2;
    public Block[] blocks;
    public Tube[] tubes;



    void Start()
    {
        activeBoss = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LostBlock() {

    }
}
