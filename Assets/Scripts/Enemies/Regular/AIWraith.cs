using UnityEngine;
using System.Collections;

public class AIWraith : MonoBehaviour
{

    GameObject player;

    //Need this for knockback maybe? remove if wraith doesnt knock back
    PlayerMovement playMove;

    Health playerHealth;
    CharacterController controller;
    public GameObject DarkOrb;

    Vector3 WayPoint;
    float DistanceToWayPoint;
    float randX, randY, EastDoorX, WestDoorX, SouthDoorY, NorthDoorY; // used for Waypoints
    public float moveSpeed;
    public float turnSpeed;
    bool attacking;
    float wayPointTimer, timer;

    float AttackTimer;
    bool AttackCD;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        Random.seed = 8675309;
        attacking = false;
        wayPointTimer = 8;
        timer = .5f;
        AttackTimer = 2;

        NorthDoorY = GameObject.FindGameObjectWithTag("NorthDoor").transform.position.y;
        SouthDoorY = GameObject.FindGameObjectWithTag("SouthDoor").transform.position.y;
        EastDoorX = GameObject.FindGameObjectWithTag("EastDoor").transform.position.x;
        WestDoorX = GameObject.FindGameObjectWithTag("WestDoor").transform.position.x;
    }

    // Update is called once per frame
    void Update()
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
            Attack();

        Turn();
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

        if(AttackCD == false && AttackTimer < 0)
        {
            
            Instantiate(DarkOrb, transform.position, transform.rotation);
            //print("Wraith: I attacked!");

            AttackCD = true;
        }




    }

    void NewWayPoint()
    {

        for (int i = 0; i < 100; i++)
        {

            randX = Random.Range(-3, 3);
            randY = Random.Range(-3, 3);
            WayPoint = new Vector3(player.transform.position.x + randX, player.transform.position.y + randY);

            if (i == 90)
                print("I reached 90");

            if (WayPoint.x < EastDoorX && WayPoint.x > WestDoorX && WayPoint.y < NorthDoorY && WayPoint.y > SouthDoorY)
                return;

        }

        print("Wraith: RETURNED BAD WAYPOINT!");
        return;
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


}
