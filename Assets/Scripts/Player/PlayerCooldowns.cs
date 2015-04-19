using UnityEngine;
using System.Collections;

public class PlayerCooldowns : MonoBehaviour
{
    public float dashCooldown;
    public float spellCooldown;
    public float meleeCooldown;
    public float collectorCooldown;

    // Update is called once per frame
    void Update()
    {
        if (dashCooldown > 0.0f)
        {
            dashCooldown -= Time.deltaTime;
            if (dashCooldown <= 0.0f)
                dashCooldown = 0.0f;
        }
        if (spellCooldown > 0.0f)
        {
            spellCooldown -= Time.deltaTime;
            if (spellCooldown <= 0.0f)
                spellCooldown = 0.0f;
        }
        if (meleeCooldown > 0.0f)
        {
            meleeCooldown -= Time.deltaTime;
            if (meleeCooldown <= 0.0f)
                meleeCooldown = 0.0f;
        }
        if (collectorCooldown > 0.0f)
        {
            collectorCooldown -= Time.deltaTime;
            if (collectorCooldown <= 0.0f)
                collectorCooldown = 0.0f;
        }
    }
}
