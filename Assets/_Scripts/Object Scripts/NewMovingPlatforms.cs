using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewMovingPlatforms : MonoBehaviour
{
    public GameObject[] destinations;

    public float speed;
    public float stopTime;
    public string[] tagsToParent;
    public GameObject ray;
    int currentDestination;
    bool reversing;
    bool stopped;
     private bool playerParented;
    GameObject player;
    AudioSource source;
    [SerializeField] AudioClip movingSound;
    [SerializeField] AudioClip stopingSound;

    private void Start() {
        source = gameObject.GetComponent<AudioSource>();
        StartCoroutine("PlaySound");
    }
    void Update()
    {
        if(Vector3.Distance(transform.position, destinations[currentDestination].transform.position) < 0.1)
        {
            if(currentDestination == destinations.Length -1) reversing = true;
            if(reversing) currentDestination--;
            else currentDestination++;
            if(currentDestination == 0) reversing = false;
            source.Stop();
          //  source.PlayOneShot(stopingSound);
            stopped = true;
            Invoke("StartPlatform", stopTime);
        }

         if(!stopped) transform.position = Vector3.MoveTowards(transform.position, destinations[currentDestination].transform.position, speed * Time.deltaTime);

         if(playerParented)
         {
             if(Vector3.Distance(transform.position, player.transform.position) > 2.5)
             {
                 player.transform.parent = null;
                 playerParented = false;
             }
         }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(tagsToParent.Contains(other.tag))
        {
            other.transform.parent = this.transform;
            if(other.tag == "Player")
            {
                playerParented = true;
                player = other.gameObject;
            }
             

        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(tagsToParent.Contains(other.tag))
        {
            other.transform.parent = null;
            if(other.tag == "Player") playerParented = false;
        }
    }
    void StartPlatform()
    {
        print("starting platform");
        stopped = false;
    }

    public void StopPlatform(bool stop)
    {
        if(stop) stopped = true;
        else stopped = false;
    }

    [ContextMenu("DrawRay")]
    public void DrawRay()
    {
      LineRenderer lr = ray.GetComponent<LineRenderer>();
      lr.positionCount = destinations.Length;
      for(int i = 0; i < destinations.Length; i++)
      {
          lr.SetPosition(i, destinations[i].transform.localPosition);
          print(" oui ");
      }
    }

    IEnumerator PlaySound()
    {
        while(!stopped)
        {
          //  source.PlayOneShot(movingSound);
           yield return new WaitForSeconds(movingSound.length);
        }
        
    }
}
