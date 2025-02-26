using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;

    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float JumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    bool readyToJump;
    bool isJump;
    bool isSprint;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;

    bool grounded;

    public Transform orientation;

    Vector3 moveDirection;
    Vector2 Input;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        isSprint = false;
        isJump = false;
    }
    private void OnMove(InputValue value)
    {
        Input = value.Get<Vector2>();

    }
    private void OnJumpStart()
    {
        isJump = true;
    }
    private void OnJumpStop()
    {
        isJump = false;
    }
    private void OnSprintStart()
    {
        isSprint = true;
    }
    private void OnSprintStop()
    {
        isSprint = false;
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        SpeedControl();
        JumpControl();
        StateHandler();
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * Input.y + orientation.right * Input.x;
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    }
    private void StateHandler()
    {
        if(grounded && isSprint)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
    private void JumpControl()
    {
        if (isJump && grounded && readyToJump)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
