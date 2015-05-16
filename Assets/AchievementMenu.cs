﻿using UnityEngine;
using System.Collections;

public class AchievementMenu : MonoBehaviour {

    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject lock4;
    public GameObject lock5;

    float timer;
	// Use this for initialization

    Options theoptions;
	void Start () {
        timer = 0;
        theoptions = GameObject.Find("TheOptions").GetComponent<Options>();
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <=0)
        {
            timer = 0.3f;

            if (theoptions.achievements[0] == true)
            {
                lock1.SetActive(false);
            }
            else
            {
                lock1.SetActive(true);
            }

            if (theoptions.achievements[1] == true)
            {
                lock2.SetActive(false);
            }
            else
            {
                lock2.SetActive(true);
            }

            if (theoptions.achievements[2] == true)
            {
                lock3.SetActive(false);
            }
            else
            {
                lock3.SetActive(true);
            }

            if (theoptions.achievements[3] == true)
            {
                lock4.SetActive(false);
            }
            else
            {
                lock4.SetActive(true);
            }

            if (theoptions.achievements[4] == true)
            {
                lock5.SetActive(false);
            }
            else
            {
                lock5.SetActive(true);
            }


        }
    
	}
}
