using UnityEngine;
using System.Collections;

public class MenuMusic : MonoBehaviour {

    int musicMultiplier;

    AudioSource soundPlayer;

	// Use this for initialization
	void Start () {
        soundPlayer = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        musicMultiplier = GameObject.Find("TheOptions").GetComponent<Options>().musicVolume;
        soundPlayer.volume = musicMultiplier * 0.01f;
	}
}
