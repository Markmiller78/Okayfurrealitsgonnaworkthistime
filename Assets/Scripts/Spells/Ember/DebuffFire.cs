using UnityEngine;
using System.Collections;

public class DebuffFire : MonoBehaviour
{

    public float dps;
    float timer;
    public GameObject target;

    Health targetHealth;
    ParticleSystem particles;
    Light light;
    void Start()
    {
        timer = 0;
        targetHealth = target.GetComponent<Health>();
        particles = gameObject.GetComponent<ParticleSystem>();
        light = gameObject.GetComponent<Light>();
    }

    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = target.transform.position + new Vector3(0, 0, -0.5f);
            timer += Time.deltaTime;



            if (timer < 3.0f)
            {
                targetHealth.LoseHealth(dps * Time.deltaTime);
            }
            else
            {
                particles.emissionRate = 0;
                light.range -= Time.deltaTime;
            }

            if (timer > 4.0f)
            {
                Destroy(gameObject);
            }
        }
    }

}
