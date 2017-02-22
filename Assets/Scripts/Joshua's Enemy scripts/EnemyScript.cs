using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
    public GameObject playerCharacter;
    public int enemyHealth = 100;
    public int enemyDamage = 10;
    public double attackInterval = 3; //Time between attacks
    public float coneAngle = 120; 
    private double timing;  // Time variable used for attacks
    public float patrolRadius = 10;
    public float detectionDistance = 100;
    public float attackRange = 3;
    public float pathTime = 0;
    enum state {Patrolling, Chasing, Attacking};
    NavMeshAgent agent;

    // Use this for initialization
    void Start() {
        if (playerCharacter == null) {
            playerCharacter = GameObject.FindGameObjectWithTag("Player");
        }
        timing = attackInterval;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Vector3.Dot(transform.forward, playerCharacter.transform.position.normalized) >= Mathf.Cos(coneAngle/2) &&
            Physics.Raycast(transform.position, playerCharacter.transform.position, out hit, detectionDistance) && hit.collider.CompareTag("Player"))
        {
            //Debug.Log("Following Player");
            agent.SetDestination(playerCharacter.transform.position);
            pathTime = 0;
            //Segment for melee attacks, currently bugged.
            /*  RaycastHit hit;
                Vector3 enemyToTarget = (enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position;
             * if (Physics.Raycast(enemy.transform.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player")) {
                timing -= Time.deltaTime;
                if (timing <= 0)
                {
                    timing = attackInterval;
                    killPlayer(enemyDamage);
                }
            }*/
        }
        else if (!agent.hasPath || pathTime >= 10) {
            timing = attackInterval;
            Vector2 randPoint = Random.insideUnitCircle * patrolRadius;
            Vector3 transPoint = new Vector3(randPoint.x + transform.position.x, transform.position.y, randPoint.y + transform.position.z);
            agent.SetDestination(new Vector3(transPoint.x, Terrain.activeTerrain.SampleHeight(transPoint), transPoint.z));
            pathTime = 0;
        }
        else
        {
            pathTime += Time.deltaTime;
        }
	}

    void killPlayer(int damage)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerCharacter.transform.position - transform.position, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                hit.transform.SendMessage("DamagePlayer", 10);
            }
        }
    }

    void Damage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Destroy(this);
        }
    }
}
