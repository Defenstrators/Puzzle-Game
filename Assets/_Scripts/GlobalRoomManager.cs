using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRoomManager : MonoBehaviour
{
   [SerializeField] GameObject[] rooms;
   [SerializeField] GameObject[] doors;


   public void puzzleSolver(int room)
   {
       rooms[room + 1].SetActive(true);
   }
   public void closeDoor(int door)
   {
      StartCoroutine("unloadRoom", door);
   }

   IEnumerator unloadRoom(int door)
   {
       doors[door + 1].GetComponent<DoorTriggeredCheck>().DoorControl(-1);
       yield return new WaitForSeconds(1f);
       rooms[door].SetActive(false);
   }


}

