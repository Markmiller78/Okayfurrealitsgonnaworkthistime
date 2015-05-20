using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

using System.IO;

public class SaveTest : MonoBehaviour {

	GameObject player;
	public float life;
	public float lightss;
    public bool shouldload = false;
    GameObject dungeon;
    RoomGeneration theRooms;
    Options options;
    PlayerStats theStats;
    public bool shouldsave = true;
    public int enemies=0;
    public bool saved = false;
	PlayerEquipment eq;
	 


	// Use this for initialization
	void Start () {
		eq = GetComponent<PlayerEquipment> ();
        options = GameObject.FindObjectOfType<Options>();
        player = GameObject.FindGameObjectWithTag("Player");
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        theRooms = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomGeneration>();
        theStats = GetComponent<PlayerStats>();
		if (options.shouldload == true)
			shouldload = true;
        if (shouldload == true)
        {
            Load();
        }
	}
	
	// Update is called once per frame
	void Update () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (dungeon == null)
        {
            dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            if (dungeon != null)
                theRooms = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomGeneration>();
        }

        else
            if (theRooms != null)
                if (enemies <= 0&&shouldsave==true&&saved==false)
                    
                    {
                        Save();
                        Debug.Log("Saved!");
                        shouldsave = false;
                        saved = true;
                    }

                    else
                        shouldsave = true;

	
	}
 
   public void Save()
    {
        if (Application.platform == RuntimePlatform.OSXWebPlayer
           || Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
			Debug.Log("OK Save");
			
            PlayerPrefs.SetFloat("PlayerHealth", gameObject.GetComponent<Health>().currentHP);
            PlayerPrefs.SetFloat("PlayerLight", gameObject.GetComponent<PlayerLight>().currentLight);
			PlayerPrefs.SetFloat("MeleeMod",theStats.meleeModifier);
			PlayerPrefs.SetFloat("SpellMod",theStats.spellModifier);
			PlayerPrefs.SetFloat("LightMod",theStats.maxLightModifier);
			PlayerPrefs.SetFloat("LifeMod",theStats.maxHPModifier);

            PlayerPrefs.SetInt("EasyMode", options.easyMode.GetHashCode());
			PlayerPrefs.SetInt("Ember",(int)eq.equippedEmber);
			PlayerPrefs.SetInt("Boot",(int)eq.equippedBoot);
			PlayerPrefs.SetInt("CurrentRoom", theRooms.currentRoom);

            PlayerPrefs.SetInt("RoomArrLenght", theRooms.finalRoomInfoArray.Length);
		 

            for (int i = 0; i < theRooms.finalRoomInfoArray.Length; i++)
            {
                string temp= "Room_"+i.ToString()+"_been there";
                string temps= "RoomID_"+i.ToString();
				string tempsss= "RoomExitDir_"+ i.ToString();
				string tempss= "RoomEntryDir_"+i.ToString();

                PlayerPrefs.SetInt(temp, theRooms.finalRoomInfoArray[i].beenThere.GetHashCode());
                PlayerPrefs.SetInt(temps, theRooms.finalRoomInfoArray[i].roomID);
				PlayerPrefs.SetInt(tempss,theRooms.finalRoomInfoArray[i].entranceDir);
				PlayerPrefs.SetInt(tempsss,theRooms.finalRoomInfoArray[i].exitDir);
				

                
            }

        }
        else
        {
			     FileStream file ;
               //  XmlSerializer xml = new XmlSerializer(typeof(PlayerData));
            BinaryFormatter bin = new BinaryFormatter();
            if (!File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
                file = File.Create(Application.persistentDataPath + "/playerinfo.dat");
            else
                file = File.Open(Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);
         
            PlayerData data = new PlayerData();
            data.health = GetComponent<Health>().currentHP;
            data.theLight = gameObject.GetComponent<PlayerLight>().currentLight;
            data.easymode = options.easyMode;
            data.maxHPModifier = theStats.maxHPModifier;
            data.maxLightModifier = theStats.maxLightModifier;
            data.meleeModifier = theStats.meleeModifier;
            data.spellModifier = theStats.spellModifier;
			data.equippedember= (int)eq.equippedEmber;
			data.equippedboot= (int)eq.equippedBoot;
			data.currentroom= theRooms.currentRoom;

            CopyRooms(data);
           // data.finalRoomInfoArray = theRooms.finalRoomInfoArray;
            bin.Serialize(file, data);
           // xml.Serialize(file, data);
            file.Close();
        
	 
		}

    }


   public void Load()
   {
       if (Application.platform == RuntimePlatform.OSXWebPlayer
   || Application.platform == RuntimePlatform.WindowsWebPlayer)
       {
			Debug.Log("OK Load");
			//Loading Hp and Light

      gameObject.GetComponent<Health>().currentHP=      PlayerPrefs.GetFloat("PlayerHealth", 100);
      gameObject.GetComponent<PlayerLight>().currentLight=      PlayerPrefs.GetFloat("PlayerLight", 100);
			//Loading stats
		theStats.meleeModifier   =	PlayerPrefs.GetFloat("MeleeMod",0 );
		theStats.spellModifier   =	PlayerPrefs.GetFloat("SpellMod",0 );
		theStats.maxLightModifier=	PlayerPrefs.GetFloat("LightMod",0 );
		theStats.maxHPModifier   =	PlayerPrefs.GetFloat("LifeMod", 0 );
			PlayerData data= new PlayerData();

			//Loading boot and embers
		eq.equippedEmber=(ember)	PlayerPrefs.GetInt("Ember",0);
		eq.equippedBoot  =	(boot)PlayerPrefs.GetInt("Boot",0 );
			data.currentroom= PlayerPrefs.GetInt("CurrentRoom", 0);
			//Loading the amount of rooms in the roomgenerator
     int teemplenght = PlayerPrefs.GetInt("RoomArrLenght", 0);
			Debug.Log(teemplenght);
			options.easyMode = PlayerPrefs.GetInt("EasyMode",1) == 1 ? true : false; 
     if (teemplenght != 0)
     {
			
        data.roominfo= new RoomData[teemplenght];
         for (int i = 0; i < teemplenght; i++)
         {
					data.roominfo[i]= new RoomData();
             string temp = "Room_" + i.ToString() + "_been there";
             string temps = "RoomID_" + i.ToString();
			 string tempsss= "RoomExitDir_"+ i.ToString();
			 string tempss= "RoomEntryDir_"+i.ToString();

             data.roominfo[i].beenThere = PlayerPrefs.GetInt(temp) == 1 ? true : false;
             data.roominfo[i].roomID = PlayerPrefs.GetInt(temps);
			 data.roominfo[i].entranceDir= PlayerPrefs.GetInt(tempss,0);
			 data.roominfo[i].exitDir=PlayerPrefs.GetInt(tempsss,0);
         }

				theRooms.currentRoom= data.currentroom;
				theRooms.loadedData=data;
     }


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

               theStats.maxHPModifier = data.maxHPModifier;
               theStats.maxLightModifier = data.maxLightModifier;
               theStats.meleeModifier = data.meleeModifier;
               theStats.spellModifier = data.spellModifier;

				eq.equippedEmber= data.equippedember;
				eq.equippedBoot= data.equippedboot; 

                options.easyMode = data.easymode;

				theRooms.currentRoom= data.currentroom;
				theRooms.loadedData= data;
		
     
    
               file.Close();
           

           }
       }
   }
    void CopyRooms(PlayerData data)
   {
       data.amountofrooms = theRooms.finalRoomInfoArray.Length;
       data.roominfo = new RoomData[data.amountofrooms];
       for (int i = 0; i < data.amountofrooms; i++)
       {
		   data.roominfo[i]            = new RoomData();
        //data.roominfo[i].width      = theRooms.finalRoomInfoArray[i].width;
        //data.roominfo[i].height     = theRooms.finalRoomInfoArray[i].height;
           // data.wallTiles               = theRooms.finalRoomInfoArray[0].wallTiles;
           //  data.floorTiles              = theRooms.finalRoomInfoArray[0].floorTiles; 
           // data.SouthDoor               = theRooms.finalRoomInfoArray[0].SouthDoor;
           // data.NorthDoor               = theRooms.finalRoomInfoArray[0].NorthDoor;
           //  data.EastDoor                = theRooms.finalRoomInfoArray[0].EastDoor;
           //  data.WestDoor                = theRooms.finalRoomInfoArray[0].WestDoor; 
           //   data.innerWallPositions      = theRooms.finalRoomInfoArray[0].innerWallPositions;
           //  data.enemiesThatCanSpawn     = theRooms.finalRoomInfoArray[0].enemiesThatCanSpawn;
           //  data.enemySpawnPoints        = theRooms.finalRoomInfoArray[0].enemySpawnPoints; 
           //  data.enemySpawnPointUsed     = theRooms.finalRoomInfoArray[0].enemySpawnPointUsed;
          // data.roominfo[i].minEnemies = theRooms.finalRoomInfoArray[i].minEnemies;
          // data.roominfo[i].maxEnemies = theRooms.finalRoomInfoArray[i].maxEnemies;
           //  data.hazard                  = theRooms.finalRoomInfoArray[0].hazard;
           //  data.hazardSpawnPoints       = theRooms.finalRoomInfoArray[0].hazardSpawnPoints;
           //  data.bottomPlayerSpawn = new Vector2(0, 0);
           //  data.bottomPlayerSpawn       = theRooms.finalRoomInfoArray[0].bottomPlayerSpawn;
           //data.rightPlayerSpawn        = theRooms.finalRoomInfoArray[0].rightPlayerSpawn;
           //data.topPlayerSpawn          = theRooms.finalRoomInfoArray[0].topPlayerSpawn;
           //data.leftPlayerSpawn         = theRooms.finalRoomInfoArray[0].leftPlayerSpawn;
          data.roominfo[i].entranceDir = theRooms.finalRoomInfoArray[i].entranceDir;
          data.roominfo[i].exitDir     = theRooms.finalRoomInfoArray[i].exitDir;
           //  data.chestSpawnLocations     = theRooms.finalRoomInfoArray[0].chestSpawnLocations;  
           data.roominfo[i].beenThere   = theRooms.finalRoomInfoArray[i].beenThere;
  //        data.roominfo[i].numEnemies  = theRooms.finalRoomInfoArray[i].numEnemies;
  // data.roominfo[i].comingFromEntrance = theRooms.finalRoomInfoArray[i].comingFromEntrance;
           //  data.waypointLocations       = theRooms.finalRoomInfoArray[0].waypointLocations;
           data.roominfo[i].roomID      = theRooms.finalRoomInfoArray[i].roomID;
       }

   }

	
	void LoadRooms(PlayerData data)
	{
	 theRooms.finalRoomInfoArray=new Room[data.amountofrooms];
	 
		for (int i = 0; i < data.amountofrooms; i++)
		{
	        
			//theRooms.finalRoomInfoArray[i].width=                             	data.roominfo[i].width ;        
			//theRooms.finalRoomInfoArray[i].height=                            	data.roominfo[i].height ;      
			//theRooms.finalRoomInfoArray[i].minEnemies=                        	data.roominfo[i].minEnemies ;
			//theRooms.finalRoomInfoArray[i].maxEnemies=                        	data.roominfo[i].maxEnemies ;
			//theRooms.finalRoomInfoArray[i].entranceDir=                      	data.roominfo[i].entranceDir;
			//theRooms.finalRoomInfoArray[i].exitDir=                          	data.roominfo[i].exitDir ;   
			theRooms.finalRoomInfoArray[i].beenThere=                       	data.roominfo[i].beenThere ; 
		    //theRooms.finalRoomInfoArray[i].numEnemies=                       	data.roominfo[i].numEnemies ;
			//theRooms.finalRoomInfoArray[i].comingFromEntrance=               data.roominfo[i].comingFromEntrance;
			theRooms.finalRoomInfoArray[i].roomID=data.roominfo[i].roomID     ;
		}


	}
}

[System.Serializable]
public class PlayerData
{
	public float health;
    public float theLight;
    public float meleeModifier;
    public float spellModifier ;
    public float maxHPModifier ;
    public float maxLightModifier ;
    public bool easymode;
    public int amountofrooms;
	public int currentroom;
    public RoomData[] roominfo ;
	public int equippedember;
	public int equippedboot;


}

[System.Serializable]
public class RoomData
{
 //public int width;
 //public int height;
 //public int minEnemies;
 //public int maxEnemies;
 public int entranceDir;
 public int exitDir;
    public bool beenThere;
  //  public int numEnemies = 0;
 //   public bool comingFromEntrance = true;
    public int roomID;
}