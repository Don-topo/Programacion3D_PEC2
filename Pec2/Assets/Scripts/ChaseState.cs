using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private EnemyAI enemyAI;
    private GameObject playerGameObject;

    public ChaseState(EnemyAI enemy, GameObject player)
    {
        enemyAI = enemy;
        playerGameObject = player;
    }

    public void GoToAlertState(){}

    public void GoToAttackState(){}

    public void GoToChaseState(){}

    public void GoToPatrolState(){}

    public void Hit(){}

    public void OnTriggerEnter(Collider coll){}

    public void OnTriggerExit(Collider coll){}

    public void OnTriggerStay(Collider coll){}

    public void UpdateState()
    {
        enemyAI.light.color = Color.red;
        enemyAI.navMeshAgent.destination = playerGameObject.transform.position;
        if (Vector3.Distance(enemyAI.transform.position, playerGameObject.GetComponent<PlayerControler>().transform.position) < 4f && enemyAI.IsAlive())
        {
            // Damage player
            playerGameObject.GetComponent<PlayerControler>().Hit(enemyAI.attackDamage);
            // Instantiate explosion
            enemyAI.Explode();
        }
    }

}
