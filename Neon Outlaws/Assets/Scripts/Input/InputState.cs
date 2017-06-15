using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputNew;


[System.Serializable]
public class ButtonInfo
{
    /// <summary>
    /// Was the button just pressed?
    /// </summary>
    public bool wasPressed = false;
    /// <summary>
    /// Was the button just released?
    /// </summary>
    public bool wasReleased = false;
    /// <summary>
    /// Is the button being held?
    /// </summary>
    public bool isHeld = false;

    private bool isDown = false;

    public ButtonInfo()
    {
        wasPressed = false;
        wasReleased = false;
        isHeld = false;
        isDown = false;
    }

    public ButtonInfo(ButtonInfo button)
    {
        wasPressed = button.wasPressed;
        wasReleased = button.wasReleased;
        isHeld = button.isHeld;
        isDown = button.isDown;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="down"></param>
    public void setPressState(bool down)
    {
        wasPressed = wasReleased = isHeld = false;
        if (down)
        {
            if (!isDown)
            {
                wasPressed = true;
                isDown = true;
            }
            isHeld = true;
            
        }
        else
        {
            if (isDown)
            {
                wasReleased = true;
                isDown = false;
            }
            isHeld = false;
        }
    }

    public static implicit operator ButtonInfo(ButtonInputControl b)
    {
        ButtonInfo ret = new ButtonInfo();
        ret.wasPressed = b.wasJustPressed;
        ret.isHeld = b.isHeld;
        ret.wasReleased = b.wasJustReleased;
        if (ret.isHeld == ret.wasPressed)
            ret.isDown = true;
        return ret;
    }

    public override string ToString()
    {
        string ret = "";
        ret = "Is Held: " + isHeld + "\nWas Just Pressed: " + wasPressed + "\nWas Just Released: " + wasReleased;
        return ret;
    }
    public bool axisDown(float axis, float deadZone)
    {
        bool ret = false;
        if(Mathf.Abs(axis) >= deadZone)
        {
            ret = true;
        }
        return ret;
    }
    public void fromAxis(float axis, float deadZone)
    {
        setPressState(axisDown(axis, deadZone));
    }
}

[System.Serializable]
public class InputState
{
    /// <summary>
    /// Value from -1 to 1. X axis Controls
    /// </summary>
    public float moveX;
    /// <summary>
    /// Value from -1 to 1. Y axis Controls
    /// </summary>
    public float moveY;
    public ButtonInfo xAsButton;
    public ButtonInfo yAsButton;
    /// <summary>
    /// Information on button state: escape
    /// </summary>
    public ButtonInfo escape;
    /// <summary>
    /// Information on button state: light attack
    /// </summary>
    public ButtonInfo lightAttack;
    /// <summary>
    /// Information on button state: heavy attack
    /// </summary>
    public ButtonInfo heavyAttack;
    /// <summary>
    /// Information on button state: ability one
    /// </summary>
    public ButtonInfo abilityOne;
    /// <summary>
    /// Information on button state: ability two
    /// </summary>
    public ButtonInfo abilityTwo;
    /// <summary>
    /// Information on button state: ability three
    /// </summary>
    public ButtonInfo abilityThree;
    /// <summary>
    /// Information on button state: ultimate ability
    /// <para>Not Currently Used</para>
    /// </summary>
    //public ButtonInfo ultimateAbility;
    /// <summary>
    /// Information on button state: block button
    /// </summary>
    public ButtonInfo buttonBlock;

    public InputState()
    {
        moveX = 0.0f;
        moveY = 0.0f;
        escape = new ButtonInfo();
        lightAttack = new ButtonInfo();
        heavyAttack = new ButtonInfo();
        abilityOne = new ButtonInfo();
        abilityTwo = new ButtonInfo();
        abilityThree = new ButtonInfo();
        buttonBlock = new ButtonInfo();
        xAsButton = new ButtonInfo();
        yAsButton = new ButtonInfo();
    }

    public InputState(InputState input)
    {
        moveX = input.moveX;
        moveY = input.moveY;
        escape = new ButtonInfo(input.escape);
        lightAttack = new ButtonInfo(input.lightAttack);
        heavyAttack = new ButtonInfo(input.heavyAttack);
        abilityOne = new ButtonInfo(input.abilityOne);
        abilityTwo = new ButtonInfo(input.abilityTwo);
        abilityThree = new ButtonInfo(input.abilityThree);
        buttonBlock = new ButtonInfo(input.buttonBlock);
        xAsButton = new ButtonInfo(input.xAsButton);
        yAsButton = new ButtonInfo(input.yAsButton);
    }

    public void clearAxes()
    {
        moveX = moveY = 0.0f;
    }
}

[System.Serializable]
public class menuInputState
{
    /// <summary>
    /// Value from -1 to 1. X axis Controls
    /// </summary>
    public float horizNav;
    /// <summary>
    /// Value from -1 to 1. Y axis Controls
    /// </summary>
    public float vertNav;
    public ButtonInfo horizAsButton;
    public ButtonInfo vertAsButton;
    /// <summary>
    /// Information on button state: accept
    /// </summary>
    public ButtonInfo accept;
    /// <summary>
    /// Information on button state: decline
    /// </summary>
    public ButtonInfo decline;
    /// <summary>
    /// Information on button state: resume
    /// </summary>
    public ButtonInfo resume;

    public menuInputState()
    {
        horizNav = 0.0f;
        vertNav = 0.0f;
        accept = new ButtonInfo();
        decline = new ButtonInfo();
        resume = new ButtonInfo();
        horizAsButton = new ButtonInfo();
        vertAsButton = new ButtonInfo();
    }

    public menuInputState(menuInputState input)
    {
        horizNav = input.horizNav;
        vertNav = input.vertNav;
        accept = new ButtonInfo(input.accept);
        decline = new ButtonInfo(input.decline);
        resume = new ButtonInfo(input.resume);
        horizAsButton = new ButtonInfo(input.horizAsButton);
        vertAsButton = new ButtonInfo(input.vertAsButton);
    }

    public void clearAxes()
    {
        horizNav = vertNav = 0.0f;
    }
}