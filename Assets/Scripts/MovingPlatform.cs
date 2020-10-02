using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector3 finishPosition = Vector3.zero;
    public float speed = 0.5f;

    private Vector3 _startPosition;
    private float _trackPercent = 0;
    private int _direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
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
    }
}
