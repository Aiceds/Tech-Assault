using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public float damage;

    //Patroling
    //public Vector3 walkPoint;
    //bool walkPointSet;
    //public float walkPointRange;

    public Transform[] points;
    private int destPoint = 0;

    private bool goingToB;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public Transform shotPoint;

    //States
    //public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private bool startTimer;
    private float timer;

    private Vector3 playerDirection;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        goingToB = true;
    }

    void Update()
    {
        // Check if player is within sight and attack range
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //Check if the player is in line of sight for attacking
        //playerDirection = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z);
        
        playerDirection = player.transform.position - transform.position;

        RaycastHit hit;

        if (Physics.Raycast(shotPoint.position, playerDirection, out hit))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                playerInAttackRange = true;
            }
            else
            {
                playerInAttackRange = false;
            }
        }

        if (!playerInSightRange && !playerInAttackRange) GoToNextPoint();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.2f && goingToB)
        {
            GoToNextPoint();
        }

        if (startTimer == true)
        {
            timer += Time.deltaTime;
        }
    }

    private void GoToNextPoint()
    {
        //if (!walkPointSet)
        //{
        //    SearchWalkPoint();
        //}

        //if (walkPointSet)
        //{
        //    agent.SetDestination(walkPoint);
        //}

        //Vector3 distanceToWalkPoint = transform.position - walkPoint;

        ////Walkpoint reached
        //if (distanceToWalkPoint.magnitude < 1f)
        //{
        //    walkPointSet = false;
        //}

        //if (!approchingA)
        //{
        //    agent.SetDestination(pointA);
        //}

        //if (approchingA)
        //{
        //    agent.SetDestination(pointB);
        //}

        goingToB = false;

        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        if (!goingToB)
        {
            // Set the agent to go to the currently selected destination.
            agent.destination = points[destPoint].position;

            goingToB = true;
        }
        
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    //private void SearchWalkPoint()
    //{
    //    //Calculate random point in range
    //    //float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    //float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    //walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //    //if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
    //    //{
    //    //    walkPointSet = true;
    //    //}
    //}

    public void FindPlayer()
    {
        playerDirection = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z);
        RaycastHit hit;

        //Raycast detection
        if (Physics.Raycast(shotPoint.position, playerDirection, out hit))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                playerInSightRange = true;
            }
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

        transform.LookAt(player);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        //agent.SetDestination(transform.position);
        
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            startTimer = true;

            // Shoots after timer hits 2 seconds
            if (timer >= 2)
            {
                //Attack code goes here
                Instantiate(projectile, shotPoint.transform.position, shotPoint.transform.rotation);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);

                timer = 0;
                startTimer = false;
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage()
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(transform.parent.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackRange);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
