using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : IAIStates
{
    private readonly EnemyAIController enemy;
    public Vector2   raycastOrigin;
    private float    raycastDistance = 1.03f;

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
        Debug.Log("Approach state active");
    }

    public void ToRetreatState()
    {
        Debug.Log("Already retreating");
    }

    public void ToWaitState()
    {
        enemy.currentState = enemy.waitState;
        Debug.Log("Wait state active");
    }

    // Retreats until it detects the wall behind it
    private void Retreat()
    {
        //raycastOrigin = enemy.rb.position + new Vector2(1.5f, 3);
        enemy.rb.AddForce(Vector2.right * enemy.speed);

        //RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.right, raycastDistance);

        //if (hit.collider != null)
        //{
            //Debug.Log("Collided with " + hit.collider.tag);
            //ToWaitState();
        //}
    }
}
