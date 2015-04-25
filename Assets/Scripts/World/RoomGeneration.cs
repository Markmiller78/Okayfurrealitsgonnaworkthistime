using UnityEngine;
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
        //Utilities.ArrayShuffle(floorOneMazes);
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
        floorOneRooms[0] = dethrosRoom;
        floorOneRoomsInfo[0] = dethrosRoomInfo;
        //floorOneRooms[0] = floorOneMazes[0];
        //floorOneRoomsInfo[0] = floorOneMazesInfo[0];
        // THIS MARKS THE END OF THE TEST CODE

        CreateRoom();
    }

    void CreateRoom()
    {
        for (int y = 0; y < floorOneRoomsInfo[currentRoom].height; y++)
        {
            bool skip = false;
            bool hasSkipped = false;
            for (int x = 0; x < floorOneRoomsInfo[currentRoom].width; x++)
            {
                if (x == 0 || x == floorOneRoomsInfo[currentRoom].width - 1 || y == 0 || y == floorOneRoomsInfo[currentRoom].height - 1)
                {
                    if (x == floorOneRoomsInfo[currentRoom].width / 2 - 1)
                    {
                        Instantiate(floorOneRoomsInfo[currentRoom].door, new Vector3(x + .5f, -y, -1.4f), Quaternion.identity);
                        skip = true;
                    }
                    else if (!skip)
                        Instantiate(floorOneRoomsInfo[currentRoom].wallTiles[0], new Vector3(x, -y, -1.4f), Quaternion.identity);
                }
                else
                {
                    Instantiate(floorOneRoomsInfo[currentRoom].floorTiles[0], new Vector3(x, -y, 0.0f), Quaternion.identity);
                }
                if (hasSkipped)
                    skip = false;
                if (skip)
                    hasSkipped = true;
            }
        }
        for (int i = 0; i < floorOneRoomsInfo[currentRoom].innerWallPositions.Length; i++)
        {
            Instantiate(floorOneRoomsInfo[currentRoom].wallTiles[0], new Vector3(floorOneRoomsInfo[currentRoom].innerWallPositions[i].x, -floorOneRoomsInfo[currentRoom].innerWallPositions[i].y, -1.4f), Quaternion.identity);
        }
        for (int i = 0; i < floorOneRoomsInfo[currentRoom].hazardSpawnPoints.Length; i++)
        {
            Instantiate(floorOneRoomsInfo[currentRoom].hazard, new Vector3(floorOneRoomsInfo[currentRoom].hazardSpawnPoints[i].x, -floorOneRoomsInfo[currentRoom].hazardSpawnPoints[i].y, -1), Quaternion.identity);
        }
        int howManyEnemies = Random.Range(floorOneRoomsInfo[currentRoom].minEnemies, floorOneRoomsInfo[currentRoom].maxEnemies);
        int enemiesSpawned = 0;
        while (enemiesSpawned < howManyEnemies)
        {
            for (int i = 0; i < floorOneRoomsInfo[currentRoom].enemySpawnPoints.Length; i++)
            {
                int chance = Random.Range(1, 5);
                if (!floorOneRoomsInfo[currentRoom].enemySpawnPointUsed[i] && chance == 1)
                {
                    Instantiate(floorOneRoomsInfo[currentRoom].enemiesThatCanSpawn[Random.Range(0, floorOneRoomsInfo[currentRoom].enemiesThatCanSpawn.Length)], new Vector3(floorOneRoomsInfo[currentRoom].enemySpawnPoints[i].x, -floorOneRoomsInfo[currentRoom].enemySpawnPoints[i].y, -1f), Quaternion.identity);
                    floorOneRoomsInfo[currentRoom].enemySpawnPointUsed[i] = true;
                    ++enemiesSpawned;
                    if (enemiesSpawned == howManyEnemies)
                        break;
                }
            }
        }
    }
}
