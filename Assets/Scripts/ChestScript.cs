using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

	public GameObject player;
	public GameObject loot;
    PlayerLight heroLight;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		heroLight=gameObject.GetComponent<PlayerLight>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Interact()
	{
		Vector3 dist = transform - player.transform;
		if (dist.magnitude < 5.0f) {
			heroLight.LoseLight (5);
			DestroyImmediate (this);
			Instantiate (loot, transform.position, new Quaternion (0, 0, 0, 0));
		}

	}
}
