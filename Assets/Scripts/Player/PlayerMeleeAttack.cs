using UnityEngine;
using System.Collections;

public class PlayerMeleeAttack : MonoBehaviour
{
    GameObject player;
    public float attackDamage = 5.0f;
    bool attacking = false;
    float hasRotated = 0.0f;
    float toRotate = 90.0f;
    public float speed = 3.0f;
    Quaternion startingRotation;

    void Start()
    {
        player = transform.parent.gameObject;
    }

    void Update()
    {
        if (attacking)
        {
            hasRotated += 90.0f * Time.deltaTime * speed;
            transform.Rotate(Vector3.forward, 90.0f * Time.deltaTime * speed);
            if (hasRotated >= toRotate)
            {
                hasRotated = 0.0f;
                attacking = false;
                transform.rotation = startingRotation;
            }
        }
    }

    void Melee()
    {
        attacking = true;
        startingRotation = transform.rotation;
    }
}
