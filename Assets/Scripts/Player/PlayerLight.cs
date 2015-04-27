using UnityEngine;
using System.Collections;

public class PlayerLight : MonoBehaviour
{


    public float maxLight;
    public float minLight;
    public float currentLight;
    GameObject player;
    Light playerLight;
    float rangeMod = 4f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLight = player.GetComponentInChildren<Light>();
    }

    void Update()
    {
        playerLight.range = (currentLight/maxLight) + rangeMod;
    }

    public void GainLight(float Amount)
    {
        currentLight += Amount;
        if (currentLight > maxLight)
            currentLight = maxLight;
        if (rangeMod < 4f)
        {
            rangeMod += Amount * .015f;
        }
    }

    public void LoseLight(float Amount)
    {
        currentLight -= Amount;
        if (currentLight <= 0)
        {
            currentLight = 0;
        }
        if (rangeMod > 1f)
        {
            rangeMod -= Amount * .015f;
        }
    }
}
