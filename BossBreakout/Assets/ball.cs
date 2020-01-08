using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Vector2 minMaxSpeed = new Vector2(5f,100f);
    public bool playerHit = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(minMaxSpeed.x, minMaxSpeed.x);
    }

    // Update is called once per frame
    void Update()
    {
            GetComponent<Renderer>().material.color = playerHit ? Color.blue : Color.red;
        
        rb.velocity = rb.velocity.normalized * Mathf.Min(minMaxSpeed.y, Mathf.Max(minMaxSpeed.x, rb.velocity.magnitude));
    }
}
