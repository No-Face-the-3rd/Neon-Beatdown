using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundVictoryManager : MonoBehaviour {

    public RoundTimerManager timerMan;
    public HealthbarManager healthMan;

    private bool roundActive;
    public int roundsToWin = 3;

    public Timer transitionTimer;

    private int currentRound = 0;

	// Use this for initialization
	void Start () {
        timerMan = GetComponent<RoundTimerManager>();
        healthMan = GetComponent<HealthbarManager>();
        startRound();	
	}
	
	// Update is called once per frame
	void Update () {
        if(roundActive)
        {
            
        }
        else
        {
            if (!transitionTimer.isActive())
                transitionTimer.startTimer();
            if(transitionTimer.isPassed())
            {
                startRound();
            }
            else
            {

            }
        }
        transitionTimer.update();
	}

    void startRound()
    {
        roundActive = true;
        timerMan.startRound();
    }

    void endMatch()
    {

    }
}
