using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

	public bool displaytooltips = false;
	public GameObject player;
	public GameObject loot;
    PlayerLight heroLight;
    Camera cameras;

    Options theoptions;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		heroLight=player.gameObject.GetComponent<PlayerLight>();
        cameras = GameObject.FindObjectOfType<Camera>();
        theoptions = GameObject.Find("TheOptions").GetComponent<Options>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (displaytooltips)
		{
        string temp="Press E to open";
            GUI.Box(new Rect(cameras.WorldToScreenPoint(player.transform.position).x + 32, Screen.height - cameras.WorldToScreenPoint(player.transform.position).y, 100, 40), temp);

		}

	}

	void Interact()
	{
		Vector3 dist = transform.position - player.transform.position;
		if (dist.magnitude < 1.0f) {
            theoptions.OpenChest();
			heroLight.LoseLight (5);
            gameObject.GetComponent<GenerateLoot>().Generateloot();
			//Instantiate (loot, transform.position, new Quaternion (0, 0, 0, 0));
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

