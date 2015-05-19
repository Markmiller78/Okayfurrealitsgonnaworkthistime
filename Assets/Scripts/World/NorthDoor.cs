using UnityEngine;
using System.Collections;

public class NorthDoor : MonoBehaviour
{
    public bool isLocked = true;
    public GameObject dungeon;
    RoomGeneration generator;
    GameObject player;
    public bool displaytooltips = false;
    int enemyCount;
    bool iHopeThisWorks;
    bool easyMode;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        generator = dungeon.GetComponent<RoomGeneration>();
        isLocked = true;
        iHopeThisWorks = true;
        easyMode = GameObject.FindObjectOfType<Options>().easyMode;
    }

    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("ShadowSpawn").Length;
        if (isLocked &&
            ((generator.currentRoom != 0 && generator.currentRoom != (easyMode ? 11 : 9) && generator.currentRoom != (easyMode ? 22 : 18)
            && generator.finalRoomInfoArray[generator.currentRoom].entranceDir == 2)
            || (generator.currentRoom < (easyMode ? 33 : 27) && generator.finalRoomInfoArray[generator.currentRoom].exitDir == 2))
            && enemyCount == 0)
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
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (iHopeThisWorks)
        {
            if (other.gameObject == player
                && generator.finalRoomInfoArray[generator.currentRoom].exitDir == 2
                && generator.currentRoom < (easyMode ? 33 : 27))
            {
                ++generator.currentRoom;
                generator.finalRoomInfoArray[generator.currentRoom].comingFromEntrance = true;
                generator.Reset();
                iHopeThisWorks = false;
            }
            else if (other.gameObject == player && generator.currentRoom > 0)
            {
                --generator.currentRoom;
                generator.finalRoomInfoArray[generator.currentRoom].comingFromEntrance = false;
                generator.Reset();
                iHopeThisWorks = false;
            }
        }
    }


}

