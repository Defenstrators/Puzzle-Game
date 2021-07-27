using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrabbableObject : MonoBehaviour
{
  public string[] collisionTags;
  public int objectOffset;
  private void OnTriggerEnter(Collider other) 
  {
    if(collisionTags.Contains(other.gameObject.tag))
    {
      Object.FindObjectOfType<GravityGun>().DropObject(false);
    }
      
      
      
  }
}
