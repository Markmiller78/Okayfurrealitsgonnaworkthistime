using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public float maxHP;
    public float currentHP;
    void Start()
    {
        maxHP = 100;
    }
    void GainHealth(float Amount)
    {
        currentHP += Amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    void LoseHealth(float Amount)
    {
        currentHP -= Amount;
        if (currentHP <= 0)
            Die();
    }

    void Die()
    {

    }
}
