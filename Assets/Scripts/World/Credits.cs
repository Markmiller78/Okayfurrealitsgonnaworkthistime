using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	 
	public Vector3 scrolling= 	new Vector3(0.0f,0.1f,0.0f);
	public Canvas creds;
	public GameObject[] Buttons= new GameObject[3];
	public AudioClip clip;
	public bool playing=false;
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
			scrolling.y+=0.9f;
	 
			if(creds.transform.position.y>=2030)
			{
				scrolling.y=-430.0f;

			}
		}
	 

 
	}
	void OnGUI()
	{
 

		if (GUI.Button (new Rect (10, 360, 100, 30), "Credits") ) {
			if(!playing)
			{
			this.GetComponentInChildren<AudioSource>().Play();
			}
			else
				this.GetComponentInChildren<AudioSource> ().Stop ();
			scrolling.y=-430.0f;
			creds.enabled=!creds.enabled;
 		}
 


	}
}
