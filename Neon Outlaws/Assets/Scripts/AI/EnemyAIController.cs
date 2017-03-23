using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour {
    public Rigidbody2D rb;
    public float speed = 10;
    public float velocityX;
    public bool turn = true;

    public bool approach = false;
    public bool retreat  = false;
    public bool wait     = true;

    [HideInInspector] public IAIStates     currentState;
    [HideInInspector] public ApproachState approachState;
    [HideInInspector] public RetreatState  retreatState;
    [HideInInspector] public WaitState     waitState;
    [HideInInspector] public GameObject    opponent;
    private Animator  animator;

    private void Awake() {
        rb            = GetComponent<Rigidbody2D>();
        animator      = GetComponent<Animator>();
        approachState = new ApproachState(this);
        retreatState  = new RetreatState(this);
        waitState     = new WaitState(this);
    }
    
	// Use this for initialization
	void Start () {
        currentState = waitState;
        Debug.Log(currentState);
        opponent = GameObject.Find("StickPunchMan");
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
        velocityX = rb.velocity.x;
        //Debug.Log(turn);

        if (velocityX > 0)
            animator.Play("SP_Walk(Forward)");
        if (velocityX == 0)

        // For testing animations
        if (approach)
        {
            currentState = approachState;
            retreat = false;
            wait    = false;
        }
            
        if (retreat)
        {
            currentState = retreatState;
            approach = false;
            wait     = false;
        }
            
        if (wait)
        {
            currentState = waitState;
            approach = false;
            retreat  = false;
        }
    }

    public void turnToOpponent() {
        turn = !turn;
        Vector3 scale = transform.localScale;
        scale.x *= 1;
        transform.localScale = scale;
    }
}