using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Options : MonoBehaviour
{
    private static Options _instance;


    public static Options instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Options>();
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

    public int sfxVolume;
    public int musicVolume;

    public Text sfxInt;
    public Text musicInt;

    public Text sfxInt2;
    public Text musicInt2;

    int numMelee;
    int numDash;
    int numEnemiesKilled;

    public bool[] achievements;
    public bool shouldload = false;
    [HideInInspector]
    public GameObject savedPlayer;
    GameObject player;
    bool beenAssigned;

    [HideInInspector]
    public bool easyMode;

    public AudioClip achvSound;
    AudioSource aPlayer;

    void Start()
    {
        achievements = new bool[8];


        achievements[0] = false; // Defeat 100 enemies
        achievements[1] = false; // Melee
        achievements[2] = false; // Chest
        achievements[3] = false; // Dash
        achievements[4] = false; // Complete Tutorial
        achievements[5] = false; // Defeat Dethros
        achievements[6] = false; // Defeat Lorne
        achievements[7] = false; // Defeat Morrius
        Load();
        DontDestroyOnLoad(gameObject);
        sfxInt.text = sfxVolume.ToString();
        musicInt.text = musicVolume.ToString();

        sfxInt2.text = sfxVolume.ToString();
        musicInt2.text = musicVolume.ToString();
        beenAssigned = false;
        numMelee = 0;
        numEnemiesKilled = 0;
        numDash = 0;

        aPlayer = GetComponent<AudioSource>();
        aPlayer.volume = sfxVolume * 0.01f;


    }




    public void AddToEnemy()
    {
        numEnemiesKilled++;
        if (numEnemiesKilled >= 100 && achievements[0] == false)
        {
            GameObject.Find("EnemyAchv").GetComponent<Image>().enabled = true;
            achievements[0] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void AddToMelee()
    {
        numMelee++;
        if (numMelee >= 50 && achievements[1] == false)
        {
            GameObject.Find("MeleeAchv").GetComponent<Image>().enabled = true;
            achievements[1] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void OpenChest()
    {
        if (achievements[2] == false)
        {
            GameObject.Find("ChestAchv").GetComponent<Image>().enabled = true;
            achievements[2] = true;
            aPlayer.PlayOneShot(achvSound);

            Save();
        }
    }

    public void AddToDash()
    {
        numDash++;
        if (numDash >= 50 && achievements[3] == false)
        {
            GameObject.Find("DashAchv").GetComponent<Image>().enabled = true;
            achievements[3] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void CompleteTutorial()
    {
        if (achievements[4] == false)
        {
            GameObject.Find("TutorialAchv").GetComponent<Image>().enabled = true;

            achievements[4] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }
    public void DethrosDie()
    {
        if (achievements[5] == false)
        {
            GameObject.Find("DethrosAchv").GetComponent<Image>().enabled = true;
            achievements[5] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }


    public void LorneDie()
    {
        if (achievements[6] == false)
        {
            GameObject.Find("LorneAchv").GetComponent<Image>().enabled = true;
            achievements[6] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void MorriusDie()
    {
        if (achievements[7] == false)
        {
            GameObject.Find("MorriusAchv").GetComponent<Image>().enabled = true;
            achievements[7] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }


    void Update()
    {
        if (!beenAssigned && Application.loadedLevelName == "Game")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            beenAssigned = true;
        }

#if UNITY_WEBPLAYER
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1280, 720, false);
            Screen.fullScreen = false;
        }
#endif
    }

    public void sfxIncrease()
    {
        sfxVolume += 5;
        if (sfxVolume > 100)
        {
            sfxVolume = 100;
        }
        if (sfxInt)
            sfxInt.text = sfxVolume.ToString();
        if (sfxInt2)
            sfxInt2.text = sfxVolume.ToString();
        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }

        aPlayer.volume = sfxVolume * 0.01f;

    }

    public void sfxDecrease()
    {
        sfxVolume -= 5;
        if (sfxVolume < 0)
        {
            sfxVolume = 0;
        }
        if (sfxInt)
        {
            sfxInt.text = sfxVolume.ToString();
        }
        if (sfxInt2)
        {
            sfxInt2.text = sfxVolume.ToString();
        }
        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }

        aPlayer.volume = sfxVolume * 0.01f;

    }

    public void musicIncrease()
    {
        print("mIncrease");
        musicVolume += 5;
        if (musicVolume > 100)
        {
            musicVolume = 100;
        }
        if (musicInt != null)
        {

            musicInt.text = musicVolume.ToString();
        }
        if (musicInt2 != null)
        {
            musicInt2.text = musicVolume.ToString();
        }

        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void musicDecrease()
    {
        print("mDecrease");

        musicVolume -= 5;
        if (musicVolume < 0)
        {
            musicVolume = 0;
        }
        if (musicInt)
            musicInt.text = musicVolume.ToString();
        if (musicInt2)
            musicInt2.text = musicVolume.ToString();
        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }
    }


    public void Save()
    {
        if (Application.platform == RuntimePlatform.OSXWebPlayer
   || Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            PlayerPrefs.SetInt("PlayerSFX", sfxVolume);
            PlayerPrefs.SetInt("PlayerMusic", musicVolume);

        }
        else
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/optioninfo.dat");
            OptionData data = new OptionData();
            data.sfxVolume = sfxVolume;
            data.musicVolume = musicVolume;
            data.achieve = achievements[0];
            data.achieve1 = achievements[1];
            data.achieve2 = achievements[2];
            data.achieve3 = achievements[3];
            data.achieve4 = achievements[4];
            data.achieve5 = achievements[5];
            data.achieve6 = achievements[6];
            data.achieve7 = achievements[7];
            bin.Serialize(file, data);
            file.Close();
        }


    }
    public void Load()
    {
        if (Application.platform == RuntimePlatform.OSXWebPlayer
|| Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            sfxVolume = PlayerPrefs.GetInt("PlayerSFX", 100);
            musicVolume = PlayerPrefs.GetInt("PlayerMusic", 100);

        }
        else
        {
            if (File.Exists(Application.persistentDataPath + "/optioninfo.dat"))
            {
                BinaryFormatter bin = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/optioninfo.dat", FileMode.Open);
                OptionData data = (OptionData)bin.Deserialize(file);
                sfxVolume = data.sfxVolume;
                musicVolume = data.musicVolume;
                achievements[0] = data.achieve;
                achievements[1] = data.achieve1;
                achievements[2] = data.achieve2;
                achievements[3] = data.achieve3;
                achievements[4] = data.achieve4;
                achievements[5] = data.achieve5;
                achievements[6] = data.achieve6;
                achievements[7] = data.achieve7;

                file.Close();

            }
        }
    }
}

[System.Serializable]
public class OptionData
{
    public int sfxVolume;
    public int musicVolume;
    public bool achieve;
    public bool achieve1;
    public bool achieve2;
    public bool achieve3;
    public bool achieve4;
    public bool achieve5;
    public bool achieve6;
    public bool achieve7;


}