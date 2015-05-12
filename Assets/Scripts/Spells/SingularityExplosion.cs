﻿using UnityEngine;
using System.Collections;

public class SingularityExplosion : MonoBehaviour
{

    Light theLight;
    PlayerEquipment heroEquipment;
    float timeAlive;

    public GameObject debuff;
    GameObject player;
    float maxLife;
    public GameObject lightRemains;
    public GameObject hpPickup;

    public Vector3 vectoplayer;
    public Vector3 playerpos;

    bool once;
    // Use this for initialization
    void Start()
    {
        timeAlive = 0;
        theLight = gameObject.GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerpos = player.transform.position;
        heroEquipment = player.GetComponent<PlayerEquipment>();

        if (heroEquipment.equippedEmber == ember.Ice)
        {
            maxLife = 1.1f;
        }
        else
        {
            maxLife = 0.6f;
        }
        once = true;

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Vector3.Distance(transform.position, Enemies[i].transform.position) < 2.5f)
            {
                if (Enemies[i].tag == "Enemy")
                {

                    if (once)
                    {
                        Instantiate(hpPickup, Enemies[i].transform.position, Enemies[i].transform.rotation);
                        once = false;
                    }


                    if (heroEquipment.equippedEmber == ember.None)
                    {

                        Enemies[i].GetComponent<Health>().LoseHealth(10);
                    }
                    else if (heroEquipment.equippedEmber == ember.Fire)
                    {
                        Enemies[i].GetComponent<Health>().LoseHealth(10);
                        GameObject tempObj = (GameObject)Instantiate(debuff, Enemies[i].transform.position, Enemies[i].transform.rotation);
                        tempObj.GetComponent<DebuffFire>().target = Enemies[i].gameObject;
                    }
                    else if (heroEquipment.equippedEmber == ember.Ice)
                    {
                        Enemies[i].GetComponent<Health>().LoseHealth(10);
                        GameObject tempObj = (GameObject)Instantiate(debuff, Enemies[i].transform.position, Enemies[i].transform.rotation);
                        tempObj.GetComponent<DebuffFrost>().target = Enemies[i].gameObject;
                    }
                    else if (heroEquipment.equippedEmber == ember.Wind)
                    {
                        Enemies[i].SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        Enemies[i].GetComponent<Health>().LoseHealth(10);
                    }
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        Enemies[i].SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        Enemies[i].GetComponent<Health>().LoseHealth(10);
                    }
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        Enemies[i].SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        Enemies[i].GetComponent<Health>().LoseHealth(10);
                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        Enemies[i].SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        Enemies[i].GetComponent<Health>().LoseHealth(13);
                    }


                }
            }
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
                Instantiate(lightRemains, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        if (once)
    //        {
    //            Instantiate(hpPickup, other.transform.position, other.transform.rotation);
    //            once = false;
    //        }


    //        if (heroEquipment.equippedEmber == ember.None)
    //        {

    //            other.GetComponent<Health>().LoseHealth(10);
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Fire)
    //        {
    //            other.GetComponent<Health>().LoseHealth(10);
    //            GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
    //            tempObj.GetComponent<DebuffFire>().target = other.gameObject;
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Ice)
    //        {
    //            other.GetComponent<Health>().LoseHealth(10);
    //            GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
    //            tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Wind)
    //        {
    //            other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
    //            other.GetComponent<Health>().LoseHealth(10);
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Life)
    //        {
    //            other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
    //            other.GetComponent<Health>().LoseHealth(10);
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Death)
    //        {
    //            other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
    //            other.GetComponent<Health>().LoseHealth(10);
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Earth)
    //        {
    //            other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
    //            other.GetComponent<Health>().LoseHealth(13);
    //        }

    //    }
    //}
}
