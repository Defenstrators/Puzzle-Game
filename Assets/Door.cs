using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool needsKey;
    bool playerIsColliding;
    GameObject player;
    Animator animator;


    private void Start() {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update() 
    {
        if(playerIsColliding)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(needsKey)
                {
                    if(player.GetComponent<KeyManager>().GetKeys() > 0)
                    {
                        player.GetComponent<KeyManager>().modifyKeys(-1);
                        openDoor();
                    }
                    else
                    {
                        print("need more Keys");
                    }
                }
                else
                {
                    openDoor();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
           playerIsColliding = true;
           player = other.gameObject;
        }
        
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            playerIsColliding = false;
        }
        
    }

    void openDoor()
    {
        animator.Play("Open");
    }
}
