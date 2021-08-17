using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsLimiter : MonoBehaviour
{
    [SerializeField] int fps;
    [SerializeField] float averageFPS;
    [SerializeField] Text text;
    int passedframes;
    bool invoked;

    string Text;
    private void Start() {
        StartCoroutine("UpdateFPS");
    }
    void Update()
    {
        Application.targetFrameRate = fps; // this will be moved to start later, but this is just for chaning it at runtime
        passedframes ++;

        averageFPS = Time.frameCount / Time.time;
       if(!invoked)
       {
           Invoke("printFPS", 5);
           invoked = true;
       } 
       Text = ("FPS: " + Mathf.Round(1f/ Time.deltaTime)); 
      
    }
    
    IEnumerator UpdateFPS()
    {
        while(true)
        {
             text.text = Text;
             yield return new WaitForSeconds(0.1f);
        }
       
    }



    
}
