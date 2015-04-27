using UnityEngine;
using System.Collections;

public class ConsumeLight : MonoBehaviour {

    GameObject shadow;
    ParticleSystem theParts;
    Vector2 waypoint;
    float distanceToShadow;
    bool doOnce;

    public GameObject theExplosion;
	// Use this for initialization
	void Start () 
    {
        shadow = GameObject.FindGameObjectWithTag("ShadowSpawn");
        theParts = gameObject.GetComponent<ParticleSystem>();
        distanceToShadow = 1;
        doOnce = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (shadow != null)
        {
            distanceToShadow = Vector3.Distance(transform.position, shadow.transform.position);
            waypoint = shadow.transform.position - gameObject.transform.position;
            waypoint.Normalize();
            waypoint *= 4 * Time.deltaTime;
            transform.position = new Vector3(waypoint.x + transform.position.x, waypoint.y + transform.position.y, -1);
        }
        else
            Destroy(gameObject);
        if(distanceToShadow < .1f && doOnce)
        {
            doOnce = false;
            Instantiate(theExplosion,shadow.transform.position, shadow.transform.rotation);
            theParts.emissionRate = 0;
            Destroy(gameObject, 2);
        }


	}
}
