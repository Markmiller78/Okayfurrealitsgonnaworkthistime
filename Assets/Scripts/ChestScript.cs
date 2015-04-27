using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

	public bool displaytooltips = false;
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

	void OnGUI()
	{
		if (displaytooltips)
		{
		
			GUI.Box (new Rect (0, 0, 100, 20), "Press E to open");
		}

	}

	void Interact()
	{
		Vector3 dist = transform.position - player.transform.position;
		if (dist.magnitude < 1.0f) {
			heroLight.LoseLight (5);
		
			Instantiate (loot, transform.position, new Quaternion (0, 0, 0, 0));
			Destroy (this.gameObject);
		}

	}

	void DisplayTooltip()
	{
		displaytooltips = true;

	 	}

	void DoNotDisplayTooltip()
	{
		displaytooltips = false;
	}

	}

