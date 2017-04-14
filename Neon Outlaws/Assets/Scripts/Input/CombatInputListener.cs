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
        if (DeviceMapper.mapper.players.Count >= playerNum && !handled)
            bindInput(playerNum - 1);
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
            controller.takeInput(curState);
        curState = new InputState();
    }

    public void bindInput(int index)
    {
        handled = true;
        DeviceMapper.PlayerInfo info = DeviceMapper.mapper.players[index];
        pInput.handle = info.handle;
        pInput.handle.maps[0].active = false;
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
        setAxis(state.moveX, out curState.moveX);
        setAxis(state.moveY, out curState.moveY);
        setButton(state.escape, out curState.escape);
        setButton(state.lightAttack, out curState.lightAttack);
        setButton(state.heavyAttack, out curState.heavyAttack);
        setButton(state.abilityOne, out curState.abilityOne);
        setButton(state.abilityTwo, out curState.abilityTwo);
        setButton(state.abilityThree, out curState.abilityThree);
        //setButton(state.ultimateAbility, out curState.ultimateAbility);
        setButton(state.buttonBlock, out curState.buttonBlock);
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
