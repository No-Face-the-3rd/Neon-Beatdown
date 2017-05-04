using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenu : MonoBehaviour {
    menuInputState inputState;

    public MainMenuButtons mainMenuButtons;
    public UnityEngine.EventSystems.EventSystem menuEventSystem;

    public GameObject characterSelectPanel;
    public GameObject stageSelectPanel;
    public GameObject mainMenuPanel;
    
    public Image player1Outline;
    public bool characterSelected;

    //void Start() {
    //    MenuInputListener menuInputListener = PlayerLocator.locator.getMenuListener(1);
    //}

    //void FixedUpdate() {
    //    if (inputState.vertNav > .05f)
    //        MoveUpInMenu();
    //    if (inputState.vertNav < .05f)
    //        MoveDownInMenu();
    //}

    //void MoveUpInMenu() {

    //}

    //void MoveDownInMenu() {
        
    //}

    //void TakeInput(menuInputState theMenuInputState) {
    //    inputState = theMenuInputState;
    //}

    public void LoadMainMenu() {
        characterSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuEventSystem.SetSelectedGameObject(mainMenuButtons.startingMainButton);
    }

    public void LoadStageSelect()
    {
        characterSelectPanel.SetActive(false);
        stageSelectPanel.SetActive(true);
    }
}