using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIStates
{
    void UpdateState();
    //void ToAttackState();   // Attack opponent
    void ToApproachState(); // Move towards opponent
    //void ToBlockState();    // Stay in place and block any attacks from opponent
    void ToRetreatState();  // Back away from opponent (like after taking a lot of damage for example)
    void ToWaitState();     // Idle state, stand in place
}
