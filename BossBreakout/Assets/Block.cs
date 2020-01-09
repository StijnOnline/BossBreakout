using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int HP = 1;

    private void OnCollisionEnter2D(Collision2D collision) {
        HP--;
        if(HP <= 0) {
            Boss.activeBoss.LostBlock();
            gameObject.SetActive(false);
        }
    }
}
