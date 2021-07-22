using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody rB;

    private void Start() {
        rB = GetComponent<Rigidbody>();
    }
    private void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        rB.MovePosition(rB.transform.position + transform.forward * (horizontal * 3f * Time.fixedDeltaTime));

    }
}
