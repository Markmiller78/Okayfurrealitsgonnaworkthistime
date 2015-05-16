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

        creds.transform.position = new Vector3(creds.transform.position.x, -130, creds.transform.position.z);
        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) || (Input.GetButtonDown("CPause") || Input.GetButtonDown("KBPause"))||(Input.GetKeyDown(KeyCode.Mouse0)))
        {
            Application.LoadLevel("MainMenu");

        }
			creds.transform.Translate(scrolling);
			scrolling.y+=1.1f;
	 
			if(creds.transform.position.y>=Screen.height*4.5f)
			{
				scrolling.y=-130.0f;

			}
		 
	 

 
	}
 
 


	}

