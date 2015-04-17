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
        speed = .05f;
    }
	// Update is called once per frame
	void Update ()
    {

        //if(Input.GetKeyDown(KeyCode "w"))
        //{

        //}
        if (Input.GetAxis("Horizontal") > .8f || Input.GetAxis("Vertical") > .8f || Input.GetAxis("Vertical") < -.8f || Input.GetAxis("Horizontal") < -.8f)
        {
            speed = 4.5f;
            print("Speedup");
        }
        else
            speed = 2.5f;
        
        MoveDirect.x = Input.GetAxis("Horizontal");
        MoveDirect.y = Input.GetAxis("Vertical");

        MoveDirect.Normalize();
        MoveDirect *= speed * Time.deltaTime;
        Move();


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


        MoveDirect.x = Input.GetAxis("RStickHorizontal");
        MoveDirect.y = Input.GetAxis("RStickVertical");
        MoveDirect.Normalize();


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
   
    void Move()
    {
        controller.Move(MoveDirect);
    }

    void Rotate()
    {

    }
}
