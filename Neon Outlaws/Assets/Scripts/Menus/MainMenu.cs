using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    menuInputState inputState;

    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;
    public GameObject characterSelectPanel;

    public Button versusButton;
    public Button trainingButton;
    public Button settingsButton;
    public Button exitButton;

    void Start() {
        MenuInputListener menuInputListener = PlayerLocator.locator.getMenuListener(1);
        
    }



    void MoveDownInMenu() {

    }

    void TakeInput(menuInputState theMenuInputState) {
        inputState = theMenuInputState;
    }

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
