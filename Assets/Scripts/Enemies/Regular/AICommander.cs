using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AICommander : MonoBehaviour {


	GameObject player;
	public GameObject[] list;
	public bool isReinforcing=false;
    public bool isAttacking = false;
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
		movementspeed=3.0f;

	}
	
	// Update is called once per frame
	void Update () {
        if (isAttacking)
        {
            atkcooldown -= Time.deltaTime;
            if (atkcooldown <= 0.0f)
            {
                atkcooldown = atkcooldownref;
                isAttacking = false;
            }
        }
		vectoplayer = transform.position - player.transform.position;
		dist = vectoplayer.magnitude;
		FacePlayer ();
        size = list.Length;
        if (size == 1)
            isReinforcing = false;
		if (!isReinforcing) {
			

			distance = new float[size];
			if (size > 1) {
				for (int i = 0; i < size; i++) {
					distance [i] = (transform.position - list [i].transform.position).magnitude;
				}

		
				FacePlayer ();

				if (dist < 3.5f) {
					RunAway ();
				}
				else
					isReinforcing=true;
			}
			else{
				MoveTowardsPlayer();
				AttackPlayer();
			}
		} else
		
		{
        
			list = GameObject.FindGameObjectsWithTag ("Enemy");
			if(dist<1.75f)
				
			{		foreach (GameObject obj in  list)
				{
					obj.SendMessage("UnReinforce", SendMessageOptions.DontRequireReceiver);
				}
				isReinforcing=false;
			}
			 else
			{
             
                    foreach (GameObject obj in list)
                    {
                        obj.SendMessage("Reinforce", SendMessageOptions.DontRequireReceiver);
                    }
                
			}
		
		}
	}

	void RunAway()
	{
	 
		controller.Move (vectoplayer.normalized* Time.deltaTime *movementspeed);
	}
	void MoveTowardsPlayer()
	{

		controller.Move (vectoplayer.normalized* Time.deltaTime *-movementspeed);
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
	void AttackPlayer()
	{
		if (dist < atkrange&&!isAttacking)
		{
			player.GetComponent<Health>().LoseHealth(atkdmg);
            isAttacking = true;
		}


	}

	void Slow()
	{
		movementspeed = movementspeed * 0.5f;
	}
	
	void Unslow()
	{
		movementspeed = movementspeed * 2;
	}

	void Decoy()
	{
		player = GameObject.FindGameObjectWithTag ("Decoy");
	}
	
	void UnDecoy()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

}
