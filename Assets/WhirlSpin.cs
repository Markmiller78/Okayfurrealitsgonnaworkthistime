using UnityEngine;
using System.Collections;

public class WhirlSpin : MonoBehaviour {

    GameObject player;

    float timer;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = 0;
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position;
        transform.Rotate(new Vector3(0, 0, -300 * Time.deltaTime));

        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
                    player.GetComponent<Animator>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        Destroy(gameObject);
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            
        }
    }
}
