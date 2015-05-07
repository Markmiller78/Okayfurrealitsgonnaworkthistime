using UnityEngine;
using System.Collections;

public class SpellLightBolt : MonoBehaviour {

    public float damage;

    public GameObject expPlacer;
    public GameObject debuff;
    public GameObject hpPickup;

    public GameObject poke;

    PlayerEquipment heroEquipment;
    ParticleSystem particles;

    GameObject kaboom;
    GameObject player;

    float timer;

    void Start()
    {
        transform.Rotate(270, 0, 0);
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = player.GetComponent<PlayerEquipment>();
        particles = gameObject.GetComponent<ParticleSystem>();
        kaboom = (GameObject)Instantiate(expPlacer, transform.position, transform.rotation);
        timer = 0;
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
            Instantiate(hpPickup, other.transform.position, other.transform.rotation);
            if (heroEquipment.equippedEmber == ember.None)
            {
                other.GetComponent<Health>().LoseHealth(damage);
                Instantiate(poke, other.transform.position, other.transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                other.GetComponent<Health>().LoseHealth(damage);
                Instantiate(poke, other.transform.position, other.transform.rotation);
                GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFire>().target = other.gameObject;
            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                other.GetComponent<Health>().LoseHealth(damage);
                Instantiate(poke, other.transform.position, other.transform.rotation);
                GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                other.GetComponent<Health>().LoseHealth(damage);
            }
            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                Camera.main.SendMessage("ScreenShake");
                other.GetComponent<Health>().LoseHealth(damage);
            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                other.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
                other.GetComponent<Health>().LoseHealth(damage);
            }
        }
    }

    void Explode()
    {
        kaboom.SendMessage("Detonate", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
