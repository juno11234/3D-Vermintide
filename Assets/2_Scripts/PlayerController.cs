using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 6f;

    [SerializeField]
    float mouseSensitivity = 100f;

    [SerializeField]
    float jumpHeight = 3f;

    [SerializeField]
    float gravity = -9.81f;

    CharacterController controller;
    InputHandler inputHandler;
    Camera cam;
    float xRotation = 0f;
    bool isGrounded;
    Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        inputHandler = GetComponent<InputHandler>();
        cam = Camera.main;
    }

    void Update()
    {
        Move(inputHandler.moveInput);
        Look(inputHandler.cameraInput);
        Jump(inputHandler.isJumping);
    }

    void Move(Vector2 input)
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

    void Look(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= mouseY * Time.deltaTime * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * mouseSensitivity));
    }

    void Jump(bool isJumping)
    {
        if (isGrounded && isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }
    }
}