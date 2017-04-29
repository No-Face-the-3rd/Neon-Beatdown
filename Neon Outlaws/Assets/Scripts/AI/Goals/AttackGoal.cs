using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGoal : BaseGoal
{
    //test class for attacking
    public AnimationCurve desireToAttack;

    public AnimationCurve desireToAttackW;

    protected override void Awake()
    {
        base.Awake();
        myValues.input = InputTarget.attack;
        desireToAttackW.keys[0].time = desireToAttack.keys[0].time;
        desireToAttackW.keys[desireToAttackW.keys.Length - 1].time = desireToAttack[desireToAttack.keys.Length - 1].time;
    }

    public override void evaluateGoal()
    {
        //temp awake fix
        if (self.cil != null && self.enemyController != null)
        {

        }

    }
}
