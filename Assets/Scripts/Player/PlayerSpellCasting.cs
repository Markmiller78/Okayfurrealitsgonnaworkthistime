using UnityEngine;
using System.Collections;

public class PlayerSpellCasting : MonoBehaviour
{

    public GameObject lightOrb;
    public GameObject fireOrb;
    public GameObject frostOrb;
    public GameObject windOrb;

    public GameObject lightSing;
    public GameObject fireSing;
    public GameObject frostSing;
    public GameObject windSing;

    public GameObject lightBlast;
    public GameObject fireBlast;
    public GameObject frostBlast;
    public GameObject windBlast;

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
        if (!heroCooldowns.spellCooling && heroLight.currentLight >= heroLight.minLight)
        {
            Camera.main.SendMessage("ScreenShake");
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
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    Instantiate(windOrb, transform.position, transform.rotation);

                }
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
                    Instantiate(frostOrb, transform.position, transform.rotation);

                }
            }
            else if (heroEquipment.equippedAccessory == accessory.Singularity)
            {
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(lightSing, transform.position, transform.rotation);
                }
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                    Instantiate(fireSing, transform.position, transform.rotation);
                }
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    Instantiate(windSing, transform.position, transform.rotation);

                }
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
                    Instantiate(frostSing, transform.position, transform.rotation);

                }
            }
            else if (heroEquipment.equippedAccessory == accessory.BlastOfLight)
            {
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(lightBlast, transform.position, transform.rotation);
                }
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                    Instantiate(fireBlast, transform.position, transform.rotation);
                }
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    Instantiate(windBlast, transform.position, transform.rotation);

                }
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
                    Instantiate(frostBlast, transform.position, transform.rotation);

                }
            }
        }

    }

}
