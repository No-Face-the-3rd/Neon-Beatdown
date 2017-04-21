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
    public ButtonAction abilityOne;
    public ButtonAction abilityTwo;
    public ButtonAction lightAttack;
    public ButtonAction heavyAttack;
    public ButtonAction abilityThree;
    //public ButtonAction ult;
    public ButtonAction buttonBlock;

    public NBCharacterController controller;
    
    public InputState curState = new InputState();

    public int playerNum;


    private bool handled = false;

    // Use this for initialization
    void Start()
    {
        pInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (overrideAI && handled)
        {
            setAxis(moveX.control.value, out curState.moveX);
            setAxis(moveY.control.value, out curState.moveY);
            setButton(escape.control, out curState.escape);
            setButton(lightAttack.control, out curState.lightAttack);
            setButton(heavyAttack.control, out curState.heavyAttack);
            setButton(abilityOne.control, out curState.abilityOne);
            setButton(abilityTwo.control, out curState.abilityTwo);
            setButton(abilityThree.control, out curState.abilityThree);
            //setButton(ult.control, out curState.ultimateAbility);
            setButton(buttonBlock.control, out curState.buttonBlock);
        }
        if (controller != null)
            controller.takeInput(getCurState());
        curState.clearAxes();
    }

    public void bindInput(int index)
    {
        handled = true;
        PlayerInfo info = DeviceMapper.mapper.players[index];
        pInput.handle = info.handle;
        playerNum = info.playerNum;
        moveX.Bind(pInput.handle);
        moveY.Bind(pInput.handle);
        escape.Bind(pInput.handle);
        abilityOne.Bind(pInput.handle);
        abilityTwo.Bind(pInput.handle);
        lightAttack.Bind(pInput.handle);
        heavyAttack.Bind(pInput.handle);
        abilityThree.Bind(pInput.handle);
        //UltBlock.Bind(pInput.handle);
        buttonBlock.Bind(pInput.handle);
    }


    public void setCurState(InputState state)
    {
        InputState toAssign = new InputState(state);
        setAxis(toAssign.moveX, out curState.moveX);
        setAxis(toAssign.moveY, out curState.moveY);
        setButton(toAssign.escape, out curState.escape);
        setButton(toAssign.lightAttack, out curState.lightAttack);
        setButton(toAssign.heavyAttack, out curState.heavyAttack);
        setButton(toAssign.abilityOne, out curState.abilityOne);
        setButton(toAssign.abilityTwo, out curState.abilityTwo);
        setButton(toAssign.abilityThree, out curState.abilityThree);
        //setButton(toAssign.ultimateAbility, out curState.ultimateAbility);
        setButton(toAssign.buttonBlock, out curState.buttonBlock);
    }

    public InputState getCurState()
    {
        InputState ret = new InputState(curState);
        return ret;
    }

    public void setAxis(float value, out float outAxis)
    {
        outAxis = value;
    }

    public void setButton(ButtonInfo value, out ButtonInfo outButton)
    {
        outButton = value;
    }

    public void setActive(bool active)
    {
        pInput.handle.maps[1].active = active;
    }
}
