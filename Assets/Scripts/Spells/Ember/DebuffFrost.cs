using UnityEngine;
using System.Collections;

public class DebuffFrost : MonoBehaviour {

    float timer;
    public GameObject target;

    Health targetHealth;
    ParticleSystem particles;
    Light light;

    bool once;


    void Start()
    {
        timer = 0;
        targetHealth = target.GetComponent<Health>();
        particles = gameObject.GetComponent<ParticleSystem>();
        light = gameObject.GetComponent<Light>();
        once = true;
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
            if (timer == 0)
            {
                target.SendMessage("Slow");                
            }
            timer += Time.deltaTime;

            if (timer > 3.0f)
            {
                if (once)
                {
                    target.SendMessage("Unslow");
                    once = false;
                }
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
