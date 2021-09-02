using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsSetter : MonoBehaviour
{

    [SerializeField] int levelID;
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(levelID));
        float volume = PlayerPrefs.GetFloat("audioLevel");
        AudioSource[] sources = Object.FindObjectsOfType<AudioSource>();
        print(volume);
        foreach(AudioSource sause in sources)
        {
            sause.volume = volume;
            print("set volume: " + volume + " for: " + sause.gameObject);
        }
    }

}
