using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour {
    public GameObject optionsMenuPanel;
    public GameObject soundMenuPanel;
    // Deactivate audio panel, bring up the options panel
    public void LoadOptionsPanel()
    {
        soundMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }
}