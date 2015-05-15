using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum equipmentType { Boot, Accessory, Ember };

public enum StatType { None = 0, MeleeMod, SpellMod, MaxHP, MaxLight };

public struct ItemStat
{
    public StatType TheStat;
    public int StatAmount;
};
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
    float sendTimer;
    public GameObject TooltipWindow;
    RawImage ToolTipBack;
    public Vector3 ToolPOS;
    SetToolTipTexts ToolTipTexts;
    Camera cameras;
    public GameObject Temp;

    GameObject player;
    PlayerEquipment equipment;

    int phase;
    float timer;
    void Start()
    {
        sendTimer = 1;
        cameras = GameObject.FindObjectOfType<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        Temp = Instantiate(TooltipWindow);
       // TooltipWindow = GameObject.FindGameObjectWithTag("ToolTipCanvas");
        
        equipment = player.GetComponent<PlayerEquipment>();
        phase = 0;
    }

    void Update()
    {
        sendTimer -= Time.deltaTime;
        if (phase == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 0.1f, transform.position.z);
            timer += Time.deltaTime;
            if (timer >= 0.8f)
            {
                timer = 0;
                phase = 1;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * 0.1f, transform.position.z);
            timer += Time.deltaTime;
            if (timer >= 0.8f)
            {
                timer = 0;
                phase = 0;
            }
        }

        //if (TooltipWindow != null)
        //{
        //    ToolTipBack.transform.localPosition = transform.position;
        //    print("Change");
        //}
        if (sendTimer < 0 && ToolTipBack != null && displaytooltips == true)
        {
            //ToolTipBack.SendMessage("ToolSetTexts", ToolTipTexts, SendMessageOptions.DontRequireReceiver);
            sendTimer = .5f;
        }
        if (ToolTipBack != null && displaytooltips == true)
        {
            ToolPOS = Camera.main.WorldToScreenPoint(transform.position);
            float offsetY = Screen.height - 310;
            ToolTipBack.transform.localPosition = new Vector3(ToolPOS.x - 550, ToolPOS.y - offsetY, -5);
        }

        
    }

    void OnGUI()
    {

        #region //if (theName.Length != 0)
        //{
        //string temp = "Press E to pick up the \n";
            //    temp += theName;
            //}
            //if (theName.Contains("Ember"))
            //    temp += "\n Durabilty: 10\n";

            //GUI.Box(new Rect(cameras.WorldToScreenPoint(player.transform.position).x + 32, Screen.height - cameras.WorldToScreenPoint(player.transform.position).y, 250, 150), temp);
            #endregion

    }
    void DisplayTooltip()
    {
        displaytooltips = true;
        if (Temp != null)
        {
            Temp.SetActive(true);
            ToolTipBack = Temp.GetComponentInChildren<RawImage>();
            ToolTipBack.SendMessage("ToolSetTexts", ToolTipTexts, SendMessageOptions.DontRequireReceiver);
        }
    }

    void DoNotDisplayTooltip()
    {
        displaytooltips = false;
        if(Temp != null)
        Temp.SetActive(false);
    }


    void OnTriggerStay(Collider other)
    {
        if ((InputManager.controller && Input.GetButtonDown("CInteract") || (!InputManager.controller && Input.GetButtonDown("KBInteract")))
            && other.gameObject == player)
        {

            GameObject[] gos = GameObject.FindGameObjectsWithTag("PickUp");

            float distance = Vector3.Distance(transform.position, player.transform.position);

            for (int i = 0; i < gos.Length; i++)
            {
                if (gos[i] == gameObject)
                {
                    continue;
                }
                if (Vector3.Distance(gos[i].transform.position, player.transform.position) <= distance)
                {
                    return;
                }
            }

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
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            Instantiate(orbOfLightPickup, transform.position, transform.rotation);
                            break;
                        case accessory.BoltOfLight:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            Instantiate(boltOfLightPickup, transform.position, transform.rotation);
                            break;
                        case accessory.BlastOfLight:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            Instantiate(blastOfLightPickup, transform.position, transform.rotation);
                            break;
                        case accessory.ChainLightning:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            Instantiate(chainLightningPickup, transform.position, transform.rotation);
                            break;
                        case accessory.Singularity:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            Instantiate(singularityPickup, transform.position, transform.rotation);
                            break;
                        case accessory.Snare:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            Instantiate(snarePickup, transform.position, transform.rotation);
                            break;
                        case accessory.LightMine:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
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
                            equipment.emberDurability = 10;
                            equipment.equippedEmber = ember.Life;
                            break;
                        case 2:
                            equipment.emberDurability = 10;

                            equipment.equippedEmber = ember.Death;
                            break;
                        case 3:
                            equipment.emberDurability = 10;

                            equipment.equippedEmber = ember.Earth;
                            break;
                        case 4:
                            equipment.emberDurability = 10;

                            equipment.equippedEmber = ember.Wind;
                            break;
                        case 5:
                            equipment.emberDurability = 10;

                            equipment.equippedEmber = ember.Fire;
                            break;
                        case 6:
                            equipment.emberDurability = 10;

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
            Destroy(Temp);
            Destroy(gameObject);
        }
    }

    void SetName(string aName)
    {
       // print("CHANGENAME");
        ToolTipTexts._ItemName = aName;
    }

    void SetStat1(ItemStat firstStat)
    {
        string theName = "nada";
        switch(firstStat.TheStat)
        {
            case StatType.None:
                {
                    theName = " ";
                    break;
                }
            case StatType.SpellMod:
                {
                    theName = "Spell Power";
                    break;
                }
            case StatType.MeleeMod:
                {
                    theName = "Attack Damage";
                    break;
                }
            case StatType.MaxHP:
                {
                    theName = "Max HP";
                    break;
                }
            case StatType.MaxLight:
                {
                    theName = "Max Light";
                    break;
                }
        }

        ToolTipTexts._ItemStat1 = theName;
        if (firstStat.StatAmount == 0)
            ToolTipTexts._ItemAmount2 = " ";
        else
        ToolTipTexts._ItemAmount1 = firstStat.StatAmount.ToString();
    }

    void SetStat2(ItemStat secondStat)
    {
        string theName = "nada";
        switch (secondStat.TheStat)
        {
            case StatType.None:
                {
                    theName = " ";
                    break;
                }
            case StatType.SpellMod:
                {
                    theName = "Spell Power";
                    break;
                }
            case StatType.MeleeMod:
                {
                    theName = "Attack Damage";
                    break;
                }
            case StatType.MaxHP:
                {
                    theName = "Max HP";
                    break;
                }
            case StatType.MaxLight:
                {
                    theName = "Max Light";
                    break;
                }
        }
        ToolTipTexts._ItemStat2 = theName;
        if (secondStat.StatAmount == 0)
            ToolTipTexts._ItemAmount2 = " ";
        else
        ToolTipTexts._ItemAmount2 = secondStat.StatAmount.ToString();
    }



}
