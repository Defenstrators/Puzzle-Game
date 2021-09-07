using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;

    [Header("Movement Settings")]
    public float jumpHeight; // how high the player can jump
    public float gravity; // how fast the player will fall
    public float moveAcceleration; // how fast the player will move
    public float fullSpeedTimer; // how long untill the player hits max speed
    public float coyoteTime; // how long can the player stand in mid air before gravity takes effect
    [SerializeField] float sprintingMultiplyer; // how much faster the player will move;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
   [SerializeField] Vector3 playerVelocity;
    bool canJump;
    [SerializeField] float moveSpeedMultiplyer;
    float trueSprintingMultiplyer; // what sprinting multiplyer will be used in the movement fomula.
    
    float cameraRotation;
    bool isFirstPerson;
    [SerializeField] Animator animator;
    public float yLookLimitation;
    [SerializeField] Vector3 move;
    [SerializeField] float maxZVelocity;
    float trueCoyoteTime;
    void Start() 
    {
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() 
    { 
         Movement();
    }
    void Movement()
    {   
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            moveSpeedMultiplyer += Time.deltaTime * acceleration;
            //moveSpeedMultiplyer = Mathf.Lerp(moveSpeedMultiplyer, 1, fullSpeedTimer);
            animator.SetBool("isWalking", true);
            animator.SetFloat("AnimationSpeed", moveSpeedMultiplyer * trueSprintingMultiplyer);
        }
        else
        {
            moveSpeedMultiplyer -= Time.deltaTime * deceleration;
            animator.SetBool("isWalking", false);
        }

        fullSpeedTimer = Mathf.Clamp(fullSpeedTimer, 0, 1);
        moveSpeedMultiplyer = Mathf.Clamp(moveSpeedMultiplyer, 0, 1);
     
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSprintingMultiplyer = sprintingMultiplyer;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSprintingMultiplyer = 1;
        }

        if(canJump)
        {
            move = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) + 
            Input.GetAxis("Horizontal") *  transform.TransformDirection(Vector3.right); //this will move the player foward, back, left and right
            move = move *  ((moveAcceleration * sprintingMultiplyer) * moveSpeedMultiplyer) * Time.deltaTime;
            move.z = Mathf.Clamp(move.z, -maxZVelocity, maxZVelocity);
            controller.Move(move);
        }
        else
        {
           // lastVelocity.z += Input.GetAxis("Vertical") * Time.deltaTime * 0.1f;
            controller.Move(move);
            
        }
        
        if(Input.GetButtonDown("Jump") && canJump)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravity);
            canJump = false;
        }

 
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

