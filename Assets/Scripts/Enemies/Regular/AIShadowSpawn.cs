using UnityEngine;
using System.Collections;

public class AIShadowSpawn : MonoBehaviour
{
    enum state { evade, flee, enrage };

    GameObject player;
    Health playerHealth;
    CharacterController controller;

    public float damage;
    public float damageRate;
    public float moveSpeed;
    float distanceToPlayer;

    state theState;

    Vector2 fleeDirection;

    float directionTimer;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        theState = state.evade;
        directionTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < 3)
        {
            theState = state.flee;
        }
        else
        {
            theState = state.evade;
        }

        if (theState == state.evade)
        {
            Evade();            
        }
        else if (theState == state.flee)
        {
            Flee();
            if (directionTimer <= 0)
            {
                directionTimer = 0.5f;
                //Vector3 modifier = player.transform.position + new Vector3(Random.Range(-10, 20), Random.Range(-10, 20), 0);
                fleeDirection = -(transform.position - player.transform.position ).normalized;
            }
        }
    }

    void Evade()
    {
        Vector2 moveTo = (transform.position - player.transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void Flee()
    {
        if (directionTimer >= 0.5f)
        {
            directionTimer -= Time.deltaTime;
        }

        controller.Move(fleeDirection * Time.deltaTime * 10);
    }
}
