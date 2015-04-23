using UnityEngine;
using System.Collections;

public class RoomGeneration : MonoBehaviour
{

    public GameObject[] rooms;
    Room[] roomsInfo;
//    GameObject player;
    public int currentRoom = 0;

    void Start()
    {
        DontDestroyOnLoad(this);
 //       player = GameObject.FindGameObjectWithTag("Player");
        roomsInfo = new Room[rooms.Length];
        for (int i = 0; i < rooms.Length; i++)
        {
            roomsInfo[i] = rooms[i].GetComponent<Room>();
            roomsInfo[i].setUsed();
        }
        CreateRoom();
    }

    void CreateRoom()
    {
        for (int y = 0; y < roomsInfo[currentRoom].height; y++)
        {
            bool skip = false;
            bool hasSkipped = false;
            for (int x = 0; x < roomsInfo[currentRoom].width; x++)
            {
                if (x == 0 || x == roomsInfo[currentRoom].width - 1 || y == 0 || y == roomsInfo[currentRoom].height - 1)
                {
                    if (x == roomsInfo[currentRoom].width / 2 - 1)
                    {
                        Instantiate(roomsInfo[currentRoom].door, new Vector3(x + .5f, -y, -1.4f), Quaternion.identity);
                        skip = true;
                    }
                    else if (!skip)
                        Instantiate(roomsInfo[currentRoom].wallTiles[0], new Vector3(x, -y, -1.4f), Quaternion.identity);
                }
                else
                {
                    Instantiate(roomsInfo[currentRoom].floorTiles[0], new Vector3(x, -y, 0.0f), Quaternion.identity);
                }
                if (hasSkipped)
                    skip = false;
                if (skip)
                    hasSkipped = true;
            }
        }
        for (int i = 0; i < roomsInfo[currentRoom].innerWallPositions.Length; i++)
        {
            Instantiate(roomsInfo[currentRoom].wallTiles[0], new Vector3(roomsInfo[currentRoom].innerWallPositions[i].x, -roomsInfo[currentRoom].innerWallPositions[i].y, -1.4f), Quaternion.identity);
        }
        for (int i = 0; i < roomsInfo[currentRoom].hazardSpawnPoints.Length; i++)
        {
            Instantiate(roomsInfo[currentRoom].hazard, new Vector3(roomsInfo[currentRoom].hazardSpawnPoints[i].x, -roomsInfo[currentRoom].hazardSpawnPoints[i].y, -1), Quaternion.identity);
        }
        int howManyEnemies = Random.Range(roomsInfo[currentRoom].minEnemies, roomsInfo[currentRoom].maxEnemies);
        int enemiesSpawned = 0;
        while (enemiesSpawned < howManyEnemies)
        {
            for (int i = 0; i < roomsInfo[currentRoom].enemySpawnPoints.Length; i++)
            {
                int chance = Random.Range(1, 5);
                if (!roomsInfo[currentRoom].enemySpawnPointUsed[i] && chance == 1)
                {
                    Instantiate(roomsInfo[currentRoom].enemiesThatCanSpawn[Random.Range(0, roomsInfo[currentRoom].enemiesThatCanSpawn.Length)], new Vector3(roomsInfo[currentRoom].enemySpawnPoints[i].x, -roomsInfo[currentRoom].enemySpawnPoints[i].y, -1f), Quaternion.identity);
                    roomsInfo[currentRoom].enemySpawnPointUsed[i] = true;
                    ++enemiesSpawned;
                    if (enemiesSpawned == howManyEnemies)
                        break;
                }
            }
        }
    }
}
