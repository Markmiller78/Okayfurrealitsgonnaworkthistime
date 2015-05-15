using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveTest : MonoBehaviour {

	GameObject player;
	public float life;
	public float lightss;
    public bool shouldload = false;
    GameObject dungeon;
    


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        if (shouldload == true)
        {
            Load();
        }
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(life + "/n" + lightss);

	
	}
 
   public void Save()
    {
        if (Application.platform == RuntimePlatform.OSXWebPlayer
           || Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            PlayerPrefs.SetFloat("PlayerHealth", gameObject.GetComponent<Health>().currentHP);
            PlayerPrefs.SetFloat("PlayerLight", gameObject.GetComponent<PlayerLight>().currentLight);

        }
        else
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerinfo.dat");
            PlayerData data = new PlayerData();
            data.health = GetComponent<Health>().currentHP;
            data.theLight = gameObject.GetComponent<PlayerLight>().currentLight;
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
               file.Close();
               Debug.Log("Loaded!");

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



}