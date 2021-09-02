using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] CameraController cc;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            cc.cutScene = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    public void UnPause()
    {
        Time.timeScale = 1; 
        pauseMenu.SetActive(false);
        cc.cutScene = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
