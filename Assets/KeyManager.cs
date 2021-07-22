using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
     public int keys;
     public void modifyKeys(int keysToAdd)
     {
         keys += keysToAdd;
     }
     
     public int GetKeys()
     {
         return keys;
     }
}
