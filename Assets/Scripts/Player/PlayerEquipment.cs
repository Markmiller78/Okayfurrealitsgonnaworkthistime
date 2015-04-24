using UnityEngine;
using System.Collections;

public enum boot { None = 0, Trailblazer, Whirlwind, Charge, Decoy, Blink };

public enum accessory { None = 0, OrbOfLight, BoltOfLight, BlastOfLight, ChainLightning, Singularity, Snare, LightMine};

public enum ember { None = 0, Life, Death, Earth, Wind, Fire, Ice};

public class PlayerEquipment : MonoBehaviour
{
    public boot equippedBoot;
    public accessory equippedAccessory;
    public ember equippedEmber;
}