using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public float maxHP;
    public float currentHP;
    public float healthPercent;
    GameObject healthBar;
    void Start()
    {
        maxHP = 100;
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        healthPercent = currentHP / maxHP * 100;
        
        //LoseHealth(20); //Used for testing
    }
    void GainHealth(float Amount)
    {
        currentHP += Amount;
        if (currentHP > maxHP)
            currentHP = maxHP;

        if(this.tag == "Player")
        {
            healthPercent = currentHP / maxHP;
            healthBar.transform.localScale = new Vector3(1, healthPercent, 1);
        }
    }

    void LoseHealth(float Amount)
    {
        currentHP -= Amount;
        if (currentHP <= 0)
            Die();

        if (this.tag == "Player")
        {
            healthPercent = currentHP / maxHP;
            healthBar.transform.localScale = new Vector3(1, healthPercent, 1);
        }
    }

    void Die()
    {

    }
}
