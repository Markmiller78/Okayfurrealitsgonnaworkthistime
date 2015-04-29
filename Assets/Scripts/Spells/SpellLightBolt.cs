using UnityEngine;
using System.Collections;

public class SpellLightBolt : MonoBehaviour {

    public float speed;
    public float damage;

    public GameObject explosion;
    public GameObject debuff;

    public GameObject poke;

    PlayerEquipment heroEquipment;

    void Start()
    {
        transform.Rotate(270, 0, 0);
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }

    void FixedUpdate()
    {
        if (heroEquipment.paused == false)
        {


            transform.position += transform.forward * speed * Time.deltaTime;
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
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
