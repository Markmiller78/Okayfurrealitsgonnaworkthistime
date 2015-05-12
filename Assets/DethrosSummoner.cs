using UnityEngine;
using System.Collections;

public class DethrosSummoner : MonoBehaviour
{
    public GameObject[] enemies;
    //public float maxTime;
    //float currTime;
    //PlayerEquipment equipment;

    void Start()
    {
        //currTime = maxTime;
        //equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }

    void Update()
    {

    }

    void SpawnThings()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length - 1)], transform.position, transform.rotation);
    }
}
