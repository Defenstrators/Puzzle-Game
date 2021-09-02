using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using AsyncOperation = UnityEngine.AsyncOperation;

public class GameManager : MonoBehaviour {
    public static GameManager instance; // SingleTon Lazy and Unsafe But Works stably
    public GameObject loadingScreen;
    private int _CurrentScene; // Use to switch from level 1 and on.
    private int sceneRemoval;
    
    private bool delay;
    
    public Animator transitionAnimator;

    private void Awake() {
        instance = this;
        SceneManager.LoadScene((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);     // Loads Title Screen.
    }

    private List<AsyncOperation> _ScenesLoading = new List<AsyncOperation>(); // List Of Scenes Loading And Unloading During Loading Screen.
    public void LoadGame() { // Loads Level One.
        sceneRemoval = _CurrentScene + 1;
        _CurrentScene = 2;
        loadingScreen.SetActive(true);
        StartCoroutine(Loading());
        // _ScenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        //     _ScenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.LEVEL1, LoadSceneMode.Additive));
        //     StartCoroutine(GetSceneLoadProgress());
        

    }
    public IEnumerator GetSceneLoadProgress() {
        for (int i = 0; i < _ScenesLoading.Count; i++) {
            while (!_ScenesLoading[i].isDone) {
                yield return null;
            }
        }
        // Note Can added % of loading on loading screen and a bar (If needed) here.
        
        
    }
    

    public void LoadNextLevel() {      
        sceneRemoval = _CurrentScene;
        _CurrentScene++;
        loadingScreen.gameObject.SetActive(true);
        StartCoroutine(Loading());
        //NOTE Ending Level animations screen can be Input here (loading Scene With animations playing in it. PLayer may have to be Moved for this Interaction) and Loading screen can be removed When animations is used.
        
    }

    private IEnumerator Loading() {
        Debug.Log(sceneRemoval);
        Debug.Log(_CurrentScene);
        transitionAnimator.Play("Close");
        yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorStateInfo(0).length + 1f);
        _ScenesLoading.Add(SceneManager.UnloadSceneAsync((int)sceneRemoval));
        _ScenesLoading.Add(SceneManager.LoadSceneAsync((int)_CurrentScene, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress());
        transitionAnimator.Play("Open");
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
