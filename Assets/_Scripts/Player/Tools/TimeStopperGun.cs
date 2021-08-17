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
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip shoot, ding, release; 
    bool objectSelected = true;
    GameObject lastSelectedObject;

    [SerializeField] float outlineWidth;
    [SerializeField] Color selectedColor;
    [SerializeField] Color frozenColor;


    bool objectStopped;
    float timeLeft;
    public bool toolActive;

    private void Update() 
    {
        lr.SetPosition(0, muzzlePoint.transform.position);
        lr.SetPosition(1, (muzzlePoint.transform.position + muzzlePoint.transform.forward * range));


        if(toolActive && !objectStopped)
        {
            RaycastHit hit;
            if(Physics.Raycast(muzzlePoint.transform.position, muzzlePoint.transform.forward, out hit, range))
            {
                if(tagsToSlow.Contains(hit.transform.tag))
                {
                    objectSelected = true;
                    lastSelectedObject = hit.transform.gameObject;
                    hit.transform.GetComponentInChildren<Outline>().OutlineColor = selectedColor;
                    hit.transform.GetComponentInChildren<Outline>().OutlineWidth = outlineWidth;
                    if(Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine("StopObject", hit);
                    }
                    print(hit.transform.tag);

                }
                else objectSelected = false;
                
            }     
        }

        if(objectSelected == false)
        {
            lastSelectedObject.GetComponentInChildren<Outline>().OutlineWidth = 0;
            objectSelected = true;
        } 
         
    }

    IEnumerator StopObject(RaycastHit hit)
    {
       
        switch (hit.collider.tag)
        {
            case "MovingPlatform":
                hit.transform.gameObject.GetComponent<NewMovingPlatforms>().StopPlatform(true);
                hit.transform.gameObject.GetComponentInChildren<Outline>().OutlineWidth = outlineWidth;
                hit.transform.gameObject.GetComponentInChildren<Outline>().OutlineColor = frozenColor;
                objectStopped = true;
                for(int i = 0; i <= timeAmmount - 1; i++)
                {
                    source.PlayOneShot(ding);
                    yield return new WaitForSeconds(1f);
                }
                hit.transform.gameObject.GetComponent<NewMovingPlatforms>().StopPlatform(false);
                objectStopped = false;
                hit.transform.gameObject.GetComponentInChildren<Outline>().OutlineWidth = 0;

            break;

            case "Interactable":
                print("Interactable");
                hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hit.transform.gameObject.GetComponentInChildren<Outline>().OutlineWidth = outlineWidth;
                hit.transform.gameObject.GetComponentInChildren<Outline>().OutlineColor = frozenColor;
                objectStopped = true;

                for(int i = 0; i <= timeAmmount - 1; i++)
                {
                    source.PlayOneShot(ding);
                    yield return new WaitForSeconds(1f);
                }

                hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                objectStopped = false;
                hit.transform.gameObject.GetComponentInChildren<Outline>().OutlineWidth = 0;

            break;

            default:

            break;
        }
        
}

}
