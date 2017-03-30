﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;
    public GameObject characterSelectPanel;

    // Load the development fight scene
    //public void LoadSceneOnSelect() {
        //SceneManager.LoadScene("Dev-Chris-AI");
    //}

    // Deactivate main panel, activate the CSS panel
    public void LoadCharacterSelectPanel() {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }

    // Deactivate main panel, activate the options panel
    public void LoadOptionsPanel() {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    // If playing through the Unity Editor, exit play mode. Otherwise exit to desktop
    public void QuitOnSelect() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
