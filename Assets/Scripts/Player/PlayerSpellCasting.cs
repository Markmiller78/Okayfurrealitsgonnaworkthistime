using UnityEngine;
using System.Collections;

public class PlayerSpellCasting : MonoBehaviour
{

    public GameObject lightOrb;
    public GameObject fireOrb;
    public GameObject frostOrb;
    public GameObject windOrb;
    public GameObject lifeOrb;

    public GameObject lightSing;
    public GameObject fireSing;
    public GameObject frostSing;
    public GameObject windSing;
    public GameObject lifeSing;

    public GameObject lightBlast;
    public GameObject fireBlast;
    public GameObject frostBlast;
    public GameObject windBlast;
    public GameObject lifeBlast;

    public GameObject lightBolt;
    public GameObject fireBolt;
    public GameObject frostBolt;
    public GameObject windBolt;
    public GameObject lifeBolt;

    public GameObject lightMine;
    public GameObject fireMine;
    public GameObject frostMine;
    public GameObject windMine;
    public GameObject lifeMine;

    public GameObject lightSnare;
    public GameObject fireSnare;
    public GameObject frostSnare;
    public GameObject windSnare;
    public GameObject lifeSnare;


    PlayerEquipment heroEquipment;
    PlayerCooldowns heroCooldowns;
    PlayerLight heroLight;
    public HUDCooldowns UICD;

    void Start()
    {
        heroEquipment = gameObject.GetComponent<PlayerEquipment>();
        heroCooldowns = gameObject.GetComponent<PlayerCooldowns>();
        heroLight = gameObject.GetComponent<PlayerLight>();
        UICD = GameObject.Find("Spell").GetComponent<HUDCooldowns>();
    }

    void CastSpell()
    {
        if (heroEquipment.paused == false)
        {
            //If the spell cast is not on cooldown
            if (!heroCooldowns.spellCooling && heroLight.currentLight >= heroLight.minLight)
            {
                Camera.main.SendMessage("ScreenShake");
                heroLight.LoseLight(5);
                heroCooldowns.spellCooling = true;
                UICD.CooldownTrigger(CooldownID.Spell);
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
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        Instantiate(lifeOrb, transform.position, transform.rotation);

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
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        Instantiate(lifeSing, transform.position, transform.rotation);
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
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        Instantiate(lifeBlast, transform.position, transform.rotation);
                    }
                }
                else if (heroEquipment.equippedAccessory == accessory.BoltOfLight)
                {
                    if (heroEquipment.equippedEmber == ember.None)
                    {
                        Instantiate(lightBolt, transform.position, transform.rotation);
                    }
                    else if (heroEquipment.equippedEmber == ember.Fire)
                    {
                        Instantiate(fireBolt, transform.position, transform.rotation);
                    }
                    else if (heroEquipment.equippedEmber == ember.Wind)
                    {
                        Instantiate(windBolt, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Ice)
                    {
                        Instantiate(frostBolt, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        Instantiate(lifeBolt, transform.position, transform.rotation);

                    }
                }
                else if (heroEquipment.equippedAccessory == accessory.LightMine)
                {
                    if (heroEquipment.equippedEmber == ember.None)
                    {
                        Instantiate(lightMine, transform.position, transform.rotation);
                    }
                    else if (heroEquipment.equippedEmber == ember.Fire)
                    {
                        Instantiate(fireMine, transform.position, transform.rotation);
                    }
                    else if (heroEquipment.equippedEmber == ember.Wind)
                    {
                        Instantiate(windMine, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Ice)
                    {
                        Instantiate(frostMine, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        Instantiate(lifeMine, transform.position, transform.rotation);

                    }
                } 
            }
            else if (heroEquipment.equippedAccessory == accessory.Snare)
            {
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(lightSnare, transform.position, transform.rotation);
                }
                else if (heroEquipment.equippedEmber == ember.Fire)
                {

                }
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    Instantiate(lightSnare, transform.position, transform.rotation);
                }
                else if (heroEquipment.equippedEmber == ember.Ice)
                {

                }
                else if (heroEquipment.equippedEmber == ember.Life)
                {

                }



            }
        }

    }

}
