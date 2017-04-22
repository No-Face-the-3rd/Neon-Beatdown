using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachState : IAIStates {
    private readonly EnemyAIController self;
    private float    raycastDistance = 10.0f;

    public ApproachState(EnemyAIController ai)
    {
        self = ai;
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
        self.currentState = self.retreatState;
        Debug.Log("Retreat state active");
    }

    public void ToWaitState()
    {
        self.currentState = self.waitState;
        Debug.Log("Wait state active");
    }

    private void Approach()
    {
        NBCharacterController selfController = self.cil.controller;
        NBCharacterController opponent = CharacterLocator.locator.getCharacter((selfController.playerNum % 2) + 1);
        float approachRange = 8.0f;
        if(Mathf.Abs(selfController.transform.position.x - opponent.transform.position.x) > approachRange)
        {
            
            self.inputState.moveX = selfController.transform.localScale.x;
        }
        else
            ToRetreatState();

        //RaycastHit2D hit;
        //if (self.opponent.GetComponent<Rigidbody2D>().position.x > self.rb.position.x)
        //{
        //    hit = Physics2D.Raycast(self.rb.position, Vector2.right, raycastDistance);
        //    if(hit.distance > 3)
        //    {
        //        self.inputState.moveX = 1;
        //    }
        //    else
        //    {
        //        //ToRetreatState();
        //    }
        //}
        //else
        //{
        //    hit = Physics2D.Raycast(self.rb.position, Vector2.left, raycastDistance);
        //    if(hit.distance > 3)
        //    {
        //        self.inputState.moveX = -1;
        //    }
        //    else
        //    {
        //        //ToRetreatState();
        //    }
        //}
        //Debug.Log(hit.distance);
    }
}