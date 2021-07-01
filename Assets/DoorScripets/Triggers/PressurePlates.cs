using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlates : MonoBehaviour {
    
    public bool isTriggered;        // Trigger Check
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionStay(Collision other) {
        if (other.collider.tag == "Player" || other.collider.tag == "Heavy?") {
            animator.SetBool("isTriggered", true);
            isTriggered = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.collider.tag == "Player" || other.collider.tag == "Heavy?") {
            animator.SetBool("isTriggered", false);
            isTriggered = false;
        }
    }


}
