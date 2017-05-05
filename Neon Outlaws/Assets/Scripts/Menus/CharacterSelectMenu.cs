using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenu : MonoBehaviour {
    menuInputState inputState;
    MenuInputListener menuInputListener;

    public MainMenu mainMenuButtons;
    public UnityEngine.EventSystems.EventSystem menuEventSystem;
    public GameObject characterSelectPanel;
    public GameObject stageSelectPanel;
    public GameObject mainMenuPanel;
    public Image player1Outline;
    public Image player2Outline;
    public Image[] characterImages;
    public bool characterSelected;

    //void Start() {
    //    MenuInputListener menuInputListener = PlayerLocator.locator.getMenuListener(1);
    //}

    void FixedUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            //if (inputState.vertNav < .05f)
            //    i += 1;
            //if (inputState.vertNav < .05f)
            //    i -= 1;

            player1Outline.transform.position = characterImages[i].transform.position;
        }        
    }

    void TakeInput(menuInputState theMenuInputState)
    {
        inputState = theMenuInputState;
    }

    public void LoadMainMenu()
    {
        characterSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuEventSystem.SetSelectedGameObject(mainMenuButtons.startingMainButton);
    }

    public void LoadStageSelect(bool isCharacterSelected)
    {
        isCharacterSelected = characterSelected;
        if (characterSelected)
        {
            characterSelectPanel.SetActive(false);
            stageSelectPanel.SetActive(true);
        }
    }
}