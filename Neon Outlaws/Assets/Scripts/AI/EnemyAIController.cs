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
    horizontal, jump, lightAttack, heavyAttack, block
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
       // enemyController = CharacterLocator.locator.getCharacter((selfController.playerNum % 2) + 1);
        //cil = PlayerLocator.locator.getCombatListener(selfController.playerNum);
        inputState    = new InputState();
        rb            = GetComponent<Rigidbody2D>();
        animator      = GetComponent<Animator>();
    }
    
	// Use this for initialization
	void Start ()
    {
        //cil.overrideAI = false;
        opponent = GameObject.Find("GameObject");
        rb = GetComponent<Rigidbody2D>();
	}
	
    //Used for consistency along framerates
	void FixedUpdate ()
    {
        //temp awake fix
        if (cil != null && enemyController != null && cil.overrideAI == false)
        {
            
            //temp menu listener fix
            if (enemyController != null)
            {
                MenuInputListener mil = PlayerLocator.locator.getMenuListener(enemyController.playerNum);
                if (mil != null)
                mil.setActive(false);
            }

            bool lightAttack = false;
            bool heavyAttack = false;
            bool block = false;
            inputState = new InputState();
            //horizontal movement evaluation
            float horizontalDesire = evaluateGoal(InputTarget.horizontal);
            inputState.moveX = horizontalDesire * selfController.transform.localScale.x;

            //light attack evaluation
            lightAttack = evaluateAttack(1.0f,InputTarget.lightAttack);
            inputState.lightAttack.setPressState(lightAttack);

            //heavy attack evaluation
            heavyAttack = evaluateAttack(1.0f, InputTarget.heavyAttack);
            inputState.heavyAttack.setPressState(heavyAttack);

            //block evaluation
            block = evaluateAttack(1.0f, InputTarget.block);
            inputState.buttonBlock.setPressState(block);

            //jump evaluation
            float jump = evaluateGoal(InputTarget.jump);
            inputState.moveY = jump;
            //send inputs
            cil.setCurState(inputState);
        }
        else
        {
            enemyController = CharacterLocator.locator.getCharacter((selfController.playerNum % 2) + 1);
            cil = PlayerLocator.locator.getCombatListener(selfController.playerNum);
        }

        //Debug.Log(turn);

    }

    //maybe have it return a float for horizontal/jump/attack/block
    public float evaluateGoal(InputTarget iTarget)
    {
        float weights = 0.0f;
        float val = 0.0f;

        List<int> toRemove = new List<int>();
        for (int i = 0; i < values.Count; ++i)
        {
            if (values[i].input == iTarget)
            {

                val += values[i].curveOutput;
                weights += values[i].weight;
                toRemove.Insert(0, i);
            }
        }
        for (int i = 0; i < toRemove.Count; i++)
        {
            values.RemoveAt(toRemove[i]);
        }
        val /= weights;

        return val;
    }

    public void addGoal(goalValues goalsIn)
    {
        values.Add(goalsIn);
    }

    //float evaluateHorizontal()
    //{
    //    float weights = 0.0f;
    //    float val = 0.0f;

    //    List<int> toRemove = new List<int>();
    //    for(int i = 0; i < values.Count; ++i)
    //    {
    //        if(values[i].input == InputTarget.horizontal)
    //        {

    //            val += values[i].curveOutput;
    //            weights += values[i].weight;
    //            toRemove.Insert(0, i);
    //        }
    //    }
    //    for(int i = 0;i < toRemove.Count;i++)
    //    {
    //        values.RemoveAt(toRemove[i]);
    //    }
    //    val /= weights;

    //    return val;
    //}

    bool evaluateAttack(float threshold, InputTarget iTarget)
    {
        float value = evaluateGoal(iTarget);

        //Debug.Log(val);
        if (value > threshold)
            return true;
        else
            return false;
    }



    /*
     plan:
     make the system more generic/clean it up
     add more curves for less predictability
     */
}