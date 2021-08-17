using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour {
    private Animator _Animator;
    [SerializeField] private int _SpikeActiveDelay;
    [SerializeField] private int _SpikeRetractTime;
    private GameObject _player;
    public Respawner Respawner;
    
    private void Awake() {
        _Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("player Found");
        StartCoroutine(SpikeTrigger());
    }

    private void OnTriggerStay(Collider other) {
        if (other != false) {
            _player = other.gameObject;
        }
        
    }


    private IEnumerator SpikeTrigger() {
        yield return new WaitForSeconds(_SpikeActiveDelay);
        _Animator.SetBool("isTriggered", true);
        if (_player != false) {
            Respawner.enabled = true;
        }
        yield return new WaitForSeconds(_SpikeRetractTime);
        Respawner.enabled = false;
        _Animator.SetBool("isTriggered", false);
    }

   

}
