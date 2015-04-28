using UnityEngine;
using System.Collections;

public class HUDCooldowns : MonoBehaviour
{


    public GameObject ImageOverlay;
    float currentScale;
    float CooldownMult;
    CooldownID myID;

    // Use this for initialization
    void Start()
    {
        myID = gameObject.GetComponent<HUDCooldownID>().theID;
        currentScale = 1;
        switch (myID)
        {
            case CooldownID.Spell:
                {
                    CooldownMult = 1.1f;
                    break;
                }
            case CooldownID.Absorb:
                {
                    CooldownMult = .1f;
                    break;
                }
            case CooldownID.Boot:
                {
                    CooldownMult = 2;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentScale < 1)
            currentScale += Time.deltaTime * CooldownMult;
        ImageOverlay.transform.localScale = new Vector3(1, currentScale, 1);

    }

    public void CooldownTrigger(CooldownID anID)
    {
        if (anID == myID)
            currentScale = 0;
    }
}
