﻿using UnityEngine;
using System.Collections;

public class AIWraith : MonoBehaviour
{

    GameObject player;

    //Need this for knockback maybe? remove if wraith doesnt knock back
    PlayerMovement playMove;
    PlayerEquipment heroEquipment;

    Health playerHealth;
    CharacterController controller;
    public GameObject DarkOrb;

    Vector3 WayPoint;
    float DistanceToWayPoint;
    float randX, randY;
    // EastDoorX, WestDoorX, SouthDoorY, NorthDoorY; // used for Waypoints
    public float moveSpeed;
    public float turnSpeed;
    bool attacking;
    public bool isInfected = false;
    public bool isReinforced = false;
    float wayPointTimer, timer;

    float AttackTimer;
    bool AttackCD;
    float snaredSpeed;
    float SnareTimer;
    bool isSnared;

    // Use this for initialization
    void Start()
    {
        isSnared = false;
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        //Random.seed = 8675309;
        attacking = false;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        wayPointTimer = 8;
        timer = .5f;
        AttackTimer = 2;

        //SouthDoorY = GameObject.FindGameObjectWithTag("SouthDoor").transform.position.y;
        //EastDoorX = GameObject.FindGameObjectWithTag("EastDoor").transform.position.x;
        //WestDoorX = GameObject.FindGameObjectWithTag("WestDoor").transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (heroEquipment.paused == false)
        {
            wayPointTimer -= Time.deltaTime;
            timer -= Time.deltaTime;

            if (wayPointTimer < 0)
            {
                wayPointTimer = 8;
                NewWayPoint();
            }

            if (timer <= 0)
            {
                timer = .5f;
                DistanceToWayPoint = Vector3.Distance(transform.position, WayPoint);
                //print(DistanceToWayPoint);
            }


            if (DistanceToWayPoint < 1.5f)
            {
                attacking = true;
            }
            else
                attacking = false;


            if (!attacking)
                Move();
            else
            {
                Attack();
            }


            Turn();
        }
    }

    void Move()
    {
        Vector2 moveTo = (WayPoint - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void Attack()
    {
        if (AttackCD)
        {
            AttackCD = false;
            AttackTimer = 2;
        }
        AttackTimer -= Time.deltaTime;

        if (AttackCD == false && AttackTimer < 0)
        {
            AttackTimer = 1.2f;
            float DistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            Vector3 Target = player.transform.position - transform.position;
            RaycastHit[] things = Physics.RaycastAll(transform.position, Target, DistanceToPlayer);
            for(int i = 0; i < things.Length; i++)
            {
                if (things[i].collider.gameObject.tag == "Wall")
                {
                    NewWayPoint(); 
                    return;
                }
            }
            Instantiate(DarkOrb, transform.position, transform.rotation);
            AttackTimer = 1.2f;
            AttackCD = true;


            


        }




    }

    void NewWayPoint()
    {

        //for (int i = 0; i < 100; i++)
        //{

        randX = Random.Range(-3, 3);
        randY = Random.Range(-3, 3);
        WayPoint = new Vector3(player.transform.position.x + randX, player.transform.position.y + randY);

        // if (i == 90)
        //    print("I reached 90");

        return;
        // }

        //  print("Wraith: RETURNED BAD WAYPOINT!");
        // return;
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

    void Turn()
    {

        if (!attacking)
        {
            Vector3 vectorToWayPoint = WayPoint - transform.position;
            float angle = Mathf.Atan2(vectorToWayPoint.y, vectorToWayPoint.x) * Mathf.Rad2Deg;
            angle -= 90.0f;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
        }
        else
        {
            Vector3 vectorToPlayer = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
            angle -= 90.0f;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
        }
    }

    void Slow()
    {
        moveSpeed = moveSpeed * 0.5f;
    }
    void Decoy(GameObject decoy)
    {
        player = decoy;
    }
    void UnDecoy(GameObject decoy)
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Unslow()
    {
        moveSpeed = moveSpeed * 2;
    }
    void Reinforce()
    {
        if (!isReinforced)
        {

            moveSpeed *= 1.5f;
            isReinforced = true;
        }

    }

    void UnReinforce()
    {
        if (isReinforced)
        {
            moveSpeed /= 1.5f;
            isReinforced = false;
        }

    }
    void GetInfected()
    {
        isInfected = true;
    }
}
