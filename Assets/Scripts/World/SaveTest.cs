using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveTest : MonoBehaviour {

	GameObject player;
	public float life;
	public float lightss;
    public bool shouldload = false;
    GameObject dungeon;
    RoomGeneration theRooms;
    Options options;
    public bool shouldsave = true;


	// Use this for initialization
	void Start () {
        options = GameObject.FindObjectOfType<Options>();
        player = GameObject.FindGameObjectWithTag("Player");
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        theRooms = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomGeneration>(); 
        if (shouldload == true)
        {
            Load();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (dungeon == null)
        {
            dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            if (dungeon != null)
                theRooms = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomGeneration>();
        }

        else
            if (theRooms != null)
                if (theRooms.finalRoomInfoArray[theRooms.currentRoom].numEnemies == 0&&shouldsave==true)
                    
                    {
                        Save();
                        shouldsave = false;
                    }

                    else
                        shouldsave = true;

	
	}
 
   public void Save()
    {
        if (Application.platform == RuntimePlatform.OSXWebPlayer
           || Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            PlayerPrefs.SetFloat("PlayerHealth", gameObject.GetComponent<Health>().currentHP);
            PlayerPrefs.SetFloat("PlayerLight", gameObject.GetComponent<PlayerLight>().currentLight);
            PlayerPrefs.SetInt("EasyMode", options.easyMode.GetHashCode());
            PlayerPrefs.SetInt("RoomArrLenght", theRooms.finalRoomInfoArray.Length);
            for (int i = 0; i < theRooms.finalRoomInfoArray.Length; i++)
            {
                string temp= "Room_"+i.ToString()+" been there";
                 string temps= "RoomID_"+i.ToString();
                PlayerPrefs.SetInt(temp, theRooms.finalRoomInfoArray[i].beenThere.GetHashCode());
                PlayerPrefs.SetInt(temps, theRooms.finalRoomInfoArray[i].roomID);
                
            }

        }
        else
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerinfo.dat");
            PlayerData data = new PlayerData();
            data.health = GetComponent<Health>().currentHP;
            data.theLight = gameObject.GetComponent<PlayerLight>().currentLight;
            data.easymode = options.easyMode;
            data.finalRoomInfoArray = theRooms.finalRoomInfoArray;
            bin.Serialize(file, data);
            file.Close();
        }

    }


   public void Load()
   {
       if (Application.platform == RuntimePlatform.OSXWebPlayer
   || Application.platform == RuntimePlatform.WindowsWebPlayer)
       {
      gameObject.GetComponent<Health>().currentHP=      PlayerPrefs.GetFloat("PlayerHealth", 100);
     gameObject.GetComponent<PlayerLight>().currentLight=      PlayerPrefs.GetFloat("PlayerLight", 100);
     int teemplenght = PlayerPrefs.GetInt("RoomArrLenght", 0);
     theRooms.finalRoomInfoArray = new Room[teemplenght];
     for (int i = 0; i < teemplenght; i++)
     {
               string temp= "Room_"+i.ToString()+" been there";
                 string temps= "RoomID_"+i.ToString();
         theRooms.finalRoomInfoArray[i].beenThere=     PlayerPrefs.GetInt(temp)==1?true:false;
               theRooms.finalRoomInfoArray[i].roomID= PlayerPrefs.GetInt(temps);
     }
     options.easyMode = PlayerPrefs.GetInt("EasyMode") == 1 ? true : false;

       }
       else
       {
           if (File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
           {
               BinaryFormatter bin = new BinaryFormatter();
               FileStream file = File.Open(Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);
               PlayerData data = (PlayerData)bin.Deserialize(file);
               player.GetComponent<Health>().currentHP = data.health;
               player.GetComponent<PlayerLight>().currentLight = data.theLight;
               options.easyMode = data.easymode;
                theRooms.finalRoomInfoArray=data.finalRoomInfoArray;
               file.Close();
           

           }
       }
   }
}
[System.Serializable]
class PlayerData
{
	public float health;
    public float theLight;
    PlayerStats stats;
    public bool easymode;
   public  Room[] finalRoomInfoArray;



}