using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyDrone : MonoBehaviour
{
    [Header("Enemy Drone Health and Damage")]
    [SerializeField] private float enemyHealth = 150f;
    [SerializeField] private float presentHealth;
    [SerializeField] public float giveDamage = 3f;
    [SerializeField] public Rigidbody rb;


    [Header("Enemy Drone Thnigs")]
    [SerializeField] public NavMeshAgent enemyAgent;
    [SerializeField] public Transform lookPoint;
    [SerializeField] public Camera shootingRaycastArea;
    [SerializeField] public Transform playerBody;
    [SerializeField] public LayerMask playerLayer;


    [Header("Enemy Drone Gaurding Var")]
    [SerializeField] public Transform[] walkPoints;
    [SerializeField] public float enemySpeed;
    int currentEnemyPosition = 0;
    float walkingPointRadius = 2.0f;


    //[Header("Sounds and UI")]

    [Header("Enemy Drone Shooting Var")]
    [SerializeField] public float timeBtwShoot;
    bool previouslyShoot;


    [Header("Enemy Drone Animation and Spark effect")]
    [SerializeField] public Animator anim;
    [SerializeField] public ParticleSystem muzzleSpark;
    [SerializeField] public ParticleSystem muzzleFlame;
    [SerializeField] public ParticleSystem destroyEffect;


    [Header("Enemy Drone mood situation")]
    [SerializeField] public float visionRadius;
    [SerializeField] public float shootingRadius;
    [SerializeField] public bool playerInVisisonRadius;
    [SerializeField] public bool playerInShootingRadius;
    [SerializeField] public float dis;

    private void Awake()
    {
        presentHealth = enemyHealth;
        playerBody = GameObject.Find("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        rb = GetComponentInChildren<Rigidbody>();
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
        if (playerInVisisonRadius && playerInShootingRadius)
        {
            ShootPlayer();
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
            if (currentEnemyPosition >= walkPoints.Length - 1)
            {
                currentEnemyPosition = 0;
                return;
            }
            currentEnemyPosition++;
        }
    }
    private void PursuePlayer()
    {
        if (enemyAgent.SetDestination(playerBody.position))
        {
            //animations
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", true);
            anim.SetBool("Shoot", false);
            anim.SetBool("Die", false);

            //inc range
            visionRadius = 30f;
            shootingRadius = 16f;
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", false);
            anim.SetBool("Shoot", false);
            anim.SetBool("Die", true);
        }
    }
    private void ShootPlayer()
    {
        enemyAgent.SetDestination(transform.position);
        transform.LookAt(lookPoint);
        if (!previouslyShoot)
        {
            muzzleSpark.Play();
            muzzleFlame.Play();

            RaycastHit hit;
            if (Physics.Raycast(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward, out hit, shootingRadius))
            {
                Debug.Log("Shotting " + hit.transform.name);
                Player playerBody = hit.transform.GetComponent<Player>();
                if (playerBody != null)
                {
                    playerBody.PlayerHitDamage(giveDamage);
                }
                anim.SetBool("Shoot", true);
                anim.SetBool("Walk", false);
                anim.SetBool("AimRun", false);
                anim.SetBool("Die", false);
            }
            previouslyShoot = true;
            Invoke(nameof(ActiveShooting), timeBtwShoot);
        }
    }
    private void ActiveShooting()
    {
        previouslyShoot = false;
    }
    public void EnemyDroneHitDamage(float takeDmage)
    {
        presentHealth -= takeDmage;

        if (presentHealth <= 0)
        {
            anim.SetBool("Shoot", false);
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", false);
            anim.SetBool("Die", true);

            rb.isKinematic = false;
            EnemyDie();
        }
    }
    private void EnemyDie()
    {
        destroyEffect.Play();
        enemyAgent.SetDestination(transform.position);
        enemySpeed = 0f;
        shootingRadius = -0f;
        visionRadius = 0f;
        playerInVisisonRadius = false;
        playerInShootingRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }
}
