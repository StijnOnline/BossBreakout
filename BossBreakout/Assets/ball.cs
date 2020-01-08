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

    public enum Type {
        Normal,
        Spiked,
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
        rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, 0, minMaxSpeed.y);

        
        if(type == Type.Spiked) {
            spikes.SetActive(true);
            //spikes.transform.Rotate(new Vector3(0,0,3),Space.Self);
        }
    }

    
}
