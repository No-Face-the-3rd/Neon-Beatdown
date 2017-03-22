using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputNew;




public class CombatInputListener : MonoBehaviour {
    public bool overrideAI;


    public PlayerInput pInput;

    public AxisAction moveX;
    public AxisAction moveY;
    public ButtonAction escape;
    public ButtonAction buttonZero;
    public ButtonAction buttonOne;
    public ButtonAction buttonTwo;
    public ButtonAction buttonThree;
    public ButtonAction buttonFour;
    //public ButtonAction buttonFive;


    public InputState curState;

    public List<InputState> inputQueue;
    public int queueSize;

    // Use this for initialization
    void Start() {
        pInput = GetComponent<PlayerInput>();
        moveX.Bind(pInput.handle);
        moveY.Bind(pInput.handle);
        escape.Bind(pInput.handle);
        buttonZero.Bind(pInput.handle);
        buttonOne.Bind(pInput.handle);
        buttonTwo.Bind(pInput.handle);
        buttonThree.Bind(pInput.handle);
        buttonFour.Bind(pInput.handle);
    }

    // Update is called once per frame
    void Update() {

    }


    void FixedUpdate()
    {
        if (overrideAI)
        {
            setAxis(moveX.control.value, out curState.moveX);
            setAxis(moveY.control.value, out curState.moveY);
            setButton(escape.control, out curState.escape);
            setButton(buttonTwo.control, out curState.lightAttack);
            setButton(buttonThree.control, out curState.heavyAttack);
            setButton(buttonZero.control, out curState.abilityOne);
            setButton(buttonOne.control, out curState.abilityTwo);
            setButton(buttonFour.control, out curState.abilityThree);
        }
        inputQueue.Add(curState);
        if (inputQueue.Count > queueSize)
            inputQueue.RemoveAt(0);
        curState = new InputState();
    }

    public void setCurState(InputState state)
    {
        setAxis(state.moveX, out curState.moveX);
        setAxis(state.moveY, out curState.moveY);
        setButton(state.escape, out curState.escape);
        setButton(state.lightAttack, out curState.lightAttack);
        setButton(state.heavyAttack, out curState.heavyAttack);
        setButton(state.abilityOne, out curState.abilityOne);
        setButton(state.abilityTwo, out curState.abilityTwo);
        setButton(state.abilityThree, out curState.abilityThree);
        //setButton(state.ultimateAbility, out curState.ultimateAbility);
    }

    public void setAxis(float value, out float outAxis)
    {
        outAxis = value;
    }

    public void setButton(ButtonInfo value, out ButtonInfo outButton)
    {
        outButton = value;
    }

}
