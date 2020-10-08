using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public Vector3 finishPosition = Vector3.zero;
    public float speed = 0.5f;

    private Vector3 _startPosition;
    private float _trackPercent = 0;
    private int _direction = 1;

    private BoxCollider2D head;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        head = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _trackPercent += _direction * speed * Time.deltaTime;
        float x = (finishPosition.x - _startPosition.x) * _trackPercent + _startPosition.x;
        float y = (finishPosition.y - _startPosition.y) * _trackPercent + _startPosition.y;
        transform.position = new Vector3(x, y, _startPosition.z);

        if((_direction == 1 && _trackPercent > .9f) || (_direction == -1 && _trackPercent < .1f))
        {
            _direction *= -1;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(head.transform.position, Vector2.up, 0.5f, LayerMask.GetMask("Player"));

        if(hit.collider != null)
        {
            gameObject.SetActive(false);
        }
    }
}
