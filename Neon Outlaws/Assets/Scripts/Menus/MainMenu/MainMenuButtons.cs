using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject soundMenuPanel;
    public GameObject characterSelectPanel;

    public UnityEngine.EventSystems.EventSystem menuEventSystem;
    public GameObject startingMainButton;
    public GameObject startingCharacterSelectButton;
    public GameObject startingAudioButton;
    
    // Deactivate main panel, activate the CSS panel
    public void LoadCharacterSelectPanel() {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
        menuEventSystem.SetSelectedGameObject(startingCharacterSelectButton);
    }

    // Deactivate main panel, activate the options panel
    public void LoadSoundPanel() {
        mainMenuPanel.SetActive(false);
        soundMenuPanel.SetActive(true);
        menuEventSystem.SetSelectedGameObject(startingAudioButton);
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