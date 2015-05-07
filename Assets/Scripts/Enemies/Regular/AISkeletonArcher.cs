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
    public float infectRange;
    public float infecttimer;
    float distanceToPlayer;
	public bool isReinforced = false;
    public bool isInfected = false;
    public GameObject projectile;
    bool hasAttacked = false;
    
    CharacterController controller;

    void Start()
    {
        infectRange = 1.5f;

        infecttimer = 3.0f; 
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        player = GameObject.FindGameObjectWithTag("Player");
        attackCooldown = attackCooldownMax;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (heroEquipment.paused == false)
        {
            if(isInfected)
            Infect();
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
    }

    void MoveTowards()
    {
        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void Infect()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in allObjects)
        {
            Vector3 dist = transform.position - obj.transform.position;
            if (obj.tag == "Enemy"&&dist.magnitude<infectRange)
                obj.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);

        }
    
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


    void Decoy(GameObject decoy)
    {
        player = decoy;
       // playMove = decoy.GetComponent<PlayerMovement>();
    }
    void UnDecoy(GameObject decoy)
    {
        player = GameObject.FindGameObjectWithTag("Player");
     //   playMove = player.GetComponent<PlayerMovement>();
    }
	void Reinforce()
	{
		if (!isReinforced) 
		{
			attackMaxRange *= 1.5f;
			moveSpeed *= 1.5f;
            isReinforced = true;
		}
		
	}

	void UnReinforce()
	{
        if (isReinforced)
        {
            attackMaxRange /= 1.5f;
            moveSpeed /= 1.5f;
            isReinforced = false;
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
    void GetInfected()
    {
        isInfected = true;
    }
}
