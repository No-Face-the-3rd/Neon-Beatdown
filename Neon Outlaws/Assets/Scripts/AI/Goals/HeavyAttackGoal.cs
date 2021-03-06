﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackGoal : BaseGoal
{
    [SerializeField]
    private int disIn, disWIn, chargeIn, chargeWIn;

    private float timer;
    protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.heavyAttack;
        timer = 0.0f;
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
            //subtract with attack range, fix later
            curDist -= 6;
            curDist  = Mathf.Clamp(curDist, ObjectDB.data.getCurve(disIn).keys[0].time,
                                           ObjectDB.data.getCurve(disIn).keys[ObjectDB.data.getCurve(disIn).keys.Length - 1].time);


            float randomNum = (Random.value) / 2;
            //if im not currently doing a heavy attack, increase timer
            if (chargeCount == 0)
            {
                timer += Time.deltaTime;
            }
            else
                timer = 0;

            if (timer > 5.0f) //5 is just the limit of the curve atm
                timer = 5.0f;

            curves[0].value.inputToCurve  = curDist + randomNum;
            curves[0].weight.inputToCurve = curDist + randomNum;

            curves[1].value.inputToCurve  = chargeCount;
            curves[1].weight.inputToCurve = chargeCount;

            curves[2].value.inputToCurve = timer;
            curves[2].weight.inputToCurve = timer;
        }
        //send desire to enemy ai controller which basegoal already accomplishes

    }
}