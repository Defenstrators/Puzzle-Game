using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{

    [SerializeField] Slider audioSlider;
    [SerializeField] Slider senceSlider;

    float audioLevel;
    float senselevel;

    private void Start() {
     
        
            PlayerPrefs.SetFloat("audioLevel", 0.5f);
     
            PlayerPrefs.SetFloat("mouseSence", 1f);

        InitiliseSliders();
    }
    private void Update() 
    {
        audioLevel = audioSlider.value;
        senselevel = senceSlider.value;

    }
    public void SaveSettings() 
    { 
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("audioLevel", audioLevel);
        PlayerPrefs.SetFloat("mouseSence", senselevel);
    }

    private void InitiliseSliders()
    {
        audioSlider.value = PlayerPrefs.GetFloat("audioLevel");
        senceSlider.value = PlayerPrefs.GetFloat("mouseSence");
    }


}
