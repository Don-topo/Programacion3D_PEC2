using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void UpdateState();
    void GoToAlertState();
    void GoToPatrolState();

    void GoToAttackState();

    void GoToChaseState();

    void OnTriggerEnter(Collider coll);

    void OnTriggerStay(Collider coll);

    void OnTriggerExit(Collider coll);

    void Hit();
}
