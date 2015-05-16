using UnityEngine;
using System.Collections;

public class RoomGeneration : MonoBehaviour
{
    GameObject player;
    public GameObject[] floorOneRooms;
    Room[] floorOneRoomsInfo;
    public GameObject[] floorOneMazes;
    Room[] floorOneMazesInfo;
    public GameObject dethrosRoom;
    Room dethrosRoomInfo;
    public GameObject[] floorTwoRooms;
    Room[] floorTwoRoomsInfo;
    //
    public GameObject[] floorTwoMazes;
    Room[] floorTwoMazesInfo;
    public GameObject lorneRoom;
    Room lorneRoomInfo;
    //public GameObject[] floorThreeRooms;
    //Room[] floorThreeRoomsInfo;
    //public GameObject[] floorThreeMazes;
    //Room[] floorThreeMazesInfo;
    //public GameObject morriusRoom;
    //Room morriusRoomInfo;
    //
    public GameObject[] finalRoomArray;
    [HideInInspector]
    public Room[] finalRoomInfoArray;
    //public int currentFloor = 1;
    public int currentRoom = 0;
    //public GameObject treasureRoom;
    //Room treasureRoomInfo;
    public GameObject hazard;
    public GameObject nDoor;
    public GameObject sDoor;
    public GameObject eDoor;
    public GameObject wDoor;
    public GameObject waypoint;
    public GameObject chest;

    public GameObject[] checkpointRooms;
    Room[] checkpointRoomsInfo;

   public bool easyMode;
    int enemyMod;
    int prevRoom = -1;

    void Start()
    {
        //DontDestroyOnLoad(this);
        player = GameObject.FindGameObjectWithTag("Player");
        Utilities.ArrayShuffle(floorOneRooms);
        floorOneRoomsInfo = new Room[floorOneRooms.Length];
        for (int i = 0; i < floorOneRooms.Length; i++)
        {
            floorOneRoomsInfo[i] = floorOneRooms[i].GetComponent<Room>();
            floorOneRoomsInfo[i].setUsed();
        }
        Utilities.ArrayShuffle(floorOneMazes, 100);
        floorOneMazesInfo = new Room[floorOneMazes.Length];
        for (int i = 0; i < floorOneMazes.Length; i++)
        {
            floorOneMazesInfo[i] = floorOneMazes[i].GetComponent<Room>();
            floorOneMazesInfo[i].setUsed();
        }
        dethrosRoomInfo = dethrosRoom.GetComponent<Room>();
        dethrosRoomInfo.setUsed();
        Utilities.ArrayShuffle(floorTwoRooms);
        floorTwoRoomsInfo = new Room[floorTwoRooms.Length];
        for (int i = 0; i < floorTwoRooms.Length; i++)
        {
            floorTwoRoomsInfo[i] = floorTwoRooms[i].GetComponent<Room>();
            floorTwoRoomsInfo[i].setUsed();
        }
        Utilities.ArrayShuffle(floorTwoMazes);
        floorTwoMazesInfo = new Room[floorTwoMazes.Length];
        for (int i = 0; i < floorTwoMazes.Length; i++)
        {
            floorTwoMazesInfo[i] = floorTwoMazes[i].GetComponent<Room>();
            floorTwoMazesInfo[i].setUsed();
        }
        lorneRoomInfo = lorneRoom.GetComponent<Room>();
        lorneRoomInfo.setUsed();
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

        //treasureRoomInfo = treasureRoom.GetComponent<Room>();

        easyMode = GameObject.FindObjectOfType<Options>().easyMode;

        if (easyMode)
        {
            checkpointRoomsInfo = new Room[checkpointRooms.Length];
            for (int i = 0; i < checkpointRooms.Length; i++)
            {
                checkpointRoomsInfo[i] = checkpointRooms[i].GetComponent<Room>();
                checkpointRoomsInfo[i].setUsed();
            }
        }

        finalRoomArray = new GameObject[easyMode ? 22 : 18];
        finalRoomInfoArray = new Room[easyMode ? 22 : 18];
        FillDungeon();

        //TESTING
        //finalRoomArray[0] = floorOneRooms[3];
        //finalRoomInfoArray[0] = floorOneRoomsInfo[3];
        //ENDTESTING

        CreateRoom();
        //Reset();
        enemyMod = (easyMode ? 11 : 9);
    }

    void CreateRoom()
    {
        bool skip = false;
        bool hasSkipped = false;

        if (prevRoom > currentRoom)
            ++enemyMod;
        else
            --enemyMod;

        if (enemyMod == 0)
        {
            enemyMod = (easyMode ? 11 : 9);
        }

        // Spawn north wall and possibly door
        for (int i = 0; i < finalRoomInfoArray[currentRoom].width; i++)
        {
            if ((finalRoomInfoArray[currentRoom].entranceDir == 2 || finalRoomInfoArray[currentRoom].exitDir == 2) && i == finalRoomInfoArray[currentRoom].width / 2 - 1)
            {
                Instantiate(finalRoomInfoArray[currentRoom].NorthDoor, new Vector3((float)i + .5f, 0, -1.4f), Quaternion.identity);
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(i, 0, 0.0f), Quaternion.identity);
                skip = true;
            }
            else if (!skip)
            {
                Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(i, 0, -1.4f), Quaternion.identity);
            }
            if (hasSkipped)
            {
                skip = false;
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(i, 0, 0.0f), Quaternion.identity);
            }
            if (skip)
            {
                hasSkipped = true;
            }
        }
        // Spawn south wall and possibly door
        skip = false;
        hasSkipped = false;
        for (int i = 0; i < finalRoomInfoArray[currentRoom].width; i++)
        {
            if ((finalRoomInfoArray[currentRoom].entranceDir == 0 || finalRoomInfoArray[currentRoom].exitDir == 0) && i == finalRoomInfoArray[currentRoom].width / 2 - 1)
            {
                Instantiate(finalRoomInfoArray[currentRoom].SouthDoor, new Vector3((float)i + .5f, -(finalRoomInfoArray[currentRoom].height - 1), -1.4f), Quaternion.identity);
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(i, -(finalRoomInfoArray[currentRoom].height - 1), 0.0f), Quaternion.identity);
                skip = true;
            }
            else if (!skip)
            {
                Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(i, -(finalRoomInfoArray[currentRoom].height - 1), -1.4f), Quaternion.identity);
            }
            if (hasSkipped)
            {
                skip = false;
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(i, -(finalRoomInfoArray[currentRoom].height - 1), 0.0f), Quaternion.identity);
            }
            if (skip)
            {
                hasSkipped = true;
            }
        }
        // Spawn west wall and possibly door
        skip = false;
        hasSkipped = false;
        for (int i = 0; i < finalRoomInfoArray[currentRoom].height; i++)
        {
            if ((finalRoomInfoArray[currentRoom].entranceDir == 1 || finalRoomInfoArray[currentRoom].exitDir == 1) && i == finalRoomInfoArray[currentRoom].height / 2 - 1)
            {
                Instantiate(finalRoomInfoArray[currentRoom].WestDoor, new Vector3(0, -((float)i + .5f), -1.4f), Quaternion.identity);
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(0, -i, 0.0f), Quaternion.identity);
                skip = true;
            }
            else if (!skip)
            {
                Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(0, -i, -1.4f), Quaternion.identity);
            }
            if (hasSkipped)
            {
                skip = false;
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(0, -i, 0.0f), Quaternion.identity);
            }
            if (skip)
            {
                hasSkipped = true;
            }
        }
        // Spawn east wall and possibly door
        skip = false;
        hasSkipped = false;
        for (int i = 0; i < finalRoomInfoArray[currentRoom].height; i++)
        {
            if ((finalRoomInfoArray[currentRoom].entranceDir == 3 || finalRoomInfoArray[currentRoom].exitDir == 3) && i == finalRoomInfoArray[currentRoom].height / 2 - 1)
            {
                Instantiate(finalRoomInfoArray[currentRoom].EastDoor, new Vector3(finalRoomInfoArray[currentRoom].width - 1, -((float)i + .5f), -1.4f), Quaternion.identity);
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(finalRoomInfoArray[currentRoom].width - 1, -i, 0.0f), Quaternion.identity);
                skip = true;
            }
            else if (!skip)
            {
                Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(finalRoomInfoArray[currentRoom].width - 1, -i, -1.4f), Quaternion.identity);
            }
            if (hasSkipped)
            {
                skip = false;
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(finalRoomInfoArray[currentRoom].width - 1, -i, 0.0f), Quaternion.identity);
            }
            if (skip)
            {
                hasSkipped = true;
            }
        }
        // Spawn the floor
        for (int x = 1; x < finalRoomInfoArray[currentRoom].width - 1; ++x)
        {
            for (int y = 1; y < finalRoomInfoArray[currentRoom].height - 1; ++y)
            {
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(x, -y, 0.0f), Quaternion.identity);
            }
        }
        //
        for (int i = 0; i < finalRoomInfoArray[currentRoom].innerWallPositions.Length; i++)
        {
            Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(finalRoomInfoArray[currentRoom].innerWallPositions[i].x, -finalRoomInfoArray[currentRoom].innerWallPositions[i].y, -1.4f), Quaternion.identity);
        }
        for (int i = 0; i < finalRoomInfoArray[currentRoom].hazardSpawnPoints.Length; i++)
        {
            Instantiate(finalRoomInfoArray[currentRoom].hazard, new Vector3(finalRoomInfoArray[currentRoom].hazardSpawnPoints[i].x, -finalRoomInfoArray[currentRoom].hazardSpawnPoints[i].y, -1), Quaternion.identity);
        }
        // Spawn enemies & possibly a chest
        if (!finalRoomInfoArray[currentRoom].beenThere)
        {
            Utilities.ArrayShuffle(finalRoomInfoArray[currentRoom].enemySpawnPoints);
            int min = finalRoomInfoArray[currentRoom].minEnemies;
            if (min - enemyMod > 0)
            {
                min -= enemyMod;
            }
            int max = finalRoomInfoArray[currentRoom].maxEnemies;
            if (max - enemyMod > min)
            {
                max -= enemyMod;
            }
            finalRoomInfoArray[currentRoom].numEnemies = Random.Range(min, max);
            int enemiesSpawned = 0;
            while (enemiesSpawned < finalRoomInfoArray[currentRoom].numEnemies)
            {
                for (int i = 0; i < finalRoomInfoArray[currentRoom].enemySpawnPoints.Length; i++)
                {
                    int chance = Random.Range(1, 50);
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
            float chanceychance = Random.Range(0f, 1f);
            if (chanceychance <= .3f)
            {
                bool spawned = false;
                while (!spawned)
                {
                    for (int i = 0; i < finalRoomInfoArray[currentRoom].chestSpawnLocations.Length; i++)
                    {
                        int c = Random.Range(1, 5);
                        if (c == 1)
                        {
                            Instantiate(chest, new Vector3(finalRoomInfoArray[currentRoom].chestSpawnLocations[i].x, -finalRoomInfoArray[currentRoom].chestSpawnLocations[i].y, -1f), Quaternion.identity);
                            spawned = true;
                            break;
                        }
                    }
                }
            }

        }
        //Spawn Waypoints
        for (int i = 0; i < finalRoomInfoArray[currentRoom].waypointLocations.Length; i++)
        {
            Instantiate(waypoint, new Vector3(finalRoomInfoArray[currentRoom].waypointLocations[i].x, -finalRoomInfoArray[currentRoom].waypointLocations[i].y, -1.0f), Quaternion.identity);
        }
        // Move player to entrance or exit
        if (finalRoomInfoArray[currentRoom].comingFromEntrance)
        {
            switch (finalRoomInfoArray[currentRoom].entranceDir)
            {
                case 0:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].bottomPlayerSpawn.x, -finalRoomInfoArray[currentRoom].bottomPlayerSpawn.y, -1.0f);
                    break;
                case 1:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].leftPlayerSpawn.x, -finalRoomInfoArray[currentRoom].leftPlayerSpawn.y, -1.0f);
                    break;
                case 2:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].topPlayerSpawn.x, -finalRoomInfoArray[currentRoom].topPlayerSpawn.y, -1.0f);
                    break;
                case 3:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].rightPlayerSpawn.x, -finalRoomInfoArray[currentRoom].rightPlayerSpawn.y, -1.0f);
                    break;
                default:
                    break;
            }
        }
        else if (!finalRoomInfoArray[currentRoom].comingFromEntrance)
        {
            switch (finalRoomInfoArray[currentRoom].exitDir)
            {
                case 0:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].bottomPlayerSpawn.x, -finalRoomInfoArray[currentRoom].bottomPlayerSpawn.y, -1.0f);
                    break;
                case 1:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].leftPlayerSpawn.x, -finalRoomInfoArray[currentRoom].leftPlayerSpawn.y, -1.0f);
                    break;
                case 2:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].topPlayerSpawn.x, -finalRoomInfoArray[currentRoom].topPlayerSpawn.y, -1.0f);
                    break;
                case 3:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].rightPlayerSpawn.x, -finalRoomInfoArray[currentRoom].rightPlayerSpawn.y, -1.0f);
                    break;
                default:
                    break;
            }
        }
        // make sure enemies can't respawn
        finalRoomInfoArray[currentRoom].beenThere = true;
        prevRoom = currentRoom;
    }

    void FillDungeon()
    {
        finalRoomArray[0] = floorOneRooms[0];
        finalRoomInfoArray[0] = floorOneRoomsInfo[0];
        finalRoomInfoArray[0].entranceDir = Random.Range(0, 4);
        do
        {
            finalRoomInfoArray[0].exitDir = Random.Range(0, 4);
        } while (finalRoomInfoArray[0].exitDir == finalRoomInfoArray[0].entranceDir);
        finalRoomArray[1] = floorOneRooms[1];
        finalRoomInfoArray[1] = floorOneRoomsInfo[1];
        switch (finalRoomInfoArray[0].exitDir)
        {
            case 0:
                finalRoomInfoArray[1].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[1].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[1].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[1].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[1].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[1].exitDir == finalRoomInfoArray[1].entranceDir);
        finalRoomArray[2] = floorOneMazes[0];
        finalRoomInfoArray[2] = floorOneMazesInfo[0];
        switch (finalRoomInfoArray[1].exitDir)
        {
            case 0:
                finalRoomInfoArray[2].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[2].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[2].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[2].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[2].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[2].exitDir == finalRoomInfoArray[2].entranceDir);
        finalRoomArray[3] = easyMode ? checkpointRooms[0] : floorOneRooms[2];
        finalRoomInfoArray[3] = easyMode ? checkpointRoomsInfo[0] : floorOneRoomsInfo[2];
        switch (finalRoomInfoArray[2].exitDir)
        {
            case 0:
                finalRoomInfoArray[3].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[3].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[3].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[3].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[3].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[3].exitDir == finalRoomInfoArray[3].entranceDir);
        finalRoomArray[4] = easyMode ? floorOneRooms[2] : floorOneRooms[3];
        finalRoomInfoArray[4] = easyMode ? floorOneRoomsInfo[2] : floorOneRoomsInfo[3];
        switch (finalRoomInfoArray[3].exitDir)
        {
            case 0:
                finalRoomInfoArray[4].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[4].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[4].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[4].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[4].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[4].exitDir == finalRoomInfoArray[4].entranceDir);
        finalRoomArray[5] = easyMode ? floorOneRooms[3] : floorOneMazes[1];
        finalRoomInfoArray[5] = easyMode ? floorOneRoomsInfo[3] : floorOneMazesInfo[1];
        switch (finalRoomInfoArray[4].exitDir)
        {
            case 0:
                finalRoomInfoArray[5].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[5].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[5].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[5].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[5].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[5].exitDir == finalRoomInfoArray[5].entranceDir);
        finalRoomArray[6] = easyMode ? floorOneMazes[1] : floorOneRooms[4];
        finalRoomInfoArray[6] = easyMode ? floorOneMazesInfo[1] : floorOneRoomsInfo[4];
        switch (finalRoomInfoArray[5].exitDir)
        {
            case 0:
                finalRoomInfoArray[6].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[6].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[6].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[6].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[6].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[6].exitDir == finalRoomInfoArray[6].entranceDir);
        finalRoomArray[7] = easyMode ? checkpointRooms[0] : floorOneRooms[5];
        finalRoomInfoArray[7] = easyMode ? checkpointRoomsInfo[0] : floorOneRoomsInfo[5];
        switch (finalRoomInfoArray[6].exitDir)
        {
            case 0:
                finalRoomInfoArray[7].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[7].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[7].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[7].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[7].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[7].exitDir == finalRoomInfoArray[7].entranceDir);
        finalRoomArray[8] = easyMode ? floorOneRooms[4] : dethrosRoom;
        finalRoomInfoArray[8] = easyMode ? floorOneRoomsInfo[4] : dethrosRoomInfo;
        switch (finalRoomInfoArray[7].exitDir)
        {
            case 0:
                finalRoomInfoArray[8].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[8].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[8].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[8].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[8].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[8].exitDir == finalRoomInfoArray[8].entranceDir);
        finalRoomArray[9] = easyMode ? floorOneRooms[5] : floorTwoRooms[0];
        finalRoomInfoArray[9] = easyMode ? floorOneRoomsInfo[5] : floorTwoRoomsInfo[0];
        switch (finalRoomInfoArray[8].exitDir)
        {
            case 0:
                finalRoomInfoArray[9].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[9].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[9].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[9].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[9].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[9].exitDir == finalRoomInfoArray[9].entranceDir);
        finalRoomArray[10] = easyMode ? dethrosRoom : floorTwoRooms[1];
        finalRoomInfoArray[10] = easyMode ? dethrosRoomInfo : floorTwoRoomsInfo[1];
        switch (finalRoomInfoArray[9].exitDir)
        {
            case 0:
                finalRoomInfoArray[10].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[10].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[10].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[10].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[10].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[10].exitDir == finalRoomInfoArray[10].entranceDir);
        finalRoomArray[11] = easyMode ? floorTwoRooms[0] : floorTwoMazes[0];
        finalRoomInfoArray[11] = easyMode ? floorTwoRoomsInfo[0] : floorTwoMazesInfo[0];
        switch (finalRoomInfoArray[10].exitDir)
        {
            case 0:
                finalRoomInfoArray[11].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[11].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[11].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[11].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[11].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[11].exitDir == finalRoomInfoArray[11].entranceDir);
        finalRoomArray[12] = easyMode ? floorTwoRooms[1] : floorTwoRooms[2];
        finalRoomInfoArray[12] = easyMode ? floorTwoRoomsInfo[1] : floorTwoRoomsInfo[2];
        switch (finalRoomInfoArray[11].exitDir)
        {
            case 0:
                finalRoomInfoArray[12].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[12].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[12].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[12].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[12].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[12].exitDir == finalRoomInfoArray[12].entranceDir);
        finalRoomArray[13] = easyMode ? floorTwoMazes[0] : floorTwoRooms[3];
        finalRoomInfoArray[13] = easyMode ? floorTwoMazesInfo[0] : floorTwoRoomsInfo[3];
        switch (finalRoomInfoArray[12].exitDir)
        {
            case 0:
                finalRoomInfoArray[13].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[13].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[13].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[13].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[13].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[13].exitDir == finalRoomInfoArray[13].entranceDir);
        finalRoomArray[14] = easyMode ? checkpointRooms[1] : floorTwoMazes[1];
        finalRoomInfoArray[14] = easyMode ? checkpointRoomsInfo[1] : floorTwoMazesInfo[1];
        switch (finalRoomInfoArray[13].exitDir)
        {
            case 0:
                finalRoomInfoArray[14].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[14].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[14].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[14].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[14].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[14].exitDir == finalRoomInfoArray[14].entranceDir);
        finalRoomArray[15] = easyMode ? floorTwoRooms[2] : floorTwoRooms[4];
        finalRoomInfoArray[15] = easyMode ? floorTwoRoomsInfo[2] : floorTwoRoomsInfo[4];
        switch (finalRoomInfoArray[14].exitDir)
        {
            case 0:
                finalRoomInfoArray[15].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[15].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[15].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[15].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[15].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[15].exitDir == finalRoomInfoArray[15].entranceDir);
        finalRoomArray[16] = easyMode ? floorTwoRooms[3] : floorTwoRooms[5];
        finalRoomInfoArray[16] = easyMode ? floorTwoRoomsInfo[3] : floorTwoRoomsInfo[5];
        switch (finalRoomInfoArray[15].exitDir)
        {
            case 0:
                finalRoomInfoArray[16].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[16].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[16].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[16].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[16].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[16].exitDir == finalRoomInfoArray[16].entranceDir);
        finalRoomArray[17] = easyMode ? floorTwoMazes[1] : lorneRoom;
        finalRoomInfoArray[17] = easyMode ? floorTwoMazesInfo[1] : lorneRoomInfo;
        switch (finalRoomInfoArray[16].exitDir)
        {
            case 0:
                finalRoomInfoArray[17].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[17].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[17].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[17].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[17].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[17].exitDir == finalRoomInfoArray[17].entranceDir);

        //
        if (easyMode)
        {
            finalRoomArray[18] = checkpointRooms[1];
            finalRoomInfoArray[18] = checkpointRoomsInfo[1];
            switch (finalRoomInfoArray[17].exitDir)
            {
                case 0:
                    finalRoomInfoArray[18].entranceDir = 2;
                    break;
                case 1:
                    finalRoomInfoArray[18].entranceDir = 3;
                    break;
                case 2:
                    finalRoomInfoArray[18].entranceDir = 0;
                    break;
                case 3:
                    finalRoomInfoArray[18].entranceDir = 1;
                    break;
                default:
                    break;
            }
            do
            {
                finalRoomInfoArray[18].exitDir = Random.Range(0, 3);
            } while (finalRoomInfoArray[18].exitDir == finalRoomInfoArray[18].entranceDir);
            finalRoomArray[19] = floorTwoRooms[4];
            finalRoomInfoArray[19] = floorTwoRoomsInfo[4];
            switch (finalRoomInfoArray[18].exitDir)
            {
                case 0:
                    finalRoomInfoArray[19].entranceDir = 2;
                    break;
                case 1:
                    finalRoomInfoArray[19].entranceDir = 3;
                    break;
                case 2:
                    finalRoomInfoArray[19].entranceDir = 0;
                    break;
                case 3:
                    finalRoomInfoArray[19].entranceDir = 1;
                    break;
                default:
                    break;
            }
            do
            {
                finalRoomInfoArray[19].exitDir = Random.Range(0, 3);
            } while (finalRoomInfoArray[19].exitDir == finalRoomInfoArray[19].entranceDir);
            finalRoomArray[20] = floorTwoRooms[5];
            finalRoomInfoArray[20] = floorTwoRoomsInfo[5];
            switch (finalRoomInfoArray[19].exitDir)
            {
                case 0:
                    finalRoomInfoArray[20].entranceDir = 2;
                    break;
                case 1:
                    finalRoomInfoArray[20].entranceDir = 3;
                    break;
                case 2:
                    finalRoomInfoArray[20].entranceDir = 0;
                    break;
                case 3:
                    finalRoomInfoArray[20].entranceDir = 1;
                    break;
                default:
                    break;
            }
            do
            {
                finalRoomInfoArray[20].exitDir = Random.Range(0, 3);
            } while (finalRoomInfoArray[20].exitDir == finalRoomInfoArray[20].entranceDir);
            finalRoomArray[21] = lorneRoom;
            finalRoomInfoArray[21] = lorneRoomInfo;
            switch (finalRoomInfoArray[20].exitDir)
            {
                case 0:
                    finalRoomInfoArray[21].entranceDir = 2;
                    break;
                case 1:
                    finalRoomInfoArray[21].entranceDir = 3;
                    break;
                case 2:
                    finalRoomInfoArray[21].entranceDir = 0;
                    break;
                case 3:
                    finalRoomInfoArray[21].entranceDir = 1;
                    break;
                default:
                    break;
            }
            do
            {
                finalRoomInfoArray[21].exitDir = Random.Range(0, 3);
            } while (finalRoomInfoArray[21].exitDir == finalRoomInfoArray[21].entranceDir);
        }
        //
        //finalRoomArray[17] = lorneRoom;
        //finalRoomInfoArray[17] = lorneRoomInfo;
        //switch (finalRoomInfoArray[16].exitDir)
        //{
        //    case 0:
        //        finalRoomInfoArray[17].entranceDir = 2;
        //        break;
        //    case 1:
        //        finalRoomInfoArray[17].entranceDir = 3;
        //        break;
        //    case 2:
        //        finalRoomInfoArray[17].entranceDir = 0;
        //        break;
        //    case 3:
        //        finalRoomInfoArray[17].entranceDir = 1;
        //        break;
        //    default:
        //        break;
        //}
        //do
        //{
        //    finalRoomInfoArray[17].exitDir = Random.Range(0, 3);
        //} while (finalRoomInfoArray[17].exitDir == finalRoomInfoArray[17].entranceDir);
        //finalRoomArray[18] = floorThreeRooms[0];
        //finalRoomInfoArray[18] = floorTwoRoomsInfo[0];
        //switch (finalRoomInfoArray[17].exitDir)
        //{
        //    case 0:
        //        finalRoomInfoArray[18].entranceDir = 2;
        //        break;
        //    case 1:
        //        finalRoomInfoArray[18].entranceDir = 3;
        //        break;
        //    case 2:
        //        finalRoomInfoArray[18].entranceDir = 0;
        //        break;
        //    case 3:
        //        finalRoomInfoArray[18].entranceDir = 1;
        //        break;
        //    default:
        //        break;
        //}
        //do
        //{
        //    finalRoomInfoArray[18].exitDir = Random.Range(0, 3);
        //} while (finalRoomInfoArray[18].exitDir == finalRoomInfoArray[18].entranceDir);
        //finalRoomArray[19] = floorThreeRooms[1];
        //finalRoomInfoArray[19] = floorTwoRoomsInfo[1];
        //switch (finalRoomInfoArray[18].exitDir)
        //{
        //    case 0:
        //        finalRoomInfoArray[19].entranceDir = 2;
        //        break;
        //    case 1:
        //        finalRoomInfoArray[19].entranceDir = 3;
        //        break;
        //    case 2:
        //        finalRoomInfoArray[19].entranceDir = 0;
        //        break;
        //    case 3:
        //        finalRoomInfoArray[19].entranceDir = 1;
        //        break;
        //    default:
        //        break;
        //}
        //do
        //{
        //    finalRoomInfoArray[19].exitDir = Random.Range(0, 3);
        //} while (finalRoomInfoArray[19].exitDir == finalRoomInfoArray[19].entranceDir);
        //finalRoomArray[20] = floorThreeMazes[0];
        //finalRoomInfoArray[20] = floorTwoMazesInfo[0];
        //switch (finalRoomInfoArray[19].exitDir)
        //{
        //    case 0:
        //        finalRoomInfoArray[20].entranceDir = 2;
        //        break;
        //    case 1:
        //        finalRoomInfoArray[20].entranceDir = 3;
        //        break;
        //    case 2:
        //        finalRoomInfoArray[20].entranceDir = 0;
        //        break;
        //    case 3:
        //        finalRoomInfoArray[20].entranceDir = 1;
        //        break;
        //    default:
        //        break;
        //}
        //do
        //{
        //    finalRoomInfoArray[20].exitDir = Random.Range(0, 3);
        //} while (finalRoomInfoArray[20].exitDir == finalRoomInfoArray[20].entranceDir);
        //finalRoomArray[21] = floorThreeRooms[2];
        //finalRoomInfoArray[21] = floorTwoRoomsInfo[2];
        //switch (finalRoomInfoArray[20].exitDir)
        //{
        //    case 0:
        //        finalRoomInfoArray[21].entranceDir = 2;
        //        break;
        //    case 1:
        //        finalRoomInfoArray[21].entranceDir = 3;
        //        break;
        //    case 2:
        //        finalRoomInfoArray[21].entranceDir = 0;
        //        break;
        //    case 3:
        //        finalRoomInfoArray[21].entranceDir = 1;
        //        break;
        //    default:
        //        break;
        //}
        //do
        //{
        //    finalRoomInfoArray[21].exitDir = Random.Range(0, 3);
        //} while (finalRoomInfoArray[21].exitDir == finalRoomInfoArray[21].entranceDir);
        //finalRoomArray[22] = floorThreeRooms[3];
        //finalRoomInfoArray[22] = floorTwoRoomsInfo[3];
        //switch (finalRoomInfoArray[21].exitDir)
        //{
        //    case 0:
        //        finalRoomInfoArray[22].entranceDir = 2;
        //        break;
        //    case 1:
        //        finalRoomInfoArray[22].entranceDir = 3;
        //        break;
        //    case 2:
        //        finalRoomInfoArray[22].entranceDir = 0;
        //        break;
        //    case 3:
        //        finalRoomInfoArray[22].entranceDir = 1;
        //        break;
        //    default:
        //        break;
        //}
        //do
        //{
        //    finalRoomInfoArray[22].exitDir = Random.Range(0, 3);
        //} while (finalRoomInfoArray[22].exitDir == finalRoomInfoArray[22].entranceDir);
        //finalRoomArray[23] = floorThreeMazes[1];
        //finalRoomInfoArray[23] = floorThreeMazesInfo[1];
        //switch (finalRoomInfoArray[22].exitDir)
        //{
        //    case 0:
        //        finalRoomInfoArray[23].entranceDir = 2;
        //        break;
        //    case 1:
        //        finalRoomInfoArray[23].entranceDir = 3;
        //        break;
        //    case 2:
        //        finalRoomInfoArray[23].entranceDir = 0;
        //        break;
        //    case 3:
        //        finalRoomInfoArray[23].entranceDir = 1;
        //        break;
        //    default:
        //        break;
        //}
        //do
        //{
        //    finalRoomInfoArray[23].exitDir = Random.Range(0, 3);
        //} while (finalRoomInfoArray[23].exitDir == finalRoomInfoArray[23].entranceDir);
        //finalRoomArray[24] = floorThreeRooms[4];
        //finalRoomInfoArray[24] = floorTwoRoomsInfo[4];
        //switch (finalRoomInfoArray[23].exitDir)
        //{
        //    case 0:
        //        finalRoomInfoArray[24].entranceDir = 2;
        //        break;
        //    case 1:
        //        finalRoomInfoArray[24].entranceDir = 3;
        //        break;
        //    case 2:
        //        finalRoomInfoArray[24].entranceDir = 0;
        //        break;
        //    case 3:
        //        finalRoomInfoArray[24].entranceDir = 1;
        //        break;
        //    default:
        //        break;
        //}
        //do
        //{
        //    finalRoomInfoArray[24].exitDir = Random.Range(0, 3);
        //} while (finalRoomInfoArray[24].exitDir == finalRoomInfoArray[24].entranceDir);
        //finalRoomArray[25] = floorThreeRooms[5];
        //finalRoomInfoArray[25] = floorTwoRoomsInfo[5];
        //switch (finalRoomInfoArray[24].exitDir)
        //{
        //    case 0:
        //        finalRoomInfoArray[25].entranceDir = 2;
        //        break;
        //    case 1:
        //        finalRoomInfoArray[25].entranceDir = 3;
        //        break;
        //    case 2:
        //        finalRoomInfoArray[25].entranceDir = 0;
        //        break;
        //    case 3:
        //        finalRoomInfoArray[25].entranceDir = 1;
        //        break;
        //    default:
        //        break;
        //}
        //do
        //{
        //    finalRoomInfoArray[25].exitDir = Random.Range(0, 3);
        //} while (finalRoomInfoArray[25].exitDir == finalRoomInfoArray[25].entranceDir);
        //finalRoomArray[26] = morriusRoom;
        //finalRoomInfoArray[26] = morriusRoomInfo;
        //switch (finalRoomInfoArray[25].exitDir)
        //{
        //    case 0:
        //        finalRoomInfoArray[26].entranceDir = 2;
        //        break;
        //    case 1:
        //        finalRoomInfoArray[26].entranceDir = 3;
        //        break;
        //    case 2:
        //        finalRoomInfoArray[26].entranceDir = 0;
        //        break;
        //    case 3:
        //        finalRoomInfoArray[26].entranceDir = 1;
        //        break;
        //    default:
        //        break;
        //}
        //do
        //{
        //    finalRoomInfoArray[26].exitDir = Random.Range(0, 3);
        //} while (finalRoomInfoArray[26].exitDir == finalRoomInfoArray[26].entranceDir);

        for (int i = 0; i < finalRoomInfoArray.Length; i++)
        {
            finalRoomInfoArray[i].comingFromEntrance = true;
        }
    }

    public void Reset()
    {
        GameObject[] objArray = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objArray)
        {
            if (obj.name.Contains("Wall") || obj.name.Contains("Floor") || obj.name.Contains("Hazard") || obj.name.Contains("Door")
                || obj.tag.Contains("Drop") || obj.tag == "LightTrail" || obj.name.Contains("Pickup") || obj.name.Contains("dead")
                || obj.name.Contains("Burn") || obj.tag == "Waypoint" || obj.name.Contains("Spawn") || obj.name.Contains("lone"))
            {
                Destroy(obj);
            }
        }
        CreateRoom();
    }
}
