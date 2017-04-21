using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    menuInputState inputState;
    //TitleMenu titleMenu;
    MenuInputListener menuInputListener = PlayerLocator.locator.getMenuListener(1);

    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;
    public GameObject characterSelectPanel;

    public GameObject[] menuButtons;
    public GameObject selectedButton;

    //public GameObject nextSelectedButton;

    //public Text versusText;
    //public Text trainingText;
    //public Text settingsText;
    //public Text exitText;

    void Start()
    {
        menuInputListener.getCurState();
        TakeInput(menuInputListener.curState);
    }

    void FixedUpdate()
    {

        if (inputState.vertNav > .05f)
            MoveUpInMenu();
        if (inputState.vertNav < .05f)
            MoveDownInMenu();
    }

    void MoveUpInMenu()
    {

    }

    void MoveDownInMenu()
    {

    }

    void TakeInput(menuInputState theMenuInputState)
    {
        inputState = theMenuInputState;
    }

    // Deactivate main panel, activate the CSS panel
    public void LoadCharacterSelectPanel() {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
        //titleMenu.menuEventSystem.SetSelectedGameObject(nextSelectedButton);
    }

    // Deactivate main panel, activate the options panel
    public void LoadOptionsPanel() {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
        //titleMenu.menuEventSystem.SetSelectedGameObject(nextSelectedButton);
    }

    // If playing through the Unity Editor, exit play mode. Otherwise exit to desktop
    public void QuitOnSelect() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}