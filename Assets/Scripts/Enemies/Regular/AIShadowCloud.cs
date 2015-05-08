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
    public bool isInfected = false;
    public float infectRange;
    public float infecttimer;
    public bool isReinforced = false;


    Health heroHP;
    Light heroLight;

    void Start()
    {
        infecttimer = 3.0f;
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
            if(isInfected)
            Infect();
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player");

            }

            Vector2 moveTo = (target.transform.position - transform.position).normalized;
            moveTo = moveTo * Time.deltaTime * moveSpeed;

            transform.position = new Vector3(moveTo.x + transform.position.x, moveTo.y + transform.position.y, -1.2f); 
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
    void Reinforce()
    {
        if (!isReinforced)
        {
            DamagePerSecond *= 1.1f;
            moveSpeed *= 1.5f;
            isReinforced = true;
        }

    }

    void UnReinforce()
    {
        if (isReinforced)
        {
            DamagePerSecond/= 1.5f;
            moveSpeed /= 1.5f;
            isReinforced = false;
        }

    }

    void Decoy(GameObject decoy)
    {
        player = decoy;
      //  playMove = decoy.GetComponent<PlayerMovement>();
    }
    void UnDecoy(GameObject decoy)
    {
        player = GameObject.FindGameObjectWithTag("Player");
     //   playMove = player.GetComponent<PlayerMovement>();
    }

    void GetInfected()
    {
        isInfected = true;
    }
    void Infect()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in allObjects)
        {
            Vector3 dist = transform.position - obj.transform.position;
            if (obj.tag == "Enemy" && dist.magnitude < infectRange)
                obj.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);

        }

    }
}
