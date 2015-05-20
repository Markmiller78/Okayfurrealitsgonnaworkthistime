using UnityEngine;
using System.Collections;

public class FairyOrbExplosion : MonoBehaviour 
{

    Light theLight;
    PlayerEquipment heroEquipment;
    float timeAlive;
    GameObject player;
    float maxLife;
    public float explodedamage;

    // Use this for initialization
    void Start()
    {
        maxLife = 0.6f;
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = player.GetComponent<PlayerEquipment>();
        theLight = gameObject.GetComponent<Light>();
        Camera.main.SendMessage("ScreenShake");
    }

    // Update is called once per frame
    void Update()
    {
        if (heroEquipment.paused == false)
        {
            timeAlive += Time.deltaTime;
            theLight.range -= Time.deltaTime * 4;
            if (timeAlive >= maxLife)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().LoseHealth(explodedamage);
            player.SendMessage("KnockBack", SendMessageOptions.DontRequireReceiver);
        }
    }
}
