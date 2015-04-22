using UnityEngine;
using System.Collections;

public class TrailProjectile : MonoBehaviour
{

    public float MaxTimeActive;
    float timeAlive;

    ParticleSystem particles;
    Light particleLight;

    void Start()
    {
        //Orient the thing to look correct
        transform.Rotate(new Vector3(270, 0, 0));
        particles = gameObject.GetComponent<ParticleSystem>();
        particleLight = gameObject.GetComponent<Light>();
    }
    void Update()
    {
        timeAlive += Time.deltaTime;

        //Turn off the particles early to make deletion look more smooth
        if (timeAlive > (MaxTimeActive - 0.7))
        {
            particles.emissionRate = 0;
            particleLight.range -= Time.deltaTime;
        }

        if (timeAlive > MaxTimeActive)
        {
            Destroy(gameObject);
        }
    }
}
