using UnityEngine;
using System.Collections;

public class LorneSpawners : MonoBehaviour {

    public GameObject SumFairies;
    public GameObject SumParts;
    //public float maxTime;
    //float currTime;
    //PlayerEquipment equipment;

    void Start()
    {
        SumParts.SetActive(false);
        //currTime = maxTime;
        //equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }

    void Update()
    {

    }

    void SpawnFairy()
    {
        Instantiate(SumFairies, transform.position, transform.rotation);

        GameObject tmp = (GameObject)Instantiate(SumParts, transform.position, transform.rotation);
        Destroy(tmp, 2);
    }

    //void TurnOnSummoning()
    //{
    //    SumParts.SetActive(true);
    //}
    //void TurnOffSummoning()
    //{
    //    SumParts.SetActive(false);
    //}
}
