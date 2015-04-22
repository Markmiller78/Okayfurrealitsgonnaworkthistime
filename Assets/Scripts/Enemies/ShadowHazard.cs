using UnityEngine;
using System.Collections;

public class ShadowHazard : MonoBehaviour {

    public float DamagePerSecond;
    public Texture HazardCookie;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Slow the player
            other.GetComponent<PlayerDashing>().dashSpeed = 2;
            other.GetComponent<PlayerMovement>().halfSpeed = 0.8f;
            other.GetComponent<PlayerMovement>().fullSpeed = 1.6f;
            other.GetComponentInChildren<Light>().cookie = HazardCookie;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().LoseHealth(DamagePerSecond * Time.deltaTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // Return the players speeds to normal
            other.GetComponent<PlayerDashing>().dashSpeed = 4;
            other.GetComponent<PlayerMovement>().halfSpeed = 1.6f;
            other.GetComponent<PlayerMovement>().fullSpeed = 3.1f;
            other.GetComponentInChildren<Light>().cookie = null;

        }
    }

}
