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
        bool lightAttack = false;
        //temp awake fix
        if (cil != null && enemyController != null)
        {
                cil.overrideAI = false;
                if (enemyController != null)
                {
                    MenuInputListener mil = PlayerLocator.locator.getMenuListener(enemyController.playerNum);
                    if (mil != null)
                        mil.setActive(false);
                }
            inputState = new InputState();
            //horizontal movement based on my horizontal animation curves
            float horizontalDesire = evaluateHorizontal();
            inputState.moveX = horizontalDesire * selfController.transform.localScale.x;
            //light attack evaluation
            lightAttack = evaluateLightAttack(.0f);
            inputState.lightAttack.setPressState(lightAttack);
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

    //maybe have it return a float for horizontal/jump/attack/block
    float evaluateHorizontal()
    {
        float weights = 0.0f;
        float val = 0.0f;

        List<int> toRemove = new List<int>();
        for(int i = 0; i < values.Count; ++i)
        {
            if(values[i].input == InputTarget.horizontal)
            {

                val += values[i].curveOutput;
                weights += values[i].weight;
                toRemove.Insert(0, i);
            }
        }
        for(int i = 0;i < toRemove.Count;i++)
        {
            values.RemoveAt(toRemove[i]);
        }
        val /= weights;

        return val;
    }

    bool evaluateLightAttack(float threshold)
    {
        float val = 0.0f;
        float weights = 0.0f;

        List<int> toRemove = new List<int>();
        for(int i = 0; i < values.Count; ++i)
        {
            if(values[i].input == InputTarget.attack)
            {
                val += values[i].curveOutput;
                weights += values[i].weight;
                toRemove.Insert(0, i);
            }
        }
        for(int i = 0; i < toRemove.Count; i++)
        {
            values.RemoveAt(toRemove[i]);
        }
        val /= weights;

        if (val > threshold)
            return true;
        else
            return false;
    }

    /*
     plan:
     goal scripts push a desire to this script based on inputs (ie horizontal, jumping, crouch, attacks, block)
     this script will take those goal's desires and divide by their weight
     based on the outcome I do desired input
     send input
     */
}