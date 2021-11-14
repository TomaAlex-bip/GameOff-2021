using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public float Speed  // gets the absolute value of updatedSpeed;
    {
        get
        {
            if (verticalMovement < 0)
            {
                return -verticalMovement * updatedMovementSpeed;
            }
            if (verticalMovement > 0)
            {
                return verticalMovement * updatedMovementSpeed;
            }
            if (horizontalMovement < 0)
            {
                return -horizontalMovement * updatedMovementSpeed;
            }
            if (horizontalMovement > 0)
            {
                return horizontalMovement * updatedMovementSpeed;
            }
            return 0;
        }
    }
    public bool Sprinting // gets the boolean value for when the player is moving faster, not when it's pressing the sprint button
    {
        get
        {
            return updatedMovementSpeed > movementSpeed;
        }
    }
    public bool Jumping // gets the negated value of isGrounded
    {
        get => !isGrounded;
    }

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;

    [SerializeField] private List<LayerMask> groundCheckLayers;
    
    
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float crouchMultiplier = 0.5f;
    [SerializeField] private float gravityMultiplier = 2f;
    [SerializeField] private float heightDivider = 0.6f;

    private const float GRAVITY = -9.81f;
    private float gravity;

    private float updatedMovementSpeed; // the actual speed of the player
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


    private LayerMask terrainLayerMask;

    private CameraLook cameraLook;


    private PlayerAnimationController playerAnim;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        SetTerrainLayerMask();
    }


    private void Start()
    {
        cameraLook = transform.GetComponent<CameraLook>();
        
        characterController = GetComponent<CharacterController>();

        playerAnim = PlayerAnimationController.Instance;
        
        groundCheck = transform.Find("GroundCheck");
        headCheck = transform.Find("HeadCheck");

        updatedMovementSpeed = movementSpeed;
        originalStepOffset = characterController.stepOffset;
        //originalHeight = transform.localScale.y;
        originalHeight = characterController.height;
        
        // just apply the multiplier to the gravity constant
        UpdateGravity();
    }
    
    private void Update()
    {
        GetInput();
        
        UpdateGravity();  // just set the actual gravity of the player to the GRAVITY const * multiplier
        
        CheckGrounding();  // check if the character is on ground
        
        CheckHittingRoof();
        
        //isGrounded = true;   // for infinite jumps bug
        UpdateJumping();
        
        UpdateMovementSpeed();
        
        MovePlayer();


        UpdateAnimations();
    }


    private void GetInput()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        
        jump = Input.GetKeyDown(KeyCode.Space);
        
        sprint = Input.GetKey(KeyCode.LeftShift);
        
        crouch = Input.GetKey(KeyCode.LeftControl) ||  Input.GetKey(KeyCode.C);
        
    }


    private void UpdateMovementSpeed()  // update the speed based on different factors, such as sprinting, crouching, being in midair, etc...
    {
        if (crouch) // crouched
        {
            updatedMovementSpeed = movementSpeed * crouchMultiplier;
            
            // !!!!! BUG: micsoareaza sau mareste si cutia pe care o tine playerul !!!!!!!
            //transform.localScale = new Vector3(transform.localScale.x, originalHeight / 2f, transform.localScale.z);
            // sa ma gandesc daca il las sau nu, pare un feature interesant

            characterController.height = originalHeight * heightDivider;
            //characterController.center = new Vector3(0f, -(originalHeight*heightDivider)/2f, 0f);
            characterController.center = new Vector3(0f, -0.4f, 0f);

            headCheck.localPosition = new Vector3(0f, 0.3f, 0f);

            cameraLook.CrouchCameraPoistion();
            
            // TO DO!!!!
            // move the ground check because is going under the floor

        }
        else if (isHittingRoof && Mathf.Abs(characterController.height - (originalHeight * heightDivider)) < 0.01f)
        {
           updatedMovementSpeed = movementSpeed * crouchMultiplier;
        }
        else if (sprint && isGrounded || sprint && jump)
        {
            if (verticalMovement > 0f)
            {
                updatedMovementSpeed = movementSpeed * sprintMultiplier;
            }
            else
            {
                updatedMovementSpeed = movementSpeed;
            }
        }
        else if (!sprint) 
        {
            updatedMovementSpeed = movementSpeed;
        }

        if (!crouch && isHittingRoof == false) // not crouched
        {
            //transform.localScale = new Vector3(transform.localScale.x, originalHeight, transform.localScale.z);
            characterController.height = originalHeight;
            characterController.center = new Vector3(0f, 0f, 0f);
            
            headCheck.localPosition = new Vector3(0f, 1, 0f);
            
            cameraLook.NormalCameraPosition();
        }

        
        
    }
    
    
    private void MovePlayer()  // WASD movement
    {
        var move = Vector3.ClampMagnitude(transform.forward * verticalMovement + transform.right * horizontalMovement, 1f);

        characterController.Move(move * updatedMovementSpeed * Time.deltaTime);
    }


    private void UpdateJumping()  // Check if the player wants to jump and simulate gravity for the character
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


    private void CheckGrounding()  // check if the character is on ground, and if it is, set it's y velocity to a small negative number, so it doesn't levitate
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, terrainLayerMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
            characterController.stepOffset = originalStepOffset;
            //updatedMovementSpeed = movementSpeed;
        }
        
    }


    private void CheckHittingRoof()  // check if the player is hitting the roof, and if he jumps, stop the velocity from going up
    {
        isHittingRoof = Physics.CheckCapsule(headCheck.position, headCheck.position + new Vector3(0f, 0.5f, 0f), 0.3f, terrainLayerMask);

        // this part here makes sure that is the player decide to jump and hits the roof, he goes down
        // otherwise, it will just stay up in the roof for the time he would be in jumping state
        if (isHittingRoof && velocity.y > 0)
        {
            velocity.y = -1f;
        }
    }

    // nu e bine
    private void UpdateAnimations()
    {
        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            if (!isGrounded)
            {
                playerAnim.JumpAnimation();
            }
            else if (Mathf.Abs(characterController.height - (originalHeight * heightDivider)) < 0.01f) // crouch
            {
                playerAnim.CrouchWalkAnimation();
            }
            else if (sprint)
            {
                playerAnim.RunAnimation();
            }
            else
            {
                playerAnim.WalkAnimation();
            }
        }
        else
        {
            if (!isGrounded)
            {
                playerAnim.JumpAnimation();
            }
            else if (Mathf.Abs(characterController.height - (originalHeight * heightDivider)) < 0.01f) // crouch
            {
                playerAnim.CrouchIdleAnimation();
            }
            else
            {
                playerAnim.IdleAnimation();
            }
            
        }
        
        
    }
    
    
    private void UpdateGravity() => gravity = GRAVITY * gravityMultiplier;


    private void SetTerrainLayerMask()
    {
        terrainLayerMask = 0;
        foreach (var layer in groundCheckLayers)
        {
            terrainLayerMask |= layer.value;
        }
    }








}
























