using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball activeBall;

    public Vector2 minMaxSpeed = new Vector2(5f,100f);

    public Rigidbody2D rb;
    //private Renderer r;

    public Type type {  get; private set; } = Type.Normal;
    public float curveforce = 5f;
    public int explodeCount { get; private set; } = 0;
    public GameObject explosionPrefab;
    public Sprite[] sprites = new Sprite[5]; //normal,curve, explode 321
    private SpriteRenderer sr;

    public enum Type {
        Normal,
        Curve,
        Explosive        
    }

    //TMEP
    public GameObject spikes;

    void Start()
    {
        activeBall = this;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.velocity = new Vector2(minMaxSpeed.x, minMaxSpeed.x);
    }


    void FixedUpdate()
    {
        //r.material.color = playerHit ? Color.blue : Color.red;
        rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, minMaxSpeed.x, minMaxSpeed.y);        

        if(type == Type.Curve) {
            //rb.AddForce( -curveforce *   new Vector2(transform.position.x, 0),ForceMode2D.Impulse);
            Vector2 dir = rb.velocity.normalized + curveforce /1000 *  new Vector2(-rb.velocity.y,rb.velocity.x);
            rb.velocity  =  rb.velocity.magnitude * dir.normalized;
        }
    }

    public void SetType(Type _type) {
        type = _type;
        if (type == Type.Explosive)
        {
            AudioPlayer.Instance.PlaySound("Countdown_0", 0.1f);
        }
        sr.sprite = sprites[(int)type];
        Debug.Log("now " + type);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        CountExplosion();
    }

    public void CountExplosion() {
        if(type == Type.Explosive) {
            explodeCount++;
            Debug.Log("Boop" + explodeCount);

            if(explodeCount >= 3) {
                Debug.Log("Boom");
                Collider2D[] results = new Collider2D[5];
                Physics2D.OverlapCircle(transform.position, 1f, new ContactFilter2D(), results);
                foreach(Collider2D item in results) {
                    Block b = item?.GetComponent<Block>();
                    if(b != null) {
                        if(!b.invurnerable)
                            b.TakeDamage(1000000);
                    }
                        
                }

                Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 10f);
                explodeCount = 0;
                SetType(Type.Normal);
            } else {
                AudioPlayer.Instance.PlaySound("Countdown_"+ (explodeCount+1), 0.1f);
                sr.sprite = sprites[explodeCount + 2];

            }
        }
    }


}
