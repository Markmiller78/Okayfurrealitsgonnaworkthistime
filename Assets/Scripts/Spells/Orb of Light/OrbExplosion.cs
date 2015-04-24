using UnityEngine;
using System.Collections;

public class OrbExplosion : MonoBehaviour
{
    Light theLight;
    float timeAlive;
    // Use this for initialization
    void Start()
    {
        timeAlive = 0;
        theLight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        theLight.range -= Time.deltaTime * 4;
        if (timeAlive >= 1.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Health>().LoseHealth(5);            
        }
    }
}
