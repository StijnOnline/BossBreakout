using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int startHP = 1;
    public int currentHP;
    public void OnEnable() {
        currentHP = startHP;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Block b = collision.transform.gameObject.GetComponent<Block>();
        if(b != null)
            Ball.activeBall.type = Ball.Type.Normal;

        currentHP--;
        if(currentHP <= 0) {
            Boss.activeBoss.LostBlock();
            gameObject.SetActive(false);
        }
    }
}
