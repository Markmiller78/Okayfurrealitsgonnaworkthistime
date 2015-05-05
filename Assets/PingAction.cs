using UnityEngine;
using System.Collections;
using UnityEditor;

public class PingAction : MonoBehaviour
{
    public GameObject pingMissle;
    SphereCollider collis;
   // SerializedObject particles;
    GameObject player;

    PlayerEquipment heroEquipment;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        collis = gameObject.GetComponent<SphereCollider>();
        //particles = new SerializedObject(GetComponent<ParticleSystem>());
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        Destroy(gameObject, 0.6f);

    }

    void Update()
    {
        if (heroEquipment.paused == false)
        {
           // transform.Rotate(new Vector3(0, 0, 50f * Time.deltaTime));

            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -3);
            collis.radius -= 80 * Time.deltaTime;

            //particles.FindProperty("ShapeModule.radius").floatValue -= 80 * Time.deltaTime;
            //particles.ApplyModifiedProperties();
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
