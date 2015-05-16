using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	 
	public Vector3 scrolling= 	new Vector3(0.0f,0.1f,0.0f);
	public Canvas creds;
	public GameObject[] Buttons= new GameObject[3];
	public AudioClip clip;
	public bool playing=false;
	// Use this for initialization
	void Start () 
    {
 
		creds = Canvas.FindObjectOfType<Canvas>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	 if(Input.GetButtonDown("KBMenuCancel") || Input.GetButtonDown("CMenuCancel"))
     {
         LevelManager.Load("MainMenu");
     }
	 
			creds.transform.Translate(scrolling);
			scrolling.y+=0.9f;
	 
			if(creds.transform.position.y>=2030)
			{
				scrolling.y=-430.0f;

			}
		 
	 

 
	}
 
 


	}

