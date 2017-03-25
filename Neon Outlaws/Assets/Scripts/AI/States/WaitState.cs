using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : IAIStates {

    private readonly EnemyAIController enemy;
    private InputState inputState;

    public WaitState(EnemyAIController ai) {
        enemy = ai;
    }

    public void UpdateState() {
        Wait();
    }

    public void ToApproachState() {
        enemy.currentState = enemy.approachState;
        Debug.Log("Approach state active");
    }

    public void ToRetreatState() {
        enemy.currentState = enemy.retreatState;
        Debug.Log("Retreat state active");
    }

    public void ToWaitState() {
        Debug.Log("Already waiting");
    }

    private void Wait() {
        //if (enemy.velocityX > 0)
        //{
        //    // if (enemy.flipRight)
        //    enemy.rb.AddForce(Vector2.right * enemy.speed);
        //}
        if (inputState.moveX < 0) {
            inputState.moveX++;
            if (inputState.moveX >= 0)
                inputState.moveX = 0;
        }
        if (inputState.moveX > 0) {
            inputState.moveX--;
            if (inputState.moveX <= 0)
                inputState.moveX = 0;
        }
    }
}
