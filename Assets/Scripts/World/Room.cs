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
    public int entranceDir; // 0 = bottom, 1 = left, 2 = top, 3 = right
    public int exitDir;
    [HideInInspector]
    public bool beenThere;
    public int numEnemies = 0;
    public bool comingFromEntrance = true;

    // 0 = left, 1 = top, 2 = right, 3 = bottom
    [HideInInspector]
    public int fromDir;
    [HideInInspector]
    public int toDir;


    public void setUsed()
    {
        enemySpawnPointUsed = new bool[enemySpawnPoints.Length];
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            enemySpawnPointUsed[i] = false;
        }
    }
}
