using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
   
    public bool isTriggered;        // Trigger Check
    private bool playerInTrigger;
    private DoorTriggeredCheck m_DoorTriggeredCheck;
    
    public Animator Leaver;
    
    private bool eKey;

    private void Start() {
        isTriggered = false;
        m_DoorTriggeredCheck = gameObject.transform.parent.transform.Find("Door").GetComponent<DoorTriggeredCheck>();
    }

    private void Update() {
        eKey = Input.GetKeyDown(KeyCode.E);
        if (eKey == true && playerInTrigger == true ) {
            isTriggered = !isTriggered;
            if (isTriggered == true) {
                StartCoroutine(SwitchOn());
            } else if (isTriggered == false) {
                StartCoroutine(SwitchOff());
            }
        }
    }

    IEnumerator SwitchOn() {
        m_DoorTriggeredCheck.DoorControl(1f);
        Leaver.SetBool("isFlipped", true);
        yield return null;
    }

    IEnumerator SwitchOff() {
        m_DoorTriggeredCheck.DoorControl(-1f);
        Leaver.SetBool("isFlipped", false);
        yield return null;
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
