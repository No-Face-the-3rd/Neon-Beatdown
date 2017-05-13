using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatGoal : BaseGoal
{
    //public AnimationCurve desireToRetreat;

    //public AnimationCurve desireToRetreatW;
    ////LA: light attack
    //public AnimationCurve desireRetreatAfterLA;
    //public AnimationCurve desireRetreatAfterLAW;

    [SerializeField]
    private int disIn, disWIn, AfterLA, AfterLAW;
    protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.horizontal;
        //start and end points on each curve is equal so no bugs
        //desireToRetreatW.keys[0].time = desireToRetreat.keys[0].time;
        //desireToRetreatW.keys[desireToRetreatW.keys.Length - 1].time = desireToRetreat[desireToRetreat.keys.Length - 1].time;
    }

    public override void evaluateGoal()
    {
        // temp awake fix
        if (self.cil != null && self.enemyController != null)
        {

            float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);
            curDist = Mathf.Clamp(curDist, ObjectDB.data.getCurve(disIn).keys[0].time, 
                    ObjectDB.data.getCurve(disIn).keys[ObjectDB.data.getCurve(disIn).keys.Length - 1].time);

            /*//int amountOfPreviousLA = 0;
            //if (self.selfController.stateQueue.Count > 0 && self.selfController.stateQueue.Count > 10)
            //{
            //    for (int i = self.selfController.stateQueue.Count; i > self.selfController.stateQueue.Count - 10; i--)
            //    {
            //        if (self.selfController.stateQueue[i] == CharacterState.LightConsecutiveRecovery
            //        || self.selfController.stateQueue[i] == CharacterState.LightRecovery)
            //        {
            //            Debug.Log("true");
            //            amountOfPreviousLA++;
            //        }
            //    }
            //}
            //int ind = stateQueue.FindIndex(state => state == CharacterState.LightRecovery);
            //if (ind > -1 && ind < maxLightRecoveryTime)
            //{
            //    DoRecoveryRetreatCurve();
            //}*/

            float desireWeight = ObjectDB.data.computeCurve(disWIn,curDist) /*+ desireRetreatAfterLAW.Evaluate(amountOfPreviousLA)*/;
            float desire = ObjectDB.data.computeCurve(disIn,curDist) /*+ desireRetreatAfterLA.Evaluate(amountOfPreviousLA)*/;

            myValues.curveOutput = desire;
            myValues.weight = desireWeight;
        }
    }

}
