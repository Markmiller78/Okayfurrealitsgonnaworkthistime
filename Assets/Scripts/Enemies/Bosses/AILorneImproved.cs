using UnityEngine;
using System.Collections;

public class AILorneImproved : MonoBehaviour {

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



    void Start()
    {
        AttackActive = false;
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
            switch(currentState)
            {
                case 0:
                    {
                        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                        if (AttackActive)
                        {
                            UpdateAttackCooldown();
                        }
                        if (distanceToPlayer >= attackRange)
                            Move();

                        Turn();
                        break;
                    }
                case 1:
                    {



                        if (distanceToPlayer <= attackRange && !AttackActive)
                        {
                            Attack();
                        }
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

}
