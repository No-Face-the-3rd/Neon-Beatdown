using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10;
    public float velocityX;
    [HideInInspector] public IAIStates     currentState;
    [HideInInspector] public ApproachState approachState;
    [HideInInspector] public RetreatState  retreatState;
    [HideInInspector] public WaitState     waitState;
    [HideInInspector] public GameObject    opponent;

    private void Awake()
    {
        approachState = new ApproachState(this);
        retreatState  = new RetreatState(this);
        waitState     = new WaitState(this);
    }
    
	// Use this for initialization
	void Start ()
    {
        currentState = retreatState;
        Debug.Log(currentState);
        opponent = GameObject.Find("StickPunchMan");
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentState.UpdateState();
        velocityX = rb.velocity.x;
        Debug.DrawRay(rb.position, Vector2.left, Color.red);
        Debug.DrawRay(rb.position, Vector2.right, Color.red);
    }
}