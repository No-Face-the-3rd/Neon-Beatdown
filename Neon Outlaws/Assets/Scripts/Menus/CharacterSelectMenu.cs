using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenu : MonoBehaviour {
    menuInputState inputState;
    
    public GameObject characterSelectPanel;
    public GameObject stageSelectPanel;
    public GameObject mainMenuPanel;

    public Button selectedImage;
    public Image player1Outline;
    public bool characterSelected;

    void Start() {
        MenuInputListener menuInputListener = PlayerLocator.locator.getMenuListener(1);
    }

    void FixedUpdate() {
        if (inputState.vertNav < .05f)
            MoveDownInMenu();
    }

    void MoveDownInMenu() {
        
    }
    void TakeInput(menuInputState theMenuInputState) {
        inputState = theMenuInputState;
    }

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