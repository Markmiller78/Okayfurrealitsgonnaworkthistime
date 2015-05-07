using UnityEngine;
using System.Collections;

public class OrbExplosion : MonoBehaviour
{
    Light theLight;
    PlayerEquipment heroEquipment;
    float timeAlive;
    public GameObject hpPickup;

    public GameObject debuff;

    GameObject player;
    public Vector3 vectoplayer;
    public Vector3 playerpos;


    float maxLife;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerpos = player.transform.position;
        heroEquipment = player.GetComponent<PlayerEquipment>();
        timeAlive = 0;
        theLight = gameObject.GetComponent<Light>();
        Camera.main.SendMessage("ScreenShake");
        if (heroEquipment.equippedEmber == ember.Ice)
        {
            maxLife = 1.1f;
        }
        else
        {
            maxLife = 0.6f;
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
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        if (other.tag == "Enemy")
        {


            Instantiate(hpPickup, other.transform.position, other.transform.rotation);

            vectoplayer = playerpos - other.transform.position;

          
              
                    if (heroEquipment.equippedEmber == ember.None)
                    {
                        other.GetComponent<Health>().LoseHealth(5);
                    }
                    else if (heroEquipment.equippedEmber == ember.Fire)
                    {
                        other.GetComponent<Health>().LoseHealth(5);
                        GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                        tempObj.GetComponent<DebuffFire>().target = other.gameObject;
                    }
                    else if (heroEquipment.equippedEmber == ember.Ice)
                    {
                        other.GetComponent<Health>().LoseHealth(5);
                        GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                        tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
                    }
                    else if (heroEquipment.equippedEmber == ember.Wind)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        other.GetComponent<Health>().LoseHealth(5);
                    }
                    else if (heroEquipment.equippedEmber == ember.Wind)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        other.GetComponent<Health>().LoseHealth(5);
                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        other.GetComponent<Health>().LoseHealth(5);
                    }
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        other.GetComponent<Health>().LoseHealth(5);
                    }
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        other.GetComponent<Health>().LoseHealth(5);
                    }
                }
            }
        }
    



      




         




































 