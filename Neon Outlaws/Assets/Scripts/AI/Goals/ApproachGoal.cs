using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InputTarget
//{
//    horizontal, jump, attack, block
//}

public class ApproachGoal : BaseGoal
{
    //Goal weight
    public float goalWeight;
    //curve indexes 
    [SerializeField]
    private int disIn, disWIn;
    private float prevHealthPer, curHealthPer;
    protected override void Awake()
    {
        base.Awake();
        //setting so my enemyAiController knows this goal is for horizontal movement
        myValues.input = InputTarget.horizontal;
        prevHealthPer = curHealthPer = 1.0f;
    }

    public override void evaluateGoal()
    {
        //temp awake fix
        if (self.cil != null && self.enemyController != null)
        {
            curHealthPer = self.selfController.getCurHealthPercent();
            float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);
            curDist       = Mathf.Clamp(curDist, ObjectDB.data.getCurve(disIn).keys[0].time, 
                                           ObjectDB.data.getCurve(disIn).keys[ObjectDB.data.getCurve(disIn).keys.Length - 1].time);
            //buggy
            //if(curHealthPer < prevHealthPer)
            //{
            //    float diff = Mathf.Abs(prevHealthPer - curHealthPer);
            //    curves[2].value.inputToCurve = diff;
            //    curves[2].weight.inputToCurve = diff;
            //}

            curves[0].value.inputToCurve  = curDist;
            curves[0].weight.inputToCurve = curDist;
            curves[1].value.inputToCurve  = curHealthPer;
            curves[1].weight.inputToCurve = curHealthPer;

            prevHealthPer = curHealthPer;
        }
        //send desire to Enemy ai controller which basegoal already accomplishes
    }
}