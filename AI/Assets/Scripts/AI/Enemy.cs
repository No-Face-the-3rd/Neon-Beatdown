using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider cc;



    [HideInInspector] public IEnemyStates currentState;
    [HideInInspector] public PatrolState patrolState;

    [HideInInspector] public UnityEngine.AI.NavMeshAgent nma;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
