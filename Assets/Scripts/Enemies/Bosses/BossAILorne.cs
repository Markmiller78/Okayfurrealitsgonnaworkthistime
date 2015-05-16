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
    enum state { happy, upset, ravingMad,spiritform };
    state currState = state.happy;
    float retreatTimer;
    float rTimerMax = 5.0f;
    float specialTimer;
    float sTimerMax = 10.0f;
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
         
           
            player = GameObject.FindGameObjectWithTag("Player");
            controller = GetComponent<CharacterController>();
            heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
            myHealth = GetComponent<Health>();   
            Instantiate(enemySpawner, new Vector3(5, -5, -1), Quaternion.Euler(0, 0, 225));
            Instantiate(enemySpawner, new Vector3(14, -5, -1), Quaternion.Euler(0, 0, 135));
            Instantiate(enemySpawner, new Vector3(5, -14, -1), Quaternion.Euler(0, 0, 315));
            Instantiate(enemySpawner, new Vector3(14, -14, -1), Quaternion.Euler(0, 0, 45));
            spawners = GameObject.FindGameObjectsWithTag("LorneSpawn");
            specialTimer = sTimerMax;
            spellTimer = spTimerMax;
        
	
	}
	
	// Update is called once per frame
	void Update ()
    
    {
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
            switch(currState)
            {
                case state.happy:

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

    void SpellAttack()
    {

    }

}
