﻿using UnityEngine;
using System.Collections;

public class RoomGeneration : MonoBehaviour
{

    public GameObject[] floorOneRooms;
    Room[] floorOneRoomsInfo;
    public GameObject[] floorOneMazes;
    Room[] floorOneMazesInfo;
    public GameObject dethrosRoom;
    Room dethrosRoomInfo;
    //public GameObject[] floorTwoRooms;
    //Room[] floorTwoRoomsInfo;
    //public GameObject[] floorTwoMazes;
    //Room[] floorTwoMazesInfo;
    //public GameObject lorneRoom;
    //Room lorneRoomInfo;
    //public GameObject[] floorThreeRooms;
    //Room[] floorThreeRoomsInfo;
    //public GameObject[] floorThreeMazes;
    //Room[] floorThreeMazesInfo;
    //public GameObject morriusRoom;
    //Room morriusRoomInfo;
    public GameObject[] finalRoomArray;
    [HideInInspector]
    public Room[] finalRoomInfoArray;
    //public int currentFloor = 1;
    public int currentRoom = 0;

    void Start()
    {
        DontDestroyOnLoad(this);
        Utilities.ArrayShuffle(floorOneRooms);
        floorOneRoomsInfo = new Room[floorOneRooms.Length];
        for (int i = 0; i < floorOneRooms.Length; i++)
        {
            floorOneRoomsInfo[i] = floorOneRooms[i].GetComponent<Room>();
            floorOneRoomsInfo[i].setUsed();
        }
        Utilities.ArrayShuffle(floorOneMazes, 3);
        floorOneMazesInfo = new Room[floorOneMazes.Length];
        for (int i = 0; i < floorOneMazes.Length; i++)
        {
            floorOneMazesInfo[i] = floorOneMazes[i].GetComponent<Room>();
            floorOneMazesInfo[i].setUsed();
        }
        dethrosRoomInfo = dethrosRoom.GetComponent<Room>();
        dethrosRoomInfo.setUsed();
        //Utilities.ArrayShuffle(floorTwoRooms);
        //floorTwoRoomsInfo = new Room[floorTwoRooms.Length];
        //for (int i = 0; i < floorTwoRooms.Length; i++)
        //{
        //    floorTwoRoomsInfo[i] = floorTwoRooms[i].GetComponent<Room>();
        //    floorTwoRoomsInfo[i].setUsed();
        //}
        //Utilities.ArrayShuffle(floorTwoMazes);
        //floorTwoMazesInfo = new Room[floorTwoMazes.Length];
        //for (int i = 0; i < floorTwoMazes.Length; i++)
        //{
        //    floorTwoMazesInfo[i] = floorTwoMazes[i].GetComponent<Room>();
        //    floorTwoMazesInfo[i].setUsed();
        //}
        //lorneRoomInfo = lorneRoom.GetComponent<Room>();
        //lorneRoomInfo.setUsed();
        //Utilities.ArrayShuffle(floorThreeRooms);
        //floorThreeRoomsInfo = new Room[floorThreeRooms.Length];
        //for (int i = 0; i < floorThreeRooms.Length; i++)
        //{
        //    floorThreeRoomsInfo[i] = floorThreeRooms[i].GetComponent<Room>();
        //    floorThreeRoomsInfo[i].setUsed();
        //}
        //Utilities.ArrayShuffle(floorThreeMazes);
        //floorThreeMazesInfo = new Room[floorThreeMazes.Length];
        //for (int i = 0; i < floorThreeMazes.Length; i++)
        //{
        //    floorThreeMazesInfo[i] = floorThreeMazes[i].GetComponent<Room>();
        //    floorThreeMazesInfo[i].setUsed();
        //}
        //morriusRoomInfo = morriusRoom.GetComponent<Room>();
        //morriusRoomInfo.setUsed();

        // FOR TESTING PURPOSES ONLY, DON'T FORGET TO DELETE ME.
        //floorOneRooms[0] = dethrosRoom;
        //floorOneRoomsInfo[0] = dethrosRoomInfo;
        //floorOneRooms[0] = floorOneMazes[1];
        //floorOneRoomsInfo[0] = floorOneMazesInfo[1];
        // THIS MARKS THE END OF THE TEST CODE

        finalRoomArray = new GameObject[9];
        finalRoomInfoArray = new Room[9];
        FillDungeon();

        CreateRoom();
    }

    void CreateRoom()
    {
        for (int y = 0; y < finalRoomInfoArray[currentRoom].height; y++)
        {
            bool skip = false;
            bool hasSkipped = false;
            for (int x = 0; x < finalRoomInfoArray[currentRoom].width; x++)
            {
                if (x == 0 || x == finalRoomInfoArray[currentRoom].width - 1 || y == 0 || y == finalRoomInfoArray[currentRoom].height - 1)
                {
                    if (x == finalRoomInfoArray[currentRoom].width / 2 - 1)
                    {
                        Instantiate(finalRoomInfoArray[currentRoom].door, new Vector3(x + .5f, -y, -1.4f), Quaternion.identity);
                        Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(x, -y, 0.0f), Quaternion.identity);
                        skip = true;
                    }
                    else if (!skip)
                        Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(x, -y, -1.4f), Quaternion.identity);
                }
                else
                {
                    Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(x, -y, 0.0f), Quaternion.identity);
                }
                if (hasSkipped)
                    skip = false;
                if (skip)
                    hasSkipped = true;
            }
        }
        for (int i = 0; i < finalRoomInfoArray[currentRoom].innerWallPositions.Length; i++)
        {
            Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(finalRoomInfoArray[currentRoom].innerWallPositions[i].x, -finalRoomInfoArray[currentRoom].innerWallPositions[i].y, -1.4f), Quaternion.identity);
        }
        for (int i = 0; i < finalRoomInfoArray[currentRoom].hazardSpawnPoints.Length; i++)
        {
            Instantiate(finalRoomInfoArray[currentRoom].hazard, new Vector3(finalRoomInfoArray[currentRoom].hazardSpawnPoints[i].x, -finalRoomInfoArray[currentRoom].hazardSpawnPoints[i].y, -1), Quaternion.identity);
        }
        finalRoomInfoArray[currentRoom].numEnemies = Random.Range(finalRoomInfoArray[currentRoom].minEnemies, finalRoomInfoArray[currentRoom].maxEnemies);
        int enemiesSpawned = 0;
        while (enemiesSpawned < finalRoomInfoArray[currentRoom].numEnemies)
        {
            for (int i = 0; i < finalRoomInfoArray[currentRoom].enemySpawnPoints.Length; i++)
            {
                int chance = Random.Range(1, 5);
                if (!finalRoomInfoArray[currentRoom].enemySpawnPointUsed[i] && chance == 1)
                {
                    Instantiate(finalRoomInfoArray[currentRoom].enemiesThatCanSpawn[Random.Range(0, finalRoomInfoArray[currentRoom].enemiesThatCanSpawn.Length)], new Vector3(finalRoomInfoArray[currentRoom].enemySpawnPoints[i].x, -finalRoomInfoArray[currentRoom].enemySpawnPoints[i].y, -1f), Quaternion.identity);
                    finalRoomInfoArray[currentRoom].enemySpawnPointUsed[i] = true;
                    ++enemiesSpawned;
                    if (enemiesSpawned == finalRoomInfoArray[currentRoom].numEnemies)
                        break;
                }
            }
        }
    }

    void FillDungeon()
    {
        finalRoomArray[0] = floorOneRooms[0];
        finalRoomInfoArray[0] = floorOneRoomsInfo[0];
        finalRoomArray[1] = floorOneRooms[1];
        finalRoomInfoArray[1] = floorOneRoomsInfo[1];
        finalRoomArray[2] = floorOneMazes[0];
        finalRoomInfoArray[2] = floorOneMazesInfo[0];
        finalRoomArray[3] = floorOneRooms[2];
        finalRoomInfoArray[3] = floorOneRoomsInfo[2];
        finalRoomArray[4] = floorOneRooms[3];
        finalRoomInfoArray[4] = floorOneRoomsInfo[3];
        finalRoomArray[5] = floorOneMazes[1];
        finalRoomInfoArray[5] = floorOneMazesInfo[1];
        finalRoomArray[6] = floorOneRooms[4];
        finalRoomInfoArray[6] = floorOneRoomsInfo[4];
        finalRoomArray[7] = floorOneRooms[5];
        finalRoomInfoArray[7] = floorOneRoomsInfo[5];
        finalRoomArray[8] = dethrosRoom;
        finalRoomInfoArray[8] = dethrosRoomInfo;
    }
}
