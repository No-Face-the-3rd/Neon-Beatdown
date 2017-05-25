using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CurveListValues
{
    public int index;
    public float inputToCurve;
}

[System.Serializable]
public struct Curves
{
    public CurveListValues value;
    public CurveListValues weight;
}

public class BaseGoal : MonoBehaviour
{
    public EnemyAIController self;
    //could make list for how many inputs this goal can affect
    public List<Curves> curves;
    public goalValues myValues;
    public InputTarget inputTarget;
    

    protected virtual void Awake()
    {
        self = GetComponent<EnemyAIController>();
    }
	
	void FixedUpdate ()
    {
        evaluateGoal();
        addUpCurves();
        sendDesire();
	}

    public virtual void evaluateGoal() {}

    //havent tested
    public virtual void addUpCurves()
    {
        if(curves.Count > 0)
        {
            for(int i = 0; i < curves.Count; ++i)
            {
                myValues.curveOutput += ObjectDB.data.computeCurve(curves[i].value.index, curves[i].value.inputToCurve);
                myValues.weight += ObjectDB.data.computeCurve(curves[i].weight.index, curves[i].weight.inputToCurve);
            }
        }
    }
    public virtual void sendDesire()
    {
        self.addGoal(myValues);
    }
}
