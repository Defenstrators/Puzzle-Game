using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject[] menus;
    public void SwitchMenu(int menuID) // 1 is main, 2 is settings, 3 is level select.
    {
        foreach(GameObject menuz in menus)
        {
            menuz.gameObject.SetActive(false);
        }

        menus[menuID].SetActive(true);


    }


        




    }


   





