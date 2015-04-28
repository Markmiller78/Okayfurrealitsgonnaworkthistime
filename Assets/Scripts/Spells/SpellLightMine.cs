using UnityEngine;
using System.Collections;

public class SpellLightMine : MonoBehaviour {

    public GameObject explosion;

    bool active;
    float timer;
    void Start()
    {
        active = false;
        timer = 0;
    }

    void Update()
    {
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
