using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Respawner respawner;

    private void Start() 
    {
        respawner = GetComponentInParent<Respawner>();
    }
   private void OnTriggerEnter(Collider other) 
   {
       if(other.tag == "Player")
       {
           respawner.spawnpoint = this.transform.position;
       }
   }
}
