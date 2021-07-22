using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<KeyManager>().modifyKeys(1);
            Destroy(gameObject);
        }
    }
}
