using UnityEngine;
using System.Collections;

public class LifeEmberStolenHealth : MonoBehaviour
{
    GameObject player;
    Health pHealth;
    public float gainAmount;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pHealth = player.GetComponent<Health>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 5f);
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
