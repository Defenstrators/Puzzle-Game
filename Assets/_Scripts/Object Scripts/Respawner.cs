using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Respawner : MonoBehaviour
{
    public GameObject objectRespawnPoint;
    public GameObject playerRespawnPoint;
    public string[] objectRespawnTags;

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
        }
    }
}
