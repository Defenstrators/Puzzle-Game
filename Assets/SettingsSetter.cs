using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSetter : MonoBehaviour
{
    void Start()
    {
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
