using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCaller : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] GameObject player;
    [SerializeField] Elevator elevator;
    [SerializeField] Material onMaterial;
    [SerializeField] GameObject lightBar;
    public float count;
    public bool multipleButtons;
    void Update()
    {
       if(Vector3.Distance(this.transform.position, player.transform.position) < range)
       {
           if(Input.GetKeyDown(KeyCode.E))
           {
             if(!multipleButtons)  elevator.PuzzleSolved();
             else elevator.SemiPuzzleSolved(count);
             lightBar.GetComponent<Renderer>().material = onMaterial;
             GetComponent<ElevatorCaller>().enabled = false;
           }
       } 
    }
}
