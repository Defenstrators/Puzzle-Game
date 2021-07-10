using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovingPlatform : MonoBehaviour
{
    Vector3 startingDestination;
    public GameObject destination;
    public float speed;
    public float waitTime;
    bool hitDestination;
    bool canMove = true;
    bool playerParented;
    GameObject player;
    public string[] tagsToParent;
    public LineRenderer lineRenderer;

    private void Start() 
    {
        startingDestination = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(!hitDestination && canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed * Time.deltaTime);

            if(Vector3.Distance(transform.transform.position, destination.transform.position) <= 0.1)
            {
                StartCoroutine("ChangeDestination", true);
            }
        }
        else if(canMove)
        {
           transform.position = Vector3.MoveTowards(transform.position, startingDestination, speed * Time.deltaTime);

            if(Vector3.Distance(transform.transform.position, startingDestination) <= 0.1)
            {
                StartCoroutine("ChangeDestination", false);
            }     
        }

        if(playerParented)
        {
            if(Vector3.Distance(this.transform.position, player.transform.position) >= 2)
            {
                print("Doing Stuff");
                player.transform.parent = null;
                playerParented = false;
            }
        }
    }
    IEnumerator ChangeDestination(bool destination)
    {
        print("CorutineStarted");
        canMove = false;
        yield return new WaitForSeconds(waitTime);
        if(destination) hitDestination = true;
        else  hitDestination = false;
        canMove = true;
    }

    void OnTriggerEnter(Collider other) 
    {
        print(other.tag);
        if(tagsToParent.Contains(other.transform.tag)) other.transform.parent = this.transform;
        if(other.tag == "Player") playerParented = true;  
        
    }
    void OnTriggerExit(Collider other) 
    {
      if(tagsToParent.Contains(other.transform.tag)) other.transform.parent = null;
      if(other.tag == "Player") playerParented = false;
    
    }
    
    [ContextMenu("DrawRay")]

    void DrawRay()
    {
        lineRenderer.SetPosition(0, transform.localPosition);
        lineRenderer.SetPosition(1, destination.transform.localPosition);
    }

   [ContextMenu("AllignPoints")]
    void AllignPoints()
    {
        destination.transform.position = this.transform.position;
    }


    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawSphere(destination.transform.position, 0.1f);
    }

    
}
