using UnityEngine;
using System.Collections;

public class YouLoseMenu : MonoBehaviour
{


    float timer;
    public int currentSelection;
    public GameObject SelectIcon;
    int maxchoices;
    bool AxisChanged;
    public float[] theChoices = new float[] { };
    public GameObject MainMenuText;
    public GameObject[] MainMenu4Highlight = new GameObject[] { };
    bool easyMode;
    RoomGeneration generator;
    Health playerHealth;
    PlayerLight dasLicht;
    PlayerEquipment equipment;

    // Use this for initialization
    void Start()
    {
        currentSelection = 0;
        maxchoices = 1;
        AxisChanged = false;
        easyMode = GameObject.FindObjectOfType<Options>().easyMode;
        generator = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomGeneration>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        dasLicht = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLight>();
        equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        SelectIcon.transform.position = new Vector3(-1.0f, theChoices[currentSelection], -5);
        if ((Input.GetAxis("CLSHorizontal") > .7f || Input.GetAxis("KBHorizontal") > 0 || Input.GetAxis("CDPadHorizontal") > .7f) && AxisChanged == false && currentSelection < 8)
        {

            if (currentSelection < maxchoices)
                currentSelection += 1;
            else
                currentSelection = 1;
            AxisChanged = true;

            //if (soundSource.isPlaying)
            //    soundSource.Stop();
            //soundSource.clip = changeSound;
            //soundSource.Play();
        }
        if ((Input.GetAxis("CLSHorizontal") < -0.7f || Input.GetAxis("KBHorizontal") < 0 || Input.GetAxis("CDPadHorizontal") < -0.7f) && AxisChanged == false && currentSelection < 8)
        {
            if (currentSelection > 0)
                currentSelection -= 1;
            else
                currentSelection = 0;
            AxisChanged = true;
            //if (soundSource.isPlaying)
            //    soundSource.Stop();
            //soundSource.clip = changeSound;
            //soundSource.Play();
        }
        if ((Input.GetAxis("CLSHorizontal") == 0 && Input.GetAxis("KBHorizontal") == 0 && Input.GetAxis("CDPadHorizontal") == 0) && currentSelection < 8)
        {
            AxisChanged = false;
        }


        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) && timer < 0)
        {
            switch (currentSelection)
            {
                case 0:
                    {
                        //
                        if (easyMode)
                        {
                            while (generator.currentRoom != 3 &&
                                generator.currentRoom != 7 &&
                                generator.currentRoom != 14 &&
                                generator.currentRoom != 18)
                            {
                                --generator.currentRoom;
                                generator.Reset();
                            }
                            generator.Reset();
                            playerHealth.currentHP = playerHealth.maxHP;
                            playerHealth.healthPercent = 100f;
                            dasLicht.currentLight = dasLicht.maxLight;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<Light>().cookie = null;
                            equipment.paused = false;
                            Destroy(this.gameObject);
                        }
                        else
                            LevelManager.Load("Game");
                        break;
                    }
                case 1:
                    {
                        LevelManager.Load("MainMenu");
                        break;
                    }
            }
        }
        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 2; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[0].SetActive(true);
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < 2; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[1].SetActive(true);
                    break;
                }
        }
    }
}
