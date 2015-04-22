using UnityEngine;
using System.Collections;

public class TrailProjectile : MonoBehaviour
{

    public float MaxTimeActive;
    float timeAlive;

    ParticleSystem particles;
    Light particleLight;

    public GameObject TrailLightRemains;

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
        if (timeAlive >= (MaxTimeActive - 0.7))
        {
            particles.emissionRate = 0;
            particleLight.range -= Time.deltaTime;
        }

        if (timeAlive > MaxTimeActive)
        {
            //Spawn the trail light remains and destory the gameobject
            Instantiate(TrailLightRemains, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //If the trail is still in its damage phase
        if (timeAlive < (MaxTimeActive - 0.7))
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<Health>().LoseHealth(3);
                timeAlive = (MaxTimeActive - 0.7f);
            }
        }
    }
}
