using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour {
    public Timer deathTime;


	// Use this for initialization
	void Start () {
        deathTime.startTimer();	
	}
	
	// Update is called once per frame
	void Update () {
        deathTime.update();

        if(deathTime.isPassed())
        {
            Destroy(gameObject);
        }
	}
}
