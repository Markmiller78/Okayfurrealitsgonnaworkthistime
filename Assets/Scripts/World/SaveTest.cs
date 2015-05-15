using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveTest : MonoBehaviour {

	GameObject player;
	public float life;
	public float lightss;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player"); 
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
            PlayerPrefs.SetFloat("PlayerLight", gameObject.GetComponent<Health>().currentHP);

        }
        else
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerinfo.dat");
            PlayerData data = new PlayerData();
            //data.health = health;
           // data.light = theLight;
            bin.Serialize(file, data);
            file.Close();
        }

    }


	public void Load()
	{

		if(File.Exists(Application.persistentDataPath+"/playerinfo.dat"))
		   {
			BinaryFormatter bin= new BinaryFormatter();
			FileStream file= File.Open(Application.persistentDataPath+"/playerinfo.dat", FileMode.Open);
			PlayerDatas data= (PlayerDatas)bin.Deserialize(file);
			life= data.health;
			lightss=data.theLight;
			file.Close();
			Debug.Log ("Loaded!");

		}
	}
}
[System.Serializable]
class PlayerDatas
{
	public float health;
    public float theLight;
    PlayerStats stats;


}