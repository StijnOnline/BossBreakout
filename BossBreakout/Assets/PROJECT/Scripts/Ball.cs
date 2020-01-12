using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball activeBall;

    public Vector2 minMaxSpeed = new Vector2(5f,100f);
    public bool playerHit = true;

    public Rigidbody2D rb;
    private Renderer r;

    public Type type;
    public float curveforce = 5f;

    public enum Type {
        Normal,
        Heal,
        Curve
    }

    //TMEP
    public GameObject spikes;

    void Start()
    {
        activeBall = this;

        rb = GetComponent<Rigidbody2D>();
        r = GetComponent<Renderer>();
        rb.velocity = new Vector2(minMaxSpeed.x, minMaxSpeed.x);
    }


    void FixedUpdate()
    {
        r.material.color = playerHit ? Color.blue : Color.red;
        rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, minMaxSpeed.x, minMaxSpeed.y);

        spikes.SetActive(type == Type.Heal);

        if(type == Type.Curve) {
            //rb.AddForce( -curveforce *   new Vector2(transform.position.x, 0),ForceMode2D.Impulse);
            Vector2 dir = rb.velocity.normalized + curveforce /1000 *  new Vector2(-rb.velocity.y,rb.velocity.x);
            rb.velocity  =  rb.velocity.magnitude * dir.normalized;
        }
    }

    
}
