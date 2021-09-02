using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] GameObject cameraHead;

    [SerializeField] GameObject player;

   
    void Update()
    {
        transform.LookAt(player.transform);
    }
}
