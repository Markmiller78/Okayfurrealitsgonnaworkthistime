using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    GameObject player;

    bool ScreenshakeOn;
    bool alternate;
    float timer;

    GameObject dungeon;
    RoomGeneration generator;

    void Start()
    {
        timer = .1f;
        ScreenshakeOn = false;
        alternate = false;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20.0f);
        Screen.SetResolution(1280, 720, false);
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        generator = dungeon.GetComponent<RoomGeneration>();
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20.0f);
        if (player.transform.position.x < 7.75f)
        {
            transform.position = new Vector3(7.75f, player.transform.position.y, -20.0f);
        }
        else if (player.transform.position.x > generator.finalRoomInfoArray[generator.currentRoom].width - 8.75f)
        {
            transform.position = new Vector3(generator.finalRoomInfoArray[generator.currentRoom].width - 8.75f, player.transform.position.y, -20.0f);
        }
        if (player.transform.position.y > -4.25f)
        {
            transform.position = new Vector3(transform.position.x, -4.25f, -20.0f);
        }
        else if (player.transform.position.y < -(generator.finalRoomInfoArray[generator.currentRoom].height - 5.25f))
        {
            transform.position = new Vector3(transform.position.x, -(generator.finalRoomInfoArray[generator.currentRoom].height - 5.25f), -20.0f);
        }
        timer -= Time.deltaTime;

        if (timer > 0)
        {
            if (alternate)
            {
                alternate = false;
                transform.position = new Vector3(transform.position.x + .05f, transform.position.y + .05f, -20.0f);
            }
            else
            {
                alternate = true;
                transform.position = new Vector3(transform.position.x - .05f, transform.position.y - .05f, -20.0f);
            }
        }
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, -20.0f);

    }

    public void ScreenShake()
    {
        timer = .1f;

    }
}
