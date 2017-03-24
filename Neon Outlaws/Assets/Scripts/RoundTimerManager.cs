using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimerManager : MonoBehaviour {

    public Timer roundTimer;
    public Text timerText;

	// Use this for initialization
	void Start () {
        roundTimer.init();
	}
	
	// Update is called once per frame
	void Update () {
        roundTimer.update();
        timerText.text = "Time: " + Mathf.Ceil(roundTimer.timeRemaining());
	}
}
