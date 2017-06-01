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

	[HideInInspector] public NBCharacterController selfController;
	[HideInInspector] public NBCharacterController enemyController;
	private Animator  animator;
	public List<goalValues> values = new List<goalValues>();

	private void Awake()
	{
		selfController = GetComponent<NBCharacterController>();
		inputState    = new InputState();
		rb            = GetComponent<Rigidbody2D>();
		animator      = GetComponent<Animator>();
	}
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	//Used for consistency along framerates
	void FixedUpdate()
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
			lightAttack = evaluateAttack(.9f, InputTarget.lightAttack);
			inputState.lightAttack.setPressState(lightAttack);

			//heavy attack evaluation
			heavyAttack = evaluateAttack(.8f, InputTarget.heavyAttack);
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
		Debug.Log(!cil.overrideAI);
		if(cil != null && !cil.overrideAI)
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

		if (value > threshold)
			return true;
		else
			return false;
	}



	/*
	 plan:
	 add more curves for less predictability
	 could make a scriptable object to store my AI types
	 */
}