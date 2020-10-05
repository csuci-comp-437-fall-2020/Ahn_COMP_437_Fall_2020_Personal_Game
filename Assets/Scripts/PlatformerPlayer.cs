using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 300.0f;
    public float jumpForce = 5.0f;
    public float poundForce = 15.0f;
    public float dashSpeed = 600.0f;

    public int collectables = 0;
    public bool canDash = false;
    public bool canDoubleJump = false;
    public bool canGroundPound = false;

    private Rigidbody2D _body;
    private Animator _animation;
    private BoxCollider2D _box;
    private Health _health;
    private SpriteRenderer _sprite;

    private float deathBound = -15f;
    
    private int jumpCount = 0;
    private const int MAX_EXTRA_JUMP = 1;
    private const float MAX_FALL_VELOCITY = 15.0f;

    public bool groundPounding = false;
    public bool dashing = false;

    private int direction;
    public float startDashTime = 0.1f;
    private float dashTime;

    public float knockback;
    public float knockbackMaxTime;
    public float knockbackCount;
    public bool knockFromRight;

    private const int ENEMY_LAYER = 8;
    private const int PLAERY_LAYER = 9;
    public float flashDuration = 0.05f;
    public int numOfFlashes = 10;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        //GOTTA RUN AT SET 60FPS OR ELSE THINGS GO ZOOM ZOOM

        _body = GetComponent<Rigidbody2D>(); 
        _animation = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
        _health = GetComponent<Health>();
        _sprite = GetComponent<SpriteRenderer>();

        dashTime = startDashTime;
        _animation.SetBool("dashing", false);
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        if(knockbackCount <= 0)
        {
            //moving stuff
            Vector2 movement = new Vector2(deltaX, _body.velocity.y);
            _body.velocity = movement;

            _animation.SetFloat("xSpeed", Mathf.Abs(deltaX));

            bool grounded = false;
            _animation.SetBool("grounded", false);
            bool jumping = true;
            _animation.SetBool("jumping", false);
            _animation.SetBool("groundPounding", false);
            
            if(hit != null && hit.tag == "ground")
            {
                grounded = true;
                jumping = false;
                _animation.SetBool("grounded", true);
                jumpCount = 0;
                groundPounding = false;
            }

            //Jumping stuff
            _body.gravityScale = grounded && deltaX == 0 ? 0 : 1;
            if(grounded && Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpCount++;
                _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            if(_body.velocity.y > 0.5)
            {
                _animation.SetBool("jumping", true);
                jumping = true;
            }
            _animation.SetFloat("yVelocity", _body.velocity.y);

            //Double Jumping
            if (canDoubleJump && !grounded && !groundPounding &&
            Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < MAX_EXTRA_JUMP)
            {
                jumpCount++;
                _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            //GroudPounding
            if (canGroundPound && jumping && Input.GetKeyDown(KeyCode.DownArrow))
            {
                _body.AddForce(-Vector2.up * poundForce, ForceMode2D.Impulse);
                _animation.SetBool("groundPounding", true);
                groundPounding = true;
            }
            if(groundPounding)
            {
                _animation.SetBool("groundPounding", true);
            }

            //Dash stuff
            if(canDash && !groundPounding)
            {
                if(direction == 0)
                {
                    if(Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        dashing = true;
                        if(deltaX < 0)
                        {
                            direction = 1; //left direction
                        }
                        else if (deltaX > 0)
                        {
                            direction = 2; //right direction
                        }
                    }
                }
                else
                {
                    if(dashTime <= 0)
                    {
                        //reset after dash
                        direction = 0;
                        dashTime = startDashTime;
                        _body.velocity = Vector2.zero;
                        _animation.SetBool("dashing", false);
                        dashing = false;
                    }
                    else
                    {
                        _animation.SetBool("dashing", true);
                        dashTime -= Time.deltaTime;
                        if(direction == 1) //left
                        {
                            float dashX = -dashSpeed * Time.deltaTime;
                            Vector2 dash = new Vector2(dashX * 2, 0);
                            _body.velocity = dash;
                        }
                        else if (direction == 2) //right
                        {
                            float dashX = dashSpeed * Time.deltaTime;
                            Vector2 dash = new Vector2(dashX * 2, 0);
                            _body.velocity = dash;
                        }
                    }
                }
            }
        }
        else
        {
            if(knockFromRight)
            {
                //goes to the left
                _body.velocity = new Vector2(-knockback, knockback/2);
            }
            if(!knockFromRight)
            {
                //goes to the right
                _body.velocity = new Vector2(knockback, knockback/2);
            }
            knockbackCount -= Time.deltaTime;
        }



        MovingPlatform platform = null;
        if(hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>();
        }
        if(platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        Death();

        
        Vector3 pScale = Vector3.one;
        if(platform != null)
        {
            pScale = platform.transform.localScale;
        }
        if(deltaX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1/pScale.y, 1);
        }
        /*
        if(!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }
        */
    }

    //Invulnerable Frames from:
    //https://www.youtube.com/watch?v=S61J3kDQ5Mk
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == ENEMY_LAYER && _health.health > 0)
        {
            StartCoroutine("Invulnerable");
        }
    }

    IEnumerator Invulnerable()
    {
        int temp = 0;
        Physics2D.IgnoreLayerCollision(ENEMY_LAYER, PLAERY_LAYER, true);
        while (temp < numOfFlashes)
        {
            _sprite.color = Color.clear;
            yield return new WaitForSeconds(flashDuration);
            _sprite.color = Color.white;
            yield return new WaitForSeconds (flashDuration);
            temp++;
        }
        Physics2D.IgnoreLayerCollision(ENEMY_LAYER, PLAERY_LAYER, false);
    }

    private void Death()
    {
        if(_health.health == 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

}
