using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenu : MonoBehaviour {
    public GameObject characterSelectPanel;
    public GameObject stageSelectPanel;
    public GameObject mainMenuPanel;

    public Button selectedImage;
    public Image player1Outline;
    public bool characterSelected;

    public void LoadMainMenu() {
        characterSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void LoadStageSelect()
    {
        characterSelectPanel.SetActive(false);
        stageSelectPanel.SetActive(true);
    }

}