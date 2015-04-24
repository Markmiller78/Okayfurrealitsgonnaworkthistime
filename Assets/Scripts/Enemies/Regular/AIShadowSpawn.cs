using UnityEngine;
using System.Collections;

public class AIShadowSpawn : MonoBehaviour
{

    GameObject player;
    Health playerHealth;
    CharacterController controller;

    public float damage;
    public float damageRate;
    public float moveSpeed;
    float distanceToPlayer;

    Vector2 fleeDirection;

    bool fleeing;
    float fleeTimer;
    float fleeDuration;
    float fleeCooldown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        fleeing = false;
        fleeTimer = 0;
        fleeDuration = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (fleeing)
        {
            Flee();
        }
        else
        {
            //If the player is close, move away
            if (distanceToPlayer <= 10)
            {
                MoveAway();
            }
        }

    }

    void MoveAway()
    {
        Vector2 moveTo = (transform.position - player.transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
       // transform.LookAt(moveTo);
    }

    void Flee()
    {
        controller.Move(fleeDirection * Time.deltaTime * 10);

        fleeTimer -= Time.deltaTime;
        if (fleeTimer <= 0)
        {
            fleeing = false;
        }
    }
}
