using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;
    GameObject pauseCanvas;
    int currOption = 0;
    int numOptions = 3;
    PlayerEquipment heroEquipment;
    bool axisChanged = false;
    public GameObject SelectorRemains;

    int currOptionOption = 0;
    int numOptionOptions = 5;

    public Text resume;
    public Text options;
    public Text quit;

    public Text sVolume;
    public Text mVolume;
    public Text fullscreen;
    public Text controlMode;
    public Text back;

    bool optionsMode = false;

    //    Vector3 RemainsPOS;
    void Start()
    {
        //        RemainsPOS = new Vector3(1, 1, 1);
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        pauseCanvas.SetActive(false);
        isPaused = false;
        sVolume.enabled = false;
        mVolume.enabled = false;
        fullscreen.enabled = false;
        controlMode.enabled = false;
        back.enabled = false;
    }

    void Update()
    {
        if (isPaused)
        {
            if (InputManager.controller)
            {
                #region Controller Input
                if (axisChanged && Input.GetAxis("CLSVertical") == 0.0f && Input.GetAxis("CDPadVertical") == 0.0f
                    && Input.GetAxis("CLSHorizontal") == 0.0f && Input.GetAxis("CDPadHorizontal") == 0.0f)
                {
                    axisChanged = false;
                }
                if (!axisChanged && (Input.GetAxis("CLSVertical") > 0.0f || Input.GetAxis("CDPadVertical") > 0.0f))
                {
                    if (!optionsMode)
                        currOption--;
                    else
                        currOptionOption--;
                    axisChanged = true;
                }
                if (!axisChanged && (Input.GetAxis("CLSVertical") < 0.0f || Input.GetAxis("CDPadVertical") < 0.0f))
                {
                    if (!optionsMode)
                        currOption++;
                    else
                        currOptionOption++;
                    axisChanged = true;
                }
                #endregion
            }
            else
            {
                #region KB/M Input
                if (axisChanged && Input.GetAxis("KBVertical") == 0.0f && Input.GetAxis("KBHorizontal") == 0f)
                {
                    axisChanged = false;
                }
                if (!axisChanged && Input.GetAxis("KBVertical") > 0.0f)
                {
                    if (!optionsMode)
                        currOption--;
                    else
                        currOptionOption--;
                    axisChanged = true;
                }
                if (!axisChanged && Input.GetAxis("KBVertical") < 0.0f)
                {
                    if (!optionsMode)
                        currOption++;
                    else
                        currOptionOption++;
                    axisChanged = true;
                }
                #endregion
            }
            #region Wrapping
            if (currOption < 0)
                currOption = numOptions - 1;
            else if (currOption >= numOptions)
                currOption = 0;

            if (currOptionOption < 0)
                currOptionOption = numOptionOptions - 1;
            else if (currOptionOption >= numOptionOptions)
                currOptionOption = 0;
            #endregion

            if (!optionsMode)
            {
                switch (currOption)
                {
                    case 0:
                        #region Resume
                        SelectorRemains.transform.localPosition = new Vector3(-100f, 80f, -5.1f);
                        if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                        {
                            // Resume Code Here
                            GameObject[] allObjects;
                            allObjects = GameObject.FindObjectsOfType<GameObject>();
                            //isPaused = !isPaused;
                            foreach (GameObject obj in allObjects)
                            {
                                obj.SendMessage("UnPause", SendMessageOptions.DontRequireReceiver);
                            }
                            GameObject.Find("InputManager").GetComponent<InputManager>().isPaused = false;
                        }
                        else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                        {
                            // Resume Code Here
                            GameObject[] allObjects;
                            allObjects = GameObject.FindObjectsOfType<GameObject>();
                            //isPaused = !isPaused;
                            foreach (GameObject obj in allObjects)
                            {
                                obj.SendMessage("UnPause", SendMessageOptions.DontRequireReceiver);
                            }
                            GameObject.Find("InputManager").GetComponent<InputManager>().isPaused = false;
                        }
                        break;
                        #endregion
                    case 1:
                        #region Options Menu Stuff
                        SelectorRemains.transform.localPosition = new Vector3(-105f, 30f, -5.1f);
                        if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                        {
                            // Options Code Here
                            SwapMode();
                        }
                        else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                        {
                            // Options Code Here
                            SwapMode();
                        }
                        break;
                        #endregion
                    case 2:
                        #region Quit
                        SelectorRemains.transform.localPosition = new Vector3(-70f, -85f, -5.1f);
                        if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                        {
                            // Save & Quit Code Here
                            //GameObject.FindGameObjectWithTag("Player").SendMessage("Save", SendMessageOptions.DontRequireReceiver);
                            //Application.Quit();
                            Application.LoadLevel("MainMenu");
                        }
                        else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                        {
                            // Save & Quit Code Here
                            //GameObject.FindGameObjectWithTag("Player").SendMessage("Save", SendMessageOptions.DontRequireReceiver);
                            //Application.Quit();
                            Application.LoadLevel("MainMenu");
                        }
                        break;
                        #endregion
                    default:
                        break;
                }
            }
            else
            {
                Debug.Log("Controller: " + InputManager.controller);
                Debug.Log("axisChanged" + axisChanged);
                Debug.Log("CLSHoriz: " + Input.GetAxis("CLSHorizontal"));
                //Debug.Log("DPadHoriz: " + Input.GetAxis("CDPadHorizontal"));
                //Debug.Log("KBHoriz: " + Input.GetAxis("KBHorizontal"));
                switch (currOptionOption)
                {
                    case 0:
                        #region SFX
                        SelectorRemains.transform.localPosition = new Vector3(-140f, 130f, -5.1f);
                        if (InputManager.controller && !axisChanged && (Input.GetAxis("CLSHorizontal") < 0.0f || Input.GetAxis("CDPadHorizontal") < 0.0f))
                        {
                            // Decrease SFX Volume
                            GameObject.FindObjectOfType<Options>().sfxDecrease();
                            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
                            axisChanged = true;
                        }
                        else if (InputManager.controller && !axisChanged && (Input.GetAxis("CLSHorizontal") > 0.0f || Input.GetAxis("CDPadHorizontal") > 0.0f))
                        {
                            // Increase SFX Volume
                            GameObject.FindObjectOfType<Options>().sfxIncrease();
                            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
                            axisChanged = true;
                        }
                        else if (!InputManager.controller && !axisChanged && Input.GetAxis("KBHorizontal") < 0.0f)
                        {
                            // Decrease SFX Volume
                            GameObject.FindObjectOfType<Options>().sfxDecrease();
                            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
                            axisChanged = true;
                        }
                        else if (!InputManager.controller && !axisChanged && Input.GetAxis("KBHorizontal") > 0.0f)
                        {
                            // Increase SFX Volume
                            GameObject.FindObjectOfType<Options>().sfxIncrease();
                            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
                            axisChanged = true;
                        }
                        #endregion
                        break;
                    case 1:
                        #region Music
                        SelectorRemains.transform.localPosition = new Vector3(-140f, 80f, -5.1f);
                        if (InputManager.controller && !axisChanged && (Input.GetAxis("CLSHorizontal") < 0.0f || Input.GetAxis("CDPadHorizontal") < 0.0f))
                        {
                            // Decrease Music Volume
                            GameObject.FindObjectOfType<Options>().musicDecrease();
                            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                            axisChanged = true;
                        }
                        else if (InputManager.controller && !axisChanged && (Input.GetAxis("CLSHorizontal") > 0.0f || Input.GetAxis("CDPadHorizontal") > 0.0f))
                        {
                            // Increase Music Volume
                            GameObject.FindObjectOfType<Options>().musicIncrease();
                            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                            axisChanged = true;
                        }
                        else if (!InputManager.controller && !axisChanged && Input.GetAxis("KBHorizontal") < 0.0f)
                        {
                            // Decrease Music Volume
                            GameObject.FindObjectOfType<Options>().musicDecrease();
                            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                            axisChanged = true;
                        }
                        else if (!InputManager.controller && !axisChanged && Input.GetAxis("KBHorizontal") > 0.0f)
                        {
                            // Increase Music Volume
                            GameObject.FindObjectOfType<Options>().musicIncrease();
                            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                            axisChanged = true;
                        }


                        //SelectorRemains.transform.localPosition = new Vector3(-130f, 80f, -5.1f);
                        //if (InputManager.controller && !axisChanged && (Input.GetAxis("CLSHorizontal") < 0.0f || Input.GetAxis("CDPadHorizontal") < 0.0f))
                        //{
                        //    // Decrease Music Volume
                        //    GameObject.FindObjectOfType<Options>().musicDecrease();
                        //    musicVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                        //    axisChanged = true;
                        //}
                        //else if (InputManager.controller && !axisChanged && (Input.GetAxis("CLSHorizontal") > 0.0f || Input.GetAxis("CDPadHorizontal") > 0.0f))
                        //{
                        //    // Increase Music Volume
                        //    GameObject.FindObjectOfType<Options>().musicIncrease();
                        //    musicVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                        //    axisChanged = true;
                        //}
                        //else if (!InputManager.controller && !axisChanged && Input.GetAxis("KBHorizontal") < 0.0f)
                        //{
                        //    // Decrease Music Volume
                        //    GameObject.FindObjectOfType<Options>().musicDecrease();
                        //    musicVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                        //    axisChanged = true;
                        //}
                        //else if (!InputManager.controller && !axisChanged && Input.GetAxis("KBHorizontal") > 0.0f)
                        //{
                        //    // Increase Music Volume
                        //    GameObject.FindObjectOfType<Options>().musicIncrease();
                        //    musicVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                        //    axisChanged = true;
                        //}
                        #endregion
                        break;
                    case 2:
                        #region Fullscreen
                        SelectorRemains.transform.localPosition = new Vector3(-100f, 30f, -5.1f);
                        if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                        {
                            if (Screen.fullScreen)
                            {
                                Screen.SetResolution(1280, 720, !Screen.fullScreen);
                            }
                            else
                            {
                                Screen.SetResolution(1280, 720, Screen.fullScreen);
                            }
                            Screen.fullScreen = !Screen.fullScreen;
                        }
                        else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                        {
                            if (Screen.fullScreen)
                            {
                                Screen.SetResolution(1280, 720, !Screen.fullScreen);
                            }
                            else
                            {
                                Screen.SetResolution(1280, 720, Screen.fullScreen);
                            }
                            Screen.fullScreen = !Screen.fullScreen;
                        }
                        #endregion
                        break;
                    case 3:
                        #region Control Mode
                        SelectorRemains.transform.localPosition = new Vector3(-180f,-25f, -5.1f);
                        if (Input.GetButtonDown("CMenuAccept"))
                        {
                            InputManager.controller = !InputManager.controller;
                            controlMode.text = "Controls: " + (InputManager.controller ? "Controller" : "KB/Mouse");
                        }
                        else if (Input.GetButtonDown("KBMenuAccept"))
                        {
                            InputManager.controller = !InputManager.controller;
                            controlMode.text = "Controls: " + (InputManager.controller ? "Controller" : "KB/Mouse");
                        }
                        #endregion
                        break;
                    case 4:
                        #region Back
                        SelectorRemains.transform.localPosition = new Vector3(-70f, -85f, -5.1f);
                        if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                        {
                            SwapMode();
                        }
                        else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                        {
                            SwapMode();
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void SwapMode()
    {
        optionsMode = !optionsMode;
        if (optionsMode)
        {
            resume.enabled = false;
            options.enabled = false;
            quit.enabled = false;

            sVolume.enabled = true;
            sVolume.text = "SFX Volume: "  + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
            mVolume.enabled = true;
            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
            fullscreen.enabled = true;
            controlMode.enabled = true;
            if (!InputManager.controller)
            {
                controlMode.text = "Controls: KB/Mouse";
            }
            else
            {
                controlMode.text = "Controls: Controller";
            }
            back.enabled = true;
        }
        else
        {
            resume.enabled = true;
            options.enabled = true;
            quit.enabled = true;

            sVolume.enabled = false;
            mVolume.enabled = false;
            fullscreen.enabled = false;
            controlMode.enabled = false;
            back.enabled = false;
        }
    }

    void Pause()
    {
        isPaused = true;
        pauseCanvas.SetActive(true);
        heroEquipment.paused = true;
    }

    void UnPause()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        heroEquipment.paused = false;
    }
}
