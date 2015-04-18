using UnityEngine;
using System.Collections;

public class PlayerDashing : MonoBehaviour {

    public float dashSpeed;
    public float dashDuration;

    CharacterController controller;
    Vector2 MoveDirect;

    float dashTimeRemaining;
    float trailBlazerDropTimer;

    public GameObject TrailBlazerPickUp;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        dashTimeRemaining = 0.0f;
        trailBlazerDropTimer = 0.0f;
    }

    void Update()
    {
        //If the player is dashing, perform the dash
        if (dashTimeRemaining > 0.0f)
        {
            //Decrement the dashTimeRemaining
            dashTimeRemaining -= Time.deltaTime;

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

            if (gameObject.GetComponent<PlayerEquipment>().equippedBoot == (int)boot.Trailblazer)
            {
                trailBlazerDropTimer += Time.deltaTime;
                if (trailBlazerDropTimer > 0.03f)
                {
                    Instantiate(TrailBlazerPickUp, transform.position, transform.rotation);
                    trailBlazerDropTimer = 0.0f;
                }
            }

        }
    }
    void Dash()
    {
        //If the player isnt already dashing, begin dashing
        if (dashTimeRemaining <= 0.0f)
        {
            dashTimeRemaining = dashDuration;
            trailBlazerDropTimer = 0.1f;
        }
    }
}
