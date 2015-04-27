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
        if (this.tag == "Player")
            healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        healthPercent = currentHP / maxHP * 100;

        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
		if(dungeon!=null)
        generator = dungeon.GetComponent<RoomGeneration>();
        player = GameObject.FindGameObjectWithTag("Player");
        equipment = player.GetComponent<PlayerEquipment>();

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
            healthBar.transform.localScale = new Vector3(1, healthPercent, 1);
        }
        else if (equipment.equippedEmber == ember.Life)
        {
            GameObject instance = lifeEmberSpawn;
            lifeEmberSpawn.GetComponent<LifeEmberStolenHealth>().gainAmount = Amount;
            Instantiate(instance, transform.position, transform.rotation);
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
