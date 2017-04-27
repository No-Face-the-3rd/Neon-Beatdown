using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundVictoryManager : MonoBehaviour {

    public RoundTimerManager timerMan;
    public HealthbarManager healthMan;

    private bool roundActive;

    public Timer transitionTimer;

	// Use this for initialization
	void Start () {
        timerMan = GetComponent<RoundTimerManager>();
        healthMan = GetComponent<HealthbarManager>();		
	}
	
	// Update is called once per frame
	void Update () {
        if(roundActive)
        {
            
        }
        else
        {
            if(transitionTimer.isPassed())
            {
                startRound();
            }
        }
        transitionTimer.update();
	}

    void startRound()
    {
        roundActive = true;

    }

    void endMatch()
    {

    }
}
