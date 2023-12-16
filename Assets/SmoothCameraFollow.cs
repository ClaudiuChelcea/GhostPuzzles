using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private Transform Target;
    [SerializeField] private float smoothTimeForward;
    [SerializeField] private float smoothTimeBack;
    private Vector3 _currentVelocity = Vector3.zero;
    [SerializeField] Ghost_Movement player;

    private void Awake()
    {
        _offset = transform.position - Target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + _offset;

        if(player.isMovingForward == true)
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTimeForward);
        else
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTimeBack);
    }
}