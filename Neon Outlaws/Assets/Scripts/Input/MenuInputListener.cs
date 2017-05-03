using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputNew;

public class MenuInputListener : MonoBehaviour {
    public PlayerInput pInput;
    public int playerNum;

    public bool overrideAI;

    public AxisAction horizNavAction;
    public AxisAction vertNavAction;
    public ButtonAction acceptAction;
    public ButtonAction declineAction;
    public ButtonAction resumeAction;

    public int selectedCharacter = -1;
    public float deadZone = 0.5f;

    public menuInputState curState = new menuInputState();

    private bool handled = false;

	// Use this for initialization
	void Start () {
        pInput = GetComponent<PlayerInput>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (overrideAI && handled)
        {
            setAxis(horizNavAction.control.value, out curState.horizNav);
            setAxis(vertNavAction.control.value, out curState.vertNav);
            setButton(acceptAction.control, out curState.accept);
            setButton(declineAction.control, out curState.decline);
            setButton(resumeAction.control, out curState.resume);
        }
        curState.horizAsButton.fromAxis(curState.horizNav, deadZone);
        curState.vertAsButton.fromAxis(curState.vertNav, deadZone);
        //conditional to take input here
        //take input here

    }



    public void bindInput(int index)
    {
        handled = true;
        PlayerInfo info = DeviceMapper.mapper.players[index];
        pInput.handle = info.handle;
        playerNum = info.playerNum;
        horizNavAction.Bind(pInput.handle);
        vertNavAction.Bind(pInput.handle);
        acceptAction.Bind(pInput.handle);
        declineAction.Bind(pInput.handle);
        resumeAction.Bind(pInput.handle);
    }

    public void setCurState(menuInputState state)
    {
        menuInputState toAssign = new menuInputState(state);
        setAxis(toAssign.horizNav, out curState.horizNav);
        setAxis(toAssign.vertNav, out curState.vertNav);
        setButton(toAssign.accept, out curState.accept);
        setButton(toAssign.decline, out curState.decline);
        setButton(toAssign.resume, out curState.resume);
    }

    public menuInputState getCurState()
    {
        menuInputState ret = new menuInputState(curState);        
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
        pInput.handle.maps[0].active = active;
    }
}
