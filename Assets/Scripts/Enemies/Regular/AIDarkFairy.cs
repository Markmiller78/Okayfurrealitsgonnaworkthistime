using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIDarkFairy : MonoBehaviour
{

    GameObject player;
    public GameObject currentlight = null;
    public List<GameObject> list = new List<GameObject>();

    CharacterController controller;
    public float atkdmg;
    public float atkrange;
    public float atkcooldown;
    public float atkcooldownref;
    public float stealrange = 0.2f;
    PlayerEquipment heroEquipment;
    public float movementspeed;
    public float[] distance;
    public bool isReinforced = false;
    public bool isCasting = false;
    public bool isInfected = false;
    public float dist;
    public Vector3 vectotarget;
    public Vector3 vectoplayer;
    public Vector3 IdleVec;
    public int size;

    float snaredSpeed;
    float SnareTimer;
    bool isSnared;
    public float timer;
    public GameObject darkOrb;
    // Use this for initialization

    void Start()
    {
        isSnared = false;
        IdleVec = new Vector3(1, 1, 0);
        timer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("LightDrop");

        for (int i = 0; i < temp.Length; i++)
        {
            list.Add(temp[i]);

        }
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heroEquipment.paused == false)
        {
            {
                atkcooldown -= Time.deltaTime;
                if (atkcooldown <= 0.0f)
                {
                    atkcooldown = atkcooldownref;
                    isCasting = false;
                }
            }
            if (!isCasting)
            {
                SpellCast();
            }

            if (currentlight == null)
            {
                FaceTarget(player);


                movementspeed = 2.0f;
                if (vectoplayer.magnitude < 3.0f)
                    RunAway();
                else
                    Idle();
                if (list.Count > 0)
                {
                    float tempdist = 10000000.0f;
                    foreach (GameObject lightdrop in list)
                    {
                        if ((transform.position - lightdrop.transform.position).magnitude < tempdist)
                        {
                            tempdist = (transform.position - lightdrop.transform.position).magnitude;
                            currentlight = lightdrop;

                        }


                    }
                }
            }
            else
            {
                movementspeed = 3.0f;
                FaceTarget(currentlight);
                MoveTowardsLight(currentlight);
                StealLightDrop();
            }


            {
                movementspeed = 3.0f;
                FaceTarget(currentlight);
                MoveTowardsLight(currentlight);
                StealLightDrop();
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

    void MoveTowardsLight(GameObject lightdrop)
    {
        Vector2 tempdir = (lightdrop.transform.position - transform.position).normalized;
        controller.Move(tempdir * Time.deltaTime * movementspeed);
    }

    void RunAway()
    {
        Vector2 tempdir = (player.transform.position - transform.position);

        controller.Move(tempdir.normalized * Time.deltaTime * -movementspeed);
    }

    void StealLightDrop()
    {

        if ((transform.position - currentlight.transform.position).magnitude < stealrange)
        {
            list.Remove(currentlight);
            Destroy(currentlight);

        }



    }
    void SpellCast()
    {
        vectoplayer = transform.position - player.transform.position;

        if (!Physics.Raycast(transform.position, vectoplayer.normalized, atkrange))
        {
            if (Random.value > 0.5f)
            {
                Instantiate(darkOrb, transform.position, transform.rotation);
                isCasting = true;
            }
        }




    }

    void Idle()
    {


        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {

            IdleVec.x = Random.value;
            if (Random.value > 0.5f)
                IdleVec.x = -IdleVec.x;
            IdleVec.y = Random.value;
            if (Random.value > 0.5f)
                IdleVec.y = -IdleVec.y;
            timer = 2.0f;
        }
        controller.Move(IdleVec.normalized * Time.deltaTime * movementspeed);

    }
    void FaceTarget(GameObject target)
    {
        //float tempangle=Vector3.Angle (transform.up,disttoplayer);
        //transform.Rotate (Vector3.back,tempangle+180);
        float tempangle;
        if (target.tag == "Player")
            tempangle = Mathf.Atan2(vectoplayer.y, vectoplayer.x) * Mathf.Rad2Deg;
        else
        {
            vectotarget = transform.position - target.transform.position;
            tempangle = Mathf.Atan2(vectoplayer.y, vectoplayer.x) * Mathf.Rad2Deg;
        }
        tempangle += 90.0f;
        Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.5f);

    }
    void AddDrop(GameObject lightdrop)
    {
        list.Add(lightdrop);

    }
    void RemoveDrop(GameObject lightdrop)
    {
        list.Remove(lightdrop);

    }

    void Reinforce()
    {
        if (!isReinforced)
        {
            stealrange *= 1.2f;
            movementspeed *= 1.2f;
            isReinforced = true;
        }

    }

    void UnReinforce()
    {
        if (isReinforced)
        {
            stealrange /= 1.2f;
            movementspeed /= 1.2f;
            isReinforced = false;
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
    void GetInfected()
    {
        isInfected = true;
    }

}
