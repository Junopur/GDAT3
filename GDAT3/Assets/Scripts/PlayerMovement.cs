using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private AudioSource Footsteps;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;
    
    private PlayerInputs playerInputs;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Gameplay.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Footsteps = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // ground check 
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        SetInputValues();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void SetInputValues()
    {
        Vector2 inputVec = PlayerInputManager.Instance.GetMovementInput();

        horizontalInput = inputVec.x;
        verticalInput = inputVec.y;
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (moveDirection == Vector3.zero && Footsteps.isPlaying)
        {
            // stop playing
        }
        else if (Footsteps.isPlaying == false)
        {
            // start playing
        }
        rb.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
    }

    private void SpeedControl()
    {
        var velocity = rb.velocity;
        Vector3 flatVel = new Vector3(velocity.x, 0f, velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
