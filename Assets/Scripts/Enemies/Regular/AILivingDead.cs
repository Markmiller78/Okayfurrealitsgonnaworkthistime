using UnityEngine;
using System.Collections;

public class AILivingDead : MonoBehaviour
{
    GameObject player;
    //    Health playerHealth;
    //    Rigidbody2D rb2d;
    CharacterController controller;
    public float attackDamage;
    public float attackRange;
    float attackCooldown;
    bool attacking;
    public float attackCooldownMax;
    public float moveSpeed;
    public float turnSpeed;
    float distanceToPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = player.GetComponent<Health>();
        attackCooldown = attackCooldownMax;
        //rb2d = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (attacking)
        {
            UpdateAttackCooldown();
        }
        if (distanceToPlayer >= (attackRange / 2.0f))
            Move();
        Turn();
        if (distanceToPlayer <= attackRange && !attacking)
        {
            Attack();
            attacking = true;
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
}
