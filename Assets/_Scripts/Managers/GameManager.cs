using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

public class GameManager : MonoBehaviour {
    public static GameManager instance; // SingleTon Lazy and Unsafe But Works stably
    public GameObject loadingScreen;
    private int _CurrentScene; // Use to switch from level 1 and on.
    private void Awake() {
        instance = this;
        SceneManager.LoadScene((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);     // Loads Title Screen.
    }

    private List<AsyncOperation> _ScenesLoading = new List<AsyncOperation>(); // List Of Scenes Loading And Unloading During Loading Screen.
    public void LoadGame() { // Loads Level One.
        loadingScreen.SetActive(true);
        _ScenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        //NOTE Ending Level animations screen can be Input here (loading Scene With animation playing it. PLayer may have to be Moved for this Interaction).
        _ScenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.LEVEL1, LoadSceneMode.Additive)); 
        StartCoroutine(GetSceneLoadProgress());
        _CurrentScene = (int)SceneIndexes.LEVEL1;

    }
    public IEnumerator GetSceneLoadProgress() {
        for (int i = 0; i < _ScenesLoading.Count; i++) {
            while (!_ScenesLoading[i].isDone) {
                yield return null;
            }   
        }
        // Note Can added % of loading on loading screen and a bar (If needed) here.
        loadingScreen.SetActive(false);
        
    }
    

    public void LoadNextLevel() {       //TODO Load Into Level 2 on trigger event.
        int sceneRemoval = _CurrentScene;
        _CurrentScene++;
        Debug.Log(sceneRemoval);
        loadingScreen.gameObject.SetActive(true);
        _ScenesLoading.Add(SceneManager.UnloadSceneAsync((int)sceneRemoval));
        //NOTE Ending Level animations screen can be Input here (loading Scene With animations playing in it. PLayer may have to be Moved for this Interaction) and Loading screen can be removed When animations is used.
        _ScenesLoading.Add(SceneManager.LoadSceneAsync((int)_CurrentScene, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress());
    }

}
