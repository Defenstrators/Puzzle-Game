using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorTriggeredCheck : MonoBehaviour {
   [Header("Modifiers")]
   [SerializeField]
   private bool closeDoor;
   [SerializeField]
   private float doorCloseTimer;
   public GameObject[] PressurePlates; // Array of Pressure Plates
   private int check; // Check how many are Triggered.
   private bool coroutineBuffer;
   private bool isTriggered;
   private void Update() {
      for (int i = 0; i < PressurePlates.Length; i++) {
         if (PressurePlates[i].tag == "PressurePlate") {
             isTriggered = PressurePlates[i].GetComponent<PressurePlates>().isTriggered;
         } else if (PressurePlates[i].tag == "Leaver") {
            isTriggered = PressurePlates[i].GetComponent<leaver>().isTriggered;
         }
         
         if (isTriggered) { // add Trigger to check.
            check++;
         }
      }
      if (closeDoor == true) {
         // If Player steps off or heavy object Steps off pressure plate disable it.
         if (check == PressurePlates.Length && coroutineBuffer == false) {
            StopCoroutine(OpenDoor());
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            coroutineBuffer = false;
         } else if (check >= PressurePlates.Length && coroutineBuffer == false) {
            // If Player steps off or heavy object Steps off pressure plate Enable it.
            StartCoroutine(OpenDoor());
            check = 0;
         } else { check = 0; }
      } else if (closeDoor == false) {
         {
            if (check == PressurePlates.Length && coroutineBuffer == false) {
               gameObject.GetComponent<Renderer>().enabled = false;
               gameObject.GetComponent<Collider>().enabled = false;
            } else { check = 0; }
         }
      }
   }

   IEnumerator OpenDoor() {
      coroutineBuffer = true;
      yield return new WaitForSeconds(doorCloseTimer);
      coroutineBuffer = false;
      Debug.Log("Check2");
      gameObject.GetComponent<Renderer>().enabled = true;
      gameObject.GetComponent<Collider>().enabled = true;
   }
}
