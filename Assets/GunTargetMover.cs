using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTargetMover : MonoBehaviour
{
    [SerializeField] GameObject target;
    void Update()
    {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
    }
}
