using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
    [SerializeField] int fps;
    [SerializeField] float averageFPS;
    int passedframes;
    bool invoked;
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
    }

    void printFPS()
    {
        print(averageFPS);
    }


    
}
