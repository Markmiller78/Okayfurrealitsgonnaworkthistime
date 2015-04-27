using UnityEngine;
using System.Collections;

public class AIShadowCloud : MonoBehaviour
{

    public GameObject target;
    public GameObject player;
    public float moveSpeed;
    public Texture HazardCookie;
    public float DamagePerSecond;


    Health heroHP;
    Light heroLight;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;
        heroHP = target.GetComponent<Health>();
        heroLight = target.GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");

        }

        Vector2 moveTo = (target.transform.position - transform.position).normalized;
        moveTo = moveTo * Time.deltaTime * moveSpeed;

        transform.position = new Vector3(moveTo.x + transform.position.x, moveTo.y + transform.position.y, -1.2f);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            heroLight.cookie = HazardCookie;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            heroHP.LoseHealth(DamagePerSecond * Time.deltaTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            heroLight.cookie = null;
        }
    }

    void Slow()
    {
        moveSpeed = moveSpeed * 0.5f;
    }

    void Unslow()
    {
        moveSpeed = moveSpeed * 2;
    }
	void Reinforce()
	{
		DamagePerSecond *= 1.2f;
		moveSpeed *= 1.5f;
		
	}
	void UnReinforce()
	{
		DamagePerSecond/= 1.2f;
	moveSpeed /= 1.5f;
		
	}

	void Decoy()
	{
		player = GameObject.FindGameObjectWithTag ("Decoy");
	}
	
	void UnDecoy()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

}
