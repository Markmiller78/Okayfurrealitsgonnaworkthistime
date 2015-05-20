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
    //Summon
    GameObject SpawnedShadow;

    //Spells
    public GameObject MorriusOrb;
    GameObject[] WallCheck;
    public GameObject MorriusExplosion;

    Health playerHealth;
    CharacterController controller;
    Vector3 WayPoint;
    Vector3 Orgin;
    float DistanceToWayPoint;
    float DistanceToPlayer;
    public float moveSpeed;
    public float turnSpeed;
    float timer;
    int currentState;
    float AttackCD;
    bool castingDarkPULL, castingDarkPUSH, CastingMorriusOrb, Charging;

    float EndSpellTimer;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        AttackCD = 0;
        castingDarkPULL = castingDarkPUSH = CastingMorriusOrb = Charging = false;
        timer = 3;
        currentState = 0;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        NewWayPoint();
        EndSpellTimer = 10;
        Orgin = transform.position;
        WayPoint = new Vector3(player.transform.position.x, player.transform.position.y - 10, -1);
    }



    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        EndSpellTimer -= Time.deltaTime;
        //Run Spells if they're on
        if (timer < 0)
        {

            timer = .15f;
            if (castingDarkPULL)
                DarkWavePull();
            if (castingDarkPUSH)
                DarkWavePush();
            if (CastingMorriusOrb)
                Attack(); //CD is in function

        }
        if(currentState == 0)
            Charge();



        print(currentState);






        if(EndSpellTimer < 0)
        {
            castingDarkPUSH = castingDarkPULL = CastingMorriusOrb = Charging = false;
            currentState++;
        }
    }

    void Move()
    {
        Vector2 moveTo = (WayPoint - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void Attack()
    {
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

    void NewWayPoint()
    {

    }

    void SpellCasting()
    {
        SpellCastingParts.SetActive(true);
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
        Vector3 AboveMorrius = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        playMove.KnockBack(AboveMorrius);
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
        if(DistanceToPlayer < 3)
            endCharge(player.transform.position);

        WallCheck = GameObject.FindGameObjectsWithTag("Wall");
        for (int i = 0; i < WallCheck.Length; i++ )
        {
            if(Vector3.Distance(transform.position, WallCheck[i].transform.position) < 2.6)
            {
                endCharge(WallCheck[i].transform.position);
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
    }
    void ShadowSwap()
    {


    }
}