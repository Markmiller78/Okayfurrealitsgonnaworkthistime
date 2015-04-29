using UnityEngine;
using System.Collections;

public class SpellSnare : MonoBehaviour {

    public float speed;
    public float damage;
    public float range;
    public float growthRate;
    float distanceTraveled;

    public GameObject explosion;
    public GameObject lightRemains;

    void Start()
    {
        distanceTraveled = 0;
    }

    void FixedUpdate()
    {

        transform.localScale += new Vector3(growthRate * Time.deltaTime, growthRate * Time.deltaTime, growthRate * Time.deltaTime);
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
    }

    void Explode()
    {

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < Enemies.Length; i++ )
        {
            if (Vector3.Distance(transform.position, Enemies[i].transform.position) < 4)
            {
                //Enemies[i].SendMessage("Slow");
                Enemies[i].GetComponent<Health>().LoseHealth(5);
            }
        }

        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(lightRemains, transform.position, transform.rotation);
        Destroy(gameObject);
        
    }
}


