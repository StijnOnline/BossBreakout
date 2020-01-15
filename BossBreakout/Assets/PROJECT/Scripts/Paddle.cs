using UnityEngine;

public class Paddle : MonoBehaviour {

    public float hitTime = 0.5f;
    private float hitTimer = 0;
    public float hitMultiplier = 1.5f;

    public float movespeed = 0.2f;
    public float dashDist = 0.5f;

    public Vector2 minMaxPos;

    float input_hor;
    bool input_dash;

    public bool grabbed = false;
    public float pauseTime = 0.1f;
    private float pauseTimer = -1f;
    private Vector2 throwspeed;

    private Rigidbody2D rb;

    public Sprite[] sprites = new Sprite[4]; //normal, ready, good, bad
    public SpriteRenderer[] renderers = new SpriteRenderer[2];

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        input_hor = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Z)) {
            hitTimer = Time.time;
            foreach(SpriteRenderer r in renderers) {
                r.sprite = sprites[1];
            }
        }
        if(Time.time % hitTime < 0.1f) {
            foreach(SpriteRenderer r in renderers) {
                r.sprite = sprites[0];
            }
        }

    }


    void FixedUpdate() {

        if(grabbed) {
            if(Time.time > pauseTimer + pauseTime) {
                Ball.activeBall.rb.isKinematic = false;
                Ball.activeBall.rb.velocity = throwspeed;
                grabbed = false;
            }
        }

        //Vector3 newpos = transform.position;
        //newpos.x = Mathf.Max(Mathf.Min(minMaxPos.y, newpos.x + , minMaxPos.x);
        //transform.position = newpos;
        rb.velocity = new Vector2(input_hor * (input_dash ? dashDist : movespeed), 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject go = collision.gameObject;
        if(grabbed)
            return;
        if(go.layer == LayerMask.NameToLayer("Ball")) {
            Ball b = go.GetComponent<Ball>();
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();










            throwspeed = (rb.velocity.magnitude * new Vector2((transform.position - go.transform.position).normalized.x * -1f, 1));
            rb.velocity = Vector2.zero;

            if(hitTimer + hitTime > Time.time) {
                throwspeed = throwspeed * hitMultiplier;

                Camera.main.GetComponent<ScreenShake>().Shake(throwspeed.magnitude);
                foreach(SpriteRenderer r in renderers) {
                    r.sprite = sprites[2];
                }
            } else {
                foreach(SpriteRenderer r in renderers) {
                    r.sprite = sprites[3];
                }
            }

            if(throwspeed.magnitude >= 5) {
                AudioPlayer.Instance.PlaySound("Paddle_Hit1", 0.1f);
                Debug.Log("1");
            }

            if(throwspeed.magnitude >= 11) {
                AudioPlayer.Instance.PlaySound("Paddle_Hit2", 0.1f);
                Debug.Log("2");
            }

            if(throwspeed.magnitude >= 16) {
                AudioPlayer.Instance.PlaySound("Paddle_Hit3", 0.1f);
                Debug.Log("3");
            }




            b.rb.isKinematic = true;
            pauseTimer = Time.time;
            grabbed = true;




            //else if(!b.playerHit) {
            //    rb.velocity = rb.velocity.normalized * b.minMaxSpeed.x;
            //}


        }
    }

}
