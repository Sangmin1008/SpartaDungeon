using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private Vector2EventChannelSO moveEventChannel;
    [SerializeField] private Vector2EventChannelSO lookEventChannel;
    [SerializeField] private VoidEventChannelSO jumpEventChannel;
    [SerializeField] private BoolEventChannelSO jumpHeldEventChannel;
    [SerializeField] private VoidEventChannelSO interactEventChannel;
    
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

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            jumpHeldEventChannel.Raise(true);
            jumpEventChannel.Raise();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            jumpHeldEventChannel.Raise(false);
        }
    }
    
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // 상호작용중인 아이템이 없을 수 있기 때문에 null일 경우 무시
            interactEventChannel?.Raise();
        }
    }
}
