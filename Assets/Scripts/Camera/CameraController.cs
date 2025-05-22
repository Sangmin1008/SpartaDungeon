using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2EventChannelSO lookEventChannel;
    [SerializeField] private float mouseSensitivity = 1.5f;
    [SerializeField] private Transform player;
    
    private Vector3 _offset = new Vector3(0, 0.6f, 0);
    private float _mouseX = 0f;
    private float _mouseY = 0f;
    private float _angle = 0f; // 좌우 회전 각도 (요)
    private float _pitch = 0f; // 상하 회전 각도 (피치)

    private void OnEnable()
    {
        lookEventChannel.OnEventRaised += OnLookInput;
    }

    private void OnDisable()
    {
        lookEventChannel.OnEventRaised -= OnLookInput;
    }

    private void FixedUpdate()
    {
        CameraRotation();
    }

    private void OnLookInput(Vector2 delta)
    {
        _mouseX = delta.x;
        _mouseY = delta.y;
    }

    private void CameraRotation()
    {
        _angle += _mouseX * mouseSensitivity;
        _pitch -= _mouseY * mouseSensitivity * 0.7f;
        _pitch = Mathf.Clamp(_pitch, -89f, 89f);

        _angle = NormalizeAngle(_angle);

        transform.position = player.position + _offset;
        transform.rotation = Quaternion.Euler(_pitch, _angle, 0);
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
