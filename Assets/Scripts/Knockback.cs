using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour
{
    public float knockbackDistance;
    public float knockbackSpeed;

    bool isGettingKnockedBack = false;
    Vector3 origin;
    Vector2 dir;
    GameObject player;
    CharacterController controller;
    float timerMax;
    float timerCurr;

    PlayerEquipment heroEquipment;

    bool once;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = player.GetComponent<PlayerEquipment>();
        controller = GetComponent<CharacterController>();
        timerMax = knockbackDistance / knockbackSpeed;
        timerCurr = timerMax;
        once = true;
    }

    void Update()
    {
        if (heroEquipment.equippedAccessory == accessory.BlastOfLight && heroEquipment.equippedEmber == ember.Wind)
        {
            if (once)
            {
                knockbackDistance *= 2;
                once = false;
            }
        }

        if (!isGettingKnockedBack)
        {
            origin = transform.position;
            dir = (transform.position - player.transform.position);
        }
        else if (isGettingKnockedBack)
        {
            timerCurr -= Time.deltaTime;
            controller.Move(dir * Time.deltaTime * knockbackSpeed);

            if (Vector3.Distance(transform.position, origin) >= knockbackDistance || timerCurr <= 0.0f)
            {
                isGettingKnockedBack = false;
                timerCurr = timerMax;
            }
        }
    }

    void GetWrecked()
    {
        isGettingKnockedBack = true;
        once = true;
    }

}
