using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	 
	public Vector3 scrolling= 	new Vector3(0.0f,0.1f,0.0f);
	public Canvas creds;
	public GameObject[] Buttons= new GameObject[3];
	public AudioClip clip;
	// Use this for initialization
	void Start () {
 
		creds = Canvas.FindObjectOfType<Canvas>();
		creds.enabled = false;	 
	 
		this.GetComponentInChildren<AudioSource> ().Stop ();
	//	clip = (AudioClip)GameObject.FindGameObjectWithTag ("CreditMusic");


	}
	
	// Update is called once per frame
	void Update () {
	
	 
		if (creds.enabled == true) 
		{
			creds.transform.Translate(scrolling);
			scrolling.y+=1.5f;
	 
			if(creds.transform.position.y>=2084)
			{
				scrolling.y=-400.0f;

			}
		}
	 

 
	}
	void OnGUI()
	{

		if ( GUI.Button (new Rect (10, 360, 100, 30), "Credits")) {
			this.GetComponentInChildren<AudioSource>().Play();
			scrolling.y=-300.0f;
			creds.enabled=!creds.enabled;
 		}
 


	}
}
