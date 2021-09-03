using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;

    [Header("Movement Settings")]
    public float jumpHeight; // how high the player can jump
    public float gravity; // how fast the player will fall
    public float moveSpeed; // how fast the player will move
    public float fullSpeedTimer; // how long untill the player hits max speed
    public float coyoteTime; // how long can the player stand in mid air before gravity takes effect
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
  

    Vector3 playerVelocity;
    bool canJump;
    [SerializeField] float moveSpeedMultiplyer;
    
    float cameraRotation;
    bool isFirstPerson;
    [SerializeField] Animator animator;
    public float yLookLimitation;

    
    float trueCoyoteTime;


    void Start() 
    {
        controller = gameObject.GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() 
    { 
         Movement();
         //if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
    
    void Movement()
    {   
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            fullSpeedTimer += Time.deltaTime * acceleration;
            moveSpeedMultiplyer = Mathf.Lerp(moveSpeedMultiplyer, 1, fullSpeedTimer);
            animator.SetBool("isWalking", true);
            animator.SetFloat("AnimationSpeed", moveSpeedMultiplyer * 2);
            
        }
        else
        {
            moveSpeedMultiplyer -= Time.deltaTime * deceleration;
            animator.SetBool("isWalking", false);
            
        }
        fullSpeedTimer = Mathf.Clamp(fullSpeedTimer, 0, 1);
     
        //Moving  
        Vector3 move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime * transform.TransformDirection(Vector3.forward) + 
            Input.GetAxis("Horizontal") * (moveSpeed / 2) * Time.deltaTime * transform.TransformDirection(Vector3.right).normalized; //this will move the player foward, back, left and right
        
        controller.Move((move * moveSpeed) * moveSpeedMultiplyer);

        if(Input.GetButtonDown("Jump") && canJump)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravity);
            canJump = false;
        }

       // Gravity
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime); 

        
        if(canJump && playerVelocity.y < 0) // 
        {
            playerVelocity.y = 0f;
            
        }

        // coyote time, this will give the player an short window. after stepping of a collider, to jump from it, even when there not on it. this will 
        // make jumps less fustrating
        if(controller.isGrounded)
        {
            canJump = true;
            trueCoyoteTime = coyoteTime;
        }
        else
        {
            trueCoyoteTime -= Time.deltaTime;
            if(trueCoyoteTime <= 0)
            {
                canJump = false;
            }
        }
    }
}

