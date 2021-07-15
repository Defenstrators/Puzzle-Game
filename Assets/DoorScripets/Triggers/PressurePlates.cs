using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlates : MonoBehaviour {
    
    public bool isTriggered;          // Trigger Check.
    private Animator animator;      // Animator of Button.

    private void Start() {
        animator = GetComponentInChildren<Animator>();
        m_DoorTriggeredCheck = gameObject.GetComponentInParent<DoorTriggeredCheck>();
    }

    private DoorTriggeredCheck m_DoorTriggeredCheck;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Interactable") {
            m_DoorTriggeredCheck.DoorControl(0.5f);
            animator.SetBool("ispressed", true);
        } else if (other.tag == "Player") {
            m_DoorTriggeredCheck.DoorControl(1f);
        }
    }

    private void OnTriggerExit(Collider other) {
        {
            if ( other.tag == "Interactable") {
                m_DoorTriggeredCheck.DoorControl(-0.5f);
                animator.SetBool("ispressed", false);
            } else if (other.tag == "Player") {
                m_DoorTriggeredCheck.DoorControl(-1f);
            }
    }
}


}
