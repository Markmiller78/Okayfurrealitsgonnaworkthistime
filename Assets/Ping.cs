using UnityEngine;
using System.Collections;

public class Ping : MonoBehaviour {

    public GameObject ping;
    public GameObject ping2;
    PlayerCooldowns heroCooldowns;

    void Start()
    {
        heroCooldowns = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCooldowns>();

    }

    void CollectLight()
    {
        if (!heroCooldowns.collectorCooling)
        {
            Instantiate(ping, transform.position, transform.rotation);
            Instantiate(ping2, transform.position, transform.rotation);
            heroCooldowns.collectorCooling = true;
        }
    }
}
