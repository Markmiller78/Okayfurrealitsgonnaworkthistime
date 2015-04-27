using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20.0f);
        Screen.SetResolution(1280, 720, false);
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20.0f);
    }
}
