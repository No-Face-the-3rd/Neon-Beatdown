using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
    public float damage = 0.0f;
    public int owner = 0;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        NBCharacterController other = collider.gameObject.GetComponent<NBCharacterController>();
        if(other != null)
        {
            if (other.playerNum != owner)
            {
                Physics2D.IgnoreCollision(collider, gameObject.GetComponent<Collider2D>());
                other.beDamaged(damage);
                Debug.Log(damage);
            }
        }
    }

}
