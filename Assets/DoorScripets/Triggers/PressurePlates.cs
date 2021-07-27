using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlates : MonoBehaviour {
    
    public bool isTriggered;            // Trigger Check.
    private Animator animator;        // Animator of Button.

    private void Start() {
        animator = GetComponentInChildren<Animator>();
        m_DoorTriggeredChecks = transform.parent.GetComponentsInChildren<DoorTriggeredCheck>();
    }

    private DoorTriggeredCheck[] m_DoorTriggeredChecks;

    private void OnTriggerEnter(Collider other) {
        if (isTriggered == false) {
            if (other.tag == "Interactable" || other.tag == "Player") {
                isTriggered = true;
                for (int i = 0; i < m_DoorTriggeredChecks.Length; i++) {    // Checks for all door Child to Parent.
                    m_DoorTriggeredChecks[i].DoorControl(1f);
                }
            animator.SetBool("ispressed", true);
        }
        }
    }

    private void OnTriggerExit(Collider other) {
        {
            if ( other.tag == "Interactable" && isTriggered == true || other.tag == "Player" && isTriggered == true) {
                isTriggered = false;
                for (int i = 0; i < m_DoorTriggeredChecks.Length; i++) {    // Checks for all door Child to Parent.
                    m_DoorTriggeredChecks[i].DoorControl(-1f);
                }
                animator.SetBool("ispressed", false);
            } 
    }
}


}
