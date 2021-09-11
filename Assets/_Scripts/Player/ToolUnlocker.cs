using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolUnlocker : MonoBehaviour
{
    public int toolToUnlock;
    public GameObject displayTool;
    public bool tutorialPopup;
    [TextArea]
    public string tutorialText;
    public int textDuration;
     public GameObject turtorialManager;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
          //  other.gameObject.GetComponentInChildren<ToolManager>().UnlockTool(toolToUnlock);
            turtorialManager.GetComponent<TutorialManager>().ShowText(tutorialText, textDuration);
            Destroy(displayTool);

        }
    }
}
