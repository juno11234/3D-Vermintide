using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerMovementActions moveAction;
    public Vector2 moveInput { get; private set; }
    public Vector2 cameraInput { get; private set; }
    public bool isJumping { get; private set; }

    void Awake()
    {
        playerInput = new PlayerInput();
        moveAction = playerInput.PlayerMovement;
    }

    private void OnEnable()
    {
        playerInput.Enable();

        moveAction.Move.performed += MoveInput;
        moveAction.Look.performed += CameraInput;
        moveAction.Jump.performed += JumpInput;
        moveAction.Jump.canceled += JumpCanceled;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        
        moveAction.Move.performed -= MoveInput;
        moveAction.Look.performed -= CameraInput;
        moveAction.Jump.performed -= JumpInput;
        moveAction.Jump.canceled -= JumpCanceled;
    }

    private void MoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void CameraInput(InputAction.CallbackContext context)
    {
        cameraInput = context.ReadValue<Vector2>();
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        isJumping = true;
    }

    private void JumpCanceled(InputAction.CallbackContext context)
    {
        isJumping = false;
    }
}