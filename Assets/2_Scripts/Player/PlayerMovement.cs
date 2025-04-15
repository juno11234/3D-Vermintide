using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    private static readonly int BLOCK = Animator.StringToHash("Block");
    private static readonly int SKILL = Animator.StringToHash("Skill");

    [SerializeField]
    float moveSpeed = 6f;

    [SerializeField]
    float mouseSensitivity = 100f;

    [SerializeField]
    float jumpHeight = 3f;

    [SerializeField]
    float gravity = -9.81f;

    CharacterController controller;
    Animator animator;
    Camera cam;
    float xRotation;
    bool isGrounded;
    Vector3 velocity;

    public void Initialized()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;
        controller.Move(transform.TransformDirection(moveDir) * (moveSpeed * Time.deltaTime));

        velocity.y += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0) velocity.y = -5f;
        controller.Move(velocity * Time.deltaTime);
        isGrounded = controller.isGrounded;
    }

    public void Look(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= mouseY * Time.deltaTime * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * mouseSensitivity));
    }

    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }
    }

    public void Attack()
    {
        if (animator.IsInTransition(0)) return;
        var currentAnimState = animator.GetCurrentAnimatorStateInfo(0);

        bool Attack1 = currentAnimState.IsName("Attack1");
        bool Attack2 = currentAnimState.IsName("Attack2");
        bool isAttack = Attack1 || Attack2;

        float normalizedTime = currentAnimState.normalizedTime;

        if (isAttack == false)
        {
            animator.ResetTrigger(ATTACK);
            animator.SetTrigger(ATTACK);
        }
        else
        {
            if (Attack2) return;

            if (0.1f < normalizedTime && normalizedTime < 0.9f)
            {
                animator.ResetTrigger(ATTACK);
                animator.SetTrigger(ATTACK);
            }
        }
    }

    public void Block(bool isBlock)
    {
        if (isBlock)
        {
            animator.SetBool(BLOCK, true);
        }
        else
        {
            animator.SetBool(BLOCK, false);
        }
    }

    public void Skill()
    {
        if (animator.IsInTransition(0)) return;
        
        var currentAnimState = animator.GetCurrentAnimatorStateInfo(0);
        bool Skill = currentAnimState.IsName("Skill");
        
        if (Skill) return;
        animator.SetTrigger(SKILL);
    }
}