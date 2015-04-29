using UnityEngine;
using System.Collections;

public class AILivingDead : MonoBehaviour
{
    PlayerEquipment heroEquipment;

    GameObject player;
    PlayerMovement playMove;
    Health playerHealth;
    //    Health playerHealth;
    //    Rigidbody2D rb2d;
	public bool isReinforced=false;
    CharacterController controller;
    public float attackDamage;
    public float attackRange;
    float attackCooldown;
    bool attacking;
    public float attackCooldownMax;
    public float moveSpeed;
    public float turnSpeed;
    float distanceToPlayer;
    float snaredSpeed;
    float SnareTimer;
    bool isSnared;

    void Start()
    {
        isSnared = false;
        moveSpeed = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        playMove = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<Health>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        //playerHealth = player.GetComponent<Health>();
        attackCooldownMax = 1;
        attackCooldown = attackCooldownMax;
        attackRange = .8f;
        //rb2d = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (heroEquipment.paused == false)
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (attacking)
            {
                UpdateAttackCooldown();
            }
            if (distanceToPlayer >= attackRange)
                Move();
            Turn();
            if (distanceToPlayer <= attackRange && !attacking)
            {
                Attack();
                attacking = true;
            } 
            if(isSnared)
            {
                SnareTimer -= Time.deltaTime;

                if(SnareTimer < 0)
                {
                    Unsnare();
                    isSnared = false;
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
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        angle -= 90.0f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }

    void Attack()
    {
        playMove.KnockBack(transform.position);
        playerHealth.LoseHealth(7);
    }

    void UpdateAttackCooldown()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0.0f)
        {
            attackCooldown = attackCooldownMax;
            attacking = false;
        }
    }

    void Slow()
    {
        moveSpeed = moveSpeed * 0.5f;
    }

    void Unslow()
    {
        moveSpeed = moveSpeed * 2;
    }

    void Snare()
    {
        isSnared = true;
        SnareTimer = 2;
        snaredSpeed = moveSpeed;
        moveSpeed = 0;
    }
    void Unsnare()
    {
        moveSpeed = snaredSpeed;
    }
	void Reinforce()
	{
		if (!isReinforced) {
			attackDamage *= 1.15f;
			moveSpeed *= 1.15f;
			isReinforced=true;
		}
		
	}
	
	void UnReinforce()
	{
		if (isReinforced) {
			attackDamage /= 1.15f;
			moveSpeed /= 1.15f;
		}
		isReinforced = false;
		
	}

}
