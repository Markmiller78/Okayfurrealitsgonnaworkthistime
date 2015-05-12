using UnityEngine;
using System.Collections;

public enum equipmentType { Boot, Accessory, Ember };

public class Pickup : MonoBehaviour
{
    public bool displaytooltips = false;
    public equipmentType typeOfEquipment;     // See above for enum.
    public int whichOneToEquip = 1;     // See PlayerEquipment script for these enums.
    public GameObject orbOfLightPickup;
    public GameObject boltOfLightPickup;
    public GameObject blastOfLightPickup;
    public GameObject chainLightningPickup;
    public GameObject singularityPickup;
    public GameObject snarePickup;
    public GameObject lightMinePickup;
    public GameObject TrailblazerBootPickup;
    public GameObject whirlwindBootPickup;
    public GameObject chargeBootPickup;
    public GameObject decoyBootPickup;
    public GameObject blinkBootPickup;
    public Font font;
    public string theName;
    Camera cameras;

    GameObject player;
    PlayerEquipment equipment;

    void Start()
    {
        cameras = GameObject.FindObjectOfType<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        equipment = player.GetComponent<PlayerEquipment>();
    }

    void OnGUI()
    {
        if (displaytooltips)
        {
            string temp = "Press E to pick up the ";
            if (theName.Length != 0)
            {
                temp += theName;
            }
            if(theName.Contains("Ember"))
            temp += "\n Durabilty: 10\n";

            GUI.Box(new Rect(cameras.WorldToScreenPoint(player.transform.position).x+32,Screen.height- cameras.WorldToScreenPoint(player.transform.position).y, 250, 150), temp);
        }

    }
    void DisplayTooltip()
    {
        displaytooltips = true;

    }

    void DoNotDisplayTooltip()
    {
        displaytooltips = false;
    }


    void OnTriggerStay(Collider other)
    {
        if ((InputManager.controller && Input.GetButtonDown("CInteract") || (!InputManager.controller && Input.GetButtonDown("KBInteract")))
            && other.gameObject == player)
        {
            switch (typeOfEquipment)
            {
                case equipmentType.Boot:
                    #region Boots
                    switch (equipment.equippedBoot)
                    {
                        case boot.None:
                            break;
                        case boot.Trailblazer:
                            Instantiate(TrailblazerBootPickup, transform.position, transform.rotation);
                            break;
                        case boot.Whirlwind:
                            Instantiate(whirlwindBootPickup, transform.position, transform.rotation);
                            break;
                        case boot.Charge:
                            Instantiate(chargeBootPickup, transform.position, transform.rotation);
                            break;
                        case boot.Decoy:
                            Instantiate(decoyBootPickup, transform.position, transform.rotation);
                            break;
                        case boot.Blink:
                            Instantiate(blinkBootPickup, transform.position, transform.rotation);
                            break;
                        default:
                            break;
                    }
                    switch (whichOneToEquip)
                    {
                        case 1:
                            equipment.equippedBoot = boot.Trailblazer;
                            break;
                        case 2:
                            equipment.equippedBoot = boot.Whirlwind;
                            break;
                        case 3:
                            equipment.equippedBoot = boot.Charge;
                            break;
                        case 4:
                            equipment.equippedBoot = boot.Decoy;
                            break;
                        case 5:
                            equipment.equippedBoot = boot.Blink;
                            break;
                        default:
                            break;
                    }
                    break;
                    #endregion
                case equipmentType.Accessory:
                    #region Accessories
                    switch (equipment.equippedAccessory)
                    {
                        case accessory.None:
                            break;
                        case accessory.OrbOfLight:
                            Instantiate(orbOfLightPickup, transform.position, transform.rotation);
                            break;
                        case accessory.BoltOfLight:
                            Instantiate(boltOfLightPickup, transform.position, transform.rotation);
                            break;
                        case accessory.BlastOfLight:
                            Instantiate(blastOfLightPickup, transform.position, transform.rotation);
                            break;
                        case accessory.ChainLightning:
                            Instantiate(chainLightningPickup, transform.position, transform.rotation);
                            break;
                        case accessory.Singularity:
                            Instantiate(singularityPickup, transform.position, transform.rotation);
                            break;
                        case accessory.Snare:
                            Instantiate(snarePickup, transform.position, transform.rotation);
                            break;
                        case accessory.LightMine:
                            Instantiate(lightMinePickup, transform.position, transform.rotation);
                            break;
                        default:
                            break;
                    }
                    switch (whichOneToEquip)
                    {
                        case 1:
                            equipment.equippedAccessory = accessory.OrbOfLight;
                            break;
                        case 2:
                            equipment.equippedAccessory = accessory.BoltOfLight;
                            break;
                        case 3:
                            equipment.equippedAccessory = accessory.BlastOfLight;
                            break;
                        case 4:
                            equipment.equippedAccessory = accessory.ChainLightning;
                            break;
                        case 5:
                            equipment.equippedAccessory = accessory.Singularity;
                            break;
                        case 6:
                            equipment.equippedAccessory = accessory.Snare;
                            break;
                        case 7:
                            equipment.equippedAccessory = accessory.LightMine;
                            break;
                        default:
                            break;
                    }
                    break;
                    #endregion
                case equipmentType.Ember:
                    #region Embers
                    switch (whichOneToEquip)
                    {
                        case 1:
                            equipment.equippedEmber = ember.Life;
                            break;
                        case 2:
                            equipment.equippedEmber = ember.Death;
                            break;
                        case 3:
                            equipment.equippedEmber = ember.Earth;
                            break;
                        case 4:
                            equipment.equippedEmber = ember.Wind;
                            break;
                        case 5:
                            equipment.equippedEmber = ember.Fire;
                            break;
                        case 6:
                            equipment.equippedEmber = ember.Ice;
                            break;
                        default:
                            break;
                    }
                    break;
                    #endregion
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }

    void SetName(string aName)
    {
        print("CHANGENAME");
        theName = aName;
    }
}
