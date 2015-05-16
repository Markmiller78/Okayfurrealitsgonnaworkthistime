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

    public bool[] achievements;

    [HideInInspector]
    public GameObject savedPlayer;
    GameObject player;
    bool beenAssigned;

    [HideInInspector]
    public bool easyMode;

    void Start()
    {
        Load();
        DontDestroyOnLoad(gameObject);
        sfxInt.text = sfxVolume.ToString();
        musicInt.text = musicVolume.ToString();

        sfxInt2.text = sfxVolume.ToString();
        musicInt2.text = musicVolume.ToString();
        beenAssigned = false;
        numMelee = 0;

        achievements = new bool[5];

        achievements[0] = false; // Game Beaten
        achievements[1] = false; // Melee
        achievements[2] = false;
        achievements[3] = false;
        achievements[4] = false;

    }

    public void AddToMelee()
    {
        numMelee++;
        if (numMelee >= 50 && achievements[1] == false)
        {
            GameObject.Find("MeleeAchv").GetComponent<Image>().enabled = true;
            achievements[1] = true;
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
        sfxInt.text = sfxVolume.ToString();
        sfxInt2.text = sfxVolume.ToString();
        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }

    }

    public void sfxDecrease()
    {
        sfxVolume -= 5;
        if (sfxVolume < 0)
        {
            sfxVolume = 0;
        }
        sfxInt.text = sfxVolume.ToString();
        sfxInt2.text = sfxVolume.ToString();
        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void musicIncrease()
    {
        musicVolume += 5;
        if (musicVolume > 100)
        {
            musicVolume = 100;
        }
        musicInt.text = musicVolume.ToString();
        musicInt2.text = musicVolume.ToString();
        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void musicDecrease()
    {
        musicVolume -= 5;
        if (musicVolume < 0)
        {
            musicVolume = 0;
        }
        musicInt.text = musicVolume.ToString();
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
            bin.Serialize(file, data);
            file.Close();
        }


    }
    public void Load()
    {
        if (Application.platform == RuntimePlatform.OSXWebPlayer
|| Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
           sfxVolume= PlayerPrefs.GetInt("PlayerSFX",100);  
            musicVolume=PlayerPrefs.GetInt("PlayerMusic",100);

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


}