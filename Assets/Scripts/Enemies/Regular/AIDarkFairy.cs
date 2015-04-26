using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIDarkFairy : MonoBehaviour {

	GameObject player;
	public GameObject currentlight;
	public List<GameObject> list= new List<GameObject>();
	 
	CharacterController controller;
	public float atkdmg;
	public float atkrange;
	public float atkcooldown;
	public float atkcooldownref;
	public float movementspeed=3.5f;
	public float[] distance;
	public float dist;
	public Vector3 vectoplayer;
	public int size;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("LightDrop");

		for (int i = 0; i < temp.Length; i++) {
			list.Add(temp[i]);

		}
	
	}
	
	// Update is called once per frame
	void Update () {

		MoveTowardsLight (list[0]);
	
	}

	void MoveTowardsLight(GameObject lightdrop )
	{
		Vector2 moveTo = (player.transform.position - transform.position).normalized;
		controller.Move(moveTo * Time.deltaTime * 2.0f);
	}

	void RunAway()
	{
	}

	void StealLightDrop()
	{
		foreach (GameObject lightdrop in list)
		{
			if((transform.position-lightdrop.transform.position).magnitude<1.0f)
			{
				Destroy(lightdrop);
				break;
			}
			
		}
	 
	}

	void AddDrop(GameObject lightdrop)
	{
		list.Add (lightdrop);

	}
	void RemoveDrop(GameObject lightdrop)
	{
		list.Remove (lightdrop);

	}
}
