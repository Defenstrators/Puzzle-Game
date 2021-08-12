using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
  public GameObject[] objectsToRespawn;
  public GameObject playerRespawnPoint;


  public void resetRoom()
  {
      foreach(GameObject objects in objectsToRespawn)
      {
          objects.GetComponent<GrabbableObject>().Respawn();
      }

      GameObject player = Object.FindObjectOfType<PlayerMovement>().gameObject;

      player.transform.position = playerRespawnPoint.transform.position;
  }
 
}
