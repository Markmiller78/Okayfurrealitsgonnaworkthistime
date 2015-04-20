using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public string credits;
	public Vector2 scrolling;
	public Rect Display= new Rect (0, 0, 1280, 920);
	public Rect whatyousee= new Rect(0,0,680,480);
	// Use this for initialization
	void Start () {
		GUI.TextArea (Display, "I totally made the game");
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GUI.Button (new Rect (10, 260, 100, 30), "Credits")) {
		scrolling.y += 0.01f;
		GUI.BeginScrollView (Display, scrolling, whatyousee);
		}
	}
}
