using UnityEngine;
using System.Collections;

public class BossAIMorrius : MonoBehaviour
{
    GameObject player;

    //Need this for knockback maybe? remove if wraith doesnt knock back
    PlayerMovement playMove;
    PlayerEquipment heroEquipment;

    //Particles
    public GameObject DarkWavePushParts;
    public GameObject DarkWavePullParts;
    public GameObject ChargeParts;
    public GameObject DarknessParts;
    public GameObject SpellCastingParts;
    public GameObject CastingEffects;
    //Summon
    GameObject SpawnedShadow;

    //Spells
    public GameObject MorriusOrb;
    GameObject[] WallCheck;
    public GameObject MorriusExplosion;
    public GameObject RemainsDropoOnWallHit;
    public GameObject RemainsDropEffect;

    Health playerHealth;
    CharacterController controller;
    Vector3 WayPoint;
    Vector3 WayPointLeft;
    Vector3 WayPointRight;
    Vector3 Orgin;
    float DistanceToWayPoint;
    float DistanceToPlayer;
    public float moveSpeed;
    public float turnSpeed;
    public bool isInfected = false;
    public bool isReinforced = false;
    float timer;
    int currentState;
    float AttackCD;
    bool castingDarkPULL, castingDarkPUSH, CastingMorriusOrb, Charging, Returning;
    float StateTimer;
    float returnOrginTimer;
    float EndSpellTimer;
    float castTimer;

    Health Myhealth;
    public GameObject BossHealthBar;
    GameObject healthB;
    GameObject HealthRemaining;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        AttackCD = 0;
        castingDarkPULL = castingDarkPUSH = CastingMorriusOrb = Charging = Returning = false;         //Spells Set to False
        timer = 3;
        currentState = 0;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        NewWayPoint();
        EndSpellTimer = 2;
        StateTimer = 5;
        returnOrginTimer = 0;
        Orgin = transform.position;
        WayPointLeft = new Vector3(Orgin.x + 8, Orgin.y, Orgin.z);
        WayPointRight = new Vector3(Orgin.x - 8, Orgin.y, Orgin.z);
        WayPoint = WayPointRight;

        currentState = 0;

        Myhealth = gameObject.GetComponent<Health>();

        healthB = (GameObject)Instantiate(BossHealthBar);
        HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");
    }



    // Update is called once per frame
    void Update()
    {

        if (HealthRemaining != null)
            HealthRemaining.transform.localScale = new Vector3(Myhealth.healthPercent, 1, 1);
        else
            HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");

        timer -= Time.deltaTime;
        EndSpellTimer -= Time.deltaTime;
        castTimer -= Time.deltaTime;
        returnOrginTimer -= Time.deltaTime;
        //CAST SPELLS (if they're active)
        if (timer < 0)
        {
            timer = .15f;
            if (castingDarkPULL)
                DarkWavePull();
            if (castingDarkPUSH)
                DarkWavePush();

        }
        if (timer < .5f)
        {
            if (CastingMorriusOrb)
                Attack(); //CD is in function
            if (Charging)
                Charge();
        }
        if (Returning)
        {
            if (Vector3.Distance(transform.position, Orgin) < 2)
                EndSpellTimer = -1;
            if (returnOrginTimer < .1f)
            {
                ReturnToOrgin();
            }

            if (returnOrginTimer < 0)
            {
                Camera.main.SendMessage("ScreenShake");
                returnOrginTimer = .4f;
            }

        }

            if (currentState == 0)
            {
                EndSpellTimer = -1;
            }

        //STATE 1 (Controlling the Durations) Activate and set the duration of preperation and casting
        if (currentState == 1 && CastingMorriusOrb == false)
        {
            WayPoint = WayPointRight;
            SpellCasting(3); //prep
            CastingMorriusOrb = true;
            EndSpellTimer = 15; // duration
        }

        if (currentState == 2)
        {
            EndSpellTimer = -1;
        }
        if (currentState == 3 && castingDarkPUSH == false)
        {
            SpellCasting(1); //prep
            castingDarkPUSH = true;
            EndSpellTimer = 2; // duration
        }


        if (currentState == 4 && Charging == false)
        {
            WayPoint = player.transform.position;
            SpellCasting(2); //prep
            Charging = true;
            EndSpellTimer = 8; //duration
        }


        if (currentState == 5)
        {
            EndSpellTimer = -1;
        }


        if (currentState == 6 && Returning == false)
        {
            returnOrginTimer = 0;
            Returning = true;
            EndSpellTimer = 8; // duration (noprep)
        }


        if (currentState == 7)
        {
            EndSpellTimer = -1;
        }






        if (castTimer < 0)
        {
            StopCasting();
        }
        if (EndSpellTimer < 0)
        {
            castingDarkPUSH = castingDarkPULL = CastingMorriusOrb = Charging = Returning = false;
            CastingEffects.SetActive(false);
            EndSpellTimer = 1000;



            currentState++;
            if (currentState == 7)
                currentState = 0;
        }
        print(currentState);

        //Left and right movement
        if (!Charging)
        {
            if (Vector3.Distance(transform.position, WayPointLeft) < 3)
                WayPoint = WayPointRight;
            if (Vector3.Distance(transform.position, WayPointRight) < 3)
                WayPoint = WayPointLeft;
        }
        //PUSH PLAYER IF HE GETS TOO CLOSE
        DistanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (DistanceToPlayer < 2)
            PushPlayerAway();
    }

    void Move()
    {
        Vector2 moveTo = (WayPoint - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }
    void MoveAwayFromWalls()
    {
        Vector2 moveTo = (new Vector3(8,-8,0) - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }
    void MoveAtPlayer()
    {
        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }
    void Attack()
    {
        moveSpeed = 7;
        if (Myhealth.healthPercent < .75)
        {
            if (returnOrginTimer < .1f)
            {
                MoveAtPlayer();
            }

            if (returnOrginTimer < 0)
            {
                Camera.main.SendMessage("ScreenShake");
                returnOrginTimer = .4f;
            }
        }
        else
        {
            if (returnOrginTimer < .1f)
            {
                Move();
            }

            if (returnOrginTimer < 0)
            {
                Camera.main.SendMessage("ScreenShake");
                returnOrginTimer = .4f;
            }
        }
        CastingEffects.SetActive(true);
        AttackCD -= Time.deltaTime;
        if (AttackCD < 0)
        {
            Vector3 temp = player.transform.position - transform.position;
            float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg + 270;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(MorriusOrb, transform.position, rot);
            AttackCD = 1;
        }
    }

    void PushPlayerAway()
    {
        player.SendMessage("KnockBack", transform.position, SendMessageOptions.DontRequireReceiver);
    }

    void NewWayPoint()
    {

    }

    void SpellCasting(float castTime)
    {

        WallCheck = GameObject.FindGameObjectsWithTag("Wall");
        for (int i = 0; i < WallCheck.Length; i++)
        {
            if (Vector3.Distance(transform.position, WallCheck[i].transform.position) < 3.5f)
            {
                MoveAwayFromWalls();
                return;
            }
        }


        castTimer = castTime;
        timer = castTime;
        SpellCastingParts.SetActive(true);
        print("Active");
    }
    void StopCasting()
    {
        SpellCastingParts.SetActive(false);
    }

    void DarkWavePush()
    {
        playMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        GameObject temp = (GameObject)Instantiate(DarkWavePushParts, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(temp, 3);
        //Vector3 AboveMorrius = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        playMove.KnockBack(transform.position);
    }

    void DarkWavePull()
    {
        playMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        GameObject temp = (GameObject)Instantiate(DarkWavePullParts, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(temp, 3);
        Vector3 AboveMorrius = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        playMove.PullThePlayer(AboveMorrius);
    }

    void Charge()
    {
        //Check if Charge has hit anything
        DistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (DistanceToPlayer < 3)
            endCharge(player.transform.position);

        WallCheck = GameObject.FindGameObjectsWithTag("Wall");
        for (int i = 0; i < WallCheck.Length; i++)
        {
            if (Vector3.Distance(transform.position, WallCheck[i].transform.position) < 2.9f)
            {
                endCharge(WallCheck[i].transform.position);
                ExplodeLightReamins();
                return;
            }
        }

        //Continue Charging if nothing hit
        moveSpeed = 10;
        Vector2 moveTo = (WayPoint - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }
    void endCharge(Vector3 ExplodeOnSpot)
    {
        EndSpellTimer = -1;
        Instantiate(MorriusExplosion, ExplodeOnSpot, transform.rotation);
        WayPoint = Orgin;
    }
    void ReturnToOrgin()
    {
        moveSpeed = 8;
        Vector2 moveTo = (Orgin - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }
    void ShadowSwap()
    {

    }
    void ExplodeLightReamins()
    {

        Instantiate(RemainsDropEffect, transform.position, new Quaternion(0, 0, 0, 0));
        for (int i = 0; i < 5; i++)
        {
            float RandX = Random.Range(-5, 5);
            float RandY = Random.Range(-5, 5);
            Instantiate(RemainsDropoOnWallHit, new Vector3(transform.position.x + RandX, transform.position.y + RandY, -1f), new Quaternion(0, 0, 0, 0));
        }
    }
    void DestroyHealthBar()
    {
        Destroy(healthB);
    }
}