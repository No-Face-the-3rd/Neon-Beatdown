using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundVictoryManager : MonoBehaviour {

    public RoundTimerManager timerMan;
    public HealthbarManager healthMan;

    private bool roundActive;

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
	}


}
