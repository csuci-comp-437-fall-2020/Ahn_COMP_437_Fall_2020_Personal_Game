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
    private float _trackPercent = 0;
    private int _direction = 1;

    private bool bounce = false;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _startPosition = transform.position;
        _sprite.flipX = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //some of this code is taken from:
        //https://www.youtube.com/watch?v=t0326lvRIK0
        if(other.tag == "ground")
        {
            _body.velocity = Vector2.zero;
            bounce = true;
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

        if (other.tag == "Player")
        {
            Destroy(gameObject);
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
        if(_body.velocity.y <= -4.5)
        {
            _animation.SetBool("grounded", true);
        }
        else
        {
            _animation.SetBool("grounded", false);
        }
    }
}
