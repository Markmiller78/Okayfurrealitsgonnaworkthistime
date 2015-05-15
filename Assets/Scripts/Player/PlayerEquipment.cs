using UnityEngine;
using System.Collections;

public enum boot { None = 0, Trailblazer, Whirlwind, Charge, Decoy, Blink };
//  1           2        3       4      5

public enum accessory { None = 0, OrbOfLight, BoltOfLight, BlastOfLight, ChainLightning, Singularity, Snare, LightMine};
//                                   1             2            3               4             5         6        7

public enum ember { None = 0, Life, Death, Earth, Wind, Fire, Ice};
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

    AudioSource audioPlayer;

    public AudioClip equipmentSound;

    public int emberDurability;

    public bool paused;

    void Start()
    {
        audioPlayer = gameObject.GetComponent<AudioSource>();
        emberDurability = 0;
        paused = false;
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
        Debug.Log("sounddd");
        audioPlayer.PlayOneShot(equipmentSound);
    }


}
