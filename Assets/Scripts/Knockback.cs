using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour
{
    bool isGettingKnockedBack = false;
    float dist = 0.0f;
    Vector3 origin;
    Vector2 dir;
    GameObject player;
    CharacterController controller;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isGettingKnockedBack)
        {
            origin = transform.position;
            dir = (transform.position - player.transform.position);
        }
        else if (isGettingKnockedBack)
        {
            controller.Move(dir * Time.deltaTime * 5.0f);
            if (Vector3.Distance(transform.position, origin) >= dist)
            {
                isGettingKnockedBack = false;
            }
        }
    }

    void GetWrecked(float knockBackDistance)
    {
        isGettingKnockedBack = true;
        dist = knockBackDistance;
    }
}
