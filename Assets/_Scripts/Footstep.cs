using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    AudioSource source;
    public AudioClip footstep; // this is public, so other scripts can modify the sound.
    [SerializeField] float minPitch, maxPitch;
    [SerializeField] CharacterController playerCC;
    bool didStep;

    private void Start() {
        source = gameObject.GetComponent<AudioSource>();
    }
    public void Step() 
    {
        if(playerCC.isGrounded)
        {
                source.pitch = Random.Range(minPitch, maxPitch);
                source.PlayOneShot(footstep);
                didStep = false;
        }

        
    }
}
