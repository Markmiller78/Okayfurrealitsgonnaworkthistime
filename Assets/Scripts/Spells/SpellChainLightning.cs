using UnityEngine;
using System.Collections;

public class SpellChainLightning : MonoBehaviour {


    public float speed;
    public float damage;

    public GameObject debuff;
    public GameObject explosion;
    public GameObject bolt;

    PlayerEquipment heroEquipment;

    bool once;

    void Start()
    {
        transform.Rotate(270, 0, 0);
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        once = true;
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
            if (once)
            {
                Instantiate(bolt, other.transform.position, other.transform.rotation);
                once = false;
            }
            if (heroEquipment.equippedEmber == ember.None)
            {
                other.GetComponent<Health>().LoseHealth(damage);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                other.GetComponent<Health>().LoseHealth(damage);
                GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFire>().target = other.gameObject;
            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                other.GetComponent<Health>().LoseHealth(damage);
                GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
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
