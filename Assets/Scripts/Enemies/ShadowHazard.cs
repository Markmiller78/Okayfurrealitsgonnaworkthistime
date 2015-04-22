using UnityEngine;
using System.Collections;

public class ShadowHazard : MonoBehaviour {

    public float DamagePerSecond;
    public Texture HazardCookie;

    GameObject hero;
    Health heroHP;
    Light heroLight;
    PlayerMovement heroMovement;
    PlayerDashing heroDash;

    AudioSource audio;

    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        heroHP = hero.GetComponent<Health>();
        heroLight = hero.GetComponentInChildren<Light>();
        heroMovement = hero.GetComponent<PlayerMovement>();
        heroDash = hero.GetComponent<PlayerDashing>();
        audio = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Slow the player
            heroMovement.halfSpeed = 0.8f;
            heroMovement.fullSpeed = 1.6f;
            heroDash.dashSpeed = 2;
            heroLight.cookie = HazardCookie;

            //Play the hazard sound attached to the player

            audio.Play();


           // other.GetComponentInChildren<AudioSource>().enabled = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            heroHP.LoseHealth(DamagePerSecond * Time.deltaTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // Return the players speeds to normal
            heroMovement.halfSpeed = 1.6f;
            heroMovement.fullSpeed = 3.1f;
            heroDash.dashSpeed = 4;
            heroLight.cookie = null;

            //Stop playing audio
            audio.Stop();


        }
    }

}
