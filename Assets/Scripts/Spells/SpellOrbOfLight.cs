using UnityEngine;
using System.Collections;

public class SpellOrbOfLight : MonoBehaviour
{

    public float speed;
    public float damage;
    public float range;
    public float change;
    PlayerEquipment heroEquipment;

    float distanceTraveled;

    public GameObject explosion;
    public GameObject lightRemains;
    public GameObject player;
    public PlayerEquipment playerequip;
    public Vector3 vectoplayer;
    public Vector3 playerpos;
    public Vector3 dir;



    void Start()
    {
        change = 0.5f;
        dir = transform.up;
        player = GameObject.FindGameObjectWithTag("Player");
        playerpos = player.transform.position;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        distanceTraveled = 0;

        playerequip = player.GetComponent<PlayerEquipment>();


    }

    void FixedUpdate()
    {
        if (heroEquipment.paused == false)
        {
            transform.position +=  dir* speed * Time.deltaTime;
            if (playerequip.equippedEmber == ember.Earth)
            {
                float tempangle = change * Mathf.Rad2Deg;
                tempangle += 90.0f;
                change += 0.1f;
                Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, rotation, Time.deltaTime * 42.5f);
            }
            distanceTraveled += speed * Time.deltaTime;

            if (distanceTraveled >= range)
            {
                Explode();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Explode();
        }
        else if (other.tag == "Enemy")
        {

            other.GetComponent<Health>().LoseHealth(5);


            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(lightRemains, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

