using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    private EnemyAI enemyAI;
    private float currentRotationTime = 0f;

    public AlertState(EnemyAI enemy)
    {
        enemyAI = enemy;
    }

    public void GoToAlertState(){}

    public void GoToPatrolState()
    {
        enemyAI.navMeshAgent.isStopped = false;
        currentRotationTime = 0f;
        enemyAI.currentState = enemyAI.patrolState;
    }

    public void GoToAttackState()
    {
        enemyAI.PlayAlertSound();
        if (enemyAI.enemyType == EnemyAI.EnemyTypes.FLY)
        {            
            enemyAI.currentState = enemyAI.attackState;
            currentRotationTime = 0f;
        }
        else
        {
            enemyAI.navMeshAgent.isStopped = false;
            enemyAI.currentState = enemyAI.chaseState;
        }
        
    }

    public void Hit(){}

    public void OnTriggerEnter(Collider coll){}

    public void OnTriggerExit(Collider coll){}

    public void OnTriggerStay(Collider coll){}

    public void UpdateState()
    {
        // Sonido
        // Cambio color
        enemyAI.light.color = Color.yellow;

        enemyAI.transform.rotation *= Quaternion.Euler(0f, Time.deltaTime * 360 * 1f / enemyAI.rotationTime, 0f);

        if (currentRotationTime > enemyAI.rotationTime)
        {
            currentRotationTime = 0;
            GoToPatrolState();
        }
        else
        {
            if(Physics.Raycast(new Ray(new Vector3(enemyAI.transform.position.x, 0.5f, enemyAI.transform.position.z), enemyAI.transform.forward * 100f), out RaycastHit hit))
            {
                Debug.DrawRay(new Vector3(enemyAI.transform.position.x, 0.5f, enemyAI.transform.position.z), enemyAI.transform.forward * 100f, Color.green);
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    GoToAttackState();
                }
            }
        }
        currentRotationTime += Time.deltaTime;
    }

    public void GoToChaseState(){}

}
