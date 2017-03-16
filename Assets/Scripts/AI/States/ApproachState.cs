using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachState : IAIStates
{
    private readonly EnemyAIController enemy;
    private float    raycastDistance = 10.0f;
    public ApproachState(EnemyAIController ai)
    {
        enemy = ai;
    }

	public void UpdateState()
    {
        Approach(); // Move towards opponent
    }

    public void ToApproachState()
    {
        Debug.Log("Already approaching");
    }

    public void ToRetreatState()
    {
        enemy.currentState = enemy.retreatState;
        Debug.Log("Retreat state active");
    }

    public void ToWaitState()
    {
        enemy.currentState = enemy.waitState;
        Debug.Log("Wait state active");
    }

    private void Approach()
    {
        enemy.rb.AddForce(Vector2.left * enemy.speed);

        RaycastHit2D hit = Physics2D.Raycast(enemy.rb.position, Vector2.left, raycastDistance);
        
        if (hit.collider != null)
        {
            Debug.Log("Collided with " + hit.collider.tag);
            ToWaitState();
        }
    }
}