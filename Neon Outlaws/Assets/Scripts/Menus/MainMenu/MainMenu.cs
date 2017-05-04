using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    menuInputState inputState;
    MenuInputListener menuInputListener;

    void FixedUpdate()
    {
        if (menuInputListener != null)
        {
            TakeInput(menuInputListener.getCurState());

            //if (inputState.vertNav > .05f) {

            //}

            //if (inputState.vertNav < .05f) {

            //}
        }
        else
        {
            menuInputListener = PlayerLocator.locator.getMenuListener(1);
        }
    }

    void TakeInput(menuInputState theMenuInputState)
    {
        inputState = theMenuInputState;
    }
}