using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    CharacterController controller;
    Vector2 MoveDirect;
    Vector2 CharRotate;
    Quaternion Rotation;
    Vector3 Rotate3d;
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        speed = 1;
    }

	void Update ()
    {
        
	}
   
    void Move()
    {

        //Check for Left Stick Axis to 
        //see if it is surpressed fully '
        //for more speed
        if (Input.GetAxis("Horizontal") > .8f || Input.GetAxis("Vertical") > .8f || Input.GetAxis("Vertical") < -.8f || Input.GetAxis("Horizontal") < -.8f)
        {
            speed = 3.1f;
            // print("Speedup");
        }
        else
            speed = 1.6f;

        //Check Left Joysticks for Movement
        MoveDirect.x = Input.GetAxis("Horizontal");
        MoveDirect.y = Input.GetAxis("Vertical");

        //Normalize the directional vector
        //Factor in speed and time
        MoveDirect.Normalize();
        MoveDirect *= speed * Time.deltaTime;

        //Actually Move the player
        controller.Move(MoveDirect);

        //Rotate the player to where they are moving
        if (MoveDirect != Vector2.zero)
        {
            float angle = Mathf.Atan2(MoveDirect.y, MoveDirect.x) * Mathf.Rad2Deg;
            angle += 270;
            controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (MoveDirect != Vector2.zero)
        {
            float angle = Mathf.Atan2(MoveDirect.y, MoveDirect.x) * Mathf.Rad2Deg;
            angle += 270;
            controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void Rotate()
    {
        //Check Right sticks for Rotation
        MoveDirect.x = Input.GetAxis("RStickHorizontal");
        MoveDirect.y = Input.GetAxis("RStickVertical");
        MoveDirect.Normalize();

        //If Right sticks have input
        //override the Move() function rotation
        //with this one for aiming
        if (MoveDirect != Vector2.zero)
        {
            float angle = Mathf.Atan2(MoveDirect.y, -MoveDirect.x) * Mathf.Rad2Deg;
            angle += 90;
            controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (MoveDirect != Vector2.zero)
        {
            float angle = Mathf.Atan2(MoveDirect.y, -MoveDirect.x) * Mathf.Rad2Deg;
            angle += 90;
            controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    void MouseRotate()
    {











    }
}
