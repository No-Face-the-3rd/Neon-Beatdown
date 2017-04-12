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
}