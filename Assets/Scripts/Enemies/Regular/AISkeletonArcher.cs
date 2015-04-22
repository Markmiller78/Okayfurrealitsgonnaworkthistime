using UnityEngine;
using System.Collections;

public class AISkeletonArcher : MonoBehaviour
{

    GameObject player;

    public float attackCooldownMax;
    float attackCooldown;
    public float attackMaxRange;
    public float attackMinRange;
    public float moveSpeed;
    public float turnSpeed;
    float distanceToPlayer;
    public GameObject projectile;
    bool hasAttacked = false;
    CharacterController controller;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attackCooldown = attackCooldownMax;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (hasAttacked)
        {
            UpdateAttackCooldown();
        }
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > (attackMaxRange + attackMinRange) / 2.0f)
        {
            MoveTowards();
        }
        else if (distanceToPlayer < (attackMaxRange + attackMinRange) / 2.0f)
        {
            MoveAway();
        }
        Turn();
        if (!hasAttacked && distanceToPlayer <= attackMaxRange && distanceToPlayer >= attackMinRange)
        {
            Attack();
            hasAttacked = true;
        }
    }

    void MoveTowards()
    {
        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void MoveAway()
    {
        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * -moveSpeed);
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
        Instantiate(projectile, transform.position, transform.rotation);
    }

    void UpdateAttackCooldown()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0.0f)
        {
            attackCooldown = attackCooldownMax;
            hasAttacked = false;
        }
    }
}
