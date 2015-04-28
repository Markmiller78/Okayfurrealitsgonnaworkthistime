using UnityEngine;
using System.Collections;

public class DarkOrbOfLight : MonoBehaviour {


    public float speed;
    public float damage;
    public float range;

    float distanceTraveled;

    public GameObject explosion;
    public GameObject hazard;


    void Start()
    {
        distanceTraveled = 0;
    }

    void FixedUpdate()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        distanceTraveled += speed * Time.deltaTime;

        if (distanceTraveled >= range)
        {
            Explode();
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
            other.GetComponent<Health>().LoseHealth(12);
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
