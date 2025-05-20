using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private Vector2EventChannelSO moveEventChannel;
    [SerializeField] private Vector2EventChannelSO lookEventChannel;

    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 5f;

    private Vector2 _currentMovementInput;
    private Vector2 _mouseDelta;
    private float _yaw;
    private float _pitch;
    
    private void OnEnable()
    {
        moveEventChannel.OnEventRaised += OnMoveInput;
        lookEventChannel.OnEventRaised += OnLookInput;
    }

    private void OnDisable()
    {
        moveEventChannel.OnEventRaised -= OnMoveInput;
        lookEventChannel.OnEventRaised -= OnLookInput;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void OnMoveInput(Vector2 input)
    {
        _currentMovementInput = input;
    }

    private void OnLookInput(Vector2 input)
    {
        _mouseDelta = input;
    }

    private void Move()
    {
        Vector3 direction = transform.forward * _currentMovementInput.y + transform.right * _currentMovementInput.x;
        direction *= moveSpeed;
        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;
    }

    private void Look()
    {
        _yaw += _mouseDelta.x * lookSensitivity * Time.deltaTime;
        _pitch -= _mouseDelta.y * lookSensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -80f, 80f);

        cameraContainer.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }
}
