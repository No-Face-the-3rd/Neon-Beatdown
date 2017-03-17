using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            if (isDown == false)
            {
                wasPressed = true;
                isDown = true;
            }
            else
            {
                isHeld = true;
            }
        }
        else
        {
            if (isDown == true)
            {
                wasReleased = true;
                isDown = false;
            }
        }
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
    public ButtonInfo ultimateAbility;

    /// <summary>
    /// Clears the data held and resets to a state of no input.
    /// </summary>
    public void Clear()
    {
        moveX = moveY = 0.0f;
        escape = lightAttack = heavyAttack = abilityOne = abilityTwo = abilityThree = ultimateAbility = new ButtonInfo();
    }

}