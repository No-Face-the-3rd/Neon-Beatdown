using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackGoal : BaseGoal
{
    [SerializeField]
    private int disIn, disWIn;
    protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.lightAttack;
    }

    public override void evaluateGoal()
    {
        //temp awake fix
        if (self.cil != null && self.enemyController != null)
        {
            //get the absolute value of the distance
            float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);

            //subtract with attack range
            curDist -= 5;
            curDist = Mathf.Clamp(curDist, ObjectDB.data.getCurve(disIn).keys[0].time,
                                           ObjectDB.data.getCurve(disIn).keys[ObjectDB.data.getCurve(disIn).keys.Length - 1].time);
            float randomNum = Random.value / 2;

            curves[0].value.inputToCurve  = curDist + randomNum;
            curves[0].weight.inputToCurve = curDist + randomNum;

            //float desire                = ObjectDB.data.computeCurve(disIn, curDist + randomNum);
            //float desireWeight          = ObjectDB.data.computeCurve(disWIn, curDist + randomNum);

            //myValues.curveOutput = desire;
            //myValues.weight = desireWeight;
        }
        //send desire to enemy ai controller which basegoal already accomplishes
    }
}
