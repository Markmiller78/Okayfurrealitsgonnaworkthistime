using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;
    Canvas pauseCanvas;
    int currOption = 0;
    int numOptions = 3;
    public GameObject arrow;
    bool axisChanged = false;

    void Start()
    {
        pauseCanvas = Canvas.FindObjectOfType<Canvas>();
        pauseCanvas.enabled = false;
        isPaused = false;
    }

    void Update()
    {
        Debug.Log("Paused = " + isPaused);
        if (isPaused)
        {
            if (InputManager.controller)
            {
                if (axisChanged && Input.GetAxis("CLSVertical") == 0.0f)
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
            }
            else
            {
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
            }
            if (currOption < 0)
                currOption = numOptions - 1;
            else if (currOption >= numOptions)
                currOption = 0;
            switch (currOption)
            {
                case 0:
                    arrow.GetComponent<RectTransform>().position = new Vector3(287.5f, 303f, 0.0f);
                    if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                    {
                        // Options Code Here
                    }
                    else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                    {
                        // Options Code Here
                    }
                    break;
                case 1:
                    arrow.GetComponent<RectTransform>().position = new Vector3(322.5f, 263f, 0.0f);
                    if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                    {
                        // Save Code Here
                    }
                    else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                    {
                        // Save Code Here
                    }
                    break;
                case 2:
                    arrow.GetComponent<RectTransform>().position = new Vector3(252.5f, 223f, 0.0f);
                    if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                    {
                        // Save & Quit Code Here
                    }
                    else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                    {
                        // Save & Quit Code Here
                    }
                    break;
                default:
                    break;
            }
        }
    }

    void Pause()
    {
        isPaused = true;
        pauseCanvas.enabled = true;
    }

    void UnPause()
    {
        isPaused = false;
        pauseCanvas.enabled = false;
    }
}
