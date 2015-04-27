using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour
{
    public float knockbackDistance;
    public float knockbackSpeed;

    bool isGettingKnockedBack = false;
    Vector3 origin;
    Vector2 dir;
    GameObject player;
    CharacterController controller;
    float timerMax;
    float timerCurr;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        timerMax = knockbackDistance / knockbackSpeed;
        timerCurr = timerMax;
    }

    void Update()
    {
        if (!isGettingKnockedBack)
        {
            origin = transform.position;
            dir = (transform.position - player.transform.position);
        }
        else if (isGettingKnockedBack)
        {
            timerCurr -= Time.deltaTime;
            controller.Move(dir * Time.deltaTime * knockbackSpeed);
            if (Vector3.Distance(transform.position, origin) >= knockbackDistance || timerCurr <= 0.0f)
            {
                isGettingKnockedBack = false;
                timerCurr = timerMax;
            }
        }
    }

   public  void GetWrecked()
    {
        isGettingKnockedBack = true;
    }
}
