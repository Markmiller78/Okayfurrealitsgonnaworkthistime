﻿using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    GameObject player;

    //bool ScreenshakeOn;
    bool alternate;
    float timer;

    float damp = .15f;
    Vector3 velocity = Vector3.zero;

    GameObject dungeon;
    RoomGeneration generator;
    int lastFrameRoom = 0;

    void Start()
    {
        timer = .1f;
        //ScreenshakeOn = false;
        alternate = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20.0f);
        Screen.SetResolution(1280, 720, false);
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        if (dungeon != null)
            generator = dungeon.GetComponent<RoomGeneration>();
    }

    void Update()
    {
        if (player != null && generator != null)
        {
            //Vector3 point = Camera.main.WorldToViewportPoint(player.transform.position);
            //Vector3 delta = player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, point.z));
            //Vector3 destination = transform.position + delta;
            //
            //transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, damp);

            Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, -20.0f);

            //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20.0f);
            if (player.transform.position.x < 7.75f)
            {
                target = new Vector3(7.75f, player.transform.position.y, -20.0f);
                //transform.position = new Vector3(7.75f, player.transform.position.y, -20.0f);
            }
            else if (player.transform.position.x > generator.finalRoomInfoArray[generator.currentRoom].width - 8.75f)
            {
                target = new Vector3(generator.finalRoomInfoArray[generator.currentRoom].width - 8.75f, player.transform.position.y, -20.0f);
                //transform.position = new Vector3(generator.finalRoomInfoArray[generator.currentRoom].width - 8.75f, player.transform.position.y, -20.0f);
            }
            if (player.transform.position.y > -4.25f)
            {
                target = new Vector3(player.transform.position.x, -4.25f, -20.0f);
                //transform.position = new Vector3(transform.position.x, -4.25f, -20.0f);
            }
            else if (player.transform.position.y < -(generator.finalRoomInfoArray[generator.currentRoom].height - 5.25f))
            {
                target = new Vector3(player.transform.position.x, -(generator.finalRoomInfoArray[generator.currentRoom].height - 5.25f), -20.0f);
                //transform.position = new Vector3(transform.position.x, -(generator.finalRoomInfoArray[generator.currentRoom].height - 5.25f), -20.0f);
            }

            Vector3 point = Camera.main.WorldToViewportPoint(target);
            Vector3 delta = target - Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, point.z));
            Vector3 destination = transform.position + delta;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, damp);

            if (lastFrameRoom != generator.currentRoom)
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20f);
            }

            if (transform.position.x < 7.75f)
            {
                transform.position = new Vector3(7.75f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > generator.finalRoomInfoArray[generator.currentRoom].width - 8.75f)
            {
                transform.position = new Vector3(generator.finalRoomInfoArray[generator.currentRoom].width - 8.75f, transform.position.y, transform.position.z);
            }
            if (transform.position.y > -4.25f)
            {
                transform.position = new Vector3(transform.position.x, -4.25f, transform.position.z);
            }
            else if (transform.position.y < -(generator.finalRoomInfoArray[generator.currentRoom].height - 5.25f))
            {
                transform.position = new Vector3(transform.position.x, -(generator.finalRoomInfoArray[generator.currentRoom].height - 5.25f), transform.position.z);
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
        if (generator)
            lastFrameRoom = generator.currentRoom;
    }

    public void ScreenShake()
    {
        timer = .1f;

    }
}
