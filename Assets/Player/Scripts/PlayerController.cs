using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private Vector2EventChannelSO moveEventChannel;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Transform camera;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector2 _currentMovementInput;
    private Vector3 _moveDirection;
    private Vector2 _mouseDelta;
    private float _yaw;
    private float _pitch;
    
    private void OnEnable()
    {
        moveEventChannel.OnEventRaised += OnMoveInput;
    }

    private void OnDisable()
    {
        moveEventChannel.OnEventRaised -= OnMoveInput;
    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    private void Update()
    {
        CalculateMoveDirection();
    }

    private void OnMoveInput(Vector2 input)
    {
        _currentMovementInput = input;
    }

    void CalculateMoveDirection()
    {
        Vector2 movementInput = _currentMovementInput;
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        _moveDirection = (cameraForward * movementInput.y + cameraRight * movementInput.x).normalized;
    }

    void Move()
    {
        Vector3 targetVelocity = _moveDirection * moveSpeed;
        Vector3 velocityDiff = targetVelocity - _rigidbody.velocity;
        velocityDiff.y = 0f;
        _rigidbody.AddForce(velocityDiff, ForceMode.VelocityChange);
    }

    void Look()
    {
        if (_moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }
}
