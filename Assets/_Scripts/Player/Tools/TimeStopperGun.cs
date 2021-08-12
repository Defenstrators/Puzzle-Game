using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimeStopperGun : MonoBehaviour
{
    public int range;
    public float timeAmmount;
    public LineRenderer lr;
    public GameObject muzzlePoint;
    public string[] tagsToSlow;

    bool objectStopped;
    float timeLeft;
    public bool toolActive;

    private void Update() 
    {
        lr.SetPosition(0, muzzlePoint.transform.position);
        lr.SetPosition(1, (muzzlePoint.transform.position + muzzlePoint.transform.forward * range));


        if(toolActive)
        {
            RaycastHit hit;
            if(Physics.Raycast(muzzlePoint.transform.position, muzzlePoint.transform.forward, out hit, range) && !objectStopped)
            {
                print("raycast hit");
                if(tagsToSlow.Contains(hit.transform.tag))
                {
                    print("Print aiming at slowable object ");
                    if(Input.GetMouseButtonDown(0))
                    {
                        print("left click it");
                        StartCoroutine("StopObject", hit);
                    }
                }
            }
        }
         
    }

    IEnumerator StopObject(RaycastHit hit)
    {
       
        switch (hit.collider.tag)
        {
            case "MovingPlatform":

                hit.transform.gameObject.GetComponent<NewMovingPlatforms>().StopPlatform(true);
                objectStopped = true;
                yield return new WaitForSeconds(timeAmmount);
                hit.transform.gameObject.GetComponent<NewMovingPlatforms>().StopPlatform(false);
                objectStopped = false;

            break;

            case "Interactable":

                hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                objectStopped = true;
                yield return new WaitForSeconds(timeAmmount);
                hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                objectStopped = false;

            break;

            default:

            break;
        }
        
}

}
