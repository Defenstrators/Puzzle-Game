using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipline : MonoBehaviour
{
    [SerializeField] GameObject[] positions; // where the zipline will move too
    [SerializeField] float speed;
    [SerializeField] GameObject ziplineHead; // what will move, and what the player will attach too;
    [SerializeField] float interactionDistance;
    GameObject player;
    int currentPos;
    bool ziplineMoving;

    private void Start() 
    {
        player = Object.FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(Vector3.Distance(ziplineHead.transform.position, player.transform.position) < interactionDistance)
            {
                ziplineMoving = true;
                player.transform.parent = ziplineHead.transform;
                StartCoroutine("MoveZipline");
                if(currentPos == 1) currentPos = 0;
                else currentPos = 1;
            }
        }
    }

    IEnumerator MoveZipline()
    {
        while(Vector3.Distance(ziplineHead.transform.position, positions[currentPos].transform.position) < 0.5)
        {
            ziplineHead.transform.position = Vector3.MoveTowards(ziplineHead.transform.position, positions[currentPos].transform.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
