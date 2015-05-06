using UnityEngine;
using System.Collections;

public class PingAction : MonoBehaviour
{
    public GameObject pingMissle;
    SphereCollider collis;
    GameObject player;

    PlayerEquipment heroEquipment;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        collis = gameObject.GetComponent<SphereCollider>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        Destroy(gameObject, 0.6f);

    }

    void Update()
    {
        if (heroEquipment.paused == false)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -3);
            collis.radius -= 80 * Time.deltaTime;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "LightDrop")
        {
            other.SendMessage("PickUp", SendMessageOptions.DontRequireReceiver);
            Instantiate(pingMissle, other.transform.position, other.transform.rotation);
        }
    }
}
