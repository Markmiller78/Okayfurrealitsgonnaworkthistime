using UnityEngine;
using System.Collections;

public class LightRemains : MonoBehaviour {

    PlayerLight heroLight;
    bool active;

    void Start()
    {
        heroLight = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLight>();
        active = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (other.tag == "Player")
            {
                heroLight.GainLight(5);
                active = false;
            }
        }

    }
}
