using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public int startHP = 1;
    private int currentHP;
    public bool invurnerable = false;
    public float speedMultiplier = 1f;
    public bool bottom;
    public bool strong;


    private SpriteRenderer SpriteRender;
    public Sprite armour_0;
    public Sprite armour_1;
    public Sprite armour_2;




    public void Start()
    {
        SpriteRender = GetComponent<SpriteRenderer>();
        
    }


    public void OnEnable() {
        currentHP = startHP;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if(Ball.activeBall.type == Ball.Type.Explosive)
            return;
        
        
        Ball.activeBall.rb.velocity = Ball.activeBall.rb.velocity * speedMultiplier;

        //Block b = collision.transform.gameObject.GetComponent<Block>();
        //if(b != null)
        //    Ball.activeBall.type = Ball.Type.Normal;


        TakeDamage(1);
    }

    public void TakeDamage(int dmg) {
        PlayHitSound();


        if (invurnerable)
            return;


        currentHP--;

        if (currentHP <= 0) {
            Boss.activeBoss.LostBlock();
            Destroyed();
        }


        if (currentHP == 3)
        {
            SpriteRender.sprite = armour_0;
        }

        if (currentHP == 2)
        {
            SpriteRender.sprite = armour_1;
        }

        if (currentHP == 1)
        {
            SpriteRender.sprite = armour_2;
        }





    }

    public virtual void PlayHitSound() {

        if(invurnerable) {
            AudioPlayer.Instance.PlaySound("InvurnerableBlock_Hit", 0.1f);
        }
        else if(strong) {
            AudioPlayer.Instance.PlaySound("StrongBlock_Hit", 0.1f);
        }
        else if(bottom) {
            AudioPlayer.Instance.PlaySound("BottomBlock_Hit", 0.1f);
        } else {
            AudioPlayer.Instance.PlaySound("NormalBlock_Hit", 0.1f);
        }
    }

    public virtual void Destroyed() {
        gameObject.SetActive(false);
        if(bottom) {
            AudioPlayer.Instance.PlaySound("BottomBlock_Break", 0.1f);
        } else if(strong) {
            AudioPlayer.Instance.PlaySound("StrongBlock_Break", 0.1f);
        } else {
            AudioPlayer.Instance.PlaySound("NormalBlock_Break", 0.1f);
        }
    }


}
