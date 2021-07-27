using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadTrigger : MonoBehaviour
{
    public int levelID;

    private void OnTriggerEnter(Collider other) 
    {
      if(other.tag == "Player")
      {
          SceneManager.LoadScene(levelID);
      }  
    }
}
