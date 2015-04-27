using UnityEngine;
using System.Collections;

public class Decoy : MonoBehaviour {


	float timer=5.0f;
	// Use this for initialization
	void Start ()  {
		GameObject[] allObjects;
		allObjects = GameObject.FindObjectsOfType<GameObject>();
		foreach (GameObject obj in allObjects)
		{
			obj.SendMessage("Decoy", SendMessageOptions.DontRequireReceiver);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
