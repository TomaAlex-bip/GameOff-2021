using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;

    [SerializeField] private LayerMask groundCheckLayer;
    
    
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float crouchMultiplier = 0.5f;
    [SerializeField] private float gravityMultiplier = 1f;

    private const float GRAVITY = -9.81f;
    private float gravity;

    private float updatedMovementSpeed;
    private float originalStepOffset;
    private float originalHeight;
    
    private float horizontalMovement;
    private float verticalMovement;
    private bool jump;
    private bool sprint;
    private bool crouch;

    private CharacterController characterController;

    private Transform groundCheck;
    private Transform headCheck;

    private bool isGrounded;
    private bool isHittingRoof;
    

    private Vector3 velocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        groundCheck = transform.Find("GroundCheck");
        headCheck = transform.Find("HeadCheck");

        updatedMovementSpeed = movementSpeed;
        originalStepOffset = characterController.stepOffset;
        originalHeight = transform.localScale.y;
        
        // just apply the multiplier to the gravity constant
        UpdateGravity();
    }
    
    private void Update()
    {
        GetInput();
        
        UpdateGravity();
        
        CheckGrounding();
        
        CheckHittingRoof();
        
        //isGrounded = true;   // for infinite jumps BUG
        UpdateJumping();
        
        UpdateMovementSpeed();
        
        MovePlayer();


    }


    private void GetInput()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        
        jump = Input.GetKeyDown(KeyCode.Space);
        
        sprint = Input.GetKey(KeyCode.LeftShift);
        
        crouch = Input.GetKey(KeyCode.LeftControl) ||  Input.GetKey(KeyCode.C);
        
    }


    private void UpdateMovementSpeed()  // update the speed based on different factors, such as sprinting, crouching, being in midair
    {
        if (crouch)
        {
            updatedMovementSpeed = movementSpeed * crouchMultiplier;
            transform.localScale = new Vector3(transform.localScale.x, originalHeight / 2f, transform.localScale.z);
        }
        else if (isHittingRoof && transform.localScale.y == originalHeight / 2f)
        {
           updatedMovementSpeed = movementSpeed * crouchMultiplier;
        }
        else if (sprint && isGrounded || sprint && jump)
        {
            updatedMovementSpeed = movementSpeed * sprintMultiplier;
        }
        else if (!sprint) 
        {
            updatedMovementSpeed = movementSpeed;
        }

        if (!crouch && isHittingRoof == false)
        {
            transform.localScale = new Vector3(transform.localScale.x, originalHeight, transform.localScale.z);
        }

        
        
    }
    
    
    private void MovePlayer()  // WASD movement
    {
        var move = Vector3.ClampMagnitude(transform.forward * verticalMovement + transform.right * horizontalMovement, 1f);

        characterController.Move(move * updatedMovementSpeed * Time.deltaTime);
    }


    private void UpdateJumping()  // Check if Jumping and simulate gravity for the character
    {
        if (jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            characterController.stepOffset = 0f;
        }

        velocity.y += gravity * Time.deltaTime;
        // nu stiu inca de ce, dar daca mut inmultirea cu Time.deltaTime sus face urat
        characterController.Move(velocity * Time.deltaTime);
    }


    private void CheckGrounding()  // check if the character is on ground
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundCheckLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
            characterController.stepOffset = originalStepOffset;
            //updatedMovementSpeed = movementSpeed;
        }
        
    }


    private void CheckHittingRoof()
    {
        
        isHittingRoof = Physics.CheckCapsule(headCheck.position, headCheck.position + new Vector3(0f, 0.5f, 0f), 0.3f, groundCheckLayer);
        

        if (isHittingRoof && velocity.y > 0)
        {
            velocity.y = -1f;
        }
    }
    
    
    
    

    private void UpdateGravity() => gravity = GRAVITY * gravityMultiplier;











}
























