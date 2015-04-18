using UnityEngine;
using System.Collections;

public enum boot { Trailblazer = 0, Whirlwind, Charge, Decoy, Blink };

public enum accessory { OrbOfLight, BoltOfLight, BlastOfLight, ChainLightning, Singularity, Snare, LightMine};

public enum ember { Life, Death, Fire, Earth, Wind, Ice};

public class PlayerEquipment : MonoBehaviour
{
    public boot equippedBoot;
    //public int equippedBoots;
    public accessory equippedAccessory;
    //public int equippedAccessory;
    public ember equippedEmber;
    //public int equippedEmber;
}
