using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//change goalValue's name later
//used for weighting different goals with curves
[System.Serializable]
public class goalValues
{
    public float weight;
    public float curveOutput;
    public InputTarget input;
}
public enum InputTarget
{
    horizontal, jump, attack, block
}


public class EnemyAIController : MonoBehaviour
{
    //replace with player locator
    public CombatInputListener cil;
    public InputState inputState;

    public Rigidbody2D rb;

    [HideInInspector] public GameObject    opponent;
    [HideInInspector] public NBCharacterController selfController;
    [HideInInspector] public NBCharacterController enemyController;
    private Animator  animator;

    public List<goalValues> values = new List<goalValues>();

    private void Awake()
    {
        //cbi           = GetComponent<CombatInputListener>();
        selfController = GetComponent<NBCharacterController>();
        enemyController = CharacterLocator.locator.getCharacter((selfController.playerNum % 2) + 1);
        cil = PlayerLocator.locator.getCombatListener(selfController.playerNum);
        inputState    = new InputState();
        rb            = GetComponent<Rigidbody2D>();
        animator      = GetComponent<Animator>();
    }
    
	// Use this for initialization
	void Start ()
    {
        cil.overrideAI = false;
        opponent = GameObject.Find("GameObject");
        rb = GetComponent<Rigidbody2D>();
	}
	
    //Used for consistency along framerates
	void FixedUpdate ()
    {
        inputState = new InputState();
        //inputState.moveX = speed;
        //inputState.moveY = jumpSpeed;

        float weights = 0.0f;
        float val = 0.0f;
        for(int i = 0;i < 3;i++)
        {
           // weights += values[i].weight.Evaluate(values[i].curveInput);
            val += values[i].curveOutput;
        }
        val /= weights;

        if (val > 0.5f)
        {

        }

        cil.setCurState(inputState);
        //Debug.Log(turn);

    }

    //public void setValue(int index, float value)
    //{
    //    if(index >= 0 && index < values.Count)
    //    {
    //        values[index].curveOutput = value;
    //    }
    //}

    public void addGoal(goalValues goalsIn)
    {
        values.Add(goalsIn);
    }

    void evaluateGoals()
    {
        float weights = 0.0f;
        float val = 0.0f;
        
        for(int i = 0; i < values.Count; ++i)
        {
            val += values[i].curveOutput;
            weights += values[i].weight;
        }
    }

    /*
     plan:
     goal scripts push a desire to this script based on inputs (ie horizontal, jumping, crouch, attacks, block)
     this script will take those goal's desires and divide by their weight
     based on the outcome I do desired input
     send input
     */
}