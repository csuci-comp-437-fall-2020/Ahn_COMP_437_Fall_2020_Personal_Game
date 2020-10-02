using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2f;

    private Vector3 _velocity = Vector3.zero;
    private float _minimumX = 1.38f;
    private float _maximumX = 100f;
    private float _minimumY = -15f;
    private float _maximumY = 15f;
    
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(Mathf.Clamp(target.position.x,_minimumX, _maximumX), Mathf.Clamp(target.position.y, _minimumY, _maximumY), transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
    }
}
