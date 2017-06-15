using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGoal : MonoBehaviour
{
    public EnemyAIController self;
    //could make list for how many inputs this goal can affect
    public goalValues myValues;
    public InputTarget inputTarget;

    protected virtual void Awake()
    {
        self = GetComponent<EnemyAIController>();
    }
	
	void FixedUpdate ()
    {
        evaluateGoal();
        sendDesire();
	}

    public virtual void evaluateGoal() {}

    public virtual void sendDesire()
    {
        self.addGoal(myValues);
    }
}
