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

            curves[0].value.inputToCurve = curDist;
            curves[0].weight.inputToCurve = curDist;
        }
        //send desire to Enemy ai controller which basegoal already accomplishes
    }
}
