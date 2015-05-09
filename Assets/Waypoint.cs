using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Archer"))
        {
            other.GetComponent<AISkeletonArcher>().usedWaypoints.pushBack(this.gameObject);
        }
    }
}
