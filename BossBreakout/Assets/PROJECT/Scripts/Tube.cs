using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    public bool leftside;
    public bool broken = false;
    public GameObject ExplosionPrefab;
    void FixedUpdate()
    {
        if(!broken) {
            for(int i = 0; i < transform.childCount; i++) {
                if(!transform.GetChild(i).gameObject.activeSelf) {
                    broken = true;
                    Boss.activeBoss.LostTube(leftside);
                    StartCoroutine(ChainReaction(i));
                    break;
                }
            }
        }
    }

    public IEnumerator ChainReaction(int startpoint) {
        int i = startpoint;

        for(int j = 0; j <= System.Math.Max(startpoint, transform.childCount-startpoint); j++) {
            yield return new WaitForSeconds(0.18f);
            if(i + j < transform.childCount) {
                transform.GetChild(i + j).gameObject.SetActive(false);
                Destroy( Instantiate(ExplosionPrefab, transform.GetChild(i + j).position, Quaternion.identity), 10f);
            }
            if(i - j >= 0) {
                transform.GetChild(i - j).gameObject.SetActive(false);
                Destroy(Instantiate(ExplosionPrefab, transform.GetChild(i + j).position, Quaternion.identity), 10f);
            }
        }
    }
}
