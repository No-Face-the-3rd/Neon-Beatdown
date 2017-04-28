using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject soundMenuPanel;
    public GameObject characterSelectPanel;

    // Deactivate main panel, activate the CSS panel
    public void LoadCharacterSelectPanel() {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
        //titleMenu.menuEventSystem.SetSelectedGameObject(nextSelectedButton);
    }

    // Deactivate main panel, activate the options panel
    public void LoadOptionsPanel() {
        mainMenuPanel.SetActive(false);
        soundMenuPanel.SetActive(true);
        //titleMenu.menuEventSystem.SetSelectedGameObject(nextSelectedButton);
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
