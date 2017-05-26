using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGoal : BaseGoal
{
    [SerializeField]
    private int disIn, disWIn;
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
            curDist = Mathf.Clamp(curDist, ObjectDB.data.getCurve(disIn).keys[0].time,
                                           ObjectDB.data.getCurve(disIn).keys[ObjectDB.data.getCurve(disIn).keys.Length - 1].time);


            curves[0].value.inputToCurve = curDist;
            curves[0].weight.inputToCurve = curDist;
        }
    }
}
