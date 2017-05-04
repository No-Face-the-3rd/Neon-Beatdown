using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleMenu : MonoBehaviour {
    MenuInputListener menuInputListener;
    menuInputState inputState;

    public GameObject mainMenuPanel;
    public GameObject titleMenuPanel;
    //public GameObject selectedButton;
    public GameObject nextSelectedButton;
    public UnityEngine.EventSystems.EventSystem menuEventSystem;

    void TakeInput(menuInputState theMenuInputState)
    {
        inputState = theMenuInputState;
    }

    void FixedUpdate()
    {
        if (menuInputListener != null)
        {
            TakeInput(menuInputListener.getCurState());
            if (inputState.accept.wasPressed)
                LoadMenuPanel();
        }
        else
        {
            menuInputListener = PlayerLocator.locator.getMenuListener(1);
        }
    }

    public void LoadMenuPanel()
    {
        titleMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuEventSystem.SetSelectedGameObject(nextSelectedButton);
    }
}
