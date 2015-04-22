using UnityEngine;
using System.Collections;

public class SpellOrbOfLight : MonoBehaviour {

    public float speed;
    public float damage;
    public float range;
    CharacterController controller;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

    }

	// Update is called once per frame
	void Update ()
    {
       // transform.Rotate(new Vector3(0, 0, 5));
        controller.Move(new Vector3(0, 5 * Time.deltaTime, 0));
	}

    void OnTriggerEnter()
    {

    }
}
