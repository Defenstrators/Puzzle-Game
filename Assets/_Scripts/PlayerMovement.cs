using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;

    public float jumpHeight;
    public float gravity;
    public float moveSpeed;
    public GameObject head;

    Vector3 playerVelocity;
    bool canJump;

    public float mouseSense;
    float cameraRotation;


    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;

    bool isFirstPerson;
    Animator animator;


    void Start() 
    {
        controller = gameObject.GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update() 
    { 
         CameraMovement();
        //  if(Input.GetKeyDown(KeyCode.LeftControl)) 
        //  {
        //       // when ctrl is preessed, move the camera to the head
        //      firstPersonCamera.SetActive(true);
        //      thirdPersonCamera.SetActive(false); 
        //      isFirstPerson = true;
        //  }
        //  if(Input.GetKeyUp(KeyCode.LeftControl))
        //  {
        //      firstPersonCamera.SetActive(false);
        //      thirdPersonCamera.SetActive(true);
        //       // when letting go of ctrl, move the camera back to the origional spot
        //      isFirstPerson = false;
        //  }
         if(!isFirstPerson) // if not in first person, do movement
         {
             Movement();
         }
    }
    void Movement()
    {
        
        if(controller.isGrounded) canJump = true;

        if(canJump && playerVelocity.y < 0) // 
        {
            playerVelocity.y = 0f;
            
        }
     
        //Moving  
        Vector3 move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime * transform.TransformDirection(Vector3.forward) + 
        Input.GetAxis("Horizontal") * (moveSpeed / 2) * Time.deltaTime * transform.TransformDirection(Vector3.right); //this will move the player foward, back, left and right

        controller.Move(move * moveSpeed);

        if(Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Walk", true);
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("Walk", false);
        }
        //Jumping

        if(Input.GetButtonDown("Jump") && canJump)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravity);
            canJump = false;
        }

       // Gravity
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime); 


      
    }
    
    void CameraMovement()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSense, 0)); // this will rotate the player
        //head.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * mouseSense, 0, 0)); // this will move the head up and down

        cameraRotation += -Input.GetAxis("Mouse Y") * (mouseSense / Camera.main.aspect);

        cameraRotation = Mathf.Clamp(cameraRotation, -90, 90);

        head.transform.localRotation = Quaternion.Euler(cameraRotation, 0, 0);
    }
}
