using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGoal : BaseGoal
{
    //test class for attacking
    public AnimationCurve desireToAttack;
    public AnimationCurve desireToAttackW;

    public AnimationCurve test;
    public AnimationCurve testw;

    protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.lightAttack;
        desireToAttackW.keys[0].time = desireToAttack.keys[0].time;
        desireToAttackW.keys[desireToAttackW.keys.Length - 1].time = desireToAttack[desireToAttack.keys.Length - 1].time;
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
            //Debug.Log("Distance" + curDist);
            float random = Random.value;
            float desire = desireToAttack.Evaluate(curDist + random);
            float desireWeight = desireToAttackW.Evaluate(curDist + random);

            myValues.curveOutput = desire;
            myValues.weight = desireWeight;
        }
        //send desire to enemy ai controller which basegoal already accomplishes
    }
}
