using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider collider;
    [SerializeField] private Transform camera;
    
    [Header("Move")]
    [SerializeField] private Vector2EventChannelSO moveEventChannel;
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Look")]
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jump")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private VoidEventChannelSO jumpEventChannel;
    [SerializeField] private BoolEventChannelSO jumpHeldEventChannel;
    [SerializeField] private FloatEventChannelSO jumpInAirEventChannel;
    [SerializeField] private FloatEventChannelSO fallDurationEventChannel;
    [SerializeField] private float jumpForce = 5f;
    
    [Header("Gravity Tuning")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    
    [Header("Game Over")]
    [SerializeField] private VoidEventChannelSO gameOverEventChannel;


    private Vector2 _currentMovementInput;
    private Vector3 _moveDirection;
    private Vector2 _mouseDelta;
    private bool _isGrounded;
    private bool _isJumpHeld;
    private bool _wasGrounded = true;
    private float _fallStartTime;
    private float _canJumpInAirDuration = 0f;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        moveEventChannel.OnEventRaised += OnMoveInput;
        jumpEventChannel.OnEventRaised += OnJumpInput;
        jumpHeldEventChannel.OnEventRaised += OnJumpHeldChanged;
        jumpInAirEventChannel.OnEventRaised += SetCanJumpInAir;
    }

    private void OnDestroy()
    {
        moveEventChannel.OnEventRaised -= OnMoveInput;
        jumpEventChannel.OnEventRaised -= OnJumpInput;
        jumpHeldEventChannel.OnEventRaised -= OnJumpHeldChanged;
        jumpInAirEventChannel.OnEventRaised -= SetCanJumpInAir;
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        Look();
        ApplyExtraGravity();
    }

    private void Update()
    {
        if (transform.position.y < -100f) Die();
        CalculateMoveDirection();
    }

    private void OnMoveInput(Vector2 input)
    {
        _currentMovementInput = input;
    }

    private void OnJumpInput()
    {
        if (_isGrounded || _canJumpInAirDuration > 0f)
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = 0;
            _rigidbody.velocity = velocity;

            _rigidbody.AddForce(transform.up * jumpForce , ForceMode.Impulse);
        }
    }
    
    private void OnJumpHeldChanged(bool isHeld)
    {
        _isJumpHeld = isHeld;
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

    private void SetCanJumpInAir(float duration)
    {
        _canJumpInAirDuration = duration;
        StartCoroutine(AirJumpCoroutine());
    }

    private IEnumerator AirJumpCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _canJumpInAirDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canJumpInAirDuration = 0f;
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

    void CheckGround()
    {
        _isGrounded = IsGrounded();
        if (!_wasGrounded && _isGrounded)
        {
            float airTime = Time.time - _fallStartTime;
            fallDurationEventChannel.Raise(airTime);
        }

        if (_wasGrounded && !_isGrounded)
        {
            _fallStartTime = Time.time;
        }

        _wasGrounded = _isGrounded;
    }
    
    bool IsGrounded()
    {
        const float epsilon = 0.001f;

        Vector3 center = transform.position;
        float bottomY = center.y - collider.bounds.extents.y;

        Vector3 point1 = new Vector3(center.x, bottomY + epsilon, center.z);
        Vector3 point2 = new Vector3(center.x, bottomY - epsilon, center.z);

        return Physics.CheckCapsule(point1, point2, collider.radius, groundLayerMask);
    }
    
    private void ApplyExtraGravity()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (_rigidbody.velocity.y > 0 && !_isJumpHeld)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void Die()
    {
        gameOverEventChannel.Raise();
    }
}
