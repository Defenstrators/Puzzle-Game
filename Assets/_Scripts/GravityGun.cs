using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
   public GameObject MuzzlePoint;
   public GameObject objectTarget;
   public float LaunchForce;
   public float range;
   bool hasGrabbedObject;
   GameObject grabbedObject;
   public float objectFollowDelay;
   public int cameraLookLimiter;
   public LineRenderer lineRenderer;

   public Material laserMaterial; 
   public float movingMultiplyer;
   AudioSource source;
   public AudioClip pickupSound;
   public AudioClip placeSound;
   public AudioClip fireSound;

    void Start() 
   {
       source = gameObject.GetComponent<AudioSource>();
   }
   
    void Update()
    {
        if(grabbedObject)
        {
            grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, objectTarget.transform.position, objectFollowDelay); //smoothly move the object to the objectTarget, so it doesnt jitter around and look unatural
            grabbedObject.transform.rotation = Quaternion.Lerp(grabbedObject.transform.rotation,objectTarget.transform.rotation, objectFollowDelay); 

            if(Input.GetButtonDown("Fire1"))
            {
                grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false; //set kinematic to false, to renable the physics of the object
                grabbedObject = null;
                hasGrabbedObject = false;
                gameObject.GetComponentInParent<PlayerMovement>().ChangeLookLimiters(80); // set the player vertical look limiters to defult
                source.PlayOneShot(placeSound); // play the placing sound
                lineRenderer.enabled = true; // turn the line render back on
            }
            if(Input.GetButtonDown("Fire2"))
            {
                grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false; //set kinematic to false, to renable the physics of the object
                if(Input.GetKey(KeyCode.W)) grabbedObject.GetComponent<Rigidbody>().AddForce(MuzzlePoint.transform.forward * (LaunchForce * movingMultiplyer)); // if the player is moving, apply a greater force to the grabbed object
                else grabbedObject.GetComponent<Rigidbody>().AddForce(MuzzlePoint.transform.forward * LaunchForce); // else just use normal values
                grabbedObject = null;
                hasGrabbedObject = false;
                gameObject.GetComponentInParent<PlayerMovement>().ChangeLookLimiters(80); // set the player vertical look limiters to defult
                source.PlayOneShot(fireSound); // play the fire sound
                lineRenderer.enabled = true; // turn the line render back on
            }
        }
        else
        {
                RaycastHit hit;
                if(Physics.Raycast(MuzzlePoint.transform.position, MuzzlePoint.transform.forward, out hit, range))
                {
                    if(hit.transform.tag == "Interactable")
                    {
                        if(Input.GetButtonDown("Fire1"))
                        {
                            grabbedObject = hit.transform.gameObject; 
                            hasGrabbedObject = true;
                            grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true; // we dont want the object to be affected by gravity when grabbed by the player;
                            gameObject.GetComponentInParent<PlayerMovement>().ChangeLookLimiters(cameraLookLimiter); // change the look limiters to constrained ones so the player cant ram the object under them.
                            lineRenderer.enabled = false; // disable the lazer
                            gameObject.GetComponentInParent<PlayerMovement>().ChangePrespective(false); // zoom the camera out
                            source.PlayOneShot(pickupSound); //play the pickup sound 
                        }

                        laserMaterial.color = Color.green; // change the lazer to green, to let the player know they can pick somthing up.
                    }
                }
                else laserMaterial.color = Color.red; // change the lazer to red so the player knows they cant pick somthing up.

                if(Input.GetButtonDown("Fire2"))
                {
                    gameObject.GetComponentInParent<PlayerMovement>().ChangePrespective(true); // this will zoom the camera in, to help the player aim
                }
                 if(Input.GetButtonUp("Fire2"))
                {
                    gameObject.GetComponentInParent<PlayerMovement>().ChangePrespective(false); // this will aim the camera out
                }
            
            lineRenderer.SetPosition(0, MuzzlePoint.transform.position); // set the positions of the line renderer
            lineRenderer.SetPosition(1, objectTarget.transform.position);
        }
    }
}