using UnityEngine;
using System.Collections;

public class BlinkParticle : MonoBehaviour {

	public float timer=0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > 0.5f)
			Destroy (this.gameObject);
	
	}
}
