using UnityEngine;
using System.Collections;

public class TrailLightRemains : MonoBehaviour {

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
                //The dash currently spawns 11 of these as of 4/22/15
                heroLight.GainLight(1);
                active = false;
            }
        }

    }
}
