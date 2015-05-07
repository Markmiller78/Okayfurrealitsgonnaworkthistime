using UnityEngine;
using System.Collections;
//using UnityEditor;

public class ExpPlacer : MonoBehaviour
{
    float timer;

    public GameObject boltExp;
    // Use this for initialization
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 25 * Time.deltaTime;
        timer += Time.deltaTime;
    }

    void Detonate()
    {
        GameObject temp = (GameObject)Instantiate(boltExp, transform.position, transform.rotation);
        temp.GetComponent<ParticleSystem>().emissionRate = timer * 4000;

       // SerializedObject ohYeah = new SerializedObject(temp.GetComponent<ParticleSystem>());
        //ohYeah.FindProperty("ShapeModule.boxZ").floatValue = timer * 50f;
        //ohYeah.ApplyModifiedProperties();

        Destroy(gameObject);
    }
}
