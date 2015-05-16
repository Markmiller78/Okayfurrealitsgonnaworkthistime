using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour
{
    public AudioClip[] clips;
    GameObject dungeon;
    RoomGeneration theRooms;
    GameObject player;
    AudioSource audioPlayer;
    bool beenAssigned;


    private static BGM _instance;

    public static BGM instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BGM>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != _instance)
        {
            Destroy(this.gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        audioPlayer = gameObject.GetComponent<AudioSource>();
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        DontDestroyOnLoad(this.gameObject);
        beenAssigned = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!beenAssigned && Application.loadedLevelName == "Game")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            beenAssigned = true;
        }



        if (dungeon == null)
        {
            dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            if (dungeon != null)
            {
                theRooms = dungeon.GetComponent<RoomGeneration>();
            }
        }

        if (theRooms != null)
        {
            if (!GameObject.FindObjectOfType<Options>().easyMode)
            {
                if (theRooms.currentRoom < 8 && audioPlayer.clip != clips[0])
                {
                    audioPlayer.Stop();
                    audioPlayer.clip = clips[0];
                    audioPlayer.Play();


                }
                else if (theRooms.currentRoom == 8 || theRooms.currentRoom == 17 && audioPlayer.clip != clips[2])
                {
                    audioPlayer.Stop();
                    audioPlayer.clip = clips[2];
                    audioPlayer.Play();

                }
                else if (theRooms.currentRoom > 8 && audioPlayer.clip != clips[1])
                {
                    audioPlayer.Stop();
                    audioPlayer.clip = clips[1];
                    audioPlayer.Play();


                }
                else if (theRooms.currentRoom == 26 && audioPlayer.clip != clips[3])
                {
                    audioPlayer.Stop();
                    audioPlayer.clip = clips[3];
                    audioPlayer.Play();
                }

            }
            else
            {
                if (theRooms.currentRoom < 10 && audioPlayer.clip != clips[0])
                {
                    audioPlayer.Stop();
                    audioPlayer.clip = clips[0];
                    audioPlayer.Play();


                }
                else if (theRooms.currentRoom == 10 || theRooms.currentRoom == 21 && audioPlayer.clip != clips[2])
                {
                    audioPlayer.Stop();
                    audioPlayer.clip = clips[2];
                    audioPlayer.Play();

                }
                else if (theRooms.currentRoom > 10 && audioPlayer.clip != clips[1])
                {
                    audioPlayer.Stop();
                    audioPlayer.clip = clips[1];
                    audioPlayer.Play();


                }
                else if (theRooms.currentRoom == 32 && audioPlayer.clip != clips[3])
                {
                    audioPlayer.Stop();
                    audioPlayer.clip = clips[3];
                    audioPlayer.Play();
                }

            }
        }



    }


}
