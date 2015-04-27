using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AICommander : MonoBehaviour {


	GameObject player;
	public GameObject[] list;
	public bool isReinforcing=false;
	CharacterController controller;
	public float atkdmg;
	public float atkrange;
	public float atkcooldown;
	public float atkcooldownref;
	public float movementspeed=3.0f;
	public float[] distance;
	public float dist;
	public Vector3 vectoplayer;
	public int size;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		list = GameObject.FindGameObjectsWithTag ("Enemy");
		controller = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {
		size = list.Length;

		distance = new float[size];
		if(size>1.0f)
		   {
			for (int i = 0; i < size; i++) {
				distance[i]= (transform.position-list[i].transform.position).magnitude;
			}

			vectoplayer=transform.position-player.transform.position;
			FacePlayer();
			dist=vectoplayer.magnitude;
			if(dist<4.0f)
			{

				RunAway();

			}
		}
	
	}

	void RunAway()
	{
	 
		controller.Move (vectoplayer.normalized* Time.deltaTime * movementspeed);
	}

	void FacePlayer()
	{
		//float tempangle=Vector3.Angle (transform.up,disttoplayer);
		//transform.Rotate (Vector3.back,tempangle+180);

 
		float tempangle = Mathf.Atan2(vectoplayer.y, vectoplayer.x) * Mathf.Rad2Deg;
		tempangle += 90.0f;
		Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime *2.5f);

	}
 
}
