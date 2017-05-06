using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStates
{
    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToPatrolState();

    void ToChaseState();

    void ToIdleState();
}
