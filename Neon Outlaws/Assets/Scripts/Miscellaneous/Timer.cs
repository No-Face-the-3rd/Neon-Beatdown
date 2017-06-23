using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static partial class JMath
{
    private const float defaultLeeway = 0.00001f;
    public static bool withinRange(int value, int min, int max)
    {
        if (value >= min && value <= max)
            return true;
        else
            return false;
    }

    public static bool withinRange(float value, float min, float max)
    {
        if (value >= min && value <= max)
            return true;
        else
            return false;
    }

    public static bool Approximately(float first, float second, float leeway = defaultLeeway)
    {
        if (Mathf.Abs(Math.Abs(first) - Math.Abs(second)) < Math.Abs(leeway))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool Approximately(Color first, Color second, float leeway = defaultLeeway, bool alphaDependant = false)
    {
        if (Approximately(first.r, second.r) &&
            Approximately(first.g, second.g) &&
            Approximately(first.b, second.b))
        {
            if (alphaDependant)
            {
                if (Approximately(first.a, second.a))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}

[System.Serializable]
public class Timer {
    public float duration = 1.0f;
    private float curTime = 0.0f;
    private bool active = false;

    public void startTimer()
    {
        resetTimer();
        setActive(true);
    }

	public void update () {
		if(active)
        {
            curTime += Time.deltaTime;
            if (curTime >= duration)
                toggleActive();
        }
	}

    public bool isPassed()
    {
        return curTime >= duration;
    }

    public float timePassed()
    {
        return curTime < duration ? curTime : duration;
    }

    public float percentPassed()
    {
        if (duration != 0.0f)
            return timePassed() / duration;
        else
            return 0.0f;
    }

    public float timeRemaining()
    {
        return (duration - curTime) > 0.0f ? duration - curTime : 0.0f;
    }

    public float percentRemaining()
    {
        if (duration != 0.0f)
            return timeRemaining() / duration;
        else
            return 0.0f;
    }

    public void resetTimer()
    {
        curTime = 0.0f;
    }

    public bool isActive()
    {
        return active;
    }
    public void toggleActive()
    {
        active = !active;
    }

    public void setActive(bool activity)
    {
        active = activity;
    }

}
