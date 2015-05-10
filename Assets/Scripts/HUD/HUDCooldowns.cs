using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDCooldowns : MonoBehaviour
{


    public Image SpellOverlay;
    public Image BootOverlay;
    public Image AbsorbOverlay;
    float currentScaleSpell;
    float currentScaleBoot;
    float currentScaleAbsorb;
    float CooldownMult;
    PlayerCooldowns theCooldowns;
    CooldownID myID;
    float coolDownTime;

    // Use this for initialization
    void Start()
    {
        currentScaleSpell =0;
        currentScaleBoot =0;
        currentScaleAbsorb =0;
        theCooldowns = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCooldowns>();
    }

    // Update is called once per frame
    void Update()
    {
        //UPDATE SPELL CD
        if (theCooldowns.spellCooling)
        {
            currentScaleSpell = theCooldowns.spellCooldown / theCooldowns.spellCooldownMax;
            SpellOverlay.fillAmount = currentScaleSpell;
        }
        else
            SpellOverlay.fillAmount = 0;
        //UPDATE BOOT CD
        if (theCooldowns.dashCooling)
        {
            currentScaleBoot = theCooldowns.dashCooldown / theCooldowns.dashCooldownMax;
            BootOverlay.fillAmount = currentScaleBoot;
        }
        else
            BootOverlay.fillAmount = 0;
        //UPDATE ABSORB CD

        if (theCooldowns.collectorCooling)
        {
            currentScaleAbsorb = theCooldowns.collectorCooldown / theCooldowns.collectorCooldownMax;
            AbsorbOverlay.fillAmount = currentScaleAbsorb;
        }
        else
            AbsorbOverlay.fillAmount = 0;



    }

}
