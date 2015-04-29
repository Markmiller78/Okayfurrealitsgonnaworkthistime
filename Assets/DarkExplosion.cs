using UnityEngine;
using System.Collections;

public class DarkExplosion : MonoBehaviour {

	// Use this for initialization
    Light theLight; 
    float timeAlive;
    float maxLife;
    // Use this for initialization
    void Start()
    {
        timeAlive = 0;
        theLight = gameObject.GetComponent<Light>();  
            maxLife = 0.6f; 
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        theLight.range -= Time.deltaTime * 4;
        if (timeAlive >= maxLife)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {          
                other.GetComponent<Health>().LoseHealth(5);
        }
    }
}
