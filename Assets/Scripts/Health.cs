using UnityEngine;
using System.Collections;
public class Health : MonoBehaviour
{

    public float maxHP;
    public float currentHP;
    public float healthPercent;
    public bool isInfected = false;
    GameObject healthBar;

    GameObject dungeon;
    RoomGeneration generator;
    public GameObject lifeEmberSpawn;
    GameObject player;
   public  GameObject explosion;
   public  GameObject lightRemains;
    PlayerEquipment equipment;
    public GameObject LoseText;
    bool playerDead;
    Animator anim;
    Health playerHealth;

    void Start()
    {
        playerDead = false;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        playerHealth = player.GetComponent<Health>();
        equipment = player.GetComponent<PlayerEquipment>();
        if (this.tag == "Player")
        {
            healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        }
        healthPercent = currentHP / maxHP * 100;

        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        if (dungeon != null)
        {
            generator = dungeon.GetComponent<RoomGeneration>();
        }

    }

    void Update()
    {


    }
    public void GainHealth(float Amount)
    {
        if (equipment.paused == false)
        {
            currentHP += Amount;
            if (currentHP > maxHP)
                currentHP = maxHP;

            if (this.tag == "Player")
            {
                healthPercent = currentHP / maxHP;
                healthBar.transform.localScale = new Vector3(healthPercent, 1, 1);
            }
        }
    }

    public void LoseHealth(float Amount)
    {
        if (equipment.paused == false)
        {
            currentHP -= Amount;
            if (currentHP <= 0)
            {
                currentHP = 0;
                playerDead = true;
                Die();
            }

            if (this.tag == "Player")
            {
                healthPercent = currentHP / maxHP;
                healthBar.transform.localScale = new Vector3(healthPercent, 1, 1);
            }
            else if (equipment.equippedEmber == ember.Life)
            {
                GameObject instance = (GameObject)Instantiate(lifeEmberSpawn, transform.position, transform.rotation);
                playerHealth.GainHealth(1);

            }
        }
    }

    void Die()
    {
        if (this.tag != "Player")
        {
            Instantiate(lightRemains, transform.position, transform.rotation);
            gameObject.GetComponent<GenerateLoot>().Generateloot();
            if (isInfected)
            {
                Explode();
            }


            Destroy(this.gameObject);
            --generator.finalRoomInfoArray[generator.currentRoom].numEnemies;
        }
        else
        {
            if (playerDead)
            {
                anim.CrossFade("Dying", 0.01f);
                equipment.paused = true;
                playerDead = true;
                Instantiate(LoseText);
            }
        }
       
      

    }
    void GetInfected()
    {
        isInfected = true;
    }
    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);  
    }
}
