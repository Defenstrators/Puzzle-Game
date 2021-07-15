using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorTriggeredCheck : MonoBehaviour {
   [Header("Modifiers")]
   
   [SerializeField] private bool openDoor;     // Check of Doors Open.
   [SerializeField] private float timeToClose;                // How long the door takes to close.
   public GameObject[] PressurePlates;       // Array of Pressure Plates.
   
   private float triggered;               // How Triggers are Pressed or active.
   private int check;                  // Check how many are Triggered.
   private bool coroutineBuffer;    // Buffer Check.
   private void Update() {
      Debug.Log(triggered);
      if (openDoor & triggered < PressurePlates.Length && coroutineBuffer == false) {
         gameObject.GetComponent<Renderer>().enabled = true;
         gameObject.GetComponent<Collider>().enabled = true;
         openDoor = false;
      }
   }

   /// <summary>
   /// Waits Time.
   /// </summary>
   /// <returns></returns>
   IEnumerator OpenDoor() {
      coroutineBuffer = true;
      yield return new WaitForSeconds(timeToClose);
      coroutineBuffer = false;

   }
   
   /// <summary>
   /// Enables Doors and Disable doors depending on how many triggers are active.
   /// </summary>
   /// <param name="i"></param>
   public void DoorControl(float i) {
      triggered += i;
      if (triggered == PressurePlates.Length && coroutineBuffer == false) {
         openDoor = true;
         StartCoroutine(OpenDoor());
         gameObject.GetComponent<Renderer>().enabled = false;
         gameObject.GetComponent<Collider>().enabled = false;
      }
   }
   
   
}

