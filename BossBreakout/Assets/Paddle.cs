using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float movespeed = 0.05f;
    public float hitMultiplier = 1.5f;
    public Vector2 minMaxPos;


    void Start()
    {
        
    }

    //TODO dash
    void Update()
    {
        float input_hor = Input.GetAxisRaw("Horizontal");
        bool input_hit = Input.GetButtonDown("Hit");

        Vector3 newpos = transform.position;
        newpos.x = Mathf.Max(Mathf.Min(minMaxPos.y, newpos.x + input_hor * movespeed),minMaxPos.x);
        transform.position = newpos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.layer == LayerMask.NameToLayer("Ball"))
        {
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            rb.velocity = (rb.velocity.magnitude * (transform.position - go.transform.position) * -1f )*  hitMultiplier;
        }
    }

}
