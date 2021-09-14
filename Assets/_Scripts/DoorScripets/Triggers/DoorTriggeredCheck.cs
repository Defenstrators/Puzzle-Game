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
   AudioSource source;
    [SerializeField] AudioClip OpenClose;
   
   
   [Header("Modifiers")]
   [SerializeField] private bool m_OpenDoor;     // Check of Doors Open.
   [SerializeField] private float m_TimeToClose;                // How long the door takes to close.
   public GameObject[] m_Triggers;       // Array of Triggers.
   private Animator m_Animator;
   
   private float m_Triggered;               // How Triggers are Pressed or active.
   private int m_Check;                  // Check how many are Triggered.
   private bool m_CoroutineBuffer;    // Buffer Check.

   bool puzzleSolved;
   [SerializeField] Elevator elevator;

   private void Start() {
      
      m_Animator = GetComponent<Animator>(); 
      source = GetComponent<AudioSource>();
   }
   private void Update() {

         //Debug.Log(m_Triggered);
         switch (m_DoorTypes) {
            case DoorTypes.OpenNClose:
               if (m_OpenDoor & m_Triggered < m_Triggers.Length && m_CoroutineBuffer == false) {
                  m_Animator.SetBool("isOpen", false);
                  source.PlayOneShot(OpenClose);
                  //gameObject.GetComponent<Renderer>().enabled = true;
                  gameObject.GetComponentInChildren<Transform>().gameObject.GetComponentInChildren<Collider>().enabled = true;
                  m_OpenDoor = false;
               }
               break;
            case DoorTypes.StaysOpen:
               if (m_OpenDoor && m_CoroutineBuffer == false) {
                  
               }
               break;

            case DoorTypes.Disappear:
               if (m_OpenDoor) {
                  gameObject.GetComponentInChildren<Transform>().gameObject.SetActive(false);
                  gameObject.GetComponentInChildren<Transform>().gameObject.GetComponentInChildren<Collider>().enabled = false;
               }
               break;
         }
   }

      /// <summary>
      /// Wait Time.
      /// </summary>
      /// <returns></returns>
      IEnumerator OpenDoor() {
         m_CoroutineBuffer = true;
         yield return new WaitForSeconds(m_TimeToClose);
         m_CoroutineBuffer = false;

      }

      /// <summary>
      /// Enables Doors and Disable doors depending on how many triggers are active.
      /// </summary>
      /// <param name="i"></param>
      ///
      private PressurePlates[] Plates;
      private Switch[] _Switches;
      public void DoorControl(float t) {
         m_Triggered += t;
         if (m_Triggered >= m_Triggers.Length && m_CoroutineBuffer == false) {
            m_OpenDoor = true;
            source.PlayOneShot(OpenClose);
            StartCoroutine(OpenDoor());
            m_Animator.SetBool("isOpen", true);
           if(!puzzleSolved)
           {
              elevator.PuzzleSolved();
              puzzleSolved = true;
           } 
         }
      }
   }

