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
	//player= 
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(life + "/n" + lightss);
	
	}
	void OnGUI()
	{

		
		if (GUI.Button (new Rect (10, 260, 100, 30), "Save")) {
			Save ();

		}
		
		if (GUI.Button (new Rect (10, 400, 100, 30), "Load")) {
			Load ();
		}
	}
	public void Save()
	{

		BinaryFormatter bin = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerinfo.dat");
		PlayerData data= new PlayerData();
		data.health= 15;
		data.light= 20;
		bin.Serialize(file,data);
		file.Close();
		Debug.Log ("Saved!");
	}

	public void Load()
	{

		if(File.Exists(Application.persistentDataPath+"/playerinfo.dat"))
		   {
			BinaryFormatter bin= new BinaryFormatter();
			FileStream file= File.Open(Application.persistentDataPath+"/playerinfo.dat", FileMode.Open);
			PlayerDatas data= (PlayerDatas)bin.Deserialize(file);
			life= data.health;
			lightss=data.light;
			file.Close();
			Debug.Log ("Loaded!");

		}
	}
}
[System.Serializable]
class PlayerDatas
{
	public float health;
	public float light;


}