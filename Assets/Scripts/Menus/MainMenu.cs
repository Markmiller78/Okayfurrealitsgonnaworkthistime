using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{


    public float[] choices = new float[] { };
    public int currentSelection = 0;
    public GameObject SelectIcon;
    bool AxisChanged = false;
    public enum Menu { Main = 0, Options, Achievements, Credits , Continue, AreYouSure, SelectDifficulty};
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



    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        SelectIcon.transform.position = new Vector3(-1.0f, choices[currentSelection], -5);
        if ((Input.GetAxis("CLSVertical") > .7f || Input.GetAxis("KBVertical") > 0) && AxisChanged == false)
        {
            if (currentSelection > 0)
                currentSelection -= 1;
            else
                currentSelection = 4;
            AxisChanged = true;
        }
        if ((Input.GetAxis("CLSVertical") < -0.7f || Input.GetAxis("KBVertical") < 0) && AxisChanged == false)
        {
            if (currentSelection < 4)
                currentSelection += 1;
            else
                currentSelection = 0;
            AxisChanged = true;

        }
        if (Input.GetAxis("CLSVertical") == 0 && Input.GetAxis("KBVertical") == 0)
        {
            AxisChanged = false;
        }

        switch(CurrMenu)
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


        if (Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept"))
        {
            for (int i = 0; i < 5; i++)
            {
                MainMenu4Highlight[i].SetActive(false);
            }
            
            switch(currentSelection)
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
                        //Exit the Game
                        break;
                    }
            }

            currentSelection = 0;
        }
    }

    void OptionsMenu()
    {
        OptionsMenuText.SetActive(true);



        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[0].SetActive(true);
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[1].SetActive(true);
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[2].SetActive(true);
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[3].SetActive(true);
                    break;
                }
            case 4:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[4].SetActive(true);
                    break;
                }
        }

    }

    void CreditsMenu()
    {


    }
    

    void AchievementsMenu()
    {



    }
}
