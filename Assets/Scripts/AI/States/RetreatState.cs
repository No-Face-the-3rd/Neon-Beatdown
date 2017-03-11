using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : IAIStates
{
    private readonly EnemyAIController enemy;

    public RetreatState(EnemyAIController ai)
    {
        enemy = ai;
    }

    public void UpdateState()
    {
        Retreat();
    }

    public void ToApproachState()
    {
        enemy.currentState = enemy.approachState;
    }

    public void ToRetreatState()
    {
        Debug.Log("Already approaching");
    }

    public void ToWaitState()
    {
        enemy.currentState = enemy.waitState;
    }

    private void Retreat()
    {
        
    }
}
