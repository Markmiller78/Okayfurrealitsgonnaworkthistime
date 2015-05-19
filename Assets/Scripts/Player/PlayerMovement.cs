using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{

    public float fullSpeed;
    public float halfSpeed;
    public bool isinHazard = false;
    float speed;
    float knockbackTimer;
//    GameObject player;
    public bool stunned = false;
    float stunTimer;
    float sTimerMax;

    CharacterController controller;
    //Rigidbody2D rb2d;
	PlayerEquipment heroEquipment;
    PlayerCooldowns heroCooldowns;
    Vector2 MoveDirect;
    Vector2 CharRotate;
    Quaternion Rotation;
    Vector3 Rotate3d;
    Vector3 KnockbackVec;
    Animator anim;

    void Start()
    {
        
//        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
		heroEquipment = gameObject.GetComponent<PlayerEquipment>();
        controller = gameObject.GetComponent<CharacterController>();
        heroCooldowns = gameObject.GetComponent<PlayerCooldowns>();
        //rb2d = GetComponent<Rigidbody2D>();
        fullSpeed = 3.1f;
        halfSpeed = 1.6f;
        knockbackTimer = 0;
        stunned = false;
        sTimerMax = .5f;
        stunTimer = sTimerMax;
    }

    void Update()
    {
        if (heroEquipment.paused == false)
        {
            if (stunned)
            {
                stunTimer -= Time.deltaTime;
                if (stunTimer <= 0f)
                {
                    stunTimer = sTimerMax;
                    stunned = false;
                }
            }
            knockbackTimer -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f);

            if (knockbackTimer > 0)
            {
                controller.Move(KnockbackVec * 9 * Time.deltaTime);
            }
        }
    }

    void CMove()
    {
        if (heroEquipment.paused == false && !stunned)
        {

            //Check for Left Stick Axis to 
            //see if it is surpressed fully
            //for more speed
          
            if (Input.GetAxis("CLSHorizontal") > .8f || Input.GetAxis("CLSVertical") > .8f || Input.GetAxis("CLSVertical") < -.8f || Input.GetAxis("CLSHorizontal") < -.8f)
            {
              
                speed = fullSpeed;
            }
            else
                speed = halfSpeed;

            //Check Left Joysticks for Movement
            MoveDirect.x = Input.GetAxis("CLSHorizontal");
            MoveDirect.y = Input.GetAxis("CLSVertical");
            if (MoveDirect != Vector2.zero && anim.name != "Spellcasting")
                anim.CrossFade("PlayerWalking", 0.01f);
            else
                anim.CrossFade("Idle", 0.01f);
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
    }

    void KBMove()
    {
        if (heroEquipment.paused == false && !stunned)
        {

            //Check for WASD to 
            //see if it is surpressed fully
            //for more speed
            if (Input.GetAxis("KBHorizontal") > .8f || Input.GetAxis("KBVertical") > .8f || Input.GetAxis("KBVertical") < -.8f || Input.GetAxis("KBHorizontal") < -.8f)
            {
                speed = fullSpeed;
            }
            else
                speed = halfSpeed;

            //Check WASD for Movement
            MoveDirect.x = Input.GetAxis("KBHorizontal");
            MoveDirect.y = Input.GetAxis("KBVertical");

            if (MoveDirect != Vector2.zero)
                anim.CrossFade("PlayerWalking", 0.01f);
            else
                anim.CrossFade("Idle", 0.01f);
            //Normalize the directional vector
            //Factor in speed and time
            MoveDirect.Normalize();
            MoveDirect *= speed * Time.deltaTime;
           
            //Actually Move the player
            controller.Move(MoveDirect);
        }
    }

    void Rotate()
    {
        if (heroEquipment.paused == false && !stunned)
        {

            //Check Right sticks for Rotation
            MoveDirect.x = Input.GetAxis("CRSHorizontal");
            MoveDirect.y = Input.GetAxis("CRSVertical");
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
    }
    void MouseRotate()
    {
        if (!stunned && heroEquipment.paused == false||( heroCooldowns.dashCooling==true&&heroEquipment.equippedBoot==boot.Whirlwind))
        {

            //if (heroEquipment.equippedBoot!=boot.Whirlwind)
            //{
            // Rotate to face the mouse at all 
            // times if Mouse/keyboar is active
            Vector3 pos = Camera.main.WorldToScreenPoint(controller.transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //}
        }
    }
    public void KnockBack(Vector3 Direction)
    {
        knockbackTimer = .1f;
        KnockbackVec = transform.position - Direction;
        KnockbackVec.Normalize();
    }
}
