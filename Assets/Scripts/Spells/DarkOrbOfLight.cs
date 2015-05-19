using UnityEngine;
using System.Collections;

public class DarkOrbOfLight : MonoBehaviour {


    public float speed;
    public float damage;
    public float range;

    float distanceTraveled;

    public GameObject explosion;
    public GameObject hazard;
    public GameObject eqp;
    PlayerEquipment heroEquipment;

    void Start()
    {
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        distanceTraveled = 0;
        range = Random.Range(5, 8);
    }

    void FixedUpdate()
    {
        if (heroEquipment.paused == false)
        {
            transform.position += transform.up * speed * Time.deltaTime;
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
        else if (other.tag == "Player")
        {
            other.GetComponent<Health>().LoseHealth(damage);
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(hazard, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
