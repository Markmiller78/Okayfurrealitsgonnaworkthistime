﻿using UnityEngine;
using System.Collections;

public class SpellOrbOfLight : MonoBehaviour {

    public float speed;
    public float damage;
    public float range;
    PlayerEquipment heroEquipment;

    float distanceTraveled;

    public GameObject explosion;
    public GameObject lightRemains;
    public GameObject player;
    public Vector3 vectoplayer;
    public Vector3 playerpos;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerpos = player.transform.position;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        distanceTraveled = 0;

    

    }

	void FixedUpdate ()
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
        else if (other.tag == "Enemy")
        {
            vectoplayer = playerpos - other.transform.position;

            if (!Physics.Raycast(playerpos, vectoplayer.normalized, range))
            {
             //   other.GetComponent<Health>().LoseHealth(5);
            }
    
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

