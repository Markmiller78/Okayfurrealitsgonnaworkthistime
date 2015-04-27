﻿using UnityEngine;
using System.Collections;

public class NorthDoor : MonoBehaviour
{
    public bool isLocked = true;
    public GameObject dungeon;
    RoomGeneration generator;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        generator = dungeon.GetComponent<RoomGeneration>();
        isLocked = true;
    }

    void Update()
    {
        if (isLocked &&
            ((generator.currentRoom > 0 && generator.finalRoomInfoArray[generator.currentRoom].entranceDir == 2)
            || (generator.currentRoom < 8 && generator.finalRoomInfoArray[generator.currentRoom].exitDir == 2))
            && generator.finalRoomInfoArray[generator.currentRoom].numEnemies == 0)
        {
            isLocked = false;
            Unlock();
        }
    }

    void Lock()
    {
        GetComponentInChildren<BoxCollider>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    void Unlock()
    {
        BoxCollider[] tryingThis = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider box in tryingThis)
        {
            if (box.gameObject.name == "Lock")
            {
                box.enabled = false;
            }
        }
        //GetComponentInChildren<BoxCollider>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player
            && generator.finalRoomInfoArray[generator.currentRoom].exitDir == 2
            && generator.currentRoom < 8)
        {
            ++generator.currentRoom;
            generator.finalRoomInfoArray[generator.currentRoom].comingFromEntrance = true;
            generator.Reset();
        }
        else if (other.gameObject == player && generator.currentRoom > 0)
        {
            --generator.currentRoom;
            generator.finalRoomInfoArray[generator.currentRoom].comingFromEntrance = false;
            generator.Reset();
        }
    }
}