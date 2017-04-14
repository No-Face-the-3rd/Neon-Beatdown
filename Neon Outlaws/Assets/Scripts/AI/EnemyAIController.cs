using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
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
    }
}