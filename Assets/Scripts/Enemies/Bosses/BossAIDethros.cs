using UnityEngine;
using System.Collections;

public class BossAIDethros : MonoBehaviour
{
    GameObject player;
    float distanceToPlayer;
    public float meleeRange;
    public float spellMinRange;
    public float spellMaxRange;
    public float moveSpeed;
    CharacterController controller;
    Health myHealth;
    PlayerEquipment heroEquipment;
    enum state { casual, aggravated, intense };
    state currState = state.casual;
    float retreatTimer;
    float rTimerMax = 5.0f;
    public GameObject meleeAttackObject;
    DethrosMeleeAttack meleeScript;

    void Start()
    {
        // Get Player ref
        player = GameObject.FindGameObjectWithTag("Player");
        // Get Controller ref for movement
        controller = GetComponent<CharacterController>();
        // Pause bool is in player
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        // Ref to Health
        myHealth = GetComponent<Health>();
        meleeAttackObject.SetActive(false);
        meleeScript = meleeAttackObject.GetComponent<DethrosMeleeAttack>();
    }

    void Update()
    {
        // Only update if game is unpaused
        if (heroEquipment.paused == false)
        {
            // Get the distance to the player
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            switch (currState)
            {
                case state.casual:
                    {
                        if (retreatTimer > 0.0f)
                        {
                            retreatTimer -= Time.deltaTime;
                            if (retreatTimer <= 0.0f)
                            {
                                retreatTimer = 0.0f;
                            }
                        }
                        if (retreatTimer == 0.0f)
                        {
                            MoveTowardPlayer();
                            if (distanceToPlayer <= 2f)
                            {
                                MeleeAttack();
                                retreatTimer = rTimerMax;
                            }
                        }
                        else
                        {
                            MoveAwayFromPlayer();
                        }

                        if (myHealth.currentHP < myHealth.maxHP * 2f / 3f)
                            currState = state.aggravated;
                    }
                    break;
                case state.aggravated:
                    {


                        if (myHealth.currentHP < myHealth.maxHP / 3f)
                            currState = state.intense;
                    }
                    break;
                case state.intense:
                    {

                    }
                    break;
                default:
                    break;
            }

            // Lock Z
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
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

    void MeleeAttack()
    {
        meleeAttackObject.SetActive(true);
        meleeScript.attacking = true;
    }

    void SpellAttack()
    {

    }

    void Summon()
    {

    }

}
