using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
   private void Update() 
   {

       if(Input.GetKeyDown(KeyCode.F10))
       {
           DoorTriggeredCheck[] doors;

          doors = Object.FindObjectsOfType<DoorTriggeredCheck>();

          foreach(DoorTriggeredCheck Doors in doors) //this is verry bad and lazy, but since this is a dev thing. i dont care.
          {
              Doors.DoorControl(100);
          }
       }
       if(Input.GetKeyDown(KeyCode.F11))
       {
           GrabbableObject[] cubes;

           cubes = Object.FindObjectsOfType<GrabbableObject>();

           foreach(GrabbableObject Cubes in cubes)
           {
               Cubes.Respawn();
           }
        
       }

   }
}
