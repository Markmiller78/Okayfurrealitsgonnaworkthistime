using UnityEngine;
using System.Collections;

public class PlayerSpellCasting : MonoBehaviour {

    public GameObject orbOfLight;

    PlayerEquipment heroEquipment;
    PlayerCooldowns heroCooldowns;


    void Start()
    {
        heroEquipment = gameObject.GetComponent<PlayerEquipment>();
        heroCooldowns = gameObject.GetComponent<PlayerCooldowns>();

    }

    void CastSpell()
    {
        //If the spell cast is not on cooldown
        if (!heroCooldowns.spellCooling)
        {
            heroCooldowns.spellCooling = true;
            //If the orb of light is equipped, fire the orb of light ability
            if (heroEquipment.equippedAccessory == accessory.OrbOfLight)
            {
                Instantiate(orbOfLight, transform.position, transform.rotation);
            }
        }

    }

}
