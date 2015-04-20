using UnityEngine;
using System.Collections;

public class PlayerMeleeAttack : MonoBehaviour
{
    GameObject player;
    PlayerStats playerStats;
    public float attackDamage = 5.0f;
    bool attacking = false;
    float hasRotated = 0.0f;
    float toRotate = 90.0f;
    float rotationDelta = 0.0f;
    public float speed = 3.0f;

    void Start()
    {
        player = transform.parent.gameObject;
        playerStats = player.GetComponent<PlayerStats>();
        rotationDelta = player.transform.rotation.z;
    }

    void Update()
    {
        if (attacking)
        {
            hasRotated += 90.0f * Time.deltaTime * speed;
            transform.Rotate(Vector3.forward, 90.0f * Time.deltaTime * speed - rotationDelta);
            rotationDelta = player.transform.rotation.z - rotationDelta;
            if (hasRotated >= toRotate)
            {
                hasRotated = 0.0f;
                attacking = false;
                transform.rotation = player.transform.rotation;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (attacking && other.gameObject != player && other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().LoseHealth(attackDamage + playerStats.meleeModifier);
            if (other.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 forceVector = (player.transform.position - other.transform.position).normalized;
                other.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(forceVector);
            }
        }
    }

    void Melee()
    {
        attacking = true;
    }
}
