using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AICommander : MonoBehaviour
{


    GameObject player;
    public GameObject[] list;
    public bool isReinforcing = false;
    public bool isAttacking = false;
    public bool isInfected = false;
    public bool isPathing = false;
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
    public Vector3 path;
    public int size;
    float snaredSpeed;
    float SnareTimer;
    bool isSnared;
    public float infectRange;
    public float infecttimer;
    // Use this for initialization
    void Start()
    {

        path = Vector3.zero;
        infecttimer = 3.0f;
        isSnared = false;
        player = GameObject.FindGameObjectWithTag("Player");
        list = GameObject.FindGameObjectsWithTag("Enemy");
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        controller = GetComponent<CharacterController>();
        movementspeed = 1.2f;
    }

    // Update is called once per frame

    void Update()
    {
        if (heroEquipment.paused == false)
        {
            if (isInfected)
                Infect();
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

        controller.Move(Pathfind() * Time.deltaTime * -movementspeed);
    }

    void FacePlayer()
    {
        //float tempangle=Vector3.Angle (transform.up,disttoplayer);
        //transform.Rotate (Vector3.back,tempangle+180);

        if (!isPathing)
        {
            float tempangle = Mathf.Atan2(vectoplayer.y, vectoplayer.x) * Mathf.Rad2Deg;
            tempangle += 90.0f;
            Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.5f);
        }

    }
    void AttackPlayer()
    {

        if (dist < atkrange && !isAttacking)
        {

            if (player != null)
                player.GetComponent<PlayerMovement>().SendMessage("Knockback",vectoplayer.normalized, SendMessageOptions.DontRequireReceiver);
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
    void Decoy(GameObject decoy)
    {
        player = decoy;
        // playMove = decoy.GetComponent<PlayerMovement>();
    }
    void UnDecoy(GameObject decoy)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //  playMove = player.GetComponent<PlayerMovement>();
    }
    void GetInfected()
    {
        isInfected = true;
    }
    void Infect()
    {

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in allObjects)
        {
            Vector3 dist = transform.position - obj.transform.position;
            if (obj.tag == "Enemy" && dist.magnitude < infectRange)
                obj.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);

        }

    }

    Vector3 Pathfind()
    {

        if (path == Vector3.zero)
        {
            path = -vectoplayer;
            Debug.Log("Setting!");
        }
        RaycastHit info;
        Physics.Raycast(transform.position, path.normalized, out info);
        {
            string ok ="We hit a ";
            Debug.Log(ok+=info.collider.tag);
            Debug.Log((info.collider.transform.position - transform.position).magnitude);
                if(info.collider.tag=="Wall"&&(info.collider.transform.position-transform.position).magnitude<=(GetComponent<Renderer>().bounds.size.x+0.25f))
                {
                    isPathing = true;
                    if (path == -vectoplayer)
                    {
                        Debug.Log("Direction should change!");
                        Debug.Log(path);
                        float temp = path.y;
                       // if(Random.value>0.5)
                        path.x = vectoplayer.y/Mathf.Sqrt(vectoplayer.x*vectoplayer.x+vectoplayer.y);
                        path.y = -vectoplayer.x * path.x / vectoplayer.y;

                
                    
                     
                    }
                    float tempangle = Mathf.Atan2(path.y, path.x) * Mathf.Rad2Deg;
                    tempangle += 90.0f;
                    Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3.5f);
                }
          
        }


        return -path.normalized;
    }
}
