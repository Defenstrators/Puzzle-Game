using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewMovingPlatforms : MonoBehaviour
{
    public GameObject[] destinations;

    public float speed;
    public float stopTime;
    public string[] tagsToParent;
    int currentDestination;
    bool reversing;
    bool stopped;
    bool playerParented;

    void Update()
    {
        if(Vector3.Distance(transform.position, destinations[currentDestination].transform.position) < 0.1)
        {
            if(currentDestination == destinations.Length -1) reversing = true;
            if(reversing) currentDestination--;
            else currentDestination++;
            if(currentDestination == 0) reversing = false;

            stopped = true;
            Invoke("StartPlatform", stopTime);
        }

         if(!stopped) transform.position = Vector3.MoveTowards(transform.position, destinations[currentDestination].transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) 
    {
        if(tagsToParent.Contains(other.tag))
        {
            other.transform.parent = this.transform;
            if(other.tag == "Player") playerParented = true;
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(tagsToParent.Contains(other.tag))
        {
            other.transform.parent = null;
        }
    }
    void StartPlatform()
    {
        stopped = false;
    }
}
