using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AttackController : MonoBehaviour
{
    public float damage = 0.0f;
    public int owner;
    public Vector3 origin, hitLoc;
    public Timer duration;
    public float attackLength;

    public bool goesLeft = false;
    protected GameObject targetHit = null;
    
    // Use this for initialization
    void Start()
    {
        duration.init();
    }

    // Update is called once per frame
    void Update()
    {
        duration.update();
        if (duration.isPassed())
            Destroy(gameObject);
    }

    public void setLeft(bool left)
    {
        goesLeft = left;
    }

    public virtual void doEffect()
    {
        if(targetHit.GetComponent<MyCharacterController>().playerNum == 1)
            HealthManager.manager.editP1Health(-damage);
        if (targetHit.GetComponent<MyCharacterController>().playerNum == 2)
            HealthManager.manager.editP2Health(-damage);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        MyCharacterController hit = other.gameObject.GetComponent<MyCharacterController>();
        if (hit != null && hit.playerNum != owner)
        {
            targetHit = other.gameObject;
            doEffect();
            hitLoc = other.gameObject.transform.position;
            hit.setGrounded(false);
            targetHit.transform.position += Vector3.up * 0.05f;
            Destroy(gameObject);
        }
    }

}




