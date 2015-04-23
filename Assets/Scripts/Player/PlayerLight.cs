using UnityEngine;
using System.Collections;

public class PlayerLight : MonoBehaviour {


    public float maxLight;
    public float currentLight;

    public void GainLight(float Amount)
    {
        currentLight += Amount;
        if (currentLight > maxLight)
            currentLight = maxLight;

    }

    public void LoseLight(float Amount)
    {
        currentLight -= Amount;
        if (currentLight <= 0)
        {
            currentLight = 0;
        }

    }
}
