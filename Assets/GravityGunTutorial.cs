using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityGunTutorial : MonoBehaviour
{
    int currentTutorial;
    int completedTutorials;
    [SerializeField] Text TutorialText;
    GameObject player;

    private void Start() 
    {
        player = Object.FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void Update() 
    {
        switch (currentTutorial)
        {
            case 1:

                if(completedTutorials > currentTutorial)
                {   
                    currentTutorial = 0;
                    break;   // if the player hits the collider early, or late dont show the tutorial.
                } 
                TutorialText.text = "Press E to Pickup Object"; 
                if(Input.GetKeyDown(KeyCode.E))
                {
                    TutorialText.text = "";
                    completedTutorials ++;
                }

            break;

            case 2:

            break;

            case 3:

            break;

            default:

            break;
        }
    }


    public void SetTutorial() 
    {
        currentTutorial ++;
    }
    
}
