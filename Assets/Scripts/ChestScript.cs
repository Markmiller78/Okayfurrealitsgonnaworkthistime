using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

	public GameObject player;
	public GameObject loot;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Interact()
	{

		Instantiate(loot, transform.position, new Quaternion(0, 0, 0, 0));

	}
}
