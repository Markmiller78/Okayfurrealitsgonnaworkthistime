using UnityEngine;
using System.Collections;

public enum equipmentType { Boot, Accessory, Ember };

public class Pickup : MonoBehaviour
{
	public bool displaytooltips = false;
    public equipmentType typeOfEquipment;     // See above for enum.
    public int whichOneToEquip = 1;     // See PlayerEquipment script for these enums.

    GameObject player;
    PlayerEquipment equipment;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        equipment = player.GetComponent<PlayerEquipment>();
    }

	void OnGUI()
	{
		if (displaytooltips)
		{
			
			GUI.Box (new Rect (0, 0, 140, 20), "Press E to pick up");
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
}
