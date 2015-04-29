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
    PlayerEquipment heroEquipment;
    bool paused;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        paused = false;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();

    }

    void Update()
    {
        if (heroEquipment.paused == false)
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            MoveTowardPlayer();

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

    }

    void SpellAttack()
    {

    }

    void Summon()
    {

    }

}
