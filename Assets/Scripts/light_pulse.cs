using UnityEngine;
using System.Collections;

public class light_pulse : MonoBehaviour {

    public Light lt;
    public float lIntensity;
    public float lRange;
    public bool grow;
    float timer = 0;
	// Use this for initialization
	void Start () {
        
        lt = GetComponent<Light>();
        timer = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (lt.intensity < 3.3f)
            grow = true;

        if (lt.intensity >= 4.3f)
            grow = false;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            lt.intensity += Random.Range(-.25f, .25f);
            //timer = .1f;
        }
        if (timer <= 0)
        {
            lt.range += Random.Range(-.25f, .25f);
            timer = .05f;
        }


        if (lt.range < 13)
            lt.range = 13;

        if (lt.range > 15)
            lt.range = 15;

        if (grow)
            lt.intensity += .5f * Time.deltaTime;
        else
            lt.intensity -= .5f * Time.deltaTime;
	}
}
