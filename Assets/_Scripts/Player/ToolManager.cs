using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    GravityGun gravityGun;
    TimeStopperGun timeStopperGun;
    private void Start() 
    {
        gravityGun = GetComponentInChildren<GravityGun>();
        timeStopperGun = GetComponentInChildren<TimeStopperGun>();
        ToolChange(1);
    }

    // 1 is no tools, 2 is gravity gun, 3 is TimeStopperGun, 4 is grapple hook.
    public void ToolChange(int i)
    {
        switch(i)
        {
            case 1:

            gravityGun.toolActive = true;
            timeStopperGun.toolActive = true;

            break;

            case 2:

            gravityGun.toolActive = true;
            timeStopperGun.toolActive = false;

            break;

            case 3:

            gravityGun.toolActive = false;
            timeStopperGun.toolActive = true;

            break;

            case 4:

            break;


        }
    }
}
