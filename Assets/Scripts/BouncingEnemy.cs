using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{

    public float bounceSpeed = 5.0f;
    public float moveSpeed;
    public Vector3 finishPosition = Vector3.zero;

    private Rigidbody2D _body;
    private Animator _animation;
    private SpriteRenderer _sprite;

    private Vector3 _startPosition;
    private int _direction = 1;

    [Header ("Item Drop")]
    public GameObject[] droppableItems;
    private float originalY;
    private bool isQuitting = false;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _startPosition = transform.position;
        _sprite.flipX = false;
        originalY = transform.position.y;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //some of this code is taken from:
        //https://www.youtube.com/watch?v=t0326lvRIK0
        if(other.tag == "ground")
        {
            _body.velocity = Vector2.zero;
            if(_direction == 1)
            {
                _sprite.flipX = true;
            }
            else
            {
                _sprite.flipX = false;
            }
            _body.AddForce(new Vector2(moveSpeed * _direction, bounceSpeed), ForceMode2D.Impulse);
        }

        if(other.tag == "Player")
        {
            other.GetComponent<Health>().health--;
            
            //knockback code from:
            //https://www.youtube.com/watch?v=sdGeGQPPW7E
            PlatformerPlayer player = other.GetComponent<PlatformerPlayer>();
            player.knockbackCount = player.knockbackMaxTime;

            if(other.transform.position.x < transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
            }

            _direction *= -1;
            _body.velocity = new Vector2(moveSpeed * _direction, _body.velocity.y); 
        }

    }

    void Update()
    {
        _animation.SetFloat("yVelocity", _body.velocity.y);

        if(transform.position.x >= finishPosition.x)
        {
            _direction = -1;
        }
        else if (transform.position.x <= _startPosition.x && _direction == -1)
        {
            _direction = 1;
        }
        if(_body.velocity.y <= -4.3)
        {
            _animation.SetBool("grounded", true);
        }
        else
        {
            _animation.SetBool("grounded", false);
        }
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDisable()
    {
        if(!isQuitting)
        {
            int numOfDrop = Random.Range(1, 6);
            for(int i = 0; i < numOfDrop; i++)
            {
                int whichItem = Random.Range(0, droppableItems.Length);
                float dropRate = Random.Range(0f, 1f);
                if(dropRate >= 0.5f)
                {
                    Vector3 itemSpawnPoint = new Vector3(transform.position.x, originalY, transform.position.z);
                    Instantiate(droppableItems[whichItem], itemSpawnPoint, transform.rotation);
                }
            }
        }
        
    }
}
