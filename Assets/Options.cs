using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    public int sfxVolume;
    public int musicVolume;

    public Text sfxInt;
    public Text musicInt;

    public Text sfxInt2;
    public Text musicInt2;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        sfxInt.text = sfxVolume.ToString();
        musicInt.text = musicVolume.ToString();

        sfxInt2.text = sfxVolume.ToString();
        musicInt2.text = musicVolume.ToString();

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
    }


}
