using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Animator animator;

    public enum playerStates { idle, isRunning, isJumping };
    public playerStates playerState = playerStates.idle;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public bool canMove;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Joystick joystick;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {

        canMove = false;

    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {

            velocity.y = -2f;

        }

        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;
        
        if (canMove && x != 0f && z != 0f)
        {

            playerState = playerStates.isRunning;
            controller.Move(move * speed * Time.deltaTime);

        }
        else
        {
            playerState = playerStates.idle;
        }

        if (SimpleInput.GetButtonDown("Jump") && isGrounded && canMove)
        {

            playerState = playerStates.isJumping;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        animator.SetInteger("playerState", (int) playerState);

    }

}
