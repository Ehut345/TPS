using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //[Header("Enemy Health and Damage")]

    [Header("Enemy Thnigs")]
    [SerializeField] public NavMeshAgent enemyAgent;
    [SerializeField] public Transform playerBody;
    [SerializeField] public LayerMask playerLayer;


    [Header("Enemy Gaurding Var")]
    [SerializeField] public Transform[] walkPoints;
    [SerializeField] public float enemySpeed;
    int currentEnemyPosition = 0;
    float walkingPointRadius = 2.0f;


    //[Header("Sounds and UI")]

    //[Header("Enemy Shooting Var")]

    //[Header("Enemy Animation and Spark effect")]

    [Header("Enemy mood situation")]
    [SerializeField] public float visionRadius;
    [SerializeField] public float shootingRadius;
    [SerializeField] public bool playerInVisisonRadius;
    [SerializeField] public bool playerInShootingRadius;
    [SerializeField] public float dis;

    private void Awake()
    {
        playerBody = GameObject.Find("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        playerInVisisonRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInShootingRadius = Physics.CheckSphere(transform.position, shootingRadius, playerLayer);
        if (!playerInVisisonRadius && !playerInShootingRadius)
        {
            Gard();
        }
        if (playerInVisisonRadius && !playerInShootingRadius)
        {
            PursuePlayer();
        }
    }
    private void Gard()
    {
        dis = Vector3.Distance(walkPoints[currentEnemyPosition].position, transform.position);
        if (dis > walkingPointRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentEnemyPosition].position, Time.deltaTime * enemySpeed);
            transform.LookAt(walkPoints[currentEnemyPosition].position);
        }
        else
        {
            if (currentEnemyPosition >= walkPoints.Length-1)
            {
                currentEnemyPosition = 0;
                return;
            }
            currentEnemyPosition++;
        }
    }
    private void PursuePlayer()
    {
        if(enemyAgent.SetDestination(playerBody.position))
        {
            //animations

            visionRadius = 80f;
            shootingRadius = 25f;
        }
    }
}
