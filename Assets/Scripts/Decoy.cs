using UnityEngine;
using System.Collections;

public class Decoy : MonoBehaviour {


	public GameObject lightRemains;
	public float timer=5.0f;
    public float damage;
    public float range;
    public GameObject explosion;
    public GameObject[] allObjects;
    GameObject player;
    PlayerStats theStats;
	// Use this for initialization
	void Start ()  {
        player = GameObject.FindGameObjectWithTag("Player");
        theStats = player.GetComponent<PlayerStats>();
        range = 2.0f;
        damage = 8.0f;
		GameObject[] allObjects;
		allObjects = GameObject.FindObjectsOfType<GameObject>();
		foreach (GameObject obj in allObjects)
		{
			obj.SendMessage("Decoy", this.gameObject, SendMessageOptions.DontRequireReceiver);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0.0f)
	 
        {
			allObjects = GameObject.FindObjectsOfType<GameObject>();
			foreach (GameObject obj in allObjects)
            {
                if (obj.tag != "Player")
                {
                    Vector3 temp = transform.position - obj.transform.position;
                    if (temp.magnitude < range)
                    {
                        if (obj.GetComponent<Health>() != null)
                        {
                            obj.GetComponent<Health>().LoseHealth(damage+theStats.spellModifier);
                            obj.GetComponent<Knockback>().SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                        }
                    }
                    obj.SendMessage("UnDecoy",this.gameObject, SendMessageOptions.DontRequireReceiver);
                }
			}
 
            Explode();
			Destroy (gameObject);

		}
	
	}
    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(lightRemains, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
