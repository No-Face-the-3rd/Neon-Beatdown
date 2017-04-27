using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState //: IAIStates
{
    //private readonly EnemyAIController self;
    //private InputState inputState;
    //public Vector2   raycastOrigin;
    //private float    raycastDistance = 1.03f;


    //public RetreatState(EnemyAIController ai)
    //{
    //    self = ai;
    //}

    //public void UpdateState()
    //{
    //    Retreat();
    //}

    //public void ToApproachState()
    //{
    //    self.currentState = self.approachState;
    //    Debug.Log("Approach state active");
    //}

    //public void ToRetreatState()
    //{
    //    Debug.Log("Already retreating");
    //}

    //public void ToWaitState()
    //{
    //    self.currentState = self.waitState;
    //    Debug.Log("Wait state active");
    //}

    //// Retreats until it detects the wall behind it
    //private void Retreat()
    //{
    //    NBCharacterController selfController = self.cil.controller;
    //    NBCharacterController opponent = CharacterLocator.locator.getCharacter((selfController.playerNum % 2) + 1);
    //    float retreatRange = 5.0f;
    //    if(Mathf.Abs(selfController.transform.position.x - opponent.transform.position.x) < retreatRange)
    //    {
    //        self.inputState.moveX = -selfController.transform.localScale.x;
    //    }
    //    else
    //        ToApproachState();

    //    /*if (self.opponent.GetComponent<Rigidbody2D>().position.x > self.rb.position.x)
    //    {
    //        hit = Physics2D.Raycast(self.rb.position, Vector2.right, raycastDistance);
    //        if (hit.distance > 3)
    //        {
    //            self.inputState.moveX = -1;
    //        }
    //        else
    //        {
    //            ToApproachState();
    //        }
    //    }
    //    else
    //    {
    //        hit = Physics2D.Raycast(self.rb.position, Vector2.left, raycastDistance);
    //        if (hit.distance > 3)
    //        {
    //            self.inputState.moveX = 1;
    //        }
    //        else
    //        {
    //            ToApproachState();
    //        }
    //    }*/
    //}
}
