using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorTriggeredCheck : MonoBehaviour {

   enum DoorTypes {
      OpenNClose,
      StaysOpen,
      Disappear
   }
   [SerializeField] private DoorTypes m_DoorTypes;
   
   
   [Header("Modifiers")]
   [SerializeField] private bool openDoor;     // Check of Doors Open.
   [SerializeField] private float timeToClose;                // How long the door takes to close.
   public GameObject[] PressurePlates;       // Array of Pressure Plates.
   private Animator animator;
   
   private float triggered;               // How Triggers are Pressed or active.
   private int check;                  // Check how many are Triggered.
   private bool coroutineBuffer;    // Buffer Check.

   private void Start() {
      animator = GetComponent<Animator>();
   }

   private void Update() {
     Debug.Log(triggered);
     switch (m_DoorTypes) {
        case DoorTypes.OpenNClose:
           if (openDoor & triggered < PressurePlates.Length && coroutineBuffer == false) {
              //gameObject.GetComponent<Renderer>().enabled = true;
              animator.SetBool("isOpen", false);
              gameObject.GetComponentInChildren<Collider>().enabled = true;
              openDoor = false;
           }
              break;
        case DoorTypes.StaysOpen :
           if (openDoor && coroutineBuffer == false) {
              gameObject.GetComponentInChildren<Renderer>().enabled = false;
              gameObject.GetComponentInChildren<Collider>().enabled = false;
           }
           break;
        
        case DoorTypes.Disappear :
           if (openDoor) {
              gameObject.GetComponentInChildren<Renderer>().enabled = false;
              gameObject.GetComponentInChildren<Collider>().enabled = false;
           }
           break;
     }
   }

   /// <summary>
   /// Wait Time.
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
         animator.SetBool("isOpen", true);
         
         gameObject.GetComponentInChildren<Collider>().enabled = false;
      }
   }
   
   
}

