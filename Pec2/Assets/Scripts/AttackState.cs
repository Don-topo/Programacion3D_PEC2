using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    private EnemyAI enemyAI;
    float timeBetweenShoots = 0f;

    public AttackState(EnemyAI enemy)
    {
        enemyAI = enemy;
    }

    public void GoToAlertState()
    {
        enemyAI.currentState = enemyAI.alertState;
    }

    public void GoToAttackState(){}

    public void GoToChaseState(){}

    public void GoToPatrolState(){}

    public void Hit(){}

    public void OnTriggerEnter(Collider coll){}

    public void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            GoToAlertState();
        }            
    }

    public void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Vector3 direction = coll.transform.position - enemyAI.transform.position;

            enemyAI.transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(direction.x, 0f, direction.z));            

            if (timeBetweenShoots > enemyAI.fireRatio)
            {
                timeBetweenShoots = 0;
                enemyAI.PlayAttackSound();
                // Hit player
                coll.gameObject.GetComponent<PlayerControler>().Hit(enemyAI.attackDamage);
            }
        }        
    }

    public void UpdateState()
    {
        enemyAI.light.color = Color.red;
        timeBetweenShoots += Time.deltaTime;
    }

}
