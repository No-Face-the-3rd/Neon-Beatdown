using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InputTarget
//{
//    horizontal, jump, attack, block
//}

public class ApproachGoal : BaseGoal
{
    //returns a desire value based on my distance
    public AnimationCurve desireToApproach;
    //returns the weight value of my desire to approach based on distance
    public AnimationCurve desireToApproachW; 

    void Awake()
    {
        //setting so my enemyAiController knows this goal is for horizontal movement
        myValues.input = InputTarget.horizontal;
    }

    public override void evaluateGoal()
    {
        float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);

        float desireWeight = desireToApproachW.Evaluate(curDist);
        float desire = desireToApproach.Evaluate(curDist);
        desire /= desireWeight;

        myValues.curveOutput = desire;
        myValues.weight = desireWeight;
        //send desire to Enemy ai controller
    }
}
