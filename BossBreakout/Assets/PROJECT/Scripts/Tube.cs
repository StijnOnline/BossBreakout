using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    public bool leftside;
    public bool broken = false;

    void FixedUpdate()
    {
        if(!broken) {
            for(int i = 0; i < transform.childCount; i++) {
                if(!transform.GetChild(i).gameObject.activeSelf) {
                    broken = true;
                    Boss.activeBoss.LostTube(leftside);
                    break;
                }
            }
        }
    }
}
