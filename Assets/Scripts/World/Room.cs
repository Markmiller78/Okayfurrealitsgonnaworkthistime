using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject[] wallTiles;
    public GameObject[] floorTiles;
    public GameObject door;
    public Vector2[] innerWallPositions;
    public GameObject[] enemiesThatCanSpawn;
    public Vector2[] enemySpawnPoints;
    [HideInInspector]
    public bool[] enemySpawnPointUsed;
    public int minEnemies;
    public int maxEnemies;
    public GameObject hazard;
    public Vector2[] hazardSpawnPoints;
    [HideInInspector]
    public bool beenThere;

    public void setUsed()
    {
        enemySpawnPointUsed = new bool[enemySpawnPoints.Length];
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            enemySpawnPointUsed[i] = false;
        }
    }
}
