using UnityEngine;
using System.Collections;

public class LifeEmberStolenHealth : MonoBehaviour
{
    GameObject player;
    Health pHealth;
    float waitTimer = .5f;
    public float gainAmount;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pHealth = player.GetComponent<Health>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 5f);
        //Vector3 vectorToPlayer = player.transform.position - transform.position;
        //float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        //angle -= 270.0f;
        //Quaternion rot = Quaternion.AngleAxis(-angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            pHealth.GainHealth(gainAmount);
            Destroy(gameObject);
        }
    }
}
