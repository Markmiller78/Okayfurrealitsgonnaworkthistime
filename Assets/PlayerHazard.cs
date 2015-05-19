using UnityEngine;
using System.Collections;

public class PlayerHazard : MonoBehaviour
{

    public bool isInHazard;

    public Texture HazardCookie;
    Light heroLight;
    GameObject hero;
    Health heroHP;
    PlayerMovement heroMovement;

    

    float timer;

    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        heroMovement = hero.GetComponent<PlayerMovement>();
        heroHP = hero.GetComponent<Health>();

        heroLight = hero.GetComponentInChildren<Light>();
        timer = 0.5f;
    }

    void Update()
    {

        if (isInHazard)
        {
            heroLight.cookie = HazardCookie;
            heroMovement.isinHazard = true;
            heroMovement.halfSpeed = 0.8f;
            heroMovement.fullSpeed = 1.6f;
            heroHP.LoseHealth(5.0f * Time.deltaTime);
        }
        else
        {
            heroLight.cookie = null;
            heroMovement.isinHazard = false;
            heroMovement.halfSpeed = 1.6f;
            heroMovement.fullSpeed = 3.1f;
        }

        timer += Time.deltaTime;

        if (timer >= 0.05f)
        {
            isInHazard = false;

            GameObject[] hazards = GameObject.FindGameObjectsWithTag("Hazard");
            for (int i = 0; i < hazards.Length; i++)
            {
                if (Vector3.Distance(transform.position, hazards[i].transform.position) < 0.8f)
                {
                    isInHazard = true;
                    break;
                }
            }

            if (isInHazard == false)
            {
                GameObject[] reapHazards = GameObject.FindGameObjectsWithTag("ReapHazard");
                for (int i = 0; i < reapHazards.Length; i++)
                {
                    if (Vector3.Distance(transform.position, reapHazards[i].transform.position) < 0.95f)
                    {
                        if (reapHazards[i].GetComponent<ReappearingHazard>().doingDamage == true)
                        {
                            isInHazard = true;
                            break;
                        }
                    }
                }

            }
            timer = 0;
        }
    }

}
