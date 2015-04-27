using UnityEngine;
using System.Collections;

public class Decoy : MonoBehaviour {


	public GameObject lightRemains;
	public float timer=5.0f;
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
		timer -= Time.deltaTime;
		if (timer <= 0.0f)
		{
			GameObject[] allObjects;
			allObjects = GameObject.FindObjectsOfType<GameObject>();
			foreach (GameObject obj in allObjects)
			{
				obj.SendMessage("UnDecoy", SendMessageOptions.DontRequireReceiver);
			}
			Instantiate(lightRemains, transform.position, transform.rotation);  
			Destroy (this.gameObject);

		}
	
	}


}
