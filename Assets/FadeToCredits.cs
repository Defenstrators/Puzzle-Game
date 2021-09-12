using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToCredits : MonoBehaviour
{
   [SerializeField] Animation anim;
   [SerializeField] int LevelID;

   private void OnTriggerEnter(Collider other) 
   {
        if(other.transform.tag == "Player") StartCoroutine("FadeToBlack");
        print("ttriger");
   }

   IEnumerator FadeToBlack()
   {
       anim.Play();
       yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(LevelID);
   }
}
