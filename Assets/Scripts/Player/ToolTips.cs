using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolTips : MonoBehaviour {

	public GameObject[] objects;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

		objects = GameObject.FindObjectsOfType <GameObject>();
		foreach (GameObject obj in objects)
		{
			if((transform.position-obj.transform.position).magnitude<1.5f)
			obj.SendMessage("DisplayTooltip", SendMessageOptions.DontRequireReceiver);
			else
				obj.SendMessage("DoNotDisplayTooltip", SendMessageOptions.DontRequireReceiver);
		}
	
	}
}
