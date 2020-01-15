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

    private float lastBounce = -100f;

    public enum Type {
        Normal,
        Curve,
        Explosive        
    }

    //TMEP
    public GameObject spikes;

    public float ghostDelay = 0;
    private float ghostDelayTimer = 0;

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

    private void Update() {

        ghostDelayTimer -= Time.deltaTime;
        if(rb.velocity.magnitude == minMaxSpeed.y && ghostDelayTimer < 0) { //DISCUSS is this timer smart?
            ghostDelayTimer = ghostDelay;
            GameObject trail = new GameObject();
            trail.transform.position = (Vector3)(rb.position - rb.velocity * 0.015f);
            trail.transform.localScale = transform.localScale;
            SpriteRenderer r = trail.AddComponent<SpriteRenderer>();
            r.sprite = sr.sprite;
            r.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            Destroy(trail, 1f);
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

        if (Time.time > lastBounce + 0.05f){
            CountExplosion();
            lastBounce = Time.time;
        }
    }

    public void CountExplosion() {
        if(type == Type.Explosive) {
            explodeCount++;
            Debug.Log("Boop" + explodeCount);

            if(explodeCount >= 3) {
                Debug.Log("Boom");
                Collider2D[] results = new Collider2D[5];
                Physics2D.OverlapCircle(transform.position, 3f, new ContactFilter2D(), results);
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
