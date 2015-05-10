using UnityEngine;
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
    Vector3 RemainsPOS;
    void Start()
    {
        RemainsPOS = new Vector3(1, 1, 1);
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
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
                    SelectorRemains.transform.localPosition = new Vector3(-106f, 37f, -5.1f);
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
                    SelectorRemains.transform.localPosition = new Vector3(-77f, -4.7f, -5.1f);
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
                    SelectorRemains.transform.localPosition = new Vector3(-147f, -42f, -5.1f);
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
        heroEquipment.paused = true;
    }

    void UnPause()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        heroEquipment.paused = false;
    }
}
