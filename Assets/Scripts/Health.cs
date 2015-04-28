using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public float maxHP;
    public float currentHP;
    public float healthPercent;
    GameObject healthBar;

    GameObject dungeon;
    RoomGeneration generator;
    public GameObject lifeEmberSpawn;
    GameObject player;
    PlayerEquipment equipment;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
    public void GainHealth(float Amount)
    {
        currentHP += Amount;
        if (currentHP > maxHP)
            currentHP = maxHP;

        if (this.tag == "Player")
        {
            healthPercent = currentHP / maxHP;
            healthBar.transform.localScale = new Vector3(1, healthPercent, 1);
        }
    }

    public void LoseHealth(float Amount)
    {
        currentHP -= Amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
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
            instance.GetComponent<LifeEmberStolenHealth>().gainAmount = Amount;

        }
    }

    void Die()
    {
        if (this.tag != "Player")
        {
            Destroy(gameObject);
            --generator.finalRoomInfoArray[generator.currentRoom].numEnemies;
        }
    }
}
