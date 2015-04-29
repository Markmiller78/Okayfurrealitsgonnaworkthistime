using UnityEngine;
using System.Collections;

public class ChargeBoot : MonoBehaviour {

	// Use this for initialization
    GameObject hero;
    float timer;
    ParticleSystem particles;
    PlayerEquipment heroEquipment;

    public GameObject explosion;

	void Start () {
        timer = 0;
        hero = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        particles = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (heroEquipment.paused == false)
        {


            timer += Time.deltaTime;

            transform.position = hero.transform.position;
            if (timer >= 0.5f)
            {
                particles.emissionRate = 0;
            }
            if (timer >= 0.6f)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
	}
}
