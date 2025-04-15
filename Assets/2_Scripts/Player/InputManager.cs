using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerMovementActions moveAction;
    private PlayerMovement movement;
    private Vector2 moveInput;
    private Vector2 cameraInput;
    private bool isBlock;

    void Awake()
    {
        playerInput = new PlayerInput();
        moveAction = playerInput.PlayerMovement;

        movement = GetComponent<PlayerMovement>();
        movement.Initialized();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        movement.Move(moveInput);
        movement.Look(cameraInput);
    }

    private void OnEnable()
    {
        playerInput.Enable();

        moveAction.Move.performed += MoveInput;
        moveAction.Look.performed += CameraInput;
        moveAction.Jump.performed += JumpInput;
        moveAction.Attack.performed += AttackInput;
        moveAction.Block.performed += BlockInput;
        moveAction.Block.canceled += BlockCancel;
        moveAction.Skill.performed += SkillInput;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        moveAction.Move.performed -= MoveInput;
        moveAction.Look.performed -= CameraInput;
        moveAction.Jump.performed -= JumpInput;
        moveAction.Attack.performed -= AttackInput;
        moveAction.Block.performed -= BlockInput;
        moveAction.Block.canceled -= BlockCancel;
        moveAction.Skill.performed -= SkillInput;
    }

    #region inputSetting

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
        movement.Jump();
    }

    private void AttackInput(InputAction.CallbackContext context)
    {
        movement.Attack();
    }

    private void BlockInput(InputAction.CallbackContext context)
    {
       movement.Block(true);
    }

    private void BlockCancel(InputAction.CallbackContext context)
    {
        movement.Block(false);
    }

    private void SkillInput(InputAction.CallbackContext context)
    {
        movement.Skill();
    }

    #endregion
}