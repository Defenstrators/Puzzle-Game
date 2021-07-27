using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Respawner : MonoBehaviour
{
    public GameObject objectRespawnPoint;
    public GameObject playerRespawnPoint;
    public string[] objectRespawnTags;
    public float objectFadeTime;
    bool lerping;
    GameObject _Object;
    float intesity;
    float currentTime;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            other.transform.position = playerRespawnPoint.transform.position;
        }
        else if(objectRespawnTags.Contains(other.tag))
        {
            other.transform.position = objectRespawnPoint.transform.position;
            if(other.GetComponent<Rigidbody>())
            {
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                
            }
            lerping = false;
            currentTime = 0;
        }
    }
    public void Hit1stTrigger(GameObject gameObject)
    {
        _Object = gameObject;
        lerping = true;
        print("hit 1st trigger called");
    }

    private void Update() 
    {
       if(lerping)
       {
        intesity = Mathf.Lerp(-1, 1, currentTime);
        if(currentTime <= objectFadeTime) currentTime += Time.deltaTime;
        _Object.GetComponent<Renderer>().material.SetFloat("DissolveAmount", intesity);
       }
    }
}
