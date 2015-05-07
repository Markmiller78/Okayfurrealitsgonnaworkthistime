using UnityEngine;
using System.Collections;

public class PingMissile : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 moveTo = (player.transform.position - transform.position).normalized;

        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 1.0f && Mathf.Abs(transform.position.y - player.transform.position.y) <= 1.0f)
        {
            moveTo = moveTo * Time.deltaTime * 25;

        }
        else
        {
            moveTo = moveTo * Time.deltaTime * 7;

        }



        transform.position = new Vector3(moveTo.x + transform.position.x, moveTo.y + transform.position.y, -3.1f);

        if ( Mathf.Abs(transform.position.x - player.transform.position.x) <= 0.3f && Mathf.Abs(transform.position.y - player.transform.position.y) <= 0.3f )
        {
            Destroy(gameObject);
        }
	}
}
