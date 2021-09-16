using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text Text;

    public void ShowText(string _text, int duration)
    {
        Text.text = _text; 
        Invoke("ClearText", duration);
    }

    void ClearText()
    {
        Text.text = "";
    }
}
