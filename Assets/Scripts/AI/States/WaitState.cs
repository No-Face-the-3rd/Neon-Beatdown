using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : IAIStates
{

    private readonly EnemyAIController enemy;

    public WaitState(EnemyAIController ai)
    {
        enemy = ai;
    }

    public void UpdateState()
    {
        Wait();
    }

    public void ToApproachState()
    {
        enemy.currentState = enemy.approachState;
    }

    public void ToRetreatState()
    {
        enemy.currentState = enemy.retreatState;
    }

    public void ToWaitState()
    {
        Debug.Log("Already waiting");
    }

    private void Wait()
    {
        enemy.rb.position = enemy.rb.position;
        enemy.speed = 0;
    }
}
