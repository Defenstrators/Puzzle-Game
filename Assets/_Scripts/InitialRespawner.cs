using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InitialRespawner : MonoBehaviour
{
public string[] tags;
  private void OnTriggerEnter(Collider other) 
  {
      if(tags.Contains(other.tag))
      {
          gameObject.GetComponentInParent<Respawner>().Hit1stTrigger(other.gameObject);
          print("cube hit trigger");
      }
  }
}
