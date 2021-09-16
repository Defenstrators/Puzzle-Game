using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformSwitch : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] GameObject player;
    [SerializeField] NewMovingPlatforms platform;
    [SerializeField] Material onMaterial;
    [SerializeField] GameObject lightBar;
    void Update()
    {
       if(Vector3.Distance(this.transform.position, player.transform.position) < 5)
       {
           if(Input.GetKeyDown(KeyCode.E))
           {
               platform.ButtonPressed();
                lightBar.GetComponent<Renderer>().material = onMaterial;
               GetComponent<MovingPlatformSwitch>().enabled = false;
           }
       } 
    }
}
