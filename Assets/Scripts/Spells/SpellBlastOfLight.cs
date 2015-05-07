using UnityEngine;
using System.Collections;

public class SpellBlastOfLight : MonoBehaviour
{


    Light theLight;
    PlayerEquipment heroEquipment;
    float timeAlive;
    
    public GameObject debuff;
    public GameObject remains;
    public GameObject player;
    public Vector3 vectoplayer;
    public Vector3 playerpos;

    float maxLife;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerpos = player.transform.position;
        timeAlive = 0;
        theLight = gameObject.GetComponent<Light>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();

        if (heroEquipment.equippedEmber == ember.Ice)
        {
            maxLife = 1.5f;
        }
        else
        {
            maxLife = 1.0f;
        }

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
                Instantiate(remains, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
                vectoplayer =  playerpos- other.transform.position;

                if (!Physics.Raycast(playerpos, vectoplayer.normalized, 100))
                {
                    if (heroEquipment.equippedEmber == ember.None)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);

                        other.GetComponent<Health>().LoseHealth(10);
                    }
                    else if (heroEquipment.equippedEmber == ember.Fire)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);

                        other.GetComponent<Health>().LoseHealth(10);
                        GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                        tempObj.GetComponent<DebuffFire>().target = other.gameObject;
                    }
                    else if (heroEquipment.equippedEmber == ember.Ice)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);

                        other.GetComponent<Health>().LoseHealth(10);
                        GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                        tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
                    }
                    else if (heroEquipment.equippedEmber == ember.Wind)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        other.GetComponent<Health>().LoseHealth(10);

                    }
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        other.GetComponent<Health>().LoseHealth(10);
                    }
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        other.GetComponent<Health>().LoseHealth(10);
                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        other.GetComponent<Health>().LoseHealth(10);
                    }
                }
        }
    }
}
