using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class goalValues
{
    public AnimationCurve weight;
    public float value;
    public float xValue;
}
public enum ValueNames
{
    Approach, Retreat
}


public class EnemyAIController : MonoBehaviour
{
    //replace with player locator
    public CombatInputListener cbi;
    public InputState inputState;

    public Rigidbody2D rb;
    public float speed = 10;
    public float jumpSpeed = 10;

    [HideInInspector] public IAIStates     currentState;
    [HideInInspector] public ApproachState approachState;
    [HideInInspector] public RetreatState  retreatState;
    [HideInInspector] public WaitState     waitState;
    [HideInInspector] public GameObject    opponent;
    private Animator  animator;

    public List<goalValues> values = new List<goalValues>();

    private void Awake()
    {
        //cbi           = GetComponent<CombatInputListener>();
        inputState    = new InputState();
        rb            = GetComponent<Rigidbody2D>();
        animator      = GetComponent<Animator>();
        approachState = new ApproachState(this);
        retreatState  = new RetreatState(this);
        waitState     = new WaitState(this);
    }
    
	// Use this for initialization
	void Start ()
    {
        cbi.overrideAI = false;
        currentState = approachState;
        Debug.Log(currentState);
        opponent = GameObject.Find("GameObject");
        rb = GetComponent<Rigidbody2D>();
	}
	
    //Used for consistency along framerates
	void FixedUpdate ()
    {
        inputState = new InputState();
        //inputState.moveX = speed;
        //inputState.moveY = jumpSpeed;

        currentState.UpdateState();
        cbi.setCurState(inputState);
        //Debug.Log(turn);

        float weights = 0.0f;
        float val = 0.0f;
        for(int i = 0;i < 3;i++)
        {
            weights += values[i].weight.Evaluate(values[i].xValue);
            val += values[i].value;
        }
        val /= weights;

        if (val > 0.5f)
        {

        }

    }

    public void setValue(int index, float value)
    {
        if(index >= 0 && index < values.Count)
        {
            values[index].value = value;
        }
    }
}