﻿using UnityEngine;
using System.Collections;

public class OrbParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(2f, 0, 0));
	}
}
