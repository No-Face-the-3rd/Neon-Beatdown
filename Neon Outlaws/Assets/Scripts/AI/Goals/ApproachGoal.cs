﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InputTarget
//{
//    horizontal, jump, attack, block
//}

public class ApproachGoal : BaseGoal
{
    //returns a desire value based on my distance (based on more later)
    public AnimationCurve desireToApproach;
    //returns the weight value of my desire to approach based on distance
    public AnimationCurve desireToApproachW; 

    protected override void Awake()
    {
        base.Awake();
        //setting so my enemyAiController knows this goal is for horizontal movement
        myValues.input = InputTarget.horizontal;
        //start and end points on each curve is equal so no bugs
        desireToApproachW.keys[0].time = desireToApproach.keys[0].time;
        desireToApproachW.keys[desireToApproachW.keys.Length - 1].time = desireToApproach[desireToApproach.keys.Length - 1].time;
    }

    public override void evaluateGoal()
    {
        //temp awake fix
        if (self.cil != null && self.enemyController != null)
        {
            float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);
            curDist = Mathf.Clamp(curDist, desireToApproach.keys[0].time, desireToApproach.keys[desireToApproach.keys.Length - 1].time);


            float desireWeight = desireToApproachW.Evaluate(curDist);
            float desire = desireToApproach.Evaluate(curDist);
            //desire /= desireWeight;

            myValues.curveOutput = desire;
            myValues.weight = desireWeight;
        }
        //send desire to Enemy ai controller which basegoal already accomplishes
    }
}