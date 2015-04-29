using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AICommander : MonoBehaviour
{


    GameObject player;
    public GameObject[] list;
    public bool isReinforcing = false;
    public bool isAttacking = false;
    CharacterController controller;
    public float atkdmg;
    public float atkrange;
    public float atkcooldown;
    public float atkcooldownref;
    public float movementspeed = 3.0f;
    public float[] distance;
    PlayerEquipment heroEquipment;
    public float dist;
    public Vector3 vectoplayer;
    public int size;
    float snaredSpeed;
    float SnareTimer;
    bool isSnared;
    // Use this for initialization
    void Start()
    {
        isSnared = false;
        player = GameObject.FindGameObjectWithTag("Player");
        list = GameObject.FindGameObjectsWithTag("Enemy");
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        controller = GetComponent<CharacterController>();
        movementspeed = 3.0f;
    }

    // Update is called once per frame

    void Update()
    {
        if (heroEquipment.paused == false)
        {
            if (isAttacking)
            {
                atkcooldown -= Time.deltaTime;
                if (atkcooldown <= 0.0f)
                {
                    atkcooldown = atkcooldownref;
                    isAttacking = false;
                }
            }
            vectoplayer = transform.position - player.transform.position;
            dist = vectoplayer.magnitude;
            FacePlayer();
            size = list.Length;
            if (size == 1)
            {
                MoveTowardsPlayer();
                AttackPlayer();
                isReinforcing = false;
            }
            if (!isReinforcing)
            {

                if (size > 1)
                {
                    distance = new float[size];
                    for (int i = 0; i < size; i++)
                    {
                        distance[i] = (transform.position - list[i].transform.position).magnitude;
                    }

                    FacePlayer();

                    if (dist < 3.5f)
                    {
                        RunAway();
                    }
                    else
                        isReinforcing = true;
                }
                else
                {
                    MoveTowardsPlayer();
                    AttackPlayer();
                }
            }

            else
            {

                list = GameObject.FindGameObjectsWithTag("Enemy");
                if (dist < 1.75f)
                {
                    foreach (GameObject obj in list)
                    {
                        obj.SendMessage("UnReinforce", SendMessageOptions.DontRequireReceiver);
                    }
                    isReinforcing = false;
                }
                else
                {

                    foreach (GameObject obj in list)
                    {
                        obj.SendMessage("Reinforce", SendMessageOptions.DontRequireReceiver);
                    }

                }
            }
            if (isSnared)
            {
                SnareTimer -= Time.deltaTime;

                if (SnareTimer < 0)
                {
                    Unsnare();
                    isSnared = false;
                }
            }
        }

    }
    void RunAway()
    {

        controller.Move(vectoplayer.normalized * Time.deltaTime * movementspeed);
    }
    void MoveTowardsPlayer()
    {

        controller.Move(vectoplayer.normalized * Time.deltaTime * -movementspeed);
    }

    void FacePlayer()
    {
        //float tempangle=Vector3.Angle (transform.up,disttoplayer);
        //transform.Rotate (Vector3.back,tempangle+180);


        float tempangle = Mathf.Atan2(vectoplayer.y, vectoplayer.x) * Mathf.Rad2Deg;
        tempangle += 90.0f;
        Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.5f);

    }
    void AttackPlayer()
    {

        if (dist < atkrange && !isAttacking)
        {
            player.GetComponent<Health>().LoseHealth(atkdmg);
            isAttacking = true;
        }


    }

    void Slow()
    {
        movementspeed = movementspeed * 0.5f;
    }

    void Unslow()
    {
        movementspeed = movementspeed * 2;
    }
    void Snare()
    {
        isSnared = true;
        SnareTimer = 2;
        snaredSpeed = movementspeed;
        movementspeed = 0;
    }
    void Unsnare()
    {
        movementspeed = snaredSpeed;
    }
    void Decoy()
    {
        player = GameObject.FindGameObjectWithTag("Decoy");
    }

    void UnDecoy()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

}
