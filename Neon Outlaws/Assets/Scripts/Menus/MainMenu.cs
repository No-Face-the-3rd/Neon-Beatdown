using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    menuInputState inputState;
    MenuInputListener menuInputListener;

    public GameObject mainMenuPanel;
    public GameObject soundMenuPanel;
    public GameObject characterSelectPanel;

    public UnityEngine.EventSystems.EventSystem menuEventSystem;
    public GameObject startingMainButton;
    public GameObject startingCharacterSelectButton;
    public GameObject startingStageSelectButton;
    public GameObject startingAudioButton;

    void FixedUpdate()
    {
        if (menuInputListener != null)
        {
            TakeInput(menuInputListener.getCurState());
        }
        else
        {
            menuInputListener = PlayerLocator.locator.getMenuListener(1);
        }
    }

    void TakeInput(menuInputState theMenuInputState)
    {
        inputState = theMenuInputState;
    }

    public void LoadCharacterSelectPanel()
    {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
        menuEventSystem.SetSelectedGameObject(startingCharacterSelectButton);
    }

    // Deactivate main panel, activate the options panel
    public void LoadSoundPanel()
    {
        mainMenuPanel.SetActive(false);
        soundMenuPanel.SetActive(true);
        menuEventSystem.SetSelectedGameObject(startingAudioButton);
    }

    // If playing through the Unity Editor, exit play mode. Otherwise exit to desktop
    public void QuitOnSelect()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}