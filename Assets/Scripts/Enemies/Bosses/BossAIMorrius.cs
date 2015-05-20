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

    Health playerHealth;
    CharacterController controller;
    Vector3 WayPoint;
    float DistanceToWayPoint;
    public float moveSpeed;
    public float turnSpeed;
    float timer;
    bool attacking;
    float AttackTimer;
    bool AttackCD;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        attacking = false;
        timer = 3;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        AttackTimer = 2;
        NewWayPoint();

    }



    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            timer = .15f;
            DarkWavePull();
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
            for (int i = 0; i < things.Length; i++)
            {
                if (things[i].collider.gameObject.tag == "Wall")
                {
                    NewWayPoint();
                    return;
                }
            }
            Instantiate(MorriusOrb, transform.position, transform.rotation);
            AttackTimer = 1.2f;
            AttackCD = true;
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

    }

    void ShadowSwap()
    {


    }
}