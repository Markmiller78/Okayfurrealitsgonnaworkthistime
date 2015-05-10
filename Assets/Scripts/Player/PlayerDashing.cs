using UnityEngine;
using System.Collections;

public class PlayerDashing : MonoBehaviour
{

    public float dashSpeed;
    public float dashDuration;
    public float earthtrailtime;

    float timeRemaining;

    CharacterController controller;
    Vector2 MoveDirect;
    PlayerEquipment heroEquipment;
    PlayerLight heroLight;

    PlayerCooldowns heroCooldowns;
    float trailBlazerDropTimer;

    Animator anim;

    public GameObject lightRemains;
    public GameObject BlinkEffect;
    public GameObject FireTrail;
    public GameObject LightTrail;
    public GameObject WindTrail;
    public GameObject IceTrail;
    public GameObject LifeTrail;
    public GameObject EarthTrail;
    public GameObject DeathTrail;


    public GameObject lightDecoy;
    public GameObject fireDecoy; 
    public GameObject iceDecoy; 
    public GameObject windDecoy; 
    public GameObject earthDecoy; 
    public GameObject lifeDecoy;
    public GameObject deathDecoy; 

    public HUDCooldowns UICD;

    public GameObject FireCharge;
    public GameObject LightCharge;
    public GameObject WindCharge;
    public GameObject IceCharge;
    public GameObject LifeCharge;
    public GameObject EarthCharge;
    public GameObject DeathCharge;
    void Start()
    {
        earthtrailtime = 0;
        anim = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        heroEquipment = gameObject.GetComponent<PlayerEquipment>();
        heroLight = gameObject.GetComponent<PlayerLight>();
        heroCooldowns = gameObject.GetComponent<PlayerCooldowns>();
        trailBlazerDropTimer = 0.0f;
        UICD = GameObject.Find("Boot").GetComponent<HUDCooldowns>();
    }

    void Update()
    {
        if (heroEquipment.paused == false)
        {

            //If the player is dashing, perform the dash
            if (timeRemaining > 0)
            {

                timeRemaining -= Time.deltaTime;

                if (heroEquipment.equippedBoot != boot.Blink)
                {
                    //Set the dash direction to the direction the player is currently facing

                    //Check if the player is touching the joysticks
                    MoveDirect.x = Input.GetAxis("CLSHorizontal");
                    MoveDirect.y = Input.GetAxis("CLSVertical");

                    //If the player wasn't touching the joysticks, check for WASD input
                    if (MoveDirect.x == 0.0f && MoveDirect.y == 0.0f)
                    {
                        MoveDirect.x = Input.GetAxis("KBHorizontal");
                        MoveDirect.y = Input.GetAxis("KBVertical");
                    }

                    //If the player wasn't using the joysticks OR the WASD keys, dash in the direction they are facing
                    if (MoveDirect.x == 0.0f && MoveDirect.y == 0.0f)
                    {
                        MoveDirect = transform.TransformDirection(Vector3.up);

                        //Factor in speed and time
                        //Increase the dashspeed to make up for the player not moving on their own during the dash
                        MoveDirect *= (dashSpeed + 3.1f) * Time.deltaTime;
                    }
                    else
                    {
                        //Factor in speed and time
                        MoveDirect *= dashSpeed * Time.deltaTime;
                    }

                    //Actually Move the player
                    controller.Move(MoveDirect);

                    if (heroEquipment.equippedBoot == boot.Trailblazer)
                    {
                        trailBlazerDropTimer += Time.deltaTime;
                        if (trailBlazerDropTimer > 0.03f)
                        {
                            //No ember equipped
                            if (heroEquipment.equippedEmber == ember.None)
                            {
                                Instantiate(LightTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Ice ember equipped
                            else if (heroEquipment.equippedEmber == ember.Ice)
                            {
                                Instantiate(IceTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Fire ember equipped
                            else if (heroEquipment.equippedEmber == ember.Fire)
                            {
                                Instantiate(FireTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Wind ember equipped
                            else if (heroEquipment.equippedEmber == ember.Wind)
                            {
                                Instantiate(WindTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Earth ember equipped
                            else if (heroEquipment.equippedEmber == ember.Earth)
                            {
                                Instantiate(EarthTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Life ember equipped
                            else if (heroEquipment.equippedEmber == ember.Life)
                            {
                                Instantiate(LifeTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Death ember equipped
                            else if (heroEquipment.equippedEmber == ember.Death)
                            {
                                Instantiate(DeathTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            else if (heroEquipment.equippedEmber == ember.Earth)
                            {
                               earthtrailtime+=Time.deltaTime;
                            
                                if (earthtrailtime >= 3.0f)
                                {
                                   Instantiate(EarthTrail, transform.position, new Quaternion(0, 0, 0, 0));
                                    earthtrailtime = 0.0f;
                                }
                            }
                            trailBlazerDropTimer = 0.0f;

                        }
                        if(heroEquipment.equippedBoot==boot.Whirlwind)
                        {
                            float tempangle = 180.0f;

                            Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
                            GetComponent<SpriteRenderer>().transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4.5f);
                        }
                    }
                } 
            }
        }
    }
    void Dash()
    {
        //If the player isnt already dashing, begin dashing
        if (!heroCooldowns.dashCooling)
        {
            //Make the player spend light
            heroLight.LoseLight(5);
            heroCooldowns.dashCooling = true;
            timeRemaining = dashDuration;
            //Change the animation state into dashing
            anim.CrossFade("PlayerDashing", 0.01f);
            //Set up the local variables
            trailBlazerDropTimer = 0.1f;

            if (heroEquipment.equippedBoot == boot.Charge)
            {
                //No ember equipped
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(LightCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Ice ember equipped
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
                    Instantiate(IceCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Fire ember equipped
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                    Instantiate(FireCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Wind ember equipped
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    Instantiate(WindCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Life ember equipped
                else if (heroEquipment.equippedEmber == ember.Life)
                {
                    Instantiate(LifeCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                else if (heroEquipment.equippedEmber == ember.Death)
                {
                    Instantiate(DeathCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                else if (heroEquipment.equippedEmber == ember.Earth)
                {
                    Instantiate(EarthCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
            }

            if (heroEquipment.equippedBoot == boot.Blink)
            {
                //No ember equipped
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(BlinkEffect, transform.position, new Quaternion(0, 0, 0, 0));
                    Instantiate(lightRemains, transform.position, transform.rotation);
                  
                }
                //Ice ember equipped
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
           
                }
                //Fire ember equipped
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                   
                }
                //Wind ember equipped
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                 
                }
                //Life ember equipped
                else if (heroEquipment.equippedEmber == ember.Life)
                {
                   
                }
                else if (heroEquipment.equippedEmber == ember.Death)
                {
                }
                else if (heroEquipment.equippedEmber == ember.Earth)
                {
                }
                Vector3 temp = transform.TransformDirection(Vector3.up.normalized);
                transform.position += temp * 3.0f;

            }
            if (heroEquipment.equippedBoot == boot.Whirlwind)
            {
                //No ember equipped
                if (heroEquipment.equippedEmber == ember.None)
                {
                    

                }
                //Ice ember equipped
                else if (heroEquipment.equippedEmber == ember.Ice)
                {

                }
                //Fire ember equipped
                else if (heroEquipment.equippedEmber == ember.Fire)
                {

                }
                //Wind ember equipped
                else if (heroEquipment.equippedEmber == ember.Wind)
                {

                }
                //Life ember equipped
                else if (heroEquipment.equippedEmber == ember.Life)
                {

                }
                //Death ember equipped
                else if (heroEquipment.equippedEmber == ember.Death)
                {

                }
                //Earth ember equipped
                else if (heroEquipment.equippedEmber == ember.Earth)
                {

                }
              

            }
            if (heroEquipment.equippedBoot == boot.Decoy)
            {
             

                 
                //No ember equipped
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(lightDecoy, transform.position, new Quaternion(0, 0, 0, 0));

                }
                //Ice ember equipped
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
                    Instantiate(iceDecoy, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Fire ember equipped
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                    Instantiate(fireDecoy, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Wind ember equipped
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    Instantiate(windDecoy, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Life ember equipped
                else if (heroEquipment.equippedEmber == ember.Life)
                {
                    Instantiate(lifeDecoy, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Death ember equipped
                else if (heroEquipment.equippedEmber == ember.Death)
                {
                    Instantiate(deathDecoy, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Earth ember equipped
                else if (heroEquipment.equippedEmber == ember.Earth)
                {
                    Instantiate(earthDecoy, transform.position, new Quaternion(0, 0, 0, 0));
                }


                
            }
        }
    }

}
