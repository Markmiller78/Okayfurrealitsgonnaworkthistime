using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{


    public Text TutorialText;
    public Text TutorialText2;
    public GameObject DoorToOpen;
    float timer;
    float standardTimeStep;
    int Step;
    Vector2 Mouse, OldMouse;
    // Use this for initialization
    void Start()
    {
        timer = 2;
        Step = 0;
        standardTimeStep = 2;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        // ONE GIANT STEP BY STEP SWITCH CASE STATEMENT, IF YOU'RE EDITING THIS... HAVE FUN
        switch (Step)
        {
            case 0:
                {
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 1:
                {
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }

                    break;
                }
            case 2:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 3:
                {
                    if (InputManager.controller)
                        ChangeText("Use the Left Stick to Move");
                    else
                        ChangeText("Use WASD Keys to Move");
                    FadeIn();
                    if (timer < 0)
                    {
                        if (InputManager.controller)
                        {
                            if (Input.GetAxis("CLSVertical") < -.7f || Input.GetAxis("CLSHorizontal") < -.7f || Input.GetAxis("CLSHorizontal") > .7f || Input.GetAxis("CLSVertical") > .7f)
                            {
                                Step = 4;
                                timer = standardTimeStep;
                            }
                        }
                        else
                        {
                            if (Input.GetAxis("KBVertical") < 0 || Input.GetAxis("KBHorizontal") < 0 || Input.GetAxis("KBHorizontal") > 0 || Input.GetAxis("KBVertical") > 0)
                            {
                                Step = 4;
                                timer = standardTimeStep;
                            }
                        }
                    }
                    break;
                }
            case 4:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;

                }
            case 5:
                {
                    ChangeText("Good!");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;

                }
            case 6:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 7:
                {
                    if (InputManager.controller)
                        ChangeText("Use the Right Stick to Aim");
                    else
                        ChangeText("Use the Mouse to Aim");
                    FadeIn();
                    OldMouse = Mouse;
                    Mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                    if (timer < 0)
                    {
                        if (InputManager.controller)
                        {
                            if (Input.GetAxis("CRSVertical") < -.7f || Input.GetAxis("CRSHorizontal") < -.7f || Input.GetAxis("CRSHorizontal") > .7f || Input.GetAxis("CRSVertical") > .7f)
                            {
                                Step = 8;
                                timer = standardTimeStep;
                            }
                        }
                        else
                        {
                            if (Mouse != OldMouse)
                            {
                                Step = 8;
                                timer = standardTimeStep;
                            }
                        }
                    }
                    break;
                }
            case 8:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 9:
                {
                    ChangeText("Good!");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 10:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 11:
                {
                    if (InputManager.controller)
                        ChangeText("Right Trigger to Melee attack");
                    else
                        ChangeText("Spacebar to Melee attack");
                    FadeIn();
                    if (timer < 0)
                    {
                        if (InputManager.controller)
                        {
                            if (Input.GetAxis("CMeleeAndSpells") < 0.0f)
                            {
                                Step++;
                                timer = standardTimeStep;
                            }
                        }
                        else
                        {
                            if (Input.GetButton("KBMelee"))
                            {
                                Step++;
                                timer = standardTimeStep;
                            }
                        }
                    }
                    break;
                }
            case 12:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 13:
                {
                    ChangeText("Great!");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 14:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 15:
                {
                    if (InputManager.controller)
                        ChangeText("Left trigger to cast equipped spell");
                    else
                        ChangeText("Left mouse click to cast equipped spell");
                    FadeIn();
                    if (timer < 0)
                    {
                        if (InputManager.controller)
                        {
                            if (Input.GetAxis("CMeleeAndSpells") > 0.0f)
                            {
                                Step++;
                                timer = standardTimeStep;
                            }
                        }
                        else
                        {
                            if (Input.GetButtonDown("KBSpells"))
                            {
                                Step++;
                                timer = standardTimeStep;
                            }
                        }
                    }
                    break;
                }
            case 16:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 17:
                {
                    ChangeText("Casting Spells Cost Light");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 18:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 19:
                {
                    ChangeText("light drops from spells and enemies");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 20:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 21:
                {
                    ChangeText("If your light gets low you will be unable to cast!");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 22:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 23:
                {
                    if (InputManager.controller)
                        ChangeText("Right Bumper to Absorb dropped light in the room");
                    else
                        ChangeText("Press Q to Absorb dropped light in the room");
                    FadeIn();
                    if (timer < 0)
                    {
                        if (InputManager.controller)
                        {
                            if (Input.GetButtonDown("CLightCollect"))
                            {
                                Step++;
                                timer = standardTimeStep;
                            }
                        }
                        else
                        {
                            if (Input.GetButtonDown("KBLightCollect"))
                            {
                                Step++;
                                timer = standardTimeStep;
                            }
                        }
                    }
                    break;
                }
            case 24:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 25:
                {
                    if (InputManager.controller)
                        ChangeText("Left Bumper to use Equipped Dash Spell");
                    else
                        ChangeText("Press Shift to use Equipped Dash Spell");
                    FadeIn();
                    if (timer < 0)
                    {
                        if (InputManager.controller)
                        {
                            if (Input.GetButtonDown("CDash"))
                            {
                                Step++;
                                timer = standardTimeStep;
                            }
                        }
                        else
                        {
                            if (Input.GetButtonDown("KBDash"))
                            {
                                Step++;
                                timer = standardTimeStep;
                            }
                        }
                    }
                    break;
                }
            case 26:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 27:
                {
                    ChangeText("Defeat Enemies in the room to Advance");
                    FadeIn();
                    if (timer < 0)
                    {
                        GameObject[] EnemyCount = GameObject.FindGameObjectsWithTag("Enemy");
                        if (EnemyCount.Length == 0)
                        {
                            Step++;
                            timer = standardTimeStep;
                        }
                    }
                    break;
                }
            case 28:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 29:
                {
                    ChangeText("Enemies hit with spells drop Gold Light");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 30:
                {
                   // FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 31:
                {
                    ChangeText2("Gold Light restores health");
                    FadeIn2();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 32:
                {
                    FadeOut();
                    FadeOut2();
                    if (timer < 0)
                    {
                        GameObject[] EnemyCount = GameObject.FindGameObjectsWithTag("Enemy");
                        if (EnemyCount.Length == 1)
                        {
                            Step++;
                            timer = standardTimeStep;
                        }
                    }
                    break;
                }
            case 33:
                {
                    ChangeText("This next enemy steals Light");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 34:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 35:
                {
                    if (InputManager.controller)
                        ChangeText("Use Absorb(RB) to Steal your light back");
                    else
                        ChangeText("Use Absorb(Q) to Steal your light back");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 36:
                {
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 37:
                {
                    ChangeText2("This deals extra damage");
                    FadeIn2();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 38:
                {
                    FadeOut();
                    FadeOut2();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 39:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        GameObject[] EnemyCount = GameObject.FindGameObjectsWithTag("Enemy");
                        if (EnemyCount.Length == 0)
                        {
                            Step++;
                            timer = standardTimeStep;
                        }
                    }
                    break;
                }
            case 40:
                {
                    if (DoorToOpen != null)
                        Destroy(DoorToOpen);
                    if (InputManager.controller)
                        ChangeText("Press X to Open Chests");
                    else
                        ChangeText("Press E to Open Chests");
                    FadeIn();
                    if (timer < 0)
                    {
                        if (InputManager.controller)
                        {
                            if (Input.GetButtonDown("CInteract"))
                            {
                                Step++;
                                timer = standardTimeStep;
                            }
                        }
                        else
                        {
                            if (Input.GetButtonDown("KBInteract"))
                            {
                                Step++;
                                timer = standardTimeStep;
                            }
                        }
                    }
                    break;
                }
            case 41:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 42:
                {
                    if (InputManager.controller)
                        ChangeText("X also equips items");
                    else
                        ChangeText("E also equips items");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 43:
                {
                    FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 44:
                {
                    ChangeText("Boots and Accessories Change your equipped spell");
                    FadeIn();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 45:
                {
                    //FadeOut();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            case 46:
                {
                    ChangeText2("Embers Augment your spells with elemental effects");
                    FadeIn2();
                    if (timer < 0)
                    {
                        Step++;
                        timer = 5;
                    }
                    break;
                }
            case 47:
                {
                    FadeOut();
                    FadeOut2();
                    if (timer < 0)
                    {
                        Step++;
                        timer = standardTimeStep;
                    }
                    break;
                }
            default:
                break;
        }


        GameObject[] EnemyCount2 = GameObject.FindGameObjectsWithTag("Enemy");
        if (EnemyCount2.Length == 0 && Step < 27)
        {
            Step = 28;
        }

        //print(Step);
    }

    void ChangeText(string theText)
    {
        TutorialText.text = theText;
    }
    void ChangeText2(string theText)
    {
        TutorialText2.text = theText;
    }

    void FadeIn()
    {
        if (TutorialText.color.a < 1)
            TutorialText.color += new Color(0, 0, 0, 1 * Time.deltaTime);
    }

    void FadeOut()
    {
        if (TutorialText.color.a > 0)
            TutorialText.color -= new Color(0, 0, 0, 1 * Time.deltaTime);
    }
    void FadeIn2()
    {
        if (TutorialText2.color.a < 1)
            TutorialText2.color += new Color(0, 0, 0, 1 * Time.deltaTime);
    }
    void FadeOut2()
    {
        if (TutorialText2.color.a > 0)
            TutorialText2.color -= new Color(0, 0, 0, 1 * Time.deltaTime);
    }

}
