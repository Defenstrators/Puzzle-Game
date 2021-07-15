using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlates : MonoBehaviour {
    
    public bool isTriggered;            // Trigger Check.
    private Animator animator;        // Animator of Button.

    private void Start() {
        animator = GetComponentInChildren<Animator>();
        m_DoorTriggeredCheck = gameObject.GetComponentInParent<DoorTriggeredCheck>();
    }

    private DoorTriggeredCheck m_DoorTriggeredCheck;

    private void OnTriggerEnter(Collider other) {
        if (isTriggered == false) {
            if (other.tag == "Interactable" || other.tag == "Player") {
                isTriggered = true;
            m_DoorTriggeredCheck.DoorControl(1f);
            animator.SetBool("ispressed", true);
        }
        }
    }

    private void OnTriggerExit(Collider other) {
        {
            if ( other.tag == "Interactable" && isTriggered == true || other.tag == "Player" && isTriggered == true) {
                isTriggered = false;
                m_DoorTriggeredCheck.DoorControl(-1f);
                animator.SetBool("ispressed", false);
            } 
    }
}


}
