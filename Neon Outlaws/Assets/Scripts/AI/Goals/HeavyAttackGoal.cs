using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackGoal : BaseGoal
{
    //public AnimationCurve distance;
    //public AnimationCurve distanceW;

    //public AnimationCurve chargeTime;
    //public AnimationCurve chargeTimeW;

    [SerializeField]
    private int disIn, disWIn, chargeIn, chargeWIn;
    protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.heavyAttack;
        //set the charge time curve's x axis to the maximum time it takes to get max damage
        //chargeTime.keys[chargeTime.keys.Length - 1].time = self.selfController.maxHeavyChargeTime;
        //chargeTimeW.keys[chargeTimeW.keys.Length - 1].time = chargeTime.keys[chargeTime.keys.Length - 1].time;

        //distanceW.keys[0].time = distance.keys[0].time;
        //distanceW.keys[distanceW.keys.Length - 1].time = distance[distance.keys.Length - 1].time;

    }

    public override void evaluateGoal()
    {
        //temp awake fix
        if (self.cil != null && self.enemyController != null)
        {
            AnimationCurve charge = ObjectDB.data.getCurve(chargeIn);
            float chargeCount = (float)self.selfController.getHeavyCharge();
            charge.keys[charge.keys.Length - 1].time = (float)self.selfController.maxHeavyChargeTime;
            //get the absolute value of the distance
            float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);
            //subtract with attack range
            curDist -= 6;



            float randomNum = (Random.value) / 2;
            float distanceDesire = ObjectDB.data.computeCurve(disIn, curDist + randomNum);
            float chargeDesire = charge.Evaluate(chargeCount);
            float desire = distanceDesire + chargeDesire; //distance.Evaluate(curDist + randomNum) + chargeTime.Evaluate(chargeCount);
            float desireWeight = ObjectDB.data.computeCurve(disWIn, curDist + randomNum) + ObjectDB.data.computeCurve(chargeWIn, chargeCount);

            myValues.curveOutput = desire;
            myValues.weight = desireWeight;
        }
        //send desire to enemy ai controller which basegoal already accomplishes
    }
}
