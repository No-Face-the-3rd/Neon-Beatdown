using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InputTarget
//{
//    horizontal, jump, attack, block
//}

public class ApproachGoal : BaseGoal
{
    //curve indexes 
    [SerializeField]
    private int disIn, disWIn;

    /*
        what curves could i add?
        health, if theyre in endlag/startup, timer since the last amount of damage ive give/taken
    */

    protected override void Awake()
    {
        base.Awake();
        //setting so my enemyAiController knows this goal is for horizontal movement
        myValues.input = InputTarget.horizontal;
    }

    public override void evaluateGoal()
    {
        //temp awake fix
        if (self.cil != null && self.enemyController != null)
        {

            float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);
            curDist = Mathf.Clamp(curDist, ObjectDB.data.getCurve(disIn).keys[0].time, 
                                           ObjectDB.data.getCurve(disIn).keys[ObjectDB.data.getCurve(disIn).keys.Length - 1].time);


            float desireWeight = ObjectDB.data.computeCurve(disWIn, curDist);
            float desire = ObjectDB.data.computeCurve(disIn, curDist);

            myValues.curveOutput = desire;
            myValues.weight = desireWeight;
        }
        //send desire to Enemy ai controller which basegoal already accomplishes
    }
}
