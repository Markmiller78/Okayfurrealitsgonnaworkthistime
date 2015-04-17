using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    GameObject player;
    Vector3 mousePrevPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
        {
            player.SendMessage("Move");
        }
        if (Input.GetAxis("RStickHorizontal") != 0.0f || Input.GetAxis("RStickVertical") != 0.0f)
        {
            player.SendMessage("Rotate");
        }
        else if (mousePrevPos != Input.mousePosition)
        {
            player.SendMessage("MouseRotate");
        }
        mousePrevPos = Input.mousePosition;
    }
}
