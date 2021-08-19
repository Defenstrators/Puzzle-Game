using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    enum SwitchType {
        TimerSwitch,
        NormalSwitch
    }
    [SerializeField] private SwitchType m_SwitchType;
    
    public bool m_InteractionCheck;        // When Enable Has been Interacted with / Is on state.
    private bool m_playerInTriggerCheck;
    private DoorTriggeredCheck[] m_DoorTriggeredChecks;
    public GameObject externalDoors;

    [SerializeField] private Transform[] _TransformsMaterials;
    [SerializeField] private Material[] _Materials;
    
    // public Animator m_Animator;
    
    private bool eKey;

    private void Start() {
        m_InteractionCheck = false;
        m_DoorTriggeredChecks = transform.parent.GetComponentsInChildren<DoorTriggeredCheck>();
    }

    private void Update() {
        eKey = Input.GetKeyDown(KeyCode.E);
        if (eKey == true && m_playerInTriggerCheck == true ) {
            switch (m_SwitchType) {
                case SwitchType.NormalSwitch:           // Used as An Leaver/Switch in this state.
                    m_InteractionCheck = !m_InteractionCheck;
                    if (m_InteractionCheck == true) {
                        StartCoroutine(SwitchOn());
                    } else if (m_InteractionCheck == false) {
                        StartCoroutine(SwitchOff());
                    }
                    break;
                case SwitchType.TimerSwitch:            // When Button is Press it Turns on for a time Then Turns off.
                    
                    break;
            }
        }
    }

    /// <summary>
    /// Put thing you want to happen in this when Switch is on.
    /// </summary>
    /// <returns>Null</returns>
    IEnumerator SwitchOn() {
        if (m_DoorTriggeredChecks != null) {
            for (int i = 0; i < m_DoorTriggeredChecks.Length; i++) {    // Checks for all door Child to Parent.
                m_DoorTriggeredChecks[i].DoorControl(1f);
                if (externalDoors != null) {
                    externalDoors.GetComponent<DoorTriggeredCheck>().DoorControl(1);
                }
                
            }
        }
        foreach (Transform transform in _TransformsMaterials) {
            transform.GetComponent<Renderer>().material = _Materials[1];
        }
       // m_Animator.SetBool("isFlipped", true);
        yield return null;
    }
    /// <summary>
    /// Put thing you want to happen when Switch is off.
    /// </summary>
    /// <returns></returns>
    IEnumerator SwitchOff() {
        if (m_DoorTriggeredChecks != null) {
            for (int i = 0; i < m_DoorTriggeredChecks.Length; i++) {    // Checks for all door Child to Parent.
                m_DoorTriggeredChecks[i].DoorControl(-1f);
                if (externalDoors != null) {
                    externalDoors.GetComponent<DoorTriggeredCheck>().DoorControl(-1);
                }
            }
        }
       // m_Animator.SetBool("isFlipped", false);
        foreach (Transform transform in _TransformsMaterials) {
            transform.GetComponent<Renderer>().material = _Materials[0];
        }
        yield return null;
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {       // Checks for player.
            m_playerInTriggerCheck = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {       // Checks for player.
            m_playerInTriggerCheck = false;
        }
    }

}
