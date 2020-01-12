using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public int startHP = 1;
    private int currentHP;
    public bool invurnerable = false;

    public void OnEnable() {
        currentHP = startHP;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if(Ball.activeBall.type == Ball.Type.Heal)
            return;
        if(invurnerable)
            return;
        
        Block b = collision.transform.gameObject.GetComponent<Block>();
        if(b != null)
            Ball.activeBall.type = Ball.Type.Normal;
        currentHP--;
        if(currentHP <= 0) {
            Boss.activeBoss.LostBlock();
            Destroyed();
        }
    }

    public virtual void Destroyed() {
        gameObject.SetActive(false);

    }
}
