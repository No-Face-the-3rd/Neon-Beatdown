using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGoal : BaseGoal
{
    public AnimationCurve distance, distanceW;


    protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.jump;
    }

    public override void evaluateGoal()
    {
        //temp awake fix
        if (self.cil != null && self.enemyController != null)
        {
            float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);
            curDist = Mathf.Clamp(curDist, distance.keys[0].time, distance.keys[distance.keys.Length - 1].time);
        }
    }
}
