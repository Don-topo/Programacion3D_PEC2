using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Serializable]
    public enum EnemyTypes
    {
        FLY,
        SUICIDE
    }

    public EnemyTypes enemyType;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    public new Light light;
    public float health = 100;
    public float fireRatio = 1.0f;
    public float attackDamage = 10f;
    public float rotationTime = 3f;
    public float precision = 0.5f;
    public Transform[] routePoints;
    public GameObject explotionPrefab;
    public AudioClip explotionSound;
    public AudioClip alertSound;
    public AudioClip attackSound;

    private AudioSource audioSource;
    private Collider[] colliders;
    [SerializeField] private float decreaseScale = 1.5f;
    [SerializeField] private GameObject[] pickUps;    

    // Start is called before the first frame update
    void Start()
    {
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this, FindAnyObjectByType<PlayerControler>().gameObject);
        currentState = patrolState;
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        colliders = GetComponents<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive())
        {
            currentState.UpdateState();
        }
        else
        {
            transform.localScale = new Vector3(
                transform.localScale.x - (Time.deltaTime * decreaseScale), 
                transform.localScale.y - (Time.deltaTime * decreaseScale), 
                transform.localScale.z - (Time.deltaTime * decreaseScale)
            );            
        }        
    }

    public void Hit(float amount)
    {
        health -= amount;        
        currentState.Hit();
        CheckDeath();
    }

    public bool IsAlive() => health > 0f;

    public void CheckDeath()
    {
        if(health <= 0f)
        {
            // Play sound
            audioSource.clip = explotionSound;
            audioSource.Play();
            // Exlosion
            GameObject newPrefab = Instantiate(explotionPrefab, transform);            
            Destroy(newPrefab, 2);
            DisableColliders();
            GeneratePickUp();
            // Destroy
            DestroyEnemy();
        }
    }

    public void Explode()
    {
        health = 0f;
        // Play sound
        audioSource.clip = explotionSound;
        audioSource.Play();
        // Exlosion
        GameObject newPrefab = Instantiate(explotionPrefab, transform);
        Destroy(newPrefab, 2);
        DisableColliders();
        DestroyEnemy();
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject, 1f);
    }

    public void PlayAlertSound()
    {
        audioSource.clip = alertSound;
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }

    private void DisableColliders()
    {
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }

    private void GeneratePickUp()
    {
        Instantiate(pickUps[UnityEngine.Random.Range(0, pickUps.Length - 1)], transform.position + Vector3.up, Quaternion.identity);
    }
    
}
