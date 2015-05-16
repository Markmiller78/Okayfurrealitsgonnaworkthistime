using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public float meleeModifier = 0.0f;
    public float spellModifier = 0.0f;
    public float maxHPModifier = 0.0f;
    public float maxLightModifier = 0.0f;

    void Start()
    {

    }

    void GetMeleeMod(float meleeMod)
    {
        meleeModifier = meleeMod;
    }
    void GetSpellMod(float spellMod)
    {
        spellModifier = spellMod;
    }
    void GetMaxHPMod(float hpMod)
    {
        maxHPModifier = hpMod;
    }
    void GetMaxLightMod(float lightMod)
    {
        maxLightModifier = lightMod;
    }
}
