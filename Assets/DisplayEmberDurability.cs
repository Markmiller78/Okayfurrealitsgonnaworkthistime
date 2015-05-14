using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayEmberDurability : MonoBehaviour {

    float timer;
    public Text theText;

    public int count;

    PlayerEquipment heroEquip;


    // Use this for initialization
    void Start()
    {
        heroEquip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();

        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 1;
            count = heroEquip.emberDurability;
            theText.text = count.ToString();
        }
    }
}
