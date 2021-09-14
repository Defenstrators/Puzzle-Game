using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] GameObject[] locations;
    [SerializeField] GameObject buttonLocation;
    [SerializeField] float buttonInteractionDistance;
    [SerializeField] float elevatorSpeed;
    GameObject player;
   [SerializeField] int currentLocation = -1;
   [SerializeField]  int completedPuzzles = -1;
    private void Start() {
        player = Object.FindObjectOfType<PlayerMovement>().gameObject;
    }

    public void PuzzleSolved()
    {
        completedPuzzles++;
    }
    private void Update() 
    {
        if(Vector3.Distance(player.transform.position, buttonLocation.transform.position) < buttonInteractionDistance)
        {
            print("close enough");
            if(Input.GetKeyDown(KeyCode.E) && currentLocation < completedPuzzles)
            {
                currentLocation ++;
                StartCoroutine("MoveToDestination");
            }
        }
    }


    IEnumerator MoveToDestination()
    {
        
        while(Vector3.Distance(this.transform.position, locations[currentLocation].transform.position) > 0.01)
        {
            print("while called");
          transform.position = Vector3.MoveTowards(transform.position, locations[currentLocation].transform.position, elevatorSpeed * Time.deltaTime * Time.timeScale);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other) {
        other.transform.parent = this.transform;
    }
    
}
