using UnityEngine;
using System.Collections;

public enum boot { None = 0, Trailblazer, Whirlwind, Charge, Decoy, Blink };
//                                1           2        3       4      5
public enum accessory { None = 0, OrbOfLight, BoltOfLight, BlastOfLight, ChainLightning, Singularity, Snare, LightMine};
//                                   1             2            3               4             5         6        7
public enum ember { None = 0, Life, Death, Earth, Wind, Fire, Ice};
//                              1     2      3      4     5    6

public class PlayerEquipment : MonoBehaviour
{
    public boot equippedBoot;
    public accessory equippedAccessory;
    public ember equippedEmber;

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
