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
    private bool UIcursor = false;

    private Player player;

    private void Awake()
    {
        playerInput = new PlayerInput();
        moveAction = playerInput.PlayerMovement;

        movement = GetComponent<PlayerMovement>();
        movement.Initialized();

        player = GetComponent<Player>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Player.CurrentPlayer.isDead)
        {
            this.enabled = false;
        }

        movement.ShowInteractText();
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

        moveAction.Reload.performed += ReloadInput;

        moveAction.SelectSlot1.performed += Slot1_Select;
        moveAction.SelectSlot2.performed += Slot2_Select;

        moveAction.CursorOn.performed += CursorOn;
        moveAction.CursorOn.canceled += CursorOff;
        moveAction.Interact.performed += InteractInput;
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
        
        moveAction.SelectSlot1.performed -= Slot1_Select;
        moveAction.SelectSlot2.performed -= Slot2_Select;
        
        moveAction.CursorOn.performed -= CursorOn;
        moveAction.CursorOn.canceled -= CursorOff;
        moveAction.Interact.performed -= InteractInput;
    }

    #region inputSetting

    private void MoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void CameraInput(InputAction.CallbackContext context)
    {
        if (UIcursor) return;
        cameraInput = context.ReadValue<Vector2>();
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        movement.Jump();
    }

    private void AttackInput(InputAction.CallbackContext context)
    {
        if (UIcursor) return;
        player.currentWeapon.Attack();
    }

    private void BlockInput(InputAction.CallbackContext context)
    {
        if (UIcursor) return;

        var weapon = player.currentWeapon;
        if (weapon != null && weapon.CanGuard())
        {
            weapon.Guard(true);
        }
    }

    private void BlockCancel(InputAction.CallbackContext context)
    {
        player.currentWeapon?.Guard(false);
    }

    private void SkillInput(InputAction.CallbackContext context)
    {
        if (UIcursor) return;

        var weapon = player.currentWeapon;
        if (weapon != null && weapon.CanSkill())
        {
            weapon.Skill();
        }
    }

    private void ReloadInput(InputAction.CallbackContext context)
    {
        if (UIcursor) return;
        
        player.currentWeapon?.Reload();
    }

    private void Slot1_Select(InputAction.CallbackContext context)
    {
        player.EquipWeaponByIndex(0);
    }

    private void Slot2_Select(InputAction.CallbackContext context)
    {
        player.EquipWeaponByIndex(1);
    }

    private void CursorOn(InputAction.CallbackContext context)
    {
        UIcursor = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void CursorOff(InputAction.CallbackContext context)
    {
        UIcursor = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void InteractInput(InputAction.CallbackContext context)
    {
        movement.TryInteract();
    }

    #endregion
}