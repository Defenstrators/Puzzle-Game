using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
     [TextArea]
    public string tutorialText;
    public int textDuration;
     public GameObject turtorialManager;

     private void OnTriggerEnter(Collider other) 
     {
         if(other.tag == "Player")
         {
             turtorialManager.GetComponent<TutorialManager>().ShowText(tutorialText, textDuration);
             Destroy(gameObject);
         }
         
     }
}
