using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    GameObject player;

    PlayerCooldowns cooldowns;
    PlayerMeleeAttack melee;
    public static bool controller = false;
    bool isPaused = false;
    bool mapMenu = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cooldowns = player.GetComponent<PlayerCooldowns>();
        melee = player.GetComponentInChildren<PlayerMeleeAttack>();
    }

    void Update()
    {
        // Toggle between KB/M and Controller modes with 0 (zero) key
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            controller = !controller;
        }
        if (controller)
        {
            #region Controller Input
            if (!isPaused && !mapMenu)
            {
                // LStick to move
                if (Input.GetAxis("CLSHorizontal") != 0.0f || Input.GetAxis("CLSVertical") != 0.0f)
                {
                    player.SendMessage("CMove");
                }
                // RStick to rotate
                if (Input.GetAxis("CRSHorizontal") != 0.0f || Input.GetAxis("CRSVertical") != 0.0f)
                {
                    player.SendMessage("Rotate");
                }
                // RTrigger to melee
                if (Input.GetAxis("CMeleeAndSpells") < 0.0f && !cooldowns.meleeCooling)
                {
                    melee.SendMessage("Melee");
                    cooldowns.meleeCooling = true;
                }
                // LTrigger to cast
                if (Input.GetAxis("CMeleeAndSpells") > 0.0f)
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
                    GameObject chest = GameObject.FindGameObjectWithTag("Chest");
                    chest.SendMessage("Interact");
                }
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
                        obj.SendMessage("Pause", SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("UnPause", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            // Back for map/stats
            if (!isPaused && Input.GetButtonDown("CMapAndStats"))
            {
                GameObject[] allObjects;
                allObjects = GameObject.FindObjectsOfType<GameObject>();
                mapMenu = !mapMenu;
                if (mapMenu)
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
                        obj.SendMessage("UnMapAndStats", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            #endregion
        }
        else
        {
            #region KB/M controls
            if (!isPaused && !mapMenu)
            {
                // WASD or arrow keys to move
                if (Input.GetAxis("KBHorizontal") != 0.0f || Input.GetAxis("KBVertical") != 0.0f)
                {
                    player.SendMessage("KBMove");
                }
                // Mouse to rotate
                player.SendMessage("MouseRotate");
                // Space to melee
                if (Input.GetButton("KBMelee") && !cooldowns.meleeCooling)
                {
                    melee.SendMessage("Melee");
                    cooldowns.meleeCooling = true;
                }
                // Left Click to cast
                if (Input.GetButtonDown("KBSpells"))
                {
                    player.SendMessage("CastSpell", SendMessageOptions.DontRequireReceiver);
                }
                // Left Shift to dash
                if (Input.GetButtonDown("KBDash"))
                {
                    player.SendMessage("Dash", SendMessageOptions.DontRequireReceiver);
                }
                // Q to collect light
                if (Input.GetButtonDown("KBLightCollect"))
                {
                    player.SendMessage("CollectLight", SendMessageOptions.DontRequireReceiver);
                }
                // E to interact
                if (Input.GetButtonDown("KBInteract"))
                {
					player.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
                    GameObject chest = GameObject.FindGameObjectWithTag("Chest");
					chest.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
                }
            }
            // Escape or P to pause
            if (!mapMenu && Input.GetButtonDown("KBPause"))
            {
                GameObject[] allObjects;
                allObjects = GameObject.FindObjectsOfType<GameObject>();
                isPaused = !isPaused;
                if (isPaused)
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("Pause", SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("UnPause", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            // ~ for map/stats
            if (!isPaused && Input.GetButtonDown("KBMapAndStats"))
            {
                GameObject[] allObjects;
                allObjects = GameObject.FindObjectsOfType<GameObject>();
                mapMenu = !mapMenu;
                if (mapMenu)
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
                        obj.SendMessage("UnMapAndStats", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            #endregion
        }
    }
}
