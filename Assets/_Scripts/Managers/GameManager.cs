using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using AsyncOperation = UnityEngine.AsyncOperation;
//using Scene = UnityEditor.SearchService.Scene;

public class GameManager : MonoBehaviour {
    public static GameManager instance;  // SingleTon Lazy and Unsafe But Works stably
    public GameObject loadingScreen;   // Transition.
    private int _CurrentScene;       // Use to switch from level 1 and on.
    private int sceneRemoval = 1;  // Scene to be removed.

    public Animator transitionAnimator;

    private void Awake() {
        instance = this;
        if(SceneManager.GetActiveScene().buildIndex == 0) SceneManager.LoadScene((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);     // Loads Title Screen.
        
    }

    private List<AsyncOperation> _ScenesLoading = new List<AsyncOperation>(); // List Of Scenes Loading And Unloading During Loading Screen.
    /// <summary>
    /// Play Button Function
    /// </summary>
    public void LoadGame() { // Loads Level One.
        sceneRemoval = 1;
        _CurrentScene = 2;
        loadingScreen.SetActive(true);
        StartCoroutine(Loading(0));
        
        // _ScenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        //     _ScenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.LEVEL1, LoadSceneMode.Additive));
        //     StartCoroutine(GetSceneLoadProgress());
        

    }   
    
    /// <summary>
    /// Gets Current Loading progress
    /// </summary>
    /// <returns> Nothing </returns>
    public IEnumerator GetSceneLoadProgress() {
        for (int i = 0; i < _ScenesLoading.Count; i++) {
            while (!_ScenesLoading[i].isDone) {
                yield return new WaitForEndOfFrame();
            }
               SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_CurrentScene));

        }
        // Note Can added % of loading on loading screen and a bar (If needed) here.
        
        
    }
    
/// <summary>
/// Loads The next Level Vira scene Index.
/// </summary>
    public void LoadNextLevel() {      
        sceneRemoval = _CurrentScene;
        _CurrentScene++;
        loadingScreen.gameObject.SetActive(true);
        StartCoroutine(Loading(0));
        //NOTE Ending Level animations screen can be Input here (loading Scene With animations playing in it. PLayer may have to be Moved for this Interaction) and Loading screen can be removed When animations is used.
    }

/// <summary>
/// Use to Pick a scene.
/// </summary>
/// <param name="i"></param>
    public void ScenePicker(int i) {
        sceneRemoval = SceneManager.GetActiveScene().buildIndex;
        _CurrentScene = i;
        StartCoroutine(Loading(i));
       
        
        
    }

    /// <summary>
    /// Plays transition, Unloads Current, loads next scene, Plays exit transition.
    /// </summary>
    /// <param name="i"></param>
    /// <returns> Wait Time. </returns>
    private IEnumerator Loading(int i) {
        Debug.Log("sceneRemoval : " + sceneRemoval);
        Debug.Log("currentScene: "+ _CurrentScene);
        transitionAnimator.Play("Close");
        yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorStateInfo(0).length + 1f);
        _ScenesLoading.Add(SceneManager.UnloadSceneAsync((int)sceneRemoval));
        if (i > 0.5f) {
            _ScenesLoading.Add(SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive));
        } else _ScenesLoading.Add(SceneManager.LoadSceneAsync((int)_CurrentScene, LoadSceneMode.Additive));
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
