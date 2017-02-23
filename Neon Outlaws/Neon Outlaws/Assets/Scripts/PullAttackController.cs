using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PullAttackController : AttackController
{
    public float pullStrength = 0.0f;
    public override void doEffect()
    {
        base.doEffect();
        Rigidbody2D rb = targetHit.GetComponent<Rigidbody2D>();
        float tmp = rb.gravityScale;
        rb.gravityScale = 0.0f;
        rb.AddForce((goesLeft ? Vector2.left : Vector2.right) * pullStrength, ForceMode2D.Impulse);
        rb.gravityScale = tmp;
    }

   // void OnCollisionEnter2D(Collision2D coll)
   // {
       // MyCharacterController hit = coll.gameObject.GetComponent<MyCharacterController>();
       // if (hit != null && hit.playerNum != owner)
           // doEffect();
    //}
}