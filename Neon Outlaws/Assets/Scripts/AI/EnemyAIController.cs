using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour {
    CombatInputListener cbi;
    InputState inputState;

    public Rigidbody2D rb;
    public float speed = 10;
    public bool turn = true;

    [HideInInspector] public IAIStates     currentState;
    [HideInInspector] public ApproachState approachState;
    [HideInInspector] public RetreatState  retreatState;
    [HideInInspector] public WaitState     waitState;
    [HideInInspector] public GameObject    opponent;
    private Animator  animator;

    private void Awake() {
        cbi           = new CombatInputListener();
        inputState    = new InputState();
        rb            = GetComponent<Rigidbody2D>();
        animator      = GetComponent<Animator>();
        approachState = new ApproachState(this);
        retreatState  = new RetreatState(this);
        waitState     = new WaitState(this);
    }
    
	// Use this for initialization
	void Start () {
        cbi.overrideAI = false;
        currentState = waitState;
        Debug.Log(currentState);
        opponent = GameObject.Find("StickPunchMan");
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        inputState.moveX = speed;
        inputState.moveY = speed;
        


        currentState.UpdateState();
        //Debug.Log(turn);
    }

    public void turnToOpponent() {
        turn = !turn;
        Vector3 scale = transform.localScale;
        scale.x *= 1;
        transform.localScale = scale;
    }
}