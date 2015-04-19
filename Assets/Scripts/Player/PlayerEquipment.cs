using UnityEngine;
using System.Collections;

public enum boot { Trailblazer = 0, Whirlwind, Charge, Decoy, Blink };

public enum accessory { OrbOfLight = 0, BoltOfLight, BlastOfLight, ChainLightning, Singularity, Snare, LightMine};

public enum ember { Life = 0, Death, Earth, Wind, Fire, Ice};

public class PlayerEquipment : MonoBehaviour
{
    public boot equippedBoot;
    //public int equippedBoots;
    public accessory equippedAccessory;
    //public int equippedAccessory;
    public ember equippedEmber;
    //public int equippedEmber;
}
