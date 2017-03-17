using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;
    public GameObject soundMenuPanel;

#region Main Menu
    // Load the development fight scene
    public void LoadSceneOnSelect()
    {
        SceneManager.LoadScene("Dev-Jacob-controls");
    }

    // Deactivate main panel, bring up the options panel
    public void LoadOptionsPanel()
    {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
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
    #endregion

#region Options Menu
    // Deactivate options panel, load sound panel
    public void LoadSoundPanel()
    {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false);
        soundMenuPanel.SetActive(true);
    }

    // Deactivate options menu, activate main panel
    public void LoadMenuPanel()
    {
        optionsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    #endregion

#region AudioMenu
    // Deactivate audio panel, bring up the options panel
    public void LoadOptionsPanelAudio()
    {
        soundMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

#endregion

}