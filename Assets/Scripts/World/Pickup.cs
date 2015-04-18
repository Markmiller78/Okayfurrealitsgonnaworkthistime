using UnityEngine;
using System.Collections;

public enum equipmentType { Boot, Accessory, Ember};

public class Pickup : MonoBehaviour
{
    public equipmentType typeOfEquipment;     // See above for enum.
    public int whichOneToEquip = 0;     // See PlayerEquipment script for these enums.
    GameObject player;
    PlayerEquipment equipment;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        equipment = player.GetComponent<PlayerEquipment>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // TODO: work on this when I come home from my meeting.
            switch (typeOfEquipment)
            {
                case equipmentType.Boot:
                    break;
                case equipmentType.Accessory:
                    break;
                case equipmentType.Ember:
                    break;
                default:
                    break;
            }
        }
    }
}
