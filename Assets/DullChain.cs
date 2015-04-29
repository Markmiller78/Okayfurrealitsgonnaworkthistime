using UnityEngine;
using System.Collections;

public class DullChain : MonoBehaviour {

    public float speed;
    public float damage;

    public GameObject debuff;

    PlayerEquipment heroEquipment;

    public GameObject target;


    void Start()
    {
        transform.Rotate(270, 0, 0);
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }

    void FixedUpdate()
    {
        if (heroEquipment.paused == false)
        {
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player");
            }
                Vector2 moveTo = (target.transform.position - transform.position).normalized;
                moveTo = moveTo * Time.deltaTime * speed;

                transform.position = new Vector3(moveTo.x + transform.position.x, moveTo.y + transform.position.y, -1.2f);
            }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Explode();
        }
        else if (other.tag == "Player")
        {
            Explode();
        }
        else if (other.tag == "Enemy")
        {
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
        Destroy(gameObject);
    }
}
