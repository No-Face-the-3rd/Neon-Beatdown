using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;
    public GameObject soundMenuPanel;
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
}
