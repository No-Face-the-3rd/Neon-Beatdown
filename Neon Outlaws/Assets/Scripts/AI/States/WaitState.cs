using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : IAIStates {

    private readonly EnemyAIController enemy;
    

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
        if (enemy.inputState.moveX < 0) {
            enemy.inputState.moveX++;
            if (enemy.inputState.moveX >= 0)
                enemy.inputState.moveX = 0;
        }
        if (enemy.inputState.moveX > 0) {
            enemy.inputState.moveX--;
            if (enemy.inputState.moveX <= 0)
                enemy.inputState.moveX = 0;
        }
    }
}
