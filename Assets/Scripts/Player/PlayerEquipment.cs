using UnityEngine;
using System.Collections;

public enum boot { None = 0, Trailblazer, Whirlwind, Charge, Decoy, Blink };
//  1           2        3       4      5

public enum accessory { None = 0, OrbOfLight, BoltOfLight, BlastOfLight, ChainLightning, Singularity, Snare, LightMine };
//                                   1             2            3               4             5         6        7

public enum ember { None = 0, Life, Death, Earth, Wind, Fire, Ice };
//                              1     2      3      4     5    6

public class PlayerEquipment : MonoBehaviour
{
    [Header("Boot Details")]
    public boot equippedBoot;
    public string BootName;
    public ItemStat BootStat1;
    public ItemStat BootStat2;

    [Header("Acessory Details")]
    public accessory equippedAccessory;
    public string AccessoryName;
    public ItemStat AccessoryStat1;
    public ItemStat AccessoryStat2;

    [Header("Ember Details")]
    public ember equippedEmber;
    public string EmberName;
    public ItemStat EmberStat1;
    public ItemStat EmberStat2;

    int TotalSP, TotalAD, MaxHP, MaxLight;


    AudioSource audioPlayer;

    public AudioClip equipmentSound;

    public int emberDurability;

    public bool paused;

    void Start()
    {
        TotalSP = TotalAD = MaxHP = MaxLight = 0;
        audioPlayer = gameObject.GetComponent<AudioSource>();
        emberDurability = 0;
        paused = false;

        BootName = "Starter Charging Boot";
        AccessoryName = "Starter Orb of Light";

    }

    public void EmberLoseDurability()
    {
        emberDurability--;
        if (emberDurability <= -1)
        {
            equippedEmber = ember.None;
        }
    }

    void PlayEquipmentSound()
    {
       // Debug.Log("sounddd");
        audioPlayer.PlayOneShot(equipmentSound);
    }


    public void CalculateStats()
    {
        TotalSP = TotalAD = MaxHP = MaxLight = 0;


        switch (AccessoryStat1.TheStat)
        {
            case StatType.SpellMod:
                {
                    TotalSP += AccessoryStat1.StatAmount;
                    break;
                }
            case StatType.MeleeMod:
                {
                    TotalAD += AccessoryStat1.StatAmount;
                    break;
                }
            case StatType.MaxHP:
                {
                    MaxHP += AccessoryStat1.StatAmount;
                    break;
                }
            case StatType.MaxLight:
                {
                    MaxLight += AccessoryStat1.StatAmount;
                    break;
                }
        }

        switch (AccessoryStat2.TheStat)
        {
            case StatType.SpellMod:
                {
                    TotalSP += AccessoryStat2.StatAmount;
                    break;
                }
            case StatType.MeleeMod:
                {
                    TotalAD += AccessoryStat2.StatAmount;
                    break;
                }
            case StatType.MaxHP:
                {
                    MaxHP += AccessoryStat2.StatAmount;
                    break;
                }
            case StatType.MaxLight:
                {
                    MaxLight += AccessoryStat2.StatAmount;
                    break;
                }
        }

        switch (BootStat1.TheStat)
        {
            case StatType.SpellMod:
                {
                    TotalSP += BootStat1.StatAmount;
                    break;
                }
            case StatType.MeleeMod:
                {
                    TotalAD += BootStat1.StatAmount;
                    break;
                }
            case StatType.MaxHP:
                {
                    MaxHP += BootStat1.StatAmount;
                    break;
                }
            case StatType.MaxLight:
                {
                    MaxLight += BootStat1.StatAmount;
                    break;
                }
        }

        switch (BootStat2.TheStat)
        {
            case StatType.SpellMod:
                {
                    TotalSP += BootStat2.StatAmount;
                    break;
                }
            case StatType.MeleeMod:
                {
                    TotalAD += BootStat2.StatAmount;
                    break;
                }
            case StatType.MaxHP:
                {
                    MaxHP += BootStat2.StatAmount;
                    break;
                }
            case StatType.MaxLight:
                {
                    MaxLight += BootStat2.StatAmount;
                    break;
                }
        }

        PlayerStats PlayStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();


        PlayStats.SendMessage("GetMeleeMod" ,TotalAD, SendMessageOptions.DontRequireReceiver);
        PlayStats.SendMessage("GetSpellMod" ,TotalSP, SendMessageOptions.DontRequireReceiver);
        PlayStats.SendMessage("GetMaxHPMod" ,MaxHP, SendMessageOptions.DontRequireReceiver);
        PlayStats.SendMessage("GetMaxLightMod" ,MaxLight, SendMessageOptions.DontRequireReceiver);
        //print("CalculatingStats");

    }

}
