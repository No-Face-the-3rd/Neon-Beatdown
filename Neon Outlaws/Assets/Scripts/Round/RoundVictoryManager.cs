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

    private List<int> numVictories = new List<int>();

	// Use this for initialization
	void Start () {
        timerMan = GetComponent<RoundTimerManager>();
        healthMan = GetComponent<HealthbarManager>();
        for(int i = 0;i < healthMan.healthBars.Count;i++)
        {
            numVictories.Add(0);
        }
        startRound();	
	}
	
	// Update is called once per frame
	void Update () {
        if(roundActive)
        {
            if(timerMan.timeEnd())
            {
                timerMan.pauseTimer(true);
                endRound();
            }
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

        doMarkers();
	}

    void startRound()
    {
        roundActive = true;
        timerMan.resetRound();
        timerMan.startRound();
    }

    void endRound()
    {

    }

    void endMatch()
    {

    }

    void doMarkers()
    {
        for(int i = 0;i < numVictories.Count;i++)
        {
            numVictories[i] = Mathf.Clamp(numVictories[i], 0, roundsToWin + 1);
            healthMan.activateMarkers(i + 1, numVictories[i]);
        }
    }
}
