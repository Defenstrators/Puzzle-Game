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
   
    void Update()
    {

        if(grabbedObject)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                grabbedObject = null;
                hasGrabbedObject = false;
                gameObject.GetComponentInParent<PlayerMovement>().ChangeLookLimiters(90);
            }
            if(Input.GetButtonDown("Fire2"))
            {
                grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody>().AddForce(MuzzlePoint.transform.forward * LaunchForce);
                grabbedObject = null;
                hasGrabbedObject = false;
                gameObject.GetComponentInParent<PlayerMovement>().ChangeLookLimiters(90);

            }
            grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, objectTarget.transform.position, objectFollowDelay);
            grabbedObject.transform.rotation = objectTarget.transform.rotation;
        }
        else
        {
             if(Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                if(Physics.Raycast(MuzzlePoint.transform.position, MuzzlePoint.transform.forward, out hit, range))
                {
                    if(hit.transform.tag == "Interactable")
                    {
                        grabbedObject = hit.transform.gameObject; 
                        hasGrabbedObject = true;
                        grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true; // we dont want the object to be affected by gravity when grabbed by the player;
                        gameObject.GetComponentInParent<PlayerMovement>().ChangeLookLimiters(cameraLookLimiter);
                    }
                }
            }
        }
    }
}
