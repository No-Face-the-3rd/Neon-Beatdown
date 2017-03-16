using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : IAIStates
{
    private readonly EnemyAIController enemy;
    private float    raycastDistance = 0.5f;

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
        enemy.rb.AddForce(Vector2.right * enemy.speed);

        RaycastHit2D hit = Physics2D.Raycast(enemy.rb.position, Vector2.right, raycastDistance);
        if (hit.collider != null)
        {
            
            ToWaitState();
        }
    }

    void Update()
    {
        
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if(other.collider.tag == "Wall")
    //    {
    //        ToWaitState();
    //    }
    //}
}
