using UnityEngine;
using System.Collections;

public class PlayerDashing : MonoBehaviour
{

    public float dashSpeed;
    public float dashDuration;

    float timeRemaining;

    CharacterController controller;
    Vector2 MoveDirect;
    PlayerEquipment heroEquipment;
    PlayerLight heroLight;

    PlayerCooldowns heroCooldowns;
    float trailBlazerDropTimer;
	public GameObject Decoy;
	public GameObject lightRemains;
    public GameObject BlinkEffect;
    public GameObject FireTrail;
    public GameObject LightTrail;
    public GameObject WindTrail;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        heroEquipment = gameObject.GetComponent<PlayerEquipment>();
        heroLight = gameObject.GetComponent<PlayerLight>();
        heroCooldowns = gameObject.GetComponent<PlayerCooldowns>();
        trailBlazerDropTimer = 0.0f;
    }

    void Update()
    {
	
        //If the player is dashing, perform the dash
        if (timeRemaining > 0) {

			timeRemaining -= Time.deltaTime;

			if (heroEquipment.equippedBoot != boot.Blink&&heroEquipment.equippedBoot != boot.Whirlwind) {
				//Set the dash direction to the direction the player is currently facing

				//Check if the player is touching the joysticks
				MoveDirect.x = Input.GetAxis ("CLSHorizontal");
				MoveDirect.y = Input.GetAxis ("CLSVertical");

				//If the player wasn't touching the joysticks, check for WASD input
				if (MoveDirect.x == 0.0f && MoveDirect.y == 0.0f) {
					MoveDirect.x = Input.GetAxis ("KBHorizontal");
					MoveDirect.y = Input.GetAxis ("KBVertical");
				}

				//If the player wasn't using the joysticks OR the WASD keys, dash in the direction they are facing
				if (MoveDirect.x == 0.0f && MoveDirect.y == 0.0f) {
					MoveDirect = transform.TransformDirection (Vector3.up);

					//Factor in speed and time
					//Increase the dashspeed to make up for the player not moving on their own during the dash
					MoveDirect *= (dashSpeed + 3.1f) * Time.deltaTime;
				} else {
					//Factor in speed and time
					MoveDirect *= dashSpeed * Time.deltaTime;
				}

				//Actually Move the player
				controller.Move (MoveDirect);

				if (heroEquipment.equippedBoot == boot.Trailblazer) {
					trailBlazerDropTimer += Time.deltaTime;
					if (trailBlazerDropTimer > 0.03f) {
						//No ember equipped
						if (heroEquipment.equippedEmber == ember.None) {
							Instantiate (LightTrail, transform.position, new Quaternion (0, 0, 0, 0));
						}
                    //Fire ember equipped
                    else if (heroEquipment.equippedEmber == ember.Fire) {
							Instantiate (FireTrail, transform.position, new Quaternion (0, 0, 0, 0));
						}
                    //Wind ember equipped
                    else if (heroEquipment.equippedEmber == ember.Wind) {
							Instantiate (WindTrail, transform.position, new Quaternion (0, 0, 0, 0));
						}
						trailBlazerDropTimer = 0.0f;
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

            //Set up the local variables
            trailBlazerDropTimer = 0.1f;

            if (heroEquipment.equippedBoot == boot.Trailblazer)
            {
                //  Instantiate(TrailBlazerExplosion, transform.position, new Quaternion(0, 0, 0, 0));

            }

			if (heroEquipment.equippedBoot == boot.Blink)
            {
                Instantiate(BlinkEffect, transform.position, new Quaternion(0, 0, 0, 0));
				Instantiate(lightRemains, transform.position, transform.rotation);  
               // blinkdropTimer = 0.0f;
                Vector3 temp = transform.TransformDirection(Vector3.up.normalized);
                transform.position += temp * 3.0f;

		
            }
			if (heroEquipment.equippedBoot == boot.Whirlwind)
			{

				float tempangle = 720.0f;
				Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime *4.5f);

			}

			if (heroEquipment.equippedBoot == boot.Decoy)
			{
				Instantiate(Decoy, transform.position, new Quaternion(0, 0, 0, 0));

			 
				
				
			}
        }
    }

// void OnCollisionEnter(Collider other)
//    {
//        if (other.tag == "Wall") 
//        {

//        }

//    }
}
