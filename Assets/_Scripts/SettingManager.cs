using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{

    [SerializeField] Slider audioSlider;
    [SerializeField] Slider senceSlider;
    [SerializeField] Slider fpsSlider;
    [SerializeField] Slider graphicsSlider;
    [SerializeField] Text audioText;
    [SerializeField] Text senseText;
    [SerializeField] Text fpsText;
    [SerializeField] Text grahpicsText;

    float audioLevel;
    float senselevel;
    float fpsLevel;
    float graphicsLevel;
    int intFPSLevel;


    private void Start() {
     
        
           
           if(!PlayerPrefs.HasKey("audioLevel"))
           {
               print("player does not have setting saved, creating some now");
               PlayerPrefs.SetFloat("audioLevel", 0.5f);
           }
           if(!PlayerPrefs.HasKey("mouseSence"))
           {
               print("player does not have setting saved, creating some now");
               PlayerPrefs.SetFloat("mouseSence", 1f);
           }
            if(!PlayerPrefs.HasKey("FPS"))
            {
                PlayerPrefs.SetInt("FPS", 165);
                
            }
            if(!PlayerPrefs.HasKey("GraphicsTeir"))
            {
                PlayerPrefs.SetInt("GraphicsTeir", 2);
            }
            
            Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicsTeir"));
        InitiliseSliders();
    }
    private void Update() 
    {
        audioLevel = audioSlider.value;
        senselevel = senceSlider.value;
        fpsLevel = fpsSlider.value;
        intFPSLevel = Mathf.FloorToInt(fpsLevel);
        graphicsLevel = graphicsSlider.value;

        audioText.text = audioSlider.value.ToString();
        senseText.text = senceSlider.value.ToString();
        fpsText.text = fpsSlider.value.ToString();
        switch (graphicsLevel)
        {
            case 0:
            grahpicsText.text = "Low";
            PlayerPrefs.SetInt("GraphicsTeir", 0);
            break;
            case 1:
            grahpicsText.text = "Medium";
            PlayerPrefs.SetInt("GraphicsTeir", 1);
            break;
            case 2:
            grahpicsText.text = "High";
            PlayerPrefs.SetInt("GraphicsTeir", 2);
            break;
        }
    }
    public void SaveSettings() 
    { 
        
        PlayerPrefs.SetFloat("audioLevel", audioLevel);
        PlayerPrefs.SetFloat("mouseSence", senselevel);
        PlayerPrefs.SetInt("FPS", intFPSLevel);
        PlayerPrefs.SetFloat("GraphicsTeir", graphicsLevel);
        PlayerPrefs.Save();

        Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
        QualitySettings.SetQualityLevel(Mathf.FloorToInt(graphicsLevel));
    }

    private void InitiliseSliders()
    {
        audioSlider.value = PlayerPrefs.GetFloat("audioLevel");
        senceSlider.value = PlayerPrefs.GetFloat("mouseSence");
        fpsSlider.value = PlayerPrefs.GetInt("FPS");
        graphicsSlider.value = PlayerPrefs.GetFloat("GraphicsTeir");
    }


}
