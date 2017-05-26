using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenu : MonoBehaviour {    
    public MainMenu mainMenuButtons;
    public UnityEngine.EventSystems.EventSystem menuEventSystem;
    public GameObject characterSelectPanel;
    public GameObject stageSelectPanel;
    public GameObject mainMenuPanel;
    public Image player1Outline;
    public Image player2Outline;
    public Image[] characterImages;
    public bool [] characterSelected;
    // Limit the amount of inputs in menus
    public float[] nextAction;
    public float actionsPerSec = 10.0f;

    void Awake() {
        characterSelected = new bool[2];
        nextAction = new float[2];
    }

    void FixedUpdate() {

        float curTime = Time.unscaledTime;

        // For the number of players
        for (int i = 0;i < DeviceMapper.mapper.maxPlayers;i++) {
            MenuInputListener menuInputListener = PlayerLocator.locator.getMenuListener(i + 1);

            // Set menu input listener if null
            if (menuInputListener != null) {
                menuInputState inputState = TakeInput(menuInputListener.getCurState());
                // characterSelected bool for each player equals menuInputListener bool hasSelected
                characterSelected[i] = menuInputListener.hasSelected; 

                if (nextAction[i] < curTime && !menuInputListener.hasSelected) {
                    if (inputState.accept.wasPressed) {
                        // If the random button is selected, select a new, random character
                        if (menuInputListener.selectedCharacter == characterImages.Length - 1) { 
                            menuInputListener.selectedCharacter = Random.Range(0, characterImages.Length - 1);
                        }
                        menuInputListener.hasSelected = true;
                        nextAction[i] = curTime + 1.0f / actionsPerSec;
                    }

                    if (inputState.horizAsButton.wasPressed || inputState.vertAsButton.wasPressed) {
                        nextAction[i] = curTime + 1.0f / actionsPerSec;
                    }

                    // Character button navigation
                    int horizontal = (inputState.horizAsButton.wasPressed ?
                        ((inputState.horizNav > 0.0f) ? 1 :
                        (inputState.horizNav < 0.0f) ? -1 : 0) : 0);
                    int vertical = (inputState.vertAsButton.wasPressed ?
                        ((inputState.vertNav < 0.0f) ? 2 :
                        (inputState.vertNav > 0.0f) ? -2 : 0) : 0);
                    menuInputListener.selectedCharacter += horizontal + vertical;
                    menuInputListener.selectedCharacter = (menuInputListener.selectedCharacter + characterImages.Length) % characterImages.Length;
                }

                else {
                    if (inputState.decline.wasReleased) {
                        menuInputListener.hasSelected = false;
                        nextAction[i] = curTime + 1.0f / actionsPerSec;
                    }

                    // Set player outline positions
                    if (i == 0) {
                        player1Outline.transform.position = characterImages[menuInputListener.selectedCharacter].transform.position;
                    }
                    if (i == 1) {
                        player2Outline.transform.position = characterImages[menuInputListener.selectedCharacter].transform.position;
                    }
                }
            }
        }
    }

    menuInputState TakeInput(menuInputState theMenuInputState) {
        return new menuInputState(theMenuInputState);
    }
    
    // Load the Main menu panel
    public void LoadMainMenu() {
        characterSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuEventSystem.SetSelectedGameObject(mainMenuButtons.startingMainButton);
    }

    // Load the State Select Panel
    public void LoadStageSelect() {
        bool allReady = true;
        for (int i = 0; i < characterSelected.Length; i++) {
            if (!characterSelected[i]) {
                allReady = false;
                break;
            }
        }

        if (allReady) {
            characterSelectPanel.SetActive(false);
            stageSelectPanel.SetActive(true);
            menuEventSystem.SetSelectedGameObject(mainMenuButtons.startingStageSelectButton);
        }
    }
}