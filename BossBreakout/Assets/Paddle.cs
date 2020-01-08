using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float movespeed = 0.05f;

    public float hitTime = 0.5f;
    private float hitTimer = 0;
    public float hitMultiplier = 1.5f;

    public Vector2 minMaxPos;


    void Start()
    {
        
    }

    //TODO dash
    void Update()
    {
        float input_hor = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Z))
        {
            hitTimer = Time.time;
        }


        if (hitTimer + hitTime > Time.time)
        {
            //TODO remove temp:
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            //TODO remove temp:
            GetComponent<Renderer>().material.color = Color.white;
        }

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
            rb.velocity = (rb.velocity.magnitude * (transform.position - go.transform.position) * -1f);
            if (hitTimer + hitTime > Time.time)
            {
                go.GetComponent<ball>().playerHit = true;
                rb.velocity = rb.velocity * hitMultiplier;
            }else if( !go.GetComponent<ball>().playerHit)
            {
                //TODO remove temp:
                GetComponent<Renderer>().material.color = Color.red;
                rb.velocity = rb.velocity.normalized * go.GetComponent<ball>().minMaxSpeed.x;
            }
        }
    }

}
