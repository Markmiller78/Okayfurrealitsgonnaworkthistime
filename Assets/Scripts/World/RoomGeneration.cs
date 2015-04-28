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

        finalRoomArray = new GameObject[9];
        finalRoomInfoArray = new Room[9];
        FillDungeon();

        //TESTING
        //finalRoomArray[0] = floorOneMazes[3];
        //finalRoomInfoArray[0] = floorOneMazesInfo[3];
        //ENDTESTING

        CreateRoom();
        //Reset();
    }

    void CreateRoom()
    {
        //for (int y = 0; y < finalRoomInfoArray[currentRoom].height; y++)
        //{
        //    bool skip = false;
        //    bool hasSkipped = false;
        //    for (int x = 0; x < finalRoomInfoArray[currentRoom].width; x++)
        //    {
        //        if (x == 0 || x == finalRoomInfoArray[currentRoom].width - 1 || y == 0 || y == finalRoomInfoArray[currentRoom].height - 1)
        //        {
        //            if (x == finalRoomInfoArray[currentRoom].width / 2 - 1)
        //            {
        //                Instantiate(finalRoomInfoArray[currentRoom].door, new Vector3(x + .5f, -y, -1.4f), Quaternion.identity);
        //                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(x, -y, 0.0f), Quaternion.identity);
        //                skip = true;
        //            }
        //            else if (!skip)
        //                Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(x, -y, -1.4f), Quaternion.identity);
        //        }
        //        else
        //        {
        //            Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(x, -y, 0.0f), Quaternion.identity);
        //        }
        //        if (hasSkipped)
        //        {
        //            skip = false;
        //            Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(x, -y, 0.0f), Quaternion.identity);
        //        }
        //        if (skip)
        //        {
        //            hasSkipped = true;
        //        }
        //    }
        //}

        bool skip = false;
        bool hasSkipped = false;
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
        for (int x = 1; x < finalRoomInfoArray[currentRoom].width - 1; ++x)
        {
            for (int y = 1; y < finalRoomInfoArray[currentRoom].height - 1; ++y)
            {
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(x, -y, 0.0f), Quaternion.identity);
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
        //player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].bottomPlayerSpawn.x, -finalRoomInfoArray[currentRoom].bottomPlayerSpawn.y, -1.0f);
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
        finalRoomArray[3] = floorOneRooms[2];
        finalRoomInfoArray[3] = floorOneRoomsInfo[2];
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
        finalRoomArray[4] = floorOneRooms[3];
        finalRoomInfoArray[4] = floorOneRoomsInfo[3];
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
        finalRoomArray[5] = floorOneMazes[1];
        finalRoomInfoArray[5] = floorOneMazesInfo[1];
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
        finalRoomArray[6] = floorOneRooms[4];
        finalRoomInfoArray[6] = floorOneRoomsInfo[4];
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
        finalRoomArray[7] = floorOneRooms[5];
        finalRoomInfoArray[7] = floorOneRoomsInfo[5];
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
        finalRoomArray[8] = dethrosRoom;
        finalRoomInfoArray[8] = dethrosRoomInfo;
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
            if (obj.name.Contains("Wall") || obj.name.Contains("Floor") || obj.name.Contains("Hazard") || obj.name.Contains("Door"))
            {
                Destroy(obj);
            }
        }
        CreateRoom();
    }
}
