using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaver : MonoBehaviour
{
   
    public bool isTriggered;        // Trigger Check
    private bool playerInTrigger;
    
    public Animator Leaver;
    
    private bool eKey;

    private void Start() {
        
    }

    private void Update() {
        eKey = Input.GetKeyDown(KeyCode.E);
        if (eKey == true && playerInTrigger == true ) {
            Leaver.SetBool("isFlipped", isTriggered);
            isTriggered = !isTriggered;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player" || other.tag == "Heavy?") {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player" || other.tag == "Heavy?") {
            playerInTrigger = false;
        }
    }

}
