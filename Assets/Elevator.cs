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
    [SerializeField]  float completedPuzzles = -1;
    [SerializeField] GameObject elevatorLight;
    [SerializeField] Material red;
    [SerializeField] Material green;
    private void Start() {
        player = Object.FindObjectOfType<PlayerMovement>().gameObject;
    }

    public void PuzzleSolved() // this can be called by anything to advance the elevator
    {
        completedPuzzles++;
    }
    public void SemiPuzzleSolved(float i)
    {
        completedPuzzles += i;
    }
    private void Update() 
    {
        if(Vector3.Distance(player.transform.position, buttonLocation.transform.position) < buttonInteractionDistance)
        {
            print("close enough");
            if(Input.GetKeyDown(KeyCode.E) && currentLocation < completedPuzzles) // if the player has solved more puzzles then their current floor, then start moving.
            {
                currentLocation ++;
                StartCoroutine("MoveToDestination");
            }
        }
        if(currentLocation < completedPuzzles)
        {
            elevatorLight.GetComponent<Renderer>().material = green;
        }
        else elevatorLight.GetComponent<Renderer>().material = red;
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
        other.transform.parent = this.transform; // stick the player to the elevator.
    }
    
}
