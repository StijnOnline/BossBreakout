using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    public Block[] parts;
    public bool broken = false;

    void FixedUpdate()
    {
        if(!broken) {
            for(int i = 0; i < transform.childCount; i++) {
                if(transform.GetChild(i).gameObject.activeSelf) {
                    broken = true;
                    break;
                }
            }
        }
    }
}
