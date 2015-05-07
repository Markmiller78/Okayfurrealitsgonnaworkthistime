using UnityEngine;
using System.Collections;

public class SkeletonProjectile : MonoBehaviour
{
    GameObject player;
    Health playerHealth;
    public float speed;
    public float damage;
    bool isActive = true;
    float timer = 0.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Decoy");
        if(player==null)
            player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && isActive)
        {
            playerHealth.LoseHealth(damage);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            isActive = false;
        }
    }

    void FixedUpdate()
    {
        if (isActive)
            transform.position += transform.up * speed * Time.deltaTime;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10.0f)
        {
            Destroy(gameObject);
        }
    }
}
