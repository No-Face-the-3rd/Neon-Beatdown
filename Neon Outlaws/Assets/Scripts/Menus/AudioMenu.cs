using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour {
    MainMenuButtons mainMenuButtons;
    public UnityEngine.EventSystems.EventSystem menuEventSystem;
    public GameObject soundMenuPanel;
    public GameObject mainMenuPanel;

    // Deactivate audio panel, bring up the main menu panel
    public void LoadMainMenuPanel()
    {
        soundMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuEventSystem.SetSelectedGameObject(mainMenuButtons.startingMainButton);
    }
}