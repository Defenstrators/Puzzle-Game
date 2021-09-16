using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsStopper : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float secconds;
    void Start()
    {
        Invoke("LoadMenu", secconds);
    }

    // Update is called once per frame
    void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }
}
