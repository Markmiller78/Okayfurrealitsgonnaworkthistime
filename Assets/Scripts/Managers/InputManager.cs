using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    GameObject player;
    Vector3 mousePrevPos;
    bool isControllerConnected = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetJoystickNames().Length != 0)
        {
            isControllerConnected = true;
        }
        if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
        {
            player.SendMessage("Move");
        }
        if (Input.GetAxis("RStickHorizontal") != 0.0f || Input.GetAxis("RStickVertical") != 0.0f)
        {
            player.SendMessage("Rotate");
        }
        else if (!isControllerConnected && mousePrevPos != Input.mousePosition)
        {
            player.SendMessage("MouseRotate");
        }
        mousePrevPos = Input.mousePosition;
        if (Input.GetButtonDown("Dash"))
        {
            player.SendMessage("Dash", SendMessageOptions.DontRequireReceiver);
        }
    }
}
