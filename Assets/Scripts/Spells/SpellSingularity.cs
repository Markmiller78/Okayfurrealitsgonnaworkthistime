using UnityEngine;
using System.Collections;

public class SpellSingularity : MonoBehaviour
{

    PlayerEquipment heroEquipment;

    float lifeTimer;

    // Update is called once per frame

    Light theLight;
    public GameObject theExplosion;
    //GameObject player;

    void Start()
    {
        lifeTimer = 0;
        theLight = gameObject.GetComponent<Light>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        //player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (heroEquipment.paused == false)
        {
            //transform.position = player.transform.position;
            transform.Rotate(new Vector3(0, 0, 150f * Time.deltaTime));

            lifeTimer += Time.deltaTime;
            if (lifeTimer >= 1.5)
            {
                theLight.range -= Time.deltaTime * 2;
            }

            if (lifeTimer >= 2.5)
            {
                Instantiate(theExplosion, transform.position, new Quaternion(0, 0, 0, 0));
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyid itsID = other.GetComponent<EnemyID>().theID;

            if (itsID == enemyid.shadowCloud)
            {
                other.GetComponent<AIShadowCloud>().target = gameObject;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyid itsID = other.GetComponent<EnemyID>().theID;

            if (itsID != enemyid.shadowCloud)
            {
                Vector2 moveTo = (transform.position - other.transform.position).normalized;
                CharacterController theController = other.GetComponent<CharacterController>();

                theController.Move(moveTo * Time.deltaTime * 2);
            }
        }
    }
}
