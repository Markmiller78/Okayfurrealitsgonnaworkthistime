using UnityEngine;
using System.Collections;

public class AIShadowSpawn : MonoBehaviour
{

    GameObject player;
    Health ShadowHealth;
    Health playerHealth;
    PlayerLight playerLight;
    CharacterController controller;
    GameObject NearestLightPickup;

    Vector2 Moveto;
    Vector3 WayPoint;

    Vector3 AmIStuck;

    public float speed = 1;
    float DistancetoPlayer;
    float DistancetoLight;
    float DistancetoPrimary;

    float timer = 1;
    float DmgTimer = 1;
    int timerCount = 1;
    int stuckCounter = 1;
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
        ShadowHealth = GetComponent<Health>();
        Moveto = new Vector3(0, 0);
        controller = GetComponent<CharacterController>();
        speed = 1;
        timerCount = 1;
        DmgTimer = 1;
        minSpeed = 1;
        maxSpeed = 1;
        newWayPoint = true;
        stuckCounter = 0;
        DistancetoPrimary = 100;
        DistancetoLight = 100;
        DistancetoPlayer = 100;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        DistancetoPlayer = DistancetoLight = DistancetoPrimary = 100;
        //calculate distance to player.
        if (timer < 0)
        {
            DistancetoPlayer = Vector3.Distance(transform.position, player.transform.position);
            timer = 1;
            //print(DistancetoPlayer);
            print(CurrentState);
            timerCount += 1;
            stuckCounter--;
        }

        //"Animated" Movement
        if (newWayPoint)
        {
            speed = maxSpeed;
            newWayPoint = false;
        }
        if (speed > 0)
            speed -= 2 * Time.deltaTime;

        //Locate Primary and 2ndary Threat
        //PrimaryThreat = findNearestWithTag(transform.position, "Player");
        //Locate Nearest Light Pickup
        NearestLightPickup = findNearestWithTag(transform.position, "Light");
        if(NearestLightPickup != null)
        DistancetoLight = Vector3.Distance(transform.position, NearestLightPickup.transform.position);
        if (DistancetoLight < DistancetoPlayer)
        {
            PrimaryThreat = NearestLightPickup;
            DistancetoPrimary = DistancetoLight;
        }
        else
        {
            PrimaryThreat = player;
            DistancetoPrimary = DistancetoPlayer;
        }

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
        if (Physics.Raycast(transform.position, Moveto, .7f))
        {
            if (Moveto.x > 0)
                Moveto.x = 1;
            else
                Moveto.x = -1;
            if (Moveto.y > 0)
                Moveto.y = 1;
            else
                Moveto.y = -1;
            //print(Moveto);
        }
        // speed = 2;
        Moveto *= speed * Time.deltaTime;

        ConsumeLight();
        if (speed > 1)
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

        if (timerCount > 1)
        {
            timerCount = 0;

            WayPoint = transform.position - player.transform.position;
            Moveto = WayPoint;
            newWayPoint = true;
        }

    }

    void SuperEvade()
    {
        minSpeed = 2;
        maxSpeed = 5;

        if (stuckCounter <= 0)
        {
            if (transform.position == AmIStuck)
            {
                Jump();
            }
            AmIStuck = transform.position;
            stuckCounter = 1;
        }
        if (timerCount > 1)
        {
            timerCount = 0;

            WayPoint = player.transform.position - transform.position;
            Moveto = WayPoint;
            newWayPoint = true;

        }

    }

    void Enrage()
    {
        if (timerCount > 1)
        {
            timerCount = 0;

            WayPoint = PrimaryThreat.transform.position - transform.position;
            Moveto = WayPoint;
            newWayPoint = true;

        }


    }

    void Jump()
    {

        Vector3 temp = player.transform.position;
        player.transform.position = transform.position;
        transform.position = temp;

        print("JUMP!");

    }

    void ConsumeLight()
    {
        DmgTimer -= Time.deltaTime;
        if (DistancetoPrimary < 2 && DmgTimer <= 0)
        {
            ShadowHealth.currentHP -= 10;
            DmgTimer = .5f;
            print(DistancetoPrimary);
            if (PrimaryThreat == player)
                playerLight.currentLight -= 10;
            if (PrimaryThreat == NearestLightPickup)
                Destroy(NearestLightPickup);
            //INSTANTIATE PARTICLES ON THE PLAYER HERE TO SIGNIFY STEALING LIGHT AND DAMAGING THE SPAWN

            //Destroy(NearestLightPickup);

        }


        //DO Dead things
        if (ShadowHealth.currentHP <= 0)
            Destroy(gameObject);





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
