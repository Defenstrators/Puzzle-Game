using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScenes : MonoBehaviour {
    public void Awake() {
    }

    public void LoadGame() {
        GameManager.instance.LoadGame(); // Loads 1st level.
    }
    public void OnTriggerEnter(Collider other) {
        GameManager.instance.LoadNextLevel();
    }

    public void LevelPicker(int toBeLoadedScreen = 2) {
        GameManager.instance.ScenePicker(toBeLoadedScreen);
    }

}
