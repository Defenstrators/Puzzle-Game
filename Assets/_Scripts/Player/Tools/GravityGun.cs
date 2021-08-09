using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
   public GameObject MuzzlePoint;
   public GameObject objectTarget;
   public GameObject objectTargetOrigionalLocation;
   public float LaunchForce;
   public float range;
   public float rotateObjectSencitivity;
   float grabbedObjectOffset;
   bool hasGrabbedObject;
   GameObject grabbedObject;
   public float objectFollowDelay;
   public int cameraLookLimiter;
   public LineRenderer lazerLineRenderer;
   public LineRenderer DropLineRenderer;
   public Material laserMaterial; 
      public float movingMultiplyer;
   public GameObject cube;
   AudioSource source;
   float temporaryDistance;
   [Header("Sounds")]
   public AudioClip pickupSound;
   public AudioClip placeSound;
   public AudioClip fireSound;

    [Header("Lazer Settings")]
   public int lazerResolution;
   public float laserUpdateTime;
   public float lazerMovingAmplutude;
   

    void Start() 
   {
       source = gameObject.GetComponent<AudioSource>();
       lazerLineRenderer.positionCount = lazerResolution;
       StartCoroutine("Lazer");
   }
    void Update()
    {
         
        if(hasGrabbedObject)
        {
            //grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, transform.position + (transform.forward * grabbedObjectOffset) , objectFollowDelay); //smoothly move the object to the objectTarget, so it doesnt jitter around and look unatural
                grabbedObject.transform.position = Vector3.MoveTowards(grabbedObject.transform.position, objectTarget.transform.position, objectFollowDelay * Time.deltaTime);
            RaycastHit rHit;
            if(Physics.Raycast(grabbedObject.transform.position, -grabbedObject.transform.up, out rHit, Mathf.Infinity))
            {
                Vector3 hitPoint = new Vector3(grabbedObject.transform.position.x, grabbedObject.transform.position.y - rHit.distance, grabbedObject.transform.position.z);
                cube.transform.position = hitPoint; // this will display a cube directly below the object, to show where it will fall
               DropLineRenderer.SetPosition(0, grabbedObject.transform.position);
               DropLineRenderer.SetPosition(1, hitPoint); // theese will draw a line from the object, to the previously mentined cube;
            }
            if(Input.GetButtonDown("Fire1"))
            {
                DropObject(false);
            }
            if(Input.GetButtonDown("Fire2"))
            {
                DropObject(true);     
            }
            if(Input.mouseScrollDelta.y > 0 || Input.mouseScrollDelta.y < 0)
            {
                print("It Works!");
            }  

            if(Input.mouseScrollDelta.y > 0 || Input.mouseScrollDelta.y < 0)
            {
                //  if(Input.GetKey(KeyCode.LeftShift))
                //  {
                //      objectTarget.transform.position = new Vector3(objectTarget.transform.position.x, objectTarget.transform.position.y, 
                //      objectTarget.transform.position.z + Input.mouseScrollDelta.y);
                //      Debug.Log(Input.mouseScrollDelta);

                //  }
                //  else if (Input.GetKey(KeyCode.Q))
                //  {

                //  }
                    grabbedObject.transform.Rotate(new Vector3(0, (Input.mouseScrollDelta.y * rotateObjectSencitivity), 0)); // this will rotate the grabbed object on the y axis
            }  
        }
        else
        {
                RaycastHit hit;
                if(Physics.Raycast(MuzzlePoint.transform.position, MuzzlePoint.transform.forward, out hit, range))
                {
                    if(hit.transform.tag == "Interactable" || hit.transform.tag == "Interactable2")
                    {
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            grabbedObject = hit.transform.gameObject; 
                            hasGrabbedObject = true;
                            grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true; // we dont want the object to be affected by gravity when grabbed by the player;
                            gameObject.GetComponentInParent<PlayerMovement>().ChangeLookLimiters(cameraLookLimiter); // change the look limiters to constrained ones so the player cant ram the object under them.
                            lazerLineRenderer.enabled = false; // disable the lazer
                            gameObject.GetComponentInParent<PlayerMovement>().ChangePrespective(false); // zoom the camera out
                            source.PlayOneShot(pickupSound); //play the pickup sound 
                            cube.SetActive(true);
                            DropLineRenderer.enabled = true;
                            laserMaterial.color = Color.red;
                            grabbedObject.transform.rotation = new Quaternion(0, 0, 0, 0); // reset the objects rotation, so when when the player roatats, it will rotate on the correct axis.
                            grabbedObjectOffset = grabbedObject.GetComponent<GrabbableObject>().objectOffset;

                          
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
        }
    }

    void OnDrawGizmos() 
    {
        lazerLineRenderer.SetPosition(0, MuzzlePoint.transform.position); 
        lazerLineRenderer.SetPosition(1, MuzzlePoint.transform.position + (MuzzlePoint.transform.forward * range));
    }
    public void DropObject(bool fireObject)
    {
                grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false; //set kinematic to false, to renable the physics of the object
                if(fireObject)
                {
                     if(Input.GetKey(KeyCode.W)) grabbedObject.GetComponent<Rigidbody>().AddForce(MuzzlePoint.transform.forward * (LaunchForce * movingMultiplyer)); // if the player is moving, apply a greater force to the grabbed object
                     else grabbedObject.GetComponent<Rigidbody>().AddForce(MuzzlePoint.transform.forward * LaunchForce); // else just use normal values
                }
                grabbedObject = null;
                hasGrabbedObject = false;
                gameObject.GetComponentInParent<PlayerMovement>().ChangeLookLimiters(80); // set the player vertical look limiters to defult
                source.PlayOneShot(placeSound); // play the placing sound
                lazerLineRenderer.enabled = true; // turn the line render back on
                DropLineRenderer.enabled = false;
                cube.SetActive(false);
                objectTarget.transform.position = objectTargetOrigionalLocation.transform.position;
                StartCoroutine("Lazer");
               
    }
    IEnumerator Lazer()
    {
        while(!grabbedObject)
        {
                float offset = range / lazerResolution;
                float currentOffset = 0;
                Vector3 randomisedPosition;
                for(int i = 0; i < lazerResolution; i++)
                {
                    randomisedPosition = new Vector3(MuzzlePoint.transform.position.x, MuzzlePoint.transform.position.y, MuzzlePoint.transform.position.z);
                    randomisedPosition += (MuzzlePoint.transform.forward * currentOffset);
                    randomisedPosition.y += Random.Range(-lazerMovingAmplutude, lazerMovingAmplutude);
                    lazerLineRenderer.SetPosition(i, randomisedPosition);
                    currentOffset += offset;
                }
                yield return new WaitForSeconds(laserUpdateTime);
        }
    }
}