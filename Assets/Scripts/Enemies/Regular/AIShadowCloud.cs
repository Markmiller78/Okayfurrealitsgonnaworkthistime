using UnityEngine;
using System.Collections;

public class AIShadowCloud : MonoBehaviour
{
    PlayerEquipment heroEquipment;

    public GameObject target;
    public GameObject player;
    public float moveSpeed;
    public Texture HazardCookie;
    public float DamagePerSecond;

    public bool isReinforced = false;
    Health heroHP;
    Light heroLight;

    float snaredSpeed;
    float SnareTimer;
    bool isSnared;
    void Start()
    {
        isReinforced = false;
        isSnared = false;
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        target = player;
        heroHP = target.GetComponent<Health>();
        heroLight = target.GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heroEquipment.paused == false)
        {
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player");

            }

            Vector2 moveTo = (target.transform.position - transform.position).normalized;
            moveTo = moveTo * Time.deltaTime * moveSpeed;

            transform.position = new Vector3(moveTo.x + transform.position.x, moveTo.y + transform.position.y, -1.2f);

            if (isSnared)
            {
                SnareTimer -= Time.deltaTime;

                if (SnareTimer < 0)
                {
                    Unsnare();
                    isSnared = false;
                }
            }
        }

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

    public void Slow()
    {
        moveSpeed = moveSpeed * 0.5f;
    }

    void Unslow()
    {
        moveSpeed = moveSpeed * 2;
    }

    void Snare()
    {
        isSnared = true;
        SnareTimer = 2;
        snaredSpeed = moveSpeed;
        moveSpeed = 0;
    }
    void Unsnare()
    {
        moveSpeed = snaredSpeed;
    }
    void Reinforce()
    {
        if (!isReinforced)
        {
            DamagePerSecond *= 1.2f;
            moveSpeed *= 1.5f;
            isReinforced = true;
        }

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
