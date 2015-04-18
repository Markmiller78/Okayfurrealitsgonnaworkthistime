using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    GameObject player;
    Vector3 mousePrevPos;
    public bool controller = false;
    bool isPaused = false;
    bool mapMenu = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Toggle between KB/M and Controller modes with 0 (zero) key
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            controller = !controller;
        }
        if (controller) // controller controls
        {
            // LStick to move
            if (Input.GetAxis("CLSHorizontal") != 0.0f || Input.GetAxis("CLSVertical") != 0.0f)
            {
                player.SendMessage("Move");
            }
            // RStick to rotate
            if (Input.GetAxis("CRSHorizontal") != 0.0f || Input.GetAxis("CRSVertical") != 0.0f)
            {
                player.SendMessage("Rotate");
            }
            // RTrigger to melee
            if (Input.GetAxis("MeleeAndSpells") < 0.0f)
            {
                player.SendMessage("Melee", SendMessageOptions.DontRequireReceiver);
            }
            // LTrigger to cast
            if (Input.GetAxis("MeleeAndSpells") > 0.0f)
            {
                player.SendMessage("CastSpell", SendMessageOptions.DontRequireReceiver);
            }
            // LB to dash
            if (Input.GetButtonDown("CDash"))
            {
                player.SendMessage("Dash", SendMessageOptions.DontRequireReceiver);
            }
            // RB to collect light
            if (Input.GetButtonDown("CLightCollect"))
            {
                player.SendMessage("CollectLight", SendMessageOptions.DontRequireReceiver);
            }
            // X to interact
            if (Input.GetButtonDown("CInteract"))
            {
                player.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
            }
            // Start to pause
            if (!mapMenu && Input.GetButtonDown("CPause"))
            {
                GameObject[] allObjects;
                allObjects = GameObject.FindObjectsOfType<GameObject>();
                isPaused = !isPaused;
                if (isPaused)
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("Unpause", SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("Pause", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            if (!isPaused && Input.GetButtonDown("CMapAndStats"))
            {
                GameObject[] allObjects;
                allObjects = GameObject.FindObjectsOfType<GameObject>();
                mapMenu = !mapMenu;
                if (isPaused)
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("MapAndStats", SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("MapAndStats", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
        else // KB/M controls
        {
            // WASD or arrow keys to move
            if (Input.GetAxis("KBHorizontal") != 0.0f || Input.GetAxis("KBVertical") != 0.0f)
            {
                player.SendMessage("Move");
            }
            // Mouse to rotate
            player.SendMessage("MouseRotate");
            // C to melee
            if (Input.GetButtonDown("KBMelee"))
            {
                player.SendMessage("Melee", SendMessageOptions.DontRequireReceiver);
            }
            // V to cast
            if (Input.GetButtonDown("KBSpells"))
            {
                player.SendMessage("CastSpell", SendMessageOptions.DontRequireReceiver);
            }
            // J to dash
            if (Input.GetButtonDown("KBDash"))
            {
                player.SendMessage("Dash", SendMessageOptions.DontRequireReceiver);
            }
            // Q to collect light
            if (Input.GetButtonDown("KBLightCollect"))
            {
                player.SendMessage("CollectLight", SendMessageOptions.DontRequireReceiver);
            }
            // K to interact
            if (Input.GetButtonDown("KBInteract"))
            {
                player.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
            }
            // Escape or P to pause
            if (Input.GetButtonDown("KBPause"))
            {
                GameObject[] allObjects;
                allObjects = GameObject.FindObjectsOfType<GameObject>();
                isPaused = !isPaused;
                if (isPaused)
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("Unpause", SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("Pause", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }
}
