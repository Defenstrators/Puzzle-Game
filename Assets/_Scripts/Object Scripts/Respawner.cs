using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Respawner : MonoBehaviour
{
    public string[] objectRespawnTags;
    public float objectFadeTime;
    [SerializeField] bool respawnAtCheckpoint;
    public Vector3 spawnpoint;
    bool lerping;
    GameObject _Object;
    float intesity;
    float currentTime;
    GameObject player;
   
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {       
            player = other.gameObject;
            StartCoroutine("PlayerRespawn");
            print("playerRespawnEvent!");
        }
        else if(objectRespawnTags.Contains(other.tag))
        {
            other.gameObject.GetComponent<GrabbableObject>().Respawn();
            lerping = false;
            currentTime = 0;
            foreach(Material material in _Object.GetComponentInChildren<Renderer>().materials)
            {
                 material.SetFloat("DissolveAmount", -1);
            }
           
        }
    }
    public void Hit1stTrigger(GameObject gameObject)
    {
        _Object = gameObject;
        lerping = true;
        print("hit 1st trigger called");
    }

    private void Update() 
    {
       if(lerping)
       {
        intesity = Mathf.Lerp(-1, 1, currentTime);
        if(currentTime <= objectFadeTime) currentTime += Time.deltaTime;

        foreach(Material material in _Object.GetComponentInChildren<Renderer>().materials)
        {
            material.SetFloat("DissolveAmount", intesity);     
        }
        
       }
    }

    IEnumerator PlayerRespawn()
    {
            player.GetComponent<CameraController>().PlayerRespawnEvent();
            yield return new WaitForSeconds(0.2f);
            if(respawnAtCheckpoint) player.transform.position = spawnpoint;
            else gameObject.GetComponentInParent<RoomManager>().resetRoom();
    }
}
