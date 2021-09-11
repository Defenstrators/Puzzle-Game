using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToStart : MonoBehaviour
{
    private bool eKey;
    private bool m_playerInTriggerCheck;
    private bool returnToStart;
    public GameObject playerStart;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        returnToStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        eKey = Input.GetKeyDown(KeyCode.E);
        if (eKey == true && m_playerInTriggerCheck == true)
        {
            //Debug.Log("Return To Start");
            returnToStart = true;
            
        }

        if (returnToStart == true)
        {
            Player.transform.position = playerStart.transform.position;
            returnToStart = false;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {       // Checks for player.
            m_playerInTriggerCheck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {       // Checks for player.
            m_playerInTriggerCheck = false;
        }
    }
}
