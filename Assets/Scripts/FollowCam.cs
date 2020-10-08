using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.1f;

    private Vector3 _velocity = Vector3.zero;
    private float _minimumX = 0f;
    private float _maximumX = 60f;
    private float _minimumY = -82.6f;
    private float _maximumY = 75.3f;
    
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(Mathf.Clamp(target.position.x,_minimumX, _maximumX), Mathf.Clamp(target.position.y, _minimumY, _maximumY), transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
    }
}
