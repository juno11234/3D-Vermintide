using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //이동 로직과 애니메이션
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

    [SerializeField]
    float interactDistance = 1.5f;

    [SerializeField]
    private TMP_Text interactText;

    CharacterController controller;
    Animator animator;
    Camera cam;
    float xRotation;
    bool isGrounded;
    Vector3 velocity;
    private IInteractable currentInteractable;

    public void Initialized()
    {
        interactText.gameObject.SetActive(false);
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
        // if (animator.IsInTransition(0)) return;
        // var currentAnimState = animator.GetCurrentAnimatorStateInfo(0);
        //
        // bool Attack1 = currentAnimState.IsName("Attack1");
        // bool Attack2 = currentAnimState.IsName("Attack2");
        // bool isAttack = Attack1 || Attack2;
        //
        // float normalizedTime = currentAnimState.normalizedTime;

        /*if (isAttack == false)
        {
            animator.ResetTrigger(ATTACK);
            animator.SetTrigger(ATTACK);
        }
        else
        {
            if (0.1f < normalizedTime && normalizedTime < 0.9f)
            {
                animator.ResetTrigger(ATTACK);
                animator.SetTrigger(ATTACK);
            }
        }*/
        animator.ResetTrigger(ATTACK);
        animator.SetTrigger(ATTACK);
    }

    public void Guard(bool isBlock)
    {
        animator.SetBool(BLOCK, isBlock);
    }

    public void Skill()
    {
        //if (animator.IsInTransition(0)) return;

        var currentAnimState = animator.GetCurrentAnimatorStateInfo(0);
        bool skill = currentAnimState.IsName("Skill");

        if (skill) return;
        animator.SetTrigger(SKILL);
    }

    public void ShowInteractText()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, LayerMask.GetMask("Interactables")))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                interactText.gameObject.SetActive(true);
                string text = interactable.InteractText();
                interactText.text = text;
                return;
            }
        }

        currentInteractable = null;
        interactText.gameObject.SetActive(false);
    }

    public void TryInteract()
    {
        currentInteractable?.Interact();
    }
}