using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AILorneImproved : MonoBehaviour
{

    PlayerEquipment heroEquipment;

    GameObject player;
    PlayerMovement playMove;
    Health playerHealth;
    //    Health playerHealth;
    //    Rigidbody2D rb2d;
    public bool isReinforced = false;
    public GameObject LorneSignature;
    GameObject LornSig;
    Health MyHealth;

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
    Vector3[] WayPoints = new Vector3[5];
    int currentWaypoint;
    float stateTimer;
    float WayPointChangeTimer;
    float fairieSpawnTimer;

    public GameObject BossHealthBar;
    GameObject HealthRemaining;
    GameObject healthB;

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
        WayPoints[3] = new Vector3(16, -16, -1);
        WayPoints[4] = new Vector3(10, -10, -1);
        player = GameObject.FindGameObjectWithTag("Player");
        playMove = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<Health>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        MyHealth = gameObject.GetComponent<Health>();
        //playerHealth = player.GetComponent<Health>();
        attackCooldownMax = 1;
        attackCooldown = attackCooldownMax;
        //rb2d = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController>();
        LornSig = (GameObject)Instantiate(LorneSignature, new Vector3(10, -10, -1), new Quaternion(0, 0, 0, 0));


        healthB = (GameObject)Instantiate(BossHealthBar);
        HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");
       
    }

    void Update()
    {

        if (HealthRemaining != null)
            HealthRemaining.transform.localScale = new Vector3(MyHealth.healthPercent, 1, 1);
        else
            HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");

        if (heroEquipment.paused == false)
        {

            stateTimer -= Time.deltaTime;
            WayPointChangeTimer -= Time.deltaTime;
            fairieSpawnTimer -= Time.deltaTime;

            if (stateTimer < 0)
            {
                if (currentState == 0)
                {
                    FairySpawners = GameObject.FindGameObjectsWithTag("DethSpawn");

                    foreach (GameObject Spawner in FairySpawners)
                    {
                        Spawner.SendMessage("ReverseRotate", SendMessageOptions.DontRequireReceiver);
                    }
                    currentState = 1;
                    currentWaypoint = 4;
                    WayPointChangeTimer = 5;
                    fairieSpawnTimer = 5f;
                    stateTimer = 25;
                }
                else
                {
                    FairySpawners = GameObject.FindGameObjectsWithTag("DethSpawn");

                    foreach (GameObject Spawner in FairySpawners)
                    {
                        Spawner.SendMessage("SpinNormal", SendMessageOptions.DontRequireReceiver);
                    }
                    LornSig.SetActive(false);
                    currentState = 0;
                    fairieSpawnTimer = 5;
                    stateTimer = 20;
                    moveSpeed = 3;
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
                        
                        //Switch Waypoints (Not Spawn Fairies)
                        if(fairieSpawnTimer < 2)
                        {
                            SignatureMove();
                        }
                        if (fairieSpawnTimer < 1)
                        {
                            LornSig.SetActive(true);
                        }

                        if (fairieSpawnTimer < 0)
                        {
                            moveSpeed = 7;
                            SummonFairies();
                            fairieSpawnTimer = 30;   
                        }

                        if (WayPointChangeTimer < 0)
                        {
                            WayPointChangeTimer = 1.8f;
                            currentWaypoint++;
                            if (currentWaypoint > 3)
                                currentWaypoint = 0;
                        }
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
        if (playMove != null && currentState == 1)
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

    void SignatureMove()
    {
        playMove = player.GetComponent<PlayerMovement>();
        if (playMove != null)
        {
            playMove.PullThePlayer(transform.position);
        }
    }
    void DestroyHealthBar()
    {
        Destroy(healthB);
    }
}
