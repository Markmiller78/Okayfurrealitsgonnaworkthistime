using UnityEngine;
using System.Collections;

public class MapAndStatsMenu : MonoBehaviour
{
    bool showing;
    GameObject player;
    PlayerEquipment equipment;
    GameObject canvas;
    GUIStyle style;
    GUIStyle crStyle;
    GameObject dungeon;
    RoomGeneration generator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        equipment = player.GetComponent<PlayerEquipment>();
        canvas = GameObject.Find("Map&StatsCanvas");
        canvas.SetActive(false);
        showing = false;

        Texture2D texture = new Texture2D(1, 1);
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.SetPixel(1, 1, new Color(.75f, .75f, .75f));
        texture.Apply();
        style = new GUIStyle();
        style.normal.background = texture;

        Texture2D texture2 = new Texture2D(1, 1);
        texture2.wrapMode = TextureWrapMode.Repeat;
        texture2.SetPixel(1, 1, Color.blue);
        texture2.Apply();
        crStyle = new GUIStyle();
        crStyle.normal.background = texture2;

        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        generator = dungeon.GetComponent<RoomGeneration>();
    }

    void Update()
    {

    }

    void OnGUI()
    {
        if (showing)
        {
            Vector2 offset = new Vector2((Screen.width / 2) - (generator.finalRoomInfoArray[0].width / 2) * 5,
                (Screen.height / 2) - (generator.finalRoomInfoArray[0].height / 2) * 5);
            for (int i = 1; i <= generator.currentRoom; i++)
            {
                switch (generator.finalRoomInfoArray[i -1].exitDir)
                    {
                        case 0:
                            offset.y += 25 + generator.finalRoomInfoArray[i - 1].height * 5;
                            break;
                        case 1:
                            offset.x += 25 + generator.finalRoomInfoArray[i - 1].width * 5;
                            break;
                        case 2:
                            offset.y -= 25 + generator.finalRoomInfoArray[i - 1].height * 5;
                            break;
                        case 3:
                            offset.x -= 25 + generator.finalRoomInfoArray[i - 1].width * 5;
                            break;
                        default:
                            break;
                    }
            }
            for (int i = 0; i < generator.finalRoomInfoArray.Length; i++)
            {
                if (generator.finalRoomInfoArray[i].beenThere)
                {
                    GUI.Label(
                        new Rect(offset.x,
                            Screen.height - (offset.y + generator.finalRoomInfoArray[i].height * 5),
                        generator.finalRoomInfoArray[i].width * 5,
                        generator.finalRoomInfoArray[i].height * 5),
                        "", i == generator.currentRoom ? crStyle : style);
                    switch (generator.finalRoomInfoArray[i].exitDir)
                    {
                        case 0:
                            offset.y -= 25 + generator.finalRoomInfoArray[i].height * 5;
                            break;
                        case 1:
                            offset.x -= 25 + generator.finalRoomInfoArray[i].width * 5;
                            break;
                        case 2:
                            offset.y += 25 + generator.finalRoomInfoArray[i].height * 5;
                            break;
                        case 3:
                            offset.x += 25 + generator.finalRoomInfoArray[i].width * 5;
                            break;
                        default:
                            break;
                    }
                    //offset.x += (20 + generator.finalRoomInfoArray[i].width * 5) + 5;
                }
            }
            //for (int i = 0; i < generator.finalRoomInfoArray.Length; i++)
            //{
            //    //GUI.Label(new Rect(i * 10, i * 10, 10, 10), "", style);
            //}
        }
    }

    void MapAndStats()
    {
        showing = true;
        canvas.SetActive(true);
        equipment.paused = true;
    }

    void UnMapAndStats()
    {
        showing = false;
        canvas.SetActive(false);
        equipment.paused = false;
    }
}
