using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{

    private EnemyAI enemyAI;
    private int nextPoint = 0;

    public PatrolState(EnemyAI enemy)
    {
        enemyAI = enemy;
    }

    public void GoToAlertState()
    {
        enemyAI.PlayAlertSound();
        enemyAI.navMeshAgent.isStopped = true;
        enemyAI.currentState = enemyAI.alertState;
    }

    public void GoToAttackState()
    {
        enemyAI.navMeshAgent.isStopped = true;
        enemyAI.currentState = enemyAI.attackState;
    }

    public void GoToChaseState(){}

    public void GoToPatrolState(){}

    public void Hit()
    {
        GoToAlertState();
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            GoToAlertState();
        }
    }

    public void OnTriggerExit(Collider coll){}

    public void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            GoToAlertState();
        }
    }

    public void UpdateState()
    {
        enemyAI.light.color = Color.green;
        if(enemyAI.navMeshAgent.destination == null) enemyAI.navMeshAgent.destination = enemyAI.routePoints[nextPoint].position;

        if(enemyAI.navMeshAgent.remainingDistance <= enemyAI.navMeshAgent.stoppingDistance)
        {
            nextPoint = (nextPoint + 1) % enemyAI.routePoints.Length;
            enemyAI.navMeshAgent.destination = enemyAI.routePoints[nextPoint].position;
        }
    }

}
