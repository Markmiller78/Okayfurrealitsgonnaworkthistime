using UnityEngine;
using System.Collections;

public class SpellOrbOfLight : MonoBehaviour {

    public float speed;
    public float damage;
    public float range;

    float distanceTraveled;

    void Start()
    {
        distanceTraveled = 0;
    }

	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        distanceTraveled += speed * Time.deltaTime;

        if (distanceTraveled >= range)
        {
            Explode();
        }
	}

    void OnTriggerEnter(Collider other)
    {

    }

    void Explode()
    {
        Destroy(gameObject);
    }
}
