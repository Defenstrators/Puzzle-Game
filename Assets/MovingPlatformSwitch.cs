using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformSwitch : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] GameObject player;
    [SerializeField] NewMovingPlatforms platform;
    void Update()
    {
       if(Vector3.Distance(this.transform.position, player.transform.position) < 5)
       {
           if(Input.GetKeyDown(KeyCode.E))
           {
               platform.ButtonPressed();
               GetComponent<MovingPlatformSwitch>().enabled = false;
           }
       } 
    }
}
