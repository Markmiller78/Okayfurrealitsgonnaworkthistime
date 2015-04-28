using UnityEngine;
using System.Collections;

public class SpellLightMine : MonoBehaviour {

    public GameObject explosion;

    bool active;
    float timer;
    float betterTimer;
    void Start()
    {
        active = false;
        timer = 0;
        betterTimer = 0;
    }

    void Update()
    {
        betterTimer += Time.deltaTime;
        if (betterTimer >= 6.5f)
        {
            active = true;
        }
        if (active)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                Explode();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            active = true;
        }
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
