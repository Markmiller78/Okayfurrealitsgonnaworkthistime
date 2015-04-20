using UnityEngine;
using System.Collections;

public class light_pulse : MonoBehaviour {

    public Light lt;
    public float lIntensity;
    public float lRange;
    public bool grow;
	// Use this for initialization
	void Start () {
        
        lt = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (lt.intensity < 2)
            grow = true;

        if (lt.intensity >= 4)
            grow = false;

        if (grow)
            lt.intensity += Time.deltaTime;
        else
            lt.intensity -= Time.deltaTime;
	}
}
