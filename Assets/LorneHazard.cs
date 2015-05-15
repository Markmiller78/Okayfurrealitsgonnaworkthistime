using UnityEngine;
using System.Collections;

public class LorneHazard : MonoBehaviour
{

    public float DamagePerSecond;
    public Texture HazardCookie;

    GameObject hero;
    Health heroHP;
    Light heroLight;
    PlayerMovement heroMovement;
    PlayerDashing heroDash;
    GameObject tocheck;
    AudioSource audioPlayer;
    public float countertodestroythefriggingbug = 0;
    GameObject[] floors;

    void Start()
    {
        tocheck = this.gameObject;
        hero = GameObject.FindGameObjectWithTag("Player");
        heroHP = hero.GetComponent<Health>();
        heroLight = hero.GetComponentInChildren<Light>();
        heroMovement = hero.GetComponent<PlayerMovement>();
        heroDash = hero.GetComponent<PlayerDashing>();
        audioPlayer = gameObject.GetComponent<AudioSource>();

       floors = GameObject.FindGameObjectsWithTag("Floor");
        if (gameObject.tag == "Temporary")
            Destroy(gameObject, 5);
        if (gameObject.tag == "Temporary2")
        {
            Destroy(gameObject, 10f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hero)
        {
            heroMovement.isinHazard = true;
            // Slow the player
            heroMovement.halfSpeed = 0.8f;
            heroMovement.fullSpeed = 1.6f;
            //heroDash.dashSpeed = 2;
            //Dont slow the player anymore
            heroLight.cookie = HazardCookie;

            //Play the hazard sound attached to the player
            ++countertodestroythefriggingbug;
            audioPlayer.Play();


            // other.GetComponentInChildren<AudioSource>().enabled = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == hero && heroMovement.isinHazard == true)
        {
            if (countertodestroythefriggingbug != 0)
                heroHP.LoseHealth(DamagePerSecond * Time.deltaTime / countertodestroythefriggingbug);
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject == hero)
        {
            --countertodestroythefriggingbug;
            if (countertodestroythefriggingbug == 0)
            {
                heroMovement.isinHazard = false;
                // Return the players speeds to normal
                heroMovement.halfSpeed = 1.6f;
                heroMovement.fullSpeed = 3.1f;
                //heroDash.dashSpeed = 4;
                heroLight.cookie = null;

                //Stop playing audio
                audioPlayer.Stop();
            }


        }
    }


}
