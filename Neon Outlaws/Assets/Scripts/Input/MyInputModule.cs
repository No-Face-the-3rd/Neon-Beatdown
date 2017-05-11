using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyInputModule : BaseInputModule
{

    public int playerToListen = 1;

    [SerializeField]
    private float inputActionsPerSecond = 10.0f;
    [SerializeField]
    private menuInputState inputState = new menuInputState();
    private float nextActionTime = 0.0f;

    public override void Process()
    {
        bool sentEvent = false;
        sentEvent = sendAxisEvent();
        if (!sentEvent)
            sendButtonEvent();
    }

    public override void ActivateModule()
    {
        base.ActivateModule();
        BaseEventData bed = GetBaseEventData();
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject, bed);
    }

    public override void DeactivateModule()
    {
        base.DeactivateModule();
        BaseEventData bed = GetBaseEventData();
        eventSystem.SetSelectedGameObject(null, bed);
    }


    void FixedUpdate()
    {
        MenuInputListener mil = PlayerLocator.locator.getMenuListener(playerToListen);
        if(mil != null)
        {
            inputState = mil.getCurState();
        }
    }

    public override bool ShouldActivateModule()
    {
        if (!base.ShouldActivateModule())
            return false;
        bool ret = false;
        ret |= inputState.accept.wasPressed;
        ret |= inputState.decline.wasPressed;
        ret |= !Mathf.Approximately(inputState.horizNav, 0.0f);
        ret |= !Mathf.Approximately(inputState.vertNav, 0.0f);
        return ret;
    }

    bool sendAxisEvent()
    {
        bool ret = false;
        float time = Time.time;
        if (allowMove(time))
            return ret;

        AxisEventData aed = GetAxisEventData(inputState.horizNav,
            inputState.vertNav, 0.5f);
        if(axisDown(aed.moveVector.x) || axisDown(aed.moveVector.y))
        {
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject,
                aed, ExecuteEvents.moveHandler);
            ret = aed.used;
        }
        nextActionTime = time + 1.0f / inputActionsPerSecond;
        return ret;
    }

    bool sendButtonEvent()
    {
        bool ret = false;
        float time = Time.time;
        if (allowButton(time))
            return ret;

        if (eventSystem.currentSelectedGameObject == null)
            return ret;

        BaseEventData bed = GetBaseEventData();

        ExecuteEvents.Execute(eventSystem.currentSelectedGameObject,
            bed, ExecuteEvents.updateSelectedHandler);

        if (inputState.accept.wasReleased)
        {
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject,
                bed, ExecuteEvents.submitHandler);
        }
        if(inputState.decline.wasPressed)
        {
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject,
                bed, ExecuteEvents.cancelHandler);
        }

        ret = bed.used;
        nextActionTime = time + 1.0f / inputActionsPerSecond;
        return ret;
    }

    bool allowMove(float curTime)
    {
        bool ret = inputState.horizAsButton.wasPressed;
        ret |= inputState.vertAsButton.wasPressed;
        ret |= (curTime > nextActionTime);
        return !ret;
    }

    bool allowButton(float curTime)
    {
        bool ret = inputState.accept.wasPressed;
        ret |= inputState.decline.wasPressed;
        ret |= (curTime > nextActionTime);
        return !ret;
    }

    bool axisDown(float axis)
    {
        bool ret = !Mathf.Approximately(axis, 0.0f);
        return ret;
    }

    
}
