using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrabbedObjectCollisionCheck : MonoBehaviour
{
  public string[] collisionTags;
  private void OnTriggerEnter(Collider other) 
  {
    if(collisionTags.Contains(other.gameObject.tag))
    {
      Object.FindObjectOfType<GravityGun>().DropObject(false);
    }
      
      
      
  }
}
