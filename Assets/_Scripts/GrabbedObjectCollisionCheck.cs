using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedObjectCollisionCheck : MonoBehaviour
{
  private void OnTriggerEnter(Collider other) 
  {
      Object.FindObjectOfType<GravityGun>().DropObject(false);
  }
}
