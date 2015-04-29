using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject[] wallTiles;
    public GameObject[] floorTiles;
    public GameObject SouthDoor;
    public GameObject NorthDoor;
    public GameObject EastDoor;
    public GameObject WestDoor;
    public Vector2[] innerWallPositions;
    public GameObject[] enemiesThatCanSpawn;
    public Vector2[] enemySpawnPoints;
    [HideInInspector]
    public bool[] enemySpawnPointUsed;
    public int minEnemies;
    public int maxEnemies;
    public GameObject hazard;
    public Vector2[] hazardSpawnPoints;
    public Vector2 bottomPlayerSpawn;
    public Vector2 rightPlayerSpawn;
    public Vector2 topPlayerSpawn;
    public Vector2 leftPlayerSpawn;
    public int entranceDir; // 0 = bottom, 1 = left, 2 = top, 3 = right, 4 = TREASURE ROOM!!!
    public int exitDir;
    [HideInInspector]
    public bool beenThere;
    public int numEnemies = 0;
    public bool comingFromEntrance = true;


    public void setUsed()
    {
        enemySpawnPointUsed = new bool[enemySpawnPoints.Length];
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            comingFromEntrance = true;
            enemySpawnPointUsed[i] = false;
        }
    }

    public void SpawnEnemies(int RoomCode)
    {
        //ROOM CODE WORKS AS A TIER THEN ROOM NUMBER
        // 11 INDICATES TIER 1 ROOM 1
        // 110 INDICATES TIER 1 ROOM 10
        // 0 AFTER TIER INDICATES A MAZE
        // 101 INDICATES TIER 1 MAZE 1
        if(RoomCode == 11)
        {

        }
        if (RoomCode == 12)
        {
            Instantiate(enemiesThatCanSpawn[Random.Range(0, 3)], new Vector3(4.89f, -1.54f, -1), new Quaternion(0,0,0,0));




        }
        if (RoomCode == 13)
        {

        }
        if (RoomCode == 14)
        {

        }
        if (RoomCode == 15)
        {

        }
        if (RoomCode == 16)
        {

        }
        if (RoomCode == 17)
        {

        }
        if (RoomCode == 18)
        {

        }
        if (RoomCode == 19)
        {

        }
        if (RoomCode == 110)
        {

        }
        if (RoomCode == 101)
        {

        }
        if (RoomCode == 102)
        {

        }
        if (RoomCode == 103)
        {

        }
        if (RoomCode == 104)
        {

        }















    }
}
