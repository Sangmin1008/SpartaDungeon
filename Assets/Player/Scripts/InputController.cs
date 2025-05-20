using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private Vector2EventChannelSO moveEventChannel;
    [SerializeField] private Vector2EventChannelSO lookEventChannel;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = Vector2.zero;

        if (context.phase == InputActionPhase.Performed)
            moveInput = context.ReadValue<Vector2>();

        moveEventChannel.Raise(moveInput);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        
        lookEventChannel.Raise(mouseDelta);
    }
}
