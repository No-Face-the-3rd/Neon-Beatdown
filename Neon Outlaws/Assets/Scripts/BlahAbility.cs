using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlahAbility : AttackController {
    public float stunLength = 1.0f;
    public float knockupHeight = 0.0f;




    public override void doEffect()
    {
        base.doEffect();
        targetHit.GetComponent<Rigidbody2D>().AddForce(Vector3.up * knockupHeight,ForceMode2D.Impulse);
    }
}
