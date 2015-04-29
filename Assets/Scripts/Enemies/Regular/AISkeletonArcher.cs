﻿using UnityEngine;
using System.Collections;

public class AISkeletonArcher : MonoBehaviour
{

    GameObject player;
    PlayerEquipment heroEquipment;

    public float attackCooldownMax;
    float attackCooldown;
    public float attackMaxRange;
    public float attackMinRange;
    public float moveSpeed;
    public float turnSpeed;
    float distanceToPlayer;
	public bool isReinforced = false;
    public GameObject projectile;
    bool hasAttacked = false;
    CharacterController controller;

    float snaredSpeed;
    float SnareTimer;
    bool isSnared;
    void Start()
    {
        isSnared = false;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        player = GameObject.FindGameObjectWithTag("Player");
        attackCooldown = attackCooldownMax;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (heroEquipment.paused == false)
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
            if (isSnared)
            {
                SnareTimer -= Time.deltaTime;

                if (SnareTimer < 0)
                {
                    Unsnare();
                    isSnared = false;
                }
            }
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


	void Decoy()
	{
		player = GameObject.FindGameObjectWithTag ("Decoy");
	}
	
	void UnDecoy()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	void Reinforce()
	{
		if (!isReinforced) 
		{
			attackMaxRange *= 1.5f;
			moveSpeed *= 1.5f;
		}
		
	}

	void UnReinforce()
	{
		attackMaxRange /= 1.5f;
		moveSpeed /= 1.5f;

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
}
