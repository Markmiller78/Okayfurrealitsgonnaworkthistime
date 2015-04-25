using UnityEngine;
using System.Collections;

public class SpellLightBolt : MonoBehaviour {

    public float speed;
    public float damage;
    public float range;

    ParticleSystem particles;

    float timer;
    void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.3f)
        {
            particles.Pause();
        }
    }

    void OnTriggerEnter()
    {

    }
}
