using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyStates
{
    private readonly Enemy enemy;
    private int nextWaypoint;

    public PatrolState(Enemy anEnemy)
    {
        enemy = anEnemy;
    }

    public void UpdateState()
    {
        Patrol();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
            ToChaseState();
    }

    public void ToPatrolState()
    {
        Debug.Log("Already in patrol state");
    }

    public void ToChaseState()
    {
        //enemy.currentState = enemy.chaseState;
    }

    public void ToIdleState()
    {
        //enemy.currentState = enemy.idleState;
    }

    private void Patrol()
    {

    }
}
