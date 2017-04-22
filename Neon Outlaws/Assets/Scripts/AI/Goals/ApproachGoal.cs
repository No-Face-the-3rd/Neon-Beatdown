using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachGoal : MonoBehaviour
{
    //returns a desire value based on my distance
    public AnimationCurve desireToApproach;
    public EnemyAIController self;

    void Awake()
    {
        self = GetComponent<EnemyAIController>();
    }

	void FixedUpdate ()
    {
        float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);
        float desire = desireToApproach.Evaluate(curDist);
        //send desire to Enemy ai controller
	}
}
