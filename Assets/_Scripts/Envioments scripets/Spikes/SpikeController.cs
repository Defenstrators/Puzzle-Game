using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SpikeController : MonoBehaviour {
    enum SpikeType {
        Normal,
        TrollSpike
    }
    [SerializeField] private SpikeType m_SpikeType;
    private Animator _Animator;
    [Header("For Troll Spike")]
    [SerializeField] private float _activeDistanceFromSpike;
    private float _disableDistanceFromSpike;
    [Header("")]
    [SerializeField] private float _spikeActiveDelay;
    [SerializeField] private float _spikeRetractTime;
    private GameObject _player;
    private bool _playerFound;
    public Respawner Respawner;
    
    private void Awake() {
        switch (m_SpikeType) {
            case  SpikeType.Normal:
                this.GetComponent<SphereCollider>().enabled = false;
                break;
            case SpikeType.TrollSpike:
                _disableDistanceFromSpike = GetComponent<SphereCollider>().radius;
                this.GetComponent<BoxCollider>().enabled = false;
                break;
        }
        _Animator = GetComponent<Animator>();
    }
    private void Update() {
        //Debug.Log(Vector3.Distance(this.transform.position, _player.transform.position));
        switch (m_SpikeType) {
            case SpikeType.Normal:
                if (_playerFound) { StartCoroutine(SpikeTrigger()); } else { StartCoroutine(SpikeRetract()); }
                break;
            case SpikeType.TrollSpike:
                
                if (_playerFound && _player != null) {
                    if (Vector3.Distance(this.transform.position, _player.transform.position) >= _disableDistanceFromSpike) {
                        StartCoroutine(SpikeTrigger());
                    } else if (Vector3.Distance(this.transform.position, _player.transform.position) <= _activeDistanceFromSpike) {
                        StartCoroutine(SpikeRetract());
                    } else if (Vector3.Distance(this.transform.position, _player.transform.position) <= 2) {
                        StartCoroutine(SpikeRetract());
                    }
                }
                break;
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && other != null) {
            _playerFound = true;
        }
    } 

    private void OnTriggerStay(Collider other) {
        if (other != false) {
            _player = other.gameObject;
        } 
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            _playerFound = false;
            _player = null;
        }
    }


    private IEnumerator SpikeTrigger() {
        yield return new WaitForSeconds(_spikeActiveDelay);
        _Animator.SetBool("isTriggered", true);
        if (_player != false) {
            Respawner.enabled = true;
            yield return null;
        }
    }
    private IEnumerator SpikeRetract() {
        yield return new WaitForSeconds(_spikeRetractTime);
        Respawner.enabled = false;
        _Animator.SetBool("isTriggered", false);
    }
}
