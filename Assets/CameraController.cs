using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject cameraTarget; // where the cinemachine brain will look at
    [SerializeField] float mouseSense; // multiplyer for camera movement

    [SerializeField] float lookLimiter; // how far up/down the player can look in degrees
    [SerializeField] Animator animator;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip deathSound;

    float cameraRotation;
    public bool cutScene; // if the player is currently in a cutscene

    private void Start() 
    {
        mouseSense = PlayerPrefs.GetFloat("mouseSence");
        source.GetComponent<AudioSource>();
    }
    void Update()
    {
       if(!cutScene)PlayerControll();
    }

    void PlayerControll()
    {
         transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSense, 0)); // this will rotate the player

        cameraRotation += -Input.GetAxis("Mouse Y") * (mouseSense / Camera.main.aspect);

        cameraRotation = Mathf.Clamp(cameraRotation, -lookLimiter, lookLimiter);

        cameraTarget.transform.localRotation = Quaternion.Euler(cameraRotation, 0, 0);
    }

    public void ChangeLookLimiters(float lookLimit)
    {
        lookLimiter = lookLimit;
    }

    public void PlayerRespawnEvent()
    {
        StartCoroutine("RespawnEvent");
    }

    IEnumerator RespawnEvent()
    {
        source.PlayOneShot(deathSound);
        cutScene = true;
        animator.Play("AnimatorImageFadeToBlack");
        yield return new WaitForSeconds(0.2f);
        animator.Play("AnimatorImageFadeToNormal");
        yield return new WaitForSeconds(1f);
        cutScene = false;
    }
}
