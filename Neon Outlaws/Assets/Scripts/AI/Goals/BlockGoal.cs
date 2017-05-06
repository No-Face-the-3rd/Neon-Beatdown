using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGoal : BaseGoal
{
    public AnimationCurve distance;
    public AnimationCurve distanceW;
    //from enemy
    public AnimationCurve startupOfAttack;
    public AnimationCurve startupOfAttackW;

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
            curDist = Mathf.Clamp(curDist, distance.keys[0].time, distance.keys[distance.keys.Length - 1].time);
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
            Debug.Log(ind);
            float desire = distance.Evaluate(curDist) + startupOfAttack.Evaluate(ind);
            float desireWeight = distanceW.Evaluate(curDist) + startupOfAttackW.Evaluate(ind);

            myValues.curveOutput = desire;
            myValues.weight = desireWeight;
        }
        //send desire to enemy ai controller which basegoal already accomplishes
    }
}
