using UnityEngine;
using System.Collections;

public class PlayerSpellCasting : MonoBehaviour {

    public GameObject lightOrb;
    public GameObject fireOrb;

    PlayerEquipment heroEquipment;
    PlayerCooldowns heroCooldowns;
    PlayerLight heroLight;

    void Start()
    {
        heroEquipment = gameObject.GetComponent<PlayerEquipment>();
        heroCooldowns = gameObject.GetComponent<PlayerCooldowns>();
        heroLight = gameObject.GetComponent<PlayerLight>();
    }

    void CastSpell()
    {
        //If the spell cast is not on cooldown
        if (!heroCooldowns.spellCooling)
        {
            heroLight.LoseLight(5);
            heroCooldowns.spellCooling = true;
            //If the orb of light is equipped, fire the orb of light ability
            if (heroEquipment.equippedAccessory == accessory.OrbOfLight)
            {
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(lightOrb, transform.position, transform.rotation);                    
                }
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                    Instantiate(fireOrb, transform.position, transform.rotation);                                        
                }
            }
        }

    }

}
