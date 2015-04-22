using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;
    GameObject pauseCanvas;
    int currOption = 0;
    int numOptions = 3;
    public GameObject arrow;
    bool axisChanged = false;

    void Start()
    {
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        pauseCanvas.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (isPaused)
        {
            if (InputManager.controller)
            {
                #region Controller Input
                if (axisChanged && Input.GetAxis("CLSVertical") == 0.0f && Input.GetAxis("CDPadVertical") == 0.0f)
                {
                    axisChanged = false;
                }
                if (!axisChanged && (Input.GetAxis("CLSVertical") > 0.0f || Input.GetAxis("CDPadVertical") > 0.0f))
                {
                    currOption--;
                    axisChanged = true;
                }
                if (!axisChanged && (Input.GetAxis("CLSVertical") < 0.0f || Input.GetAxis("CDPadVertical") < 0.0f))
                {
                    currOption++;
                    axisChanged = true;
                }
                #endregion
            }
            else
            {
                #region KB/M Input
                if (axisChanged && Input.GetAxis("KBVertical") == 0.0f)
                {
                    axisChanged = false;
                }
                if (!axisChanged && Input.GetAxis("KBVertical") > 0.0f)
                {
                    currOption--;
                    axisChanged = true;
                }
                if (!axisChanged && Input.GetAxis("KBVertical") < 0.0f)
                {
                    currOption++;
                    axisChanged = true;
                }
                #endregion
            }
            #region Wrapping
            if (currOption < 0)
                currOption = numOptions - 1;
            else if (currOption >= numOptions)
                currOption = 0;
            #endregion
            switch (currOption)
            {
                case 0:
                    #region Options Menu Stuff
                    arrow.GetComponent<RectTransform>().position = new Vector3(527.5f, 420f, 0.0f);
                    if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                    {
                        // Options Code Here                       
                    }
                    else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                    {
                        // Options Code Here
                    }
                    break;
                    #endregion
                case 1:
                    #region Saving
                    arrow.GetComponent<RectTransform>().position = new Vector3(562.5f, 380f, 0.0f);
                    if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                    {
                        // Save Code Here
                    }
                    else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                    {
                        // Save Code Here
                    }
                    break;
                    #endregion
                case 2:
                    #region Save & Quit
                    arrow.GetComponent<RectTransform>().position = new Vector3(492.5f, 340f, 0.0f);
                    if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                    {
                        // Save & Quit Code Here
                    }
                    else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                    {
                        // Save & Quit Code Here
                    }
                    break;
                    #endregion
                default:
                    break;
            }
        }
    }

    void Pause()
    {
        isPaused = true;
        pauseCanvas.SetActive(true);
    }

    void UnPause()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
    }
}
