using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundVictoryManager : MonoBehaviour {

    public RoundTimerManager timerMan;
    public HealthbarManager healthMan;

    public int roundsToWin = 3;

    public Timer transitionTimer;

    private int currentRound = 0;
    public StageInitData stage;

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
        stage = FindObjectOfType<StageInitData>();
        prepareRound();
	}
	
	// Update is called once per frame
	void Update () {


        transitionTimer.update();

        switch(transition)
        {
            case TransitionState.PREROUND:
                if (transitionTimer.isPassed())
                {
                    startRound();
                }
                break;
            case TransitionState.POSTROUND:
                if(transitionTimer.isPassed())
                {
                    prepareRound();
                }
                break;
            case TransitionState.ROUND:



                break;
        }


        doMarkers();
	}


    void spawnCharacters()
    {
        for(int i = 0;i < DeviceMapper.mapper.maxPlayers;i++)
        {
            MenuInputListener mil = PlayerLocator.locator.getMenuListener(i + 1);
            GameObject tmp = Instantiate(ObjectDB.data.getCharacter(
                mil.selectedCharacter),stage.startingPos[i],Quaternion.identity);
                mil.setActive(false);

        }
    }

    void prepareRound()
    {
        transition = TransitionState.PREROUND;
        timerMan.resetRound();
        stage.resetPositions();
    }

    void startRound()
    {
        transition = TransitionState.ROUND;
        timerMan.startRound();
        transitionTimer.resetTimer();
    }

    void endRound()
    {
        transition = TransitionState.POSTROUND;
        timerMan.resetRound();
        transitionTimer.resetTimer();
        transitionTimer.setActive(true);
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
