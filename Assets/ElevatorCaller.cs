using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCaller : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] GameObject player;
    [SerializeField] Elevator elevator;
    void Update()
    {
       if(Vector3.Distance(this.transform.position, player.transform.position) < range)
       {
           if(Input.GetKeyDown(KeyCode.E))
           {
               elevator.PuzzleSolved();
               GetComponent<ElevatorCaller>().enabled = false;
           }
       } 
    }
}
