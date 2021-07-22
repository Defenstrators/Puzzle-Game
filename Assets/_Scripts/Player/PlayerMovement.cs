using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;
    public float jumpHeight; // how high the player can jump
    public float gravity; // how fast the player will fall
    public float moveSpeed; // how fast the player will move
    public float fullSpeedTimer; // how long untill the player hits max speed
    public float coyoteTime; // how long can the player stand in mid air before gravity takes effect
    public float mouseSense; // multiplyer for how much the mouse will rotate the camera

    Vector3 playerVelocity;
    bool canJump;
float moveSpeedMultiplyer;
    
    float cameraRotation;


    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public GameObject head;
    bool isFirstPerson;
    Animator animator;
    public float yLookLimitation;

    
    float trueCoyoteTime;


    void Start() 
    {
        controller = gameObject.GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        // animator = gameObject.GetComponent<Animator>();
    }

    void Update() 
    { 
         Movement();
         CameraMovement();
    }
    
    void Movement()
    {
        
        if(Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Vertical") < 0.1 || Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < 0.1)
        {
            moveSpeedMultiplyer = Mathf.Lerp(moveSpeedMultiplyer, 1, fullSpeedTimer);
        }
        else
        {
            moveSpeedMultiplyer = 0;
        }
     
        //Moving  
        Vector3 move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime * transform.TransformDirection(Vector3.forward) + 
        Input.GetAxis("Horizontal") * (moveSpeed / 2) * Time.deltaTime * transform.TransformDirection(Vector3.right); //this will move the player foward, back, left and right

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
    
    void CameraMovement()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSense, 0)); // this will rotate the player
        //head.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * mouseSense, 0, 0)); // this will move the head up and down

        cameraRotation += -Input.GetAxis("Mouse Y") * (mouseSense / Camera.main.aspect);

        cameraRotation = Mathf.Clamp(cameraRotation, -yLookLimitation, yLookLimitation);

         head.transform.localRotation = Quaternion.Euler(cameraRotation, 0, 0);
    }

    public void ChangeLookLimiters(int value)
    {
        yLookLimitation = value;
        print("Changed Look Limters to: " + value);
    }

    public void ChangePrespective(bool prespective)
    {
        if (prespective == true)
        {
              firstPersonCamera.SetActive(true);
             thirdPersonCamera.SetActive(false); 
        }
        else
        {
              firstPersonCamera.SetActive(false);
             thirdPersonCamera.SetActive(true);
        }
    }
}

