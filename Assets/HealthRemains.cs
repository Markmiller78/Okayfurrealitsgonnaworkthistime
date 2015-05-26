using UnityEngine;
using System.Collections;

public class HealthRemains : MonoBehaviour
{

    public GameObject missile;
    SpriteRenderer sprite;
    Light particleLight;
    ParticleSystem particles;

    bool active;

    Health heroHp;
    float deathTimer;

    GameObject player;


    // Use this for initialization
    void Start()
    {
        heroHp = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
        particleLight = gameObject.GetComponent<Light>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        active = true;
        deathTimer = 0;

        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            deathTimer += Time.deltaTime;

            particles.emissionRate = 0;
            sprite.enabled = false;
            particleLight.range -= Time.deltaTime;

            if (deathTimer > 0.7)
            {


                Destroy(gameObject);
            }


        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 1f)
            {
                PickUp();
            }
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (active)
    //    {
    //        if (other.tag == "Player")
    //        {
    //            PickUp();
    //        }
    //    }

    //}

    void PickUp()
    {
        Instantiate(missile, transform.position, transform.rotation);
        heroHp.GainHealth(1.2f);
        active = false;
    }
}
