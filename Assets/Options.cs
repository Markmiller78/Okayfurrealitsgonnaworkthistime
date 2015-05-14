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

    void Start()
    {
        Load();
        DontDestroyOnLoad(gameObject);
        sfxInt.text = sfxVolume.ToString();
        musicInt.text = musicVolume.ToString();

        sfxInt2.text = sfxVolume.ToString();
        musicInt2.text = musicVolume.ToString();

    }

    void Update()
    {
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
        sfxVolume++;
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
        sfxVolume--;
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
        musicVolume++;
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
        musicVolume--;
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

        BinaryFormatter bin = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/optioninfo.dat");
        OptionData data = new OptionData();
        data.sfxVolume = sfxVolume;
        data.musicVolume = musicVolume;
        bin.Serialize(file, data);
        file.Close();

    }
    public void Load()
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

[System.Serializable]
public class OptionData
{
    public int sfxVolume;
    public int musicVolume;


}