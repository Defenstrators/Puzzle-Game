using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrabbableObject : MonoBehaviour
{
  public string[] collisionTags;
  public float objectOffset;
  public GameObject home;


      private void Start() 
      {
        StartCoroutine("OutOfBoundsCheck");
      }

      private void OnTriggerEnter(Collider other) 
      {
        if(collisionTags.Contains(other.gameObject.tag))
        {
          Object.FindObjectOfType<GravityGun>().DropObject(false);
        }
          
        
      }

     public void Respawn()
      {
        transform.position = home.transform.position;
      }

      IEnumerator OutOfBoundsCheck()
      {
            while(true)
            {
              if(transform.position.y < -30)
              {
                Respawn();
              }

              yield return new WaitForSeconds(1);
            }
      }
}
