using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGoal : BaseGoal
{
    [SerializeField]
    private int disIn, disWIn, startIn, startWIn;
    protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.block;
    }

    public override void evaluateGoal()
    {
        //temp awake fix
        if (self.cil != null && self.enemyController != null)
        {
            float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);
            curDist = Mathf.Clamp(curDist, ObjectDB.data.getCurve(disIn).keys[0].time,
                                           ObjectDB.data.getCurve(disIn).keys[ObjectDB.data.getCurve(disIn).keys.Length - 1].time);
            float randomNum = Random.value;

            int checkAttackThreshold = 3;
            int ind = 0;
            if (self.enemyController.stateQueue.Count > checkAttackThreshold)
            {
                for (int i = self.enemyController.stateQueue.Count - 1;
                        i > (self.enemyController.stateQueue.Count - 1) - checkAttackThreshold; --i)
                {
                    if (self.enemyController.stateQueue[i] == CharacterState.Light
                    || self.enemyController.stateQueue[i] == CharacterState.Heavy
                    || self.enemyController.stateQueue[i] == CharacterState.HeavyCharge)
                    {
                        ind++;
                    }
                }
            }

            curves[0].value.inputToCurve  = curDist + randomNum;
            curves[0].weight.inputToCurve = curDist + randomNum;

            curves[1].value.inputToCurve  = ind;
            curves[1].weight.inputToCurve = ind;
        }
        //send desire to enemy ai controller which basegoal already accomplishes
    }
}
