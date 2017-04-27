using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimerManager : MonoBehaviour {

    public Timer roundTimer;
    public Text timerText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        roundTimer.update();
        timerText.text = "Time: " + Mathf.Ceil(roundTimer.timeRemaining());
	}

    public void resetRound()
    {
        roundTimer.resetTimer();
    }

    public void startRound()
    {
        roundTimer.startTimer();
    }

    public bool timeEnd()
    {
        return roundTimer.isPassed();
    }

}
