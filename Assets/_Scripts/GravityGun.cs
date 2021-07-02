using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
   public GameObject target;
   public Vector3 boxCastSize;
   public float LaunchForce;
   public float range;
   bool hasGrabbedObject;
   GameObject grabbedObject;
   
    void Update()
    {

        if(grabbedObject)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                grabbedObject.transform.parent = null;
                grabbedObject = null;
                hasGrabbedObject = false;
            }
            if(Input.GetButtonDown("Fire2"))
            {
                grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody>().AddForce(target.transform.forward * LaunchForce);
                grabbedObject.transform.parent = null;
                grabbedObject = null;
                hasGrabbedObject = false;

            }
        }
        else
        {
             if(Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                if(Physics.BoxCast(target.transform.position, boxCastSize, target.transform.forward, out hit, target.transform.rotation, range))
                {
                    if(hit.transform.tag == "Interactable")
                    {
                        hit.transform.position = target.transform.position;
                        hit.transform.parent = target.transform;
                        grabbedObject = hit.transform.gameObject; 
                        hasGrabbedObject = true;
                        grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true; // we dont want the object to be affected by gravity when grabbed by the player;
                    }
                }
            }
        }
    }
}
