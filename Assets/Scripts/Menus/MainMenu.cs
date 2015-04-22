using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{


    public float[] choices = new float[] { };
    int maxchoices = 4;

    public int currentSelection = 0;
    public GameObject SelectIcon;
    bool AxisChanged = false;
    public enum Menu { Main = 0, Options, Achievements, Credits, Continue, AreYouSure, SelectDifficulty };
    Menu CurrMenu = Menu.Main;
    public GameObject MainMenuText;
    public GameObject[] MainMenu4Highlight = new GameObject[] { };

    public GameObject ContinueText;
    public GameObject[] ContinueTextHighlight = new GameObject[] { };

    public GameObject AreYouSureText;
    public GameObject[] AreYouSureText4Highlight = new GameObject[] { };

    public GameObject SelectDifficulty;
    public GameObject[] SelectDifficulty4Highlight = new GameObject[] { };

    public GameObject OptionsMenuText;
    public GameObject[] OptionsMenuText4Highlight = new GameObject[] { };



    public GameObject Achievements;
    public GameObject[] Achievements4Highlight = new GameObject[] { };

    public GameObject Credits;
    public GameObject[] Credits4Highlight = new GameObject[] { };

    float timer = 0;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        SelectIcon.transform.position = new Vector3(-1.0f, choices[currentSelection], -5);
        if ((Input.GetAxis("CLSVertical") > .7f || Input.GetAxis("KBVertical") > 0) && AxisChanged == false)
        {
            if (currentSelection > 0)
                currentSelection -= 1;
            else
                currentSelection = maxchoices;
            AxisChanged = true;
        }
        if ((Input.GetAxis("CLSVertical") < -0.7f || Input.GetAxis("KBVertical") < 0) && AxisChanged == false)
        {
            if (currentSelection < maxchoices)
                currentSelection += 1;
            else
                currentSelection = 0;
            AxisChanged = true;

        }
        if (Input.GetAxis("CLSVertical") == 0 && Input.GetAxis("KBVertical") == 0)
        {
            AxisChanged = false;
        }

        switch (CurrMenu)
        {
            case Menu.Main:
                {
                    MainMenuSelect();
                    break;
                }
            case Menu.Options:
                {
                    OptionsMenu();
                    break;
                }
            case Menu.Credits:
                {
                    CreditsMenu();
                    break;
                }
            case Menu.Achievements:
                {
                    AchievementsMenu();
                    break;
                }
        }

    }


    void MainMenuSelect()
    {
        maxchoices = 4;
        choices[0] = 1.8f;
        choices[1] = 1.1f;
        choices[2] = 0.4f;
        choices[3] = -0.3f;
        choices[4] = -1.7f;
        MainMenuText.SetActive(true);

        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[0].SetActive(true);
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[1].SetActive(true);
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[2].SetActive(true);
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[3].SetActive(true);
                    break;
                }
            case 4:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[4].SetActive(true);
                    break;
                }
        }


        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) && timer < 0)
        {
            timer = .5f;
            for (int i = 0; i < 5; i++)
            {
                MainMenu4Highlight[i].SetActive(false);
            }

            switch (currentSelection)
            {
                case 0:
                    {
                        MainMenuText.SetActive(false);
                        CurrMenu = Menu.Continue;
                        break;
                    }
                case 1:
                    {
                        MainMenuText.SetActive(false);
                        CurrMenu = Menu.Options;
                        break;
                    }
                case 2:
                    {
                        MainMenuText.SetActive(false);
                        CurrMenu = Menu.Credits;
                        break;
                    }
                case 3:
                    {
                        MainMenuText.SetActive(false);
                        CurrMenu = Menu.Achievements;
                        break;
                    }
                case 4:
                    {
                        Application.Quit();
                        UnityEditor.EditorApplication.isPlaying = false;
                        break;
                    }
            }

            currentSelection = 0;
        }
    }

    void OptionsMenu()
    {
        maxchoices = 3;
        choices[0] = 1.8f;
        choices[1] = 1.1f;
        choices[2] = 0.4f;
        choices[3] = -1.7f;
        OptionsMenuText.SetActive(true);



        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[0].SetActive(true);
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[1].SetActive(true);
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[2].SetActive(true);
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[3].SetActive(true);
                    break;
                }

            case 4:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[4].SetActive(true);
                    break;
                }
            case 5:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[5].SetActive(true);
                    break;
                }
        }

        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) && timer < 0)
        {
            timer = .5f;
            for (int i = 0; i < 4; i++)
            {
                OptionsMenuText4Highlight[i].SetActive(false);
            }

            switch (currentSelection)
            {
                case 0:
                    {
                        currentSelection = 6;
                        break;
                    }
                case 1:
                    {

                        break;
                    }
                case 2:
                    {
                        //OptionsMenuText.SetActive(false);
                        //CurrMenu = Menu.Main;
                        //currentSelection = 1;
                        break;
                    }
                case 3:
                    {
                        OptionsMenuText.SetActive(false);
                        CurrMenu = Menu.Main;
                        currentSelection = 1;
                        break;
                    }
            }

        }
    }

    void CreditsMenu()
    {
        maxchoices = 0;
        choices[0] = -1.7f;
        Credits.SetActive(true);



        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Credits4Highlight[i].SetActive(false);
                    }
                    Credits4Highlight[0].SetActive(true);
                    break;
                }
        }
        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) && timer < 0)
        {
            timer = 1;
            for (int i = 0; i < 1; i++)
            {
                Credits4Highlight[i].SetActive(false);
            }

            switch (currentSelection)
            {
                case 0:
                    {
                        Credits.SetActive(false);
                        CurrMenu = Menu.Main;
                        currentSelection = 2;
                        break;
                    }
            }
        }
    }


    void AchievementsMenu()
    {
        maxchoices = 0;
        choices[0] = -1.7f;
        Achievements.SetActive(true);



        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Achievements4Highlight[i].SetActive(false);
                    }
                    Achievements4Highlight[0].SetActive(true);
                    break;
                }
        }
        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) && timer < 0)
        {
            timer = 1;
            for (int i = 0; i < 1; i++)
            {
                Achievements4Highlight[i].SetActive(false);
            }

            switch (currentSelection)
            {
                case 0:
                    {
                        Achievements.SetActive(false);
                        CurrMenu = Menu.Main;
                        currentSelection = 3;
                        break;
                    }
            }
        }
    }
}
