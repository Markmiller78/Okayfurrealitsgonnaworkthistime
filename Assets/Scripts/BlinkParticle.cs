using UnityEngine;
using System.Collections;

public class BlinkParticle : MonoBehaviour {

	public GameObject lightleftbehind;
	public GameObject[] enemies;
	public float timer=0.0f;
	public float range = 3.0f;


	// Use this for initialization
	void Start () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (var item in enemies) {
			if((transform.position-item.transform.position).magnitude<range)
			{
				item.GetComponent<Health>().LoseHealth(5);
				item.GetComponent<Knockback>().GetWrecked();
			}
			
		}
	

	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > 0.5f) 
		{

			Destroy (gameObject);
			Instantiate(lightleftbehind, transform.position, transform.rotation);  
		
		}
	
	}
}
