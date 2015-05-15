using UnityEngine;
using System.Collections;

public class SpellLightBolt : MonoBehaviour {

    public float damage;

    public GameObject expPlacer;
    public GameObject debuff;
    public GameObject hpPickup;

    public GameObject poke;
    public GameObject burns;
    PlayerStats theStats;
    PlayerEquipment heroEquipment;
    //ParticleSystem particles;

    GameObject kaboom;
    GameObject player;

    float timer;
    bool once;
    void Start()
    {
        transform.Rotate(270, 0, 0);
        player = GameObject.FindGameObjectWithTag("Player");
        theStats = player.GetComponent<PlayerStats>();
        heroEquipment = player.GetComponent<PlayerEquipment>();
        //particles = gameObject.GetComponent<ParticleSystem>();
        kaboom = (GameObject)Instantiate(expPlacer, transform.position, transform.rotation);
        timer = 0;
        once = true;
    }

    void FixedUpdate()
    {
        if (heroEquipment.paused == false)
        {
            transform.localScale = transform.localScale + new Vector3(0, 0, Time.deltaTime * 20.0f);
            transform.position = player.transform.position;
            timer += Time.deltaTime;
            if (timer >= 0.15f)
            {
                Explode();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Explode();
        }
        else if (other.tag == "Enemy")
        {
            if (once)
            {
                Instantiate(hpPickup, other.transform.position, other.transform.rotation);
                once = false;
            }
            Instantiate(burns, new Vector3(other.transform.position.x, other.transform.position.y, -0.5f), new Quaternion(0, 0, 0, 0));
            if (heroEquipment.equippedEmber == ember.None)
            {
                other.GetComponent<Health>().LoseHealth(damage+theStats.spellModifier);
                Instantiate(poke, other.transform.position, other.transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
                Instantiate(poke, other.transform.position, other.transform.rotation);
                GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFire>().target = other.gameObject;
            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
                Instantiate(poke, other.transform.position, other.transform.rotation);
                GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
            }
            else if (heroEquipment.equippedEmber == ember.Life)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
            }
            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                Camera.main.SendMessage("ScreenShake");
                other.GetComponent<Health>().LoseHealth(damage + 3 + theStats.spellModifier);
            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                other.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
            }
        }
    }

    void Explode()
    {
        kaboom.SendMessage("Detonate", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
