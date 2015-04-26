using UnityEngine;
using System.Collections;

public class AIShadowSpawn : MonoBehaviour
{

    GameObject player;
    Health playerHealth;
    PlayerLight playerLight;
    CharacterController controller;

    Vector2 Moveto;
    Vector3 WayPoint;
    public float speed = 1;
    float DistancetoPlayer;
    float timer = 1;
    int timerCount = 1;
    bool newWayPoint = true;
    float minSpeed = 1;
    float maxSpeed = 5;

    GameObject PrimaryThreat;
    GameObject SecondaryThreat;
    public enum State { Idle = 0, Evade, SuperEvade, Enrage }
    State CurrentState = State.Idle;

    void Start()
    {
        Random.seed = 42;
        player = GameObject.FindGameObjectWithTag("Player");
        playerLight = player.GetComponent<PlayerLight>();
        Moveto = new Vector3(0, 0);
        controller = GetComponent<CharacterController>();
        speed = 1;
        timerCount = 1;
        minSpeed = 1;
        maxSpeed = 1;
        newWayPoint = true;


    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        //calculate distance to player.
        if (timer < 0)
        {
            float num1 = (this.transform.position.x - player.transform.position.x);
            num1 *= num1;
            float num2 = (this.transform.position.y - player.transform.position.y);
            num2 *= num2;
            DistancetoPlayer = Mathf.Sqrt(num1 + num2);
            timer = 1;
            print(DistancetoPlayer);
            print(CurrentState);
            timerCount += 1;
        }

        //"Animated" Movement
        if (newWayPoint)
        {
            speed = maxSpeed;
            newWayPoint = false;
        }
        if(speed > 0)
            speed -= 2* Time.deltaTime;

        //Locate Primary and 2ndary Threat
        PrimaryThreat = findNearestWithTag(transform.position, "Player");
        




        //Determine Which behavior to run
        if (playerLight.currentLight < 10)
        {
            CurrentState = State.Enrage;
        }
        if (DistancetoPlayer > 7)
            CurrentState = State.Idle;
        else if (DistancetoPlayer < 7 && DistancetoPlayer > 3)
        {
            CurrentState = State.Evade;
        }
        else if (DistancetoPlayer < 3)
        {
            CurrentState = State.SuperEvade;
        }




        //Call the Current behavior
        switch (CurrentState)
        {
            case State.Idle:
                {
                    Idle();
                    break;
                }
            case State.Evade:
                {
                    Evade();
                    break;
                }
            case State.SuperEvade:
                {
                    SuperEvade();
                    break;
                }
            case State.Enrage:
                {
                    Enrage();
                    break;
                }
        }
        Moveto.Normalize();
        Moveto *= speed * Time.deltaTime;
        if(speed > 1)
        controller.Move(Moveto);
    }

    void Idle()
    {
        minSpeed = 0;
        maxSpeed = 3;

        //Change Idle Waypoint every 2 seconds.
        if (timerCount > 2)
        {
            timerCount = 0;
            float newX = Random.Range(-2f, 2f);
            float newY = Random.Range(-2f, 2f);
            WayPoint = new Vector3(transform.position.x + newX, transform.position.y + newY, -1.4f);

            Moveto = transform.position - WayPoint;
            newWayPoint = true;
        }
    }

    void Evade()
    {
        
        if(timerCount > 1 )
        {
            timerCount = 0;

            WayPoint = PrimaryThreat.transform.position - transform.position;
            Moveto = WayPoint;
            newWayPoint = true;
        }

    }

    void SuperEvade()
    {
        minSpeed = 1;
        maxSpeed = 4;
        if (timerCount > 1)
        {
            timerCount = 0;

            WayPoint = PrimaryThreat.transform.position - transform.position;
            Moveto = WayPoint;
            newWayPoint = true;
            Jump();
        }

    }

    void Enrage()
    {
        if (timerCount > 1)
        {
            timerCount = 0;

            WayPoint = transform.position - PrimaryThreat.transform.position;
            Moveto = WayPoint;
            newWayPoint = true;
            
        }
        

    }

    void Jump()
    {

        speed = 7;
    }

    public static GameObject findNearestWithTag(Vector3 fromPosition, string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);

        if (gos.Length == 1)
            return gos[0];

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = fromPosition;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }

}
