using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    GameObject player;

    bool ScreenshakeOn;
    bool alternate;
    float timer;

    void Start()
    {
        timer = .1f;
        ScreenshakeOn = false;
        alternate = false;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20.0f);
        Screen.SetResolution(1280, 720, false);
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20.0f);
        timer -= Time.deltaTime;

        if (timer > 0 )
        {
            if (alternate)
            {
                alternate = false;
                transform.position = new Vector3(transform.position.x + .05f, player.transform.position.y + .05f, -20.0f);
            }
            else
            {
                alternate = true;
                transform.position = new Vector3(transform.position.x - .05f, player.transform.position.y - .05f, -20.0f);
            }
        }
        else
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20.0f);

    }

    public void ScreenShake()
    {
        timer = .1f;

    }
}
