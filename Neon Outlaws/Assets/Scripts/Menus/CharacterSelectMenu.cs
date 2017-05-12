using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenu : MonoBehaviour {
    MainMenu mainMenu;
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
    public bool [] characterSelected;
    public float[] nextAction;
    public float actionsPerSec = 10.0f;

    void Awake() {
        characterSelected = new bool[2];
        nextAction = new float[2];
    }
    //void Start()
    //{
    //    MenuInputListener menuInputListener = PlayerLocator.locator.getMenuListener(1);
    //}

    void FixedUpdate() {
        if (menuInputListener != null)
        {
            TakeInput(menuInputListener.getCurState());
        }
        else
        {
            menuInputListener = PlayerLocator.locator.getMenuListener(1);
        }

        for (int i = 0; i < characterImages.Length; i++)
        {
            //if (inputState.vertNav < .05f)
            //    i += 1;
            //if (inputState.vertNav < .05f)
            //    i -= 1;            

            player1Outline.transform.position = characterImages[i].transform.position;
        }
        
        for(int i = 1;i <= DeviceMapper.mapper.maxPlayers;i++)
        {
            menuInputListener = PlayerLocator.locator.getMenuListener(i);
            if(menuInputListener != null)
            {
                TakeInput(menuInputListener.getCurState());
                characterSelected[i - 1] = menuInputListener.hasSelected;
                if (nextAction[i - 1] < Time.time)
                {

                    if (!menuInputListener.hasSelected)
                    {
                        if (inputState.accept.wasPressed)
                        {
                            if (menuInputListener.selectedCharacter == 4)
                                menuInputListener.selectedCharacter = Random.Range(0, 4);
                            menuInputListener.hasSelected = true;
                            nextAction[i - 1] = Time.time + 1.0f / actionsPerSec;
                        }

                        if(inputState.horizAsButton.wasPressed ||
                            inputState.vertAsButton.wasPressed)
                        {
                            nextAction[i - 1] = Time.time + 1.0f / actionsPerSec;
                        }
                        int horizontal = (inputState.horizAsButton.wasPressed ?
                            (inputState.horizNav > 0.0f ? 1 :
                            (inputState.horizNav < 0.0f ? -1 : 0)) : 0);
                        int vertical = (inputState.vertAsButton.wasPressed ?
                            (inputState.vertNav > 0.0f ? 2 :
                            (inputState.vertNav < 0.0f ? -2 : 0)) : 0);
                        menuInputListener.selectedCharacter += horizontal + vertical;
                        menuInputListener.selectedCharacter = (menuInputListener.selectedCharacter + 5) % 5;
                    }
                    else
                    {
                        if (inputState.decline.wasReleased)
                        {
                            menuInputListener.hasSelected = false;
                            nextAction[i - 1] = Time.time + 1.0f / actionsPerSec;
                        }
                    }
                }

                if (i == 1)
                {
   
                    player1Outline.transform.position = characterImages[menuInputListener.selectedCharacter].transform.position;
                }
                if(i == 2)
                {
                    player2Outline.transform.position = characterImages[menuInputListener.selectedCharacter].transform.position;
                }

                                   
            }
        }       
    }



    void TakeInput(menuInputState theMenuInputState) {
        inputState = new menuInputState(theMenuInputState);
    }

    public void LoadMainMenu() {
        characterSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuEventSystem.SetSelectedGameObject(mainMenuButtons.startingMainButton);
    }

    public void LoadStageSelect() {
        bool allReady = true;
        for (int i = 0; i < characterSelected.Length; i++)
        {
            if (!characterSelected[i])
            {
                allReady = false;
                break;
            }
        }

        if (allReady)
        {
            characterSelectPanel.SetActive(false);
            stageSelectPanel.SetActive(true);
            menuEventSystem.SetSelectedGameObject(mainMenu.startingStageSelectButton);
        }
    }
}