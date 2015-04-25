using UnityEngine;
using System.Collections;

public class SpellSingularity : MonoBehaviour {


    float lifeTimer;
	
	// Update is called once per frame

    Light theLight;
    public GameObject theExplosion;
    GameObject player;

    void Start()
    {
        lifeTimer = 0;
        theLight = gameObject.GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	void Update ()
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

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Vector2 moveTo = (transform.position - other.transform.position).normalized;
            other.GetComponent<CharacterController>().Move(moveTo * Time.deltaTime * 2);
        }
    }
}
