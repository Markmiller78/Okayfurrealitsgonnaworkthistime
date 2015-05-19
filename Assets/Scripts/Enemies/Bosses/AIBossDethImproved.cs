using UnityEngine;
using System.Collections;

public class AIBossDethImproved : MonoBehaviour
{

    GameObject player;

    //Need this for knockback maybe? remove if wraith doesnt knock back
    PlayerMovement playMove;
    PlayerEquipment heroEquipment;
    Health playerHealth;
    CharacterController controller;
    public GameObject enemySpawner;

    public GameObject DarkOrb;
    public GameObject VanishParts;
    public GameObject AppearParts;
    bool vanishbool;
    bool vanishbool2;
    float vanishTimer;
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
    float angleOffset;
    bool increaseOffset = true;
    float AttackTimer;
    bool AttackCD;
    float snaredSpeed;
    float TopDoor, LeftDoor, roomWidth, roomHeight;
    public Rect Bounds;

    // Use this for initialization
    void Start()
    {
        vanishTimer = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        //Random.seed = 8675309;
        attacking = false;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        Instantiate(enemySpawner, new Vector3(5, -5, -1), Quaternion.Euler(0, 0, 225));
        Instantiate(enemySpawner, new Vector3(14, -5, -1), Quaternion.Euler(0, 0, 135));
        Instantiate(enemySpawner, new Vector3(5, -14, -1), Quaternion.Euler(0, 0, 315));
        Instantiate(enemySpawner, new Vector3(14, -14, -1), Quaternion.Euler(0, 0, 45));
        wayPointTimer = 8;
        timer = .5f;
        AttackTimer = 2;
        DetermineDoorPositions();
        Bounds = new Rect(TopDoor, LeftDoor, roomWidth, roomHeight);
        NewWayPoint();
        angleOffset = 0;
        vanishbool = true;
        vanishbool2 = true;
    }




    void DetermineDoorPositions()
    {

        GameObject[] Doors = GameObject.FindGameObjectsWithTag("Door");
        float PossibleBoundary;
        TopDoor = 0;
        LeftDoor = 100000;
        float BottomDoor = 0;
        float RightDoor = 0;


        //FIND TOP DOOR LOCATION
        for (int i = 0; i < Doors.Length; i++)
        {
            PossibleBoundary = Doors[i].transform.position.y;
            if (TopDoor < PossibleBoundary)
                TopDoor = PossibleBoundary;
        }
        PossibleBoundary = 0;
        for (int i = 0; i < Doors.Length; i++)
        {
            PossibleBoundary = Doors[i].transform.position.y;
            if (BottomDoor > PossibleBoundary)
                BottomDoor = PossibleBoundary;
        }

        //Width
        for (int i = 0; i < Doors.Length; i++)
        {
            PossibleBoundary = Doors[i].transform.position.x;
            if (LeftDoor > PossibleBoundary)
                LeftDoor = PossibleBoundary;
        }

        PossibleBoundary = 0;
        for (int i = 0; i < Doors.Length; i++)
        {
            PossibleBoundary = Doors[i].transform.position.x;
            if (RightDoor < PossibleBoundary)
                RightDoor = PossibleBoundary;
        }

        roomWidth = RightDoor - LeftDoor;
        roomHeight = BottomDoor - TopDoor;

    }

    // Update is called once per frame
    void Update()
    {


        if (heroEquipment.paused == false)
        {
            wayPointTimer -= Time.deltaTime;
            timer -= Time.deltaTime;

            if (wayPointTimer < .5f && vanishbool)
            {
                VanishEffect();
                vanishbool = false;
            }
            if (wayPointTimer < 0)
            {
                VanishEffect();
                vanishbool = true;
                Vanish();
                wayPointTimer = 8;
                NewWayPoint();
                DistanceToWayPoint = Vector3.Distance(transform.position, WayPoint);
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


            vanishTimer -= Time.deltaTime;

            if (vanishTimer < .3f && vanishbool2 == true)
            {
                AppearEffect();
                vanishbool2 = false;
            }
            if (vanishTimer < 0)
            {
                vanishTimer = 10000;
                AppearEffect();
                Appear();
            }

            Turn();
        }

        if (angleOffset < -50)
        {
            increaseOffset = true;
        }
        if (angleOffset > 50)
        {
            increaseOffset = false;
        }


        if (increaseOffset)
            angleOffset += Time.deltaTime * 55;
        else
            angleOffset -= Time.deltaTime * 55;



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
            AttackTimer = .5f;
        }
        AttackTimer -= Time.deltaTime;

        if (AttackCD == false && AttackTimer < 0)
        {

            Instantiate(DarkOrb, transform.position, transform.rotation);
            AttackTimer = .5f;
            AttackCD = true;





        }




    }

    void NewWayPoint()
    {
        vanishbool2 = true;
        for (int i = 0; i < 40; i++)
        {
            for (int k = 0; k < 30; k++)
            {
                randX = Random.Range(-5, 5);
                randY = Random.Range(-5, 5);

                Vector2 b1 = new Vector2(player.transform.position.x + randX, player.transform.position.y + randY);
                if (b1.x > Bounds.xMin && b1.x < Bounds.xMax && b1.y < Bounds.yMin && b1.y > Bounds.yMax)
                {
                    break;
                }

            }


            WayPoint = new Vector3(player.transform.position.x + randX, player.transform.position.y + randY);

            GameObject[] Walls = GameObject.FindGameObjectsWithTag("Wall");
            float WaytoWall = 0;
            bool BadPoint = false;

            for (int p = 0; p < Walls.Length; p++)
            {
                WaytoWall = Vector3.Distance(WayPoint, Walls[p].transform.position);

                if (WaytoWall < 2)
                {
                    BadPoint = true;
                }


            }

            if (BadPoint == false)
            {
                break;
            }
        }


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


            Quaternion rot = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);

            print(angleOffset);
        }
    }

    void VanishEffect()
    {
        Instantiate(VanishParts, transform.position, transform.rotation);
    }
    void Vanish()
    {
        print("Disappeared");
        transform.position = new Vector3(transform.position.x, transform.position.y, 5);
        NewWayPoint();
        vanishTimer = 2;
        vanishbool2 = true;

    }
    void AppearEffect()
    {
        Vector3 goingToAppear = new Vector3(transform.position.x, transform.position.y, -1);
        Instantiate(AppearParts, goingToAppear, transform.rotation);
    }
    void Appear()
    {
        print("Re-appeared");
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }
}
