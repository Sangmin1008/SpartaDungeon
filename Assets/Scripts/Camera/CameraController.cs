using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2EventChannelSO lookEventChannel;
    [SerializeField] private float mouseSensitivity = 1.5f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private Transform player;
    
    private Vector3 _offset = new Vector3(0, 10, -15);
    private float mouseX = 0f;
    private float _targetAngle = 0f;
    private float _currentAngle = 0f;

    private void OnEnable()
    {
        lookEventChannel.OnEventRaised += OnLookInput;
    }

    private void OnDisable()
    {
        lookEventChannel.OnEventRaised -= OnLookInput;
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void OnLookInput(Vector2 delta)
    {
        mouseX = Mathf.Clamp(delta.x, -5f, 5f);
    }

    private void CameraRotation()
    {
        _targetAngle += mouseX * mouseSensitivity;
        float angleDifference = Mathf.DeltaAngle(_currentAngle, _targetAngle);

        _currentAngle += angleDifference * rotationSpeed * Time.deltaTime * 3;

        _currentAngle = NormalizeAngle(_currentAngle);
        _targetAngle = NormalizeAngle(_targetAngle);
        
        Quaternion rotation = Quaternion.Euler(0, _currentAngle, 0);
        Vector3 rotatedOffset = rotation * _offset;

        transform.position = player.transform.position + rotatedOffset;
        transform.rotation = Quaternion.Euler(Mathf.Rad2Deg * Mathf.Atan2(1, 2), _currentAngle, 0);
    }
    
    private float NormalizeAngle(float angle)
    {
        while (angle > 360)
            angle -= 360;
        while (angle < 0)
            angle += 360;
        return angle;
    }
}
