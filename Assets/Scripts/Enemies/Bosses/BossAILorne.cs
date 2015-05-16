using UnityEngine;
using System.Collections;

public class BossAILorne : MonoBehaviour {
    GameObject player;
    float distanceToPlayer;
 
    public float spellMinRange;
    public float spellMaxRange;
    public float moveSpeed;
    public float turnSpeed;
    CharacterController controller;
    Health myHealth;
    PlayerEquipment heroEquipment;
   public GameObject darkOrb;
    int maxcasts;
    bool isCasting = false;
    enum state { happy, upset, ravingMad,spiritform };
    state currState = state.happy;
    float retreatTimer;
    int currentcasts;
    float rTimerMax = 5.0f;
    float specialTimer;
    float spellTimerMax = 5.0f;
    bool wiggled;
    bool wiggling;
    float wiggleTimer;
    float wTimerMax = .5f;
    Vector3 wiggleStartPos;
    public GameObject meleeAttackObject;
    
    public GameObject enemySpawner;
    GameObject[] spawners;
    public GameObject spellAttack;
    float spellTimer;
    float spTimerMax = 2.5f;
    public GameObject hazard;
    float hazardPlacementTimer;
    float hTimerMax = 1.0f;
	// Use this for initialization
	void Start () 
    {

        maxcasts = 3;
        currentcasts = 0;

            player = GameObject.FindGameObjectWithTag("Player");
            controller = GetComponent<CharacterController>();
            heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
            myHealth = GetComponent<Health>();   
            Instantiate(enemySpawner, new Vector3(5, -5, -1), Quaternion.Euler(0, 0, 225));
            Instantiate(enemySpawner, new Vector3(14, -5, -1), Quaternion.Euler(0, 0, 135));
            Instantiate(enemySpawner, new Vector3(5, -14, -1), Quaternion.Euler(0, 0, 315));
            Instantiate(enemySpawner, new Vector3(14, -14, -1), Quaternion.Euler(0, 0, 45));
            spawners = GameObject.FindGameObjectsWithTag("LorneSpawn");
            specialTimer = spellTimerMax;
            spellTimer = spTimerMax;
        
	
	}
	
	// Update is called once per frame
	void Update ()
    
    {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        if (transform.position.x < 0f ||
            transform.position.x > 20f ||
            transform.position.y > 0f ||
            transform.position.y < -20f)
        {
            transform.position = new Vector3(9.5f, -9.5f, -1f);
        }
        if (heroEquipment.paused == false)
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            Turn();
            if(isCasting)
            {
                spellTimer -= Time.deltaTime;
                if(spellTimer<=0.0f)
                {
                    spellTimer = spellTimerMax;
                    isCasting = false;
                }
            
            }
            switch(currState)
            {
                case state.happy:
                    if (distanceToPlayer<spellMinRange)
                    MoveAwayFromPlayer();
                   
                    SpellCast();
                        
                    break;
                case state.upset:

                    break;
                case state.ravingMad:

                    break;
                case state.spiritform:

                    break;


            }
        }
	
	}
    void MoveTowardPlayer()
    {

        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void MoveAwayFromPlayer()
    {
        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * -moveSpeed);
    }
    void Turn()
    {
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        angle += 90.0f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }

    void SpellCast()
    {
        Vector3 vectoplayer = transform.position - player.transform.position;
        
        if (!Physics.Raycast(transform.position, vectoplayer.normalized, spellMaxRange))
        {
            if (Random.value > 0.2f&&!isCasting)
            {
                Instantiate(darkOrb, transform.position, transform.rotation);
                ++currentcasts;
                    if(currentcasts==maxcasts)
                    {
                        spellTimer = spellTimerMax;
                isCasting = true;
                currentcasts = 0;
                    }
            }
        }




    }

}
