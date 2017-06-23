using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundVictoryManager : MonoBehaviour {

    public RoundTimerManager timerMan;
    public HealthbarManager healthMan;

    public int roundsToWin = 3;

    public Timer transitionTimer;

    public StageInitData stage;

    public Image Winner;
    public Image Round;


    public List<Sprite> winStuff;
    public List<Sprite> roundStuff;

    int roundInd = 0;
    int winInd = 0;
    public bool inDebug = false;

    private enum TransitionState
    {
        NONE,
        PREROUND,
        ROUND,
        POSTROUND
    };

    TransitionState transition = TransitionState.NONE;

    public List<int> numVictories = new List<int>();

	// Use this for initialization
	void Start () {
        timerMan = GetComponent<RoundTimerManager>();
        healthMan = GetComponent<HealthbarManager>();
        for(int i = 0;i < healthMan.healthBars.Count;i++)
        {
            numVictories.Add(0);
        }
        stage = FindObjectOfType<StageInitData>();
	}
	
	// Update is called once per frame
	void Update () {
        timerMan.roundTimer.setActive(!inDebug);

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
                if(timerMan.timeLeft() <= 0.0f)
                {
                    int victorTime = healthMan.hasMostHealth();
                    if(victorTime >= 0)
                    {
                        numVictories[victorTime]++;
                        winInd = victorTime;
                    }
                    endRound();
                }
                int victorHealth = healthMan.someoneDied();
                if(victorHealth >= 0)
                {
                    numVictories[victorHealth]++;
                    winInd = victorHealth;
                    endRound();
                }
                break;
            case TransitionState.NONE:
                initRound();
                break;
        }


        doMarkers();
	}


    void spawnCharacters()
    {
        //for (int i = 0; i < DeviceMapper.mapper.maxPlayers; i++)
        //{
        //    MenuInputListener mil = PlayerLocator.locator.getMenuListener(i + 1);
        //    GameObject tmp = Instantiate(ObjectDB.data.getCharacter(
        //        mil.selectedCharacter), stage.startingPos[i], Quaternion.identity);
        //    mil.setActive(false);
        //    NBCharacterController charCon = tmp.GetComponent<NBCharacterController>();
        //    charCon.playerNum = i + 1;
        //}
    }

    void prepareRound()
    {
        transition = TransitionState.PREROUND;
        Round.gameObject.SetActive(true);
        Winner.gameObject.SetActive(false);
        timerMan.resetRound();
        transitionTimer.resetTimer();
        transitionTimer.setActive(true);
        stage.resetPositions();
        int ind = 0;
        for (int i = 0; i < numVictories.Count; i++)
            ind += numVictories[i];
        roundInd = ind;
        Round.sprite = roundStuff[roundInd];
    }

    void startRound()
    {
        transition = TransitionState.ROUND;
        Round.gameObject.SetActive(false);
        Winner.gameObject.SetActive(false);
        timerMan.startRound();
        transitionTimer.resetTimer();
        for (int i = 1; i <= DeviceMapper.mapper.maxPlayers; i++)
        {
            NBCharacterController chara = CharacterLocator.locator.getCharacter(i);
            if (chara != null)
            {
                chara.reset();
                chara.doInput = true;
            }
        }
    }

    void endRound()
    {
        transition = TransitionState.POSTROUND;
        timerMan.resetRound();
        Winner.sprite = winStuff[winInd];
        Round.gameObject.SetActive(false);
        Winner.gameObject.SetActive(true);
        transitionTimer.resetTimer();
        transitionTimer.setActive(true);
        for (int i = 1; i <= DeviceMapper.mapper.maxPlayers; i++)
        {
            NBCharacterController chara = CharacterLocator.locator.getCharacter(i);
            if (chara != null)
            {
                chara.reset();
                chara.doInput = false;
            }
        }
        int resInd = numVictories.FindIndex(win => (win >= roundsToWin));
        if(resInd >= 0)
        {
            for (int i = 0; i < numVictories.Count; i++)
            {
                numVictories[i] = 0;
            }
        }
    }

    void initRound()
    {
        transition = TransitionState.PREROUND;
        Round.gameObject.SetActive(true);
        Round.sprite = roundStuff[roundInd];
        Winner.gameObject.SetActive(false);
        transitionTimer.startTimer();
        for (int i = 1; i <= DeviceMapper.mapper.maxPlayers; i++)
        {
            NBCharacterController chara = CharacterLocator.locator.getCharacter(i);
            if(chara != null)
            {
                chara.doInput = false;
            }
        }
    }

    void endMatch()
    {

    }

    void doMarkers()
    {
        for (int i = 0; i < numVictories.Count; i++)
        {
            numVictories[i] = Mathf.Clamp(numVictories[i], 0, roundsToWin + 1);
            healthMan.activateMarkers(i + 1, numVictories[i]);
        }
    }
}
