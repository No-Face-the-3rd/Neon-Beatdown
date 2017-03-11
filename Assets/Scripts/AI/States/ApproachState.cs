using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachState : IAIStates
{
    private readonly EnemyAIController enemy;
    
    public ApproachState(EnemyAIController ai)
    {
        enemy = ai;
    }

	public void UpdateState()
    {
        Approach(); // Move towards 
    }

    public void ToApproachState()
    {
        Debug.Log("Already approaching");
    }

    public void ToRetreatState()
    {
        enemy.currentState = enemy.retreatState;
    }

    public void ToWaitState()
    {
        enemy.currentState = enemy.waitState;
    }

    private void Approach()
    {
        //Vector2.MoveTowards(enemy.rb.position, enemy.opponent.GetComponent<Rigidbody2D>().position, 25);
        enemy.rb.AddForce(Vector2.left * enemy.speed);

        RaycastHit2D hit = Physics2D.Raycast(enemy.rb.position, Vector2.left, 7.5f);
        if (hit.collider != null)
        {
            ToWaitState();
        }
    }
}