using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatGoal : BaseGoal
{
    public AnimationCurve desireToRetreat;

    public AnimationCurve desireToRetreatW;
	
	protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.horizontal;
        //start and end points on each curve is equal so no bugs
        desireToRetreatW.keys[0].time = desireToRetreat.keys[0].time;
        desireToRetreatW.keys[desireToRetreatW.keys.Length - 1].time = desireToRetreat[desireToRetreat.keys.Length - 1].time;
    }

    public override void evaluateGoal()
    {
        // temp awake fix
        if (self.cil != null && self.enemyController != null)
        {
            float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);
            curDist = Mathf.Clamp(curDist, desireToRetreat.keys[0].time, desireToRetreat.keys[desireToRetreat.keys.Length - 1].time);


            float desireWeight = desireToRetreatW.Evaluate(curDist);
            float desire = desireToRetreat.Evaluate(curDist);

            myValues.curveOutput = desire;
            myValues.weight = desireWeight;
        }
    }

}
