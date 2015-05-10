﻿using UnityEngine;
using System.Collections;

public class PlayerSpellCasting : MonoBehaviour
{
    [Header("Orb of Light")]
    public GameObject lightOrb;
    public GameObject fireOrb;
    public GameObject frostOrb;
    public GameObject windOrb;
    public GameObject lifeOrb;
    public GameObject deathOrb;
    public GameObject earthOrb;

    [Header("Singularity")]
    public GameObject lightSing;
    public GameObject fireSing;
    public GameObject frostSing;
    public GameObject windSing;
    public GameObject lifeSing;
    public GameObject deathSing;
    public GameObject earthSing;

    [Header("Blast of Light")]
    public GameObject lightBlast;
    public GameObject fireBlast;
    public GameObject frostBlast;
    public GameObject windBlast;
    public GameObject lifeBlast;
    public GameObject deathBlast;
    public GameObject earthBlast;

    [Header("Bolt of Light")]
    public GameObject lightBolt;
    public GameObject fireBolt;
    public GameObject frostBolt;
    public GameObject windBolt;
    public GameObject lifeBolt;
    public GameObject deathBolt;
    public GameObject earthBolt;

    [Header("Light Mine")]
    public GameObject lightMine;
    public GameObject fireMine;
    public GameObject frostMine;
    public GameObject windMine;
    public GameObject lifeMine;
    public GameObject deathMine;
    public GameObject earthMine;

    [Header("Chain Lightning")]
    public GameObject lightChain;
    public GameObject fireChain;
    public GameObject frostChain;
    public GameObject windChain;
    public GameObject lifeChain;
    public GameObject deathChain;
    public GameObject earthChain;

    [Header("Ensnare")]
    public GameObject lightSnare;
    public GameObject fireSnare;
    public GameObject frostSnare;
    public GameObject windSnare;
    public GameObject lifeSnare;
    public GameObject deathSnare;
    public GameObject earthSnare;


    PlayerEquipment heroEquipment;
    PlayerCooldowns heroCooldowns;
    PlayerLight heroLight;
    Animator anim;
    public HUDCooldowns UICD;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
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
                anim.CrossFade("Spellcasting", 0.01f);
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
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        Instantiate(deathOrb, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        Instantiate(earthOrb, transform.position, transform.rotation);

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
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        Instantiate(deathSing, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        Instantiate(earthSing, transform.position, transform.rotation);

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
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        Instantiate(deathBlast, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        Instantiate(earthBlast, transform.position, transform.rotation);

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
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        Instantiate(deathBolt, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        Instantiate(earthBolt, transform.position, transform.rotation);

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
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        Instantiate(deathMine, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        Instantiate(earthMine, transform.position, transform.rotation);

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
                        Instantiate(fireSnare, transform.position, transform.rotation);
                    }
                    else if (heroEquipment.equippedEmber == ember.Wind)
                    {
                        Instantiate(windSnare, transform.position, transform.rotation);
                    }
                    else if (heroEquipment.equippedEmber == ember.Ice)
                    {
                        Instantiate(frostSnare, transform.position, transform.rotation);
                    }
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        Instantiate(lifeSnare, transform.position, transform.rotation);
                    }
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        Instantiate(deathSnare, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        Instantiate(earthSnare, transform.position, transform.rotation);

                    }
                }
                else if (heroEquipment.equippedAccessory == accessory.ChainLightning)
                {
                    if (heroEquipment.equippedEmber == ember.None)
                    {
                        Instantiate(lightChain, transform.position, transform.rotation);
                    }
                    else if (heroEquipment.equippedEmber == ember.Fire)
                    {
                        Instantiate(fireChain, transform.position, transform.rotation);
                    }
                    else if (heroEquipment.equippedEmber == ember.Wind)
                    {
                        Instantiate(windChain, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Ice)
                    {
                        Instantiate(frostChain, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        Instantiate(lifeChain, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        Instantiate(deathChain, transform.position, transform.rotation);

                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        Instantiate(earthChain, transform.position, transform.rotation);

                    }
                }
            }
        }

    }

}
