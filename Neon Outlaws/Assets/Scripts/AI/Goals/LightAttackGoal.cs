using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackGoal : BaseGoal
{
    //public AnimationCurve desireToAttack;
    //public AnimationCurve desireToAttackW;
    [SerializeField]
    private int disIn, disWIn;
    protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.lightAttack;
        //desireToAttackW.keys[0].time = desireToAttack.keys[0].time;
        //desireToAttackW.keys[desireToAttackW.keys.Length - 1].time = desireToAttack[desireToAttack.keys.Length - 1].time;
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
            Debug.Log(curDist);
            float randomNum = Random.value / 2;
            float desire = ObjectDB.data.computeCurve(disIn, curDist + randomNum);
            float desireWeight = ObjectDB.data.computeCurve(disWIn, curDist + randomNum);

            myValues.curveOutput = desire;
            myValues.weight = desireWeight;
        }
        //send desire to enemy ai controller which basegoal already accomplishes
    }
}
