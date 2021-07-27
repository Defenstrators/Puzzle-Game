using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolUnlocker : MonoBehaviour
{
    public int toolToUnlock;
    public GameObject displayTool;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponentInChildren<ToolManager>().UnlockTool(toolToUnlock);
            Destroy(displayTool);

        }
    }
}
