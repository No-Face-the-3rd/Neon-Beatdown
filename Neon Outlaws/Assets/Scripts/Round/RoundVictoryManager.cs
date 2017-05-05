using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundVictoryManager : MonoBehaviour {

    public RoundTimerManager timerMan;
    public HealthbarManager healthMan;

    public int roundsToWin = 3;

    public Timer transitionTimer;

    private int currentRound = 0;

    private enum TransitionState
    {
        NONE,
        PREROUND,
        ROUND,
        POSTROUND
    };

    TransitionState transition;

    private List<int> numVictories = new List<int>();

	// Use this for initialization
	void Start () {
        timerMan = GetComponent<RoundTimerManager>();
        healthMan = GetComponent<HealthbarManager>();
        for(int i = 0;i < healthMan.healthBars.Count;i++)
        {
            numVictories.Add(0);
        }

	}
	
	// Update is called once per frame
	void Update () {


        transitionTimer.update();

        doMarkers();
	}

    void prepareRound()
    {
        transition = TransitionState.PREROUND;
        timerMan.resetRound();
    }

    void startRound()
    {
        transition = TransitionState.ROUND;
        timerMan.startRound();
    }

    void endRound()
    {
        transition = TransitionState.POSTROUND;
        timerMan.resetRound();
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
