using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDesireGoal : BaseGoal
{
    [SerializeField]
    private int disAIn, disRIn;

    private float prevHealthPer, curHealthPer;
    protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.horizontal;
        prevHealthPer = curHealthPer = 1.0f;
    }

    public override void evaluateGoal()
    {
        if(self.cil != null && self.enemyController != null)
        {
            curHealthPer = self.selfController.getCurHealthPercent();
            float curDist = Mathf.Abs(self.selfController.transform.position.x - self.enemyController.transform.position.x);
            float appDist = Mathf.Clamp(curDist, ObjectDB.data.getCurve(disAIn).keys[0].time,
                    ObjectDB.data.getCurve(disAIn).keys[ObjectDB.data.getCurve(disAIn).keys.Length - 1].time);
            float reDist = Mathf.Clamp(curDist, ObjectDB.data.getCurve(disRIn).keys[0].time,
                    ObjectDB.data.getCurve(disRIn).keys[ObjectDB.data.getCurve(disRIn).keys.Length - 1].time);

            curves[0].value.inputToCurve = appDist;
            curves[0].weight.inputToCurve = appDist;
            curves[1].value.inputToCurve = curHealthPer;
            curves[1].weight.inputToCurve = curHealthPer;

            //curves[4].value.inputToCurve = timerSinceDamageTaken;
            //curves[]
        }
    }
}
