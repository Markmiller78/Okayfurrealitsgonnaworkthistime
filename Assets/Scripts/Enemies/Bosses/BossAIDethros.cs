using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossAIDethros : MonoBehaviour
{
    GameObject player;
    float distanceToPlayer;
    public float meleeRange;
    public float spellMinRange;
    public float spellMaxRange;
    public float moveSpeed;
    public float turnSpeed;
    CharacterController controller;
    Health myHealth;
    PlayerEquipment heroEquipment;
    enum state { casual, aggravated, intense };
    state currState = state.casual;
    float retreatTimer;
    float rTimerMax = 5.0f;
    public GameObject meleeAttackObject;
    DethrosMeleeAttack meleeScript;
    public Text YouWinText;
    bool Victory;
    float VictoryTimer;

    void Start()
    {
        Victory = false;
        VictoryTimer = 5;
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
        if (Victory)
        {
            VictoryTimer -= Time.deltaTime;
        }
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
                            Turn();
                            if (distanceToPlayer <= 2f)
                            {
                                MeleeAttack();
                                retreatTimer = rTimerMax;
                            }
                        }
                        else
                        {
                            MoveAwayFromPlayer();
                            Turn();
                        }

                        if (myHealth.currentHP < myHealth.maxHP * 2f / 3f)
                            currState = state.aggravated;
                    }
                    break;
                case state.aggravated:
                    {

                        YouWinText.text = "You Win!";
                        heroEquipment.paused = true;
#if UNITY_STANDALONE
                        if (VictoryTimer < 0)
                        {
                            YouWinText.text = " ";
                            Application.Quit();
                        }
#elif UNITY_EDITOR
                
                        UnityEditor.EditorApplication.isPlaying = false;
#endif

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

    void Summon()
    {

    }

}
