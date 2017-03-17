using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputNew;




public class CombatInputListener : MonoBehaviour {
    public bool overrideAI { get; set; }


    public PlayerInput pInput;

    public InputState curState;

    public List<InputState> inputQueue;
    public int queueSize;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    void FixedUpdate()
    {
        if (!overrideAI)
        {


        }
        inputQueue.Add(curState);
        if (inputQueue.Count > queueSize)
            inputQueue.RemoveAt(0);
        curState.Clear();
    }

    public void setCurState(InputState state)
    {

    }

    public void setMoveX(float value)
    {
        curState.moveX = value;
    }

    public void setMoveY(float value)
    {
        curState.moveY = value;
    }

    public void setEscape(ButtonInfo value)
    {

    }
}
