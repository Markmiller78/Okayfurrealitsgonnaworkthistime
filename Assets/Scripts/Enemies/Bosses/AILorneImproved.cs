using UnityEngine;
using System.Collections;

public class AILorneImproved : MonoBehaviour
{

    PlayerEquipment heroEquipment;

    GameObject player;
    PlayerMovement playMove;
    Health playerHealth;
    //    Health playerHealth;
    //    Rigidbody2D rb2d;
    public bool isReinforced = false;

    CharacterController controller;
    public float attackDamage;
    public float attackRange;
    public bool isInfected = false;
    float attackCooldown;
    int currentState;
    public float attackCooldownMax;
    public float moveSpeed;
    public float turnSpeed;
    float distanceToPlayer;
    bool AttackActive;
    GameObject[] Fairies;
    GameObject[] FairySpawners;
    Vector3[] WayPoints = new Vector3[4];
    int currentWaypoint;
    float stateTimer;
    float WayPointChangeTimer;
    float fairieSpawnTimer;

    void Start()
    {
        currentState = 0;
        currentWaypoint = 1;
        stateTimer = 5;
        WayPointChangeTimer = 5;
        fairieSpawnTimer = 5;
        AttackActive = false;
        WayPoints[0] = new Vector3(4, -16, -1);
        WayPoints[1] = new Vector3(4, -4, -1);
        WayPoints[2] = new Vector3(16, -4, -1);
        WayPoints[2] = new Vector3(16, -16, -1);
        player = GameObject.FindGameObjectWithTag("Player");
        playMove = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<Health>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        //playerHealth = player.GetComponent<Health>();
        attackCooldownMax = 1;
        attackCooldown = attackCooldownMax;
        //rb2d = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (heroEquipment.paused == false)
        {

            stateTimer -= Time.deltaTime;
            WayPointChangeTimer -= Time.deltaTime;
            fairieSpawnTimer -= Time.deltaTime;

            if (stateTimer < 0)
            {
                if (currentState == 0)
                {
                    currentState = 1;
                    fairieSpawnTimer = 1.5f;
                    stateTimer = 20;
                }
                else
                {
                    currentState = 0;
                    fairieSpawnTimer = 5;
                    stateTimer = 20;
                }

            }


            switch (currentState)
            {
                case 0:
                    {
                        moveSpeed = 2;
                        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                        if (AttackActive)
                        {
                            UpdateAttackCooldown();
                        }
                        if (distanceToPlayer >= attackRange)
                            Move();

                        Turn();

                        if (distanceToPlayer <= attackRange && !AttackActive)
                        {
                            Attack();
                        }
                        break;
                    }
                case 1:
                    {
                        moveSpeed = 3;
                        //Switch Waypoints (Not Spawn Fairies)
                        if (fairieSpawnTimer < 0)
                        {
                            SummonFairies();
                            fairieSpawnTimer = 30;
                        }

                        if (WayPointChangeTimer < 0)
                        {
                            WayPointChangeTimer = 4;
                            currentWaypoint++;
                            if (currentWaypoint > 3)
                                currentWaypoint = 0;
                        }

                        if(fairieSpawnTimer < 28)
                        MoveToWayPoint();



                        break;
                    }
            }

        }
    }

    void Move()
    {
        //rb2d.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed));
        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void MoveToWayPoint()
    {
        Vector2 moveTo = (WayPoints[currentWaypoint] - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void Turn()
    {
        //Vector3 vectorToPlayer = player.transform.position - transform.position;
        //float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        //angle -= 90.0f;
        //Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);w
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }

    void Attack()
    {
        if (playMove != null)
        {
            playMove.KnockBack(transform.position);
            player.GetComponent<Health>().LoseHealth(attackDamage);
        }
    }

    void UpdateAttackCooldown()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0.0f)
        {
            attackCooldown = attackCooldownMax;
            AttackActive = false;
        }
    }
    void SummonFairies()
    {
        FairySpawners = GameObject.FindGameObjectsWithTag("DethSpawn");

        foreach (GameObject Spawner in FairySpawners)
        {
            Spawner.SendMessage("SpawnFairy", SendMessageOptions.DontRequireReceiver);
        }
    }
}
