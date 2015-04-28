using UnityEngine;
using System.Collections;

public class BoltExp : MonoBehaviour
{
    Light theLight;
    public GameObject lightRemains;
    // Use this for initialization
    void Start()
    {
        theLight = gameObject.GetComponent<Light>();
        Instantiate(lightRemains, transform.position, new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        theLight.range -= Time.deltaTime * 8;
    }
}
