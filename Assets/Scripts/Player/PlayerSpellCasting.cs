using UnityEngine;
using System.Collections;

public class PlayerSpellCasting : MonoBehaviour {

    public float spellCooldown;

    public GameObject orbOfLight;

    void CastSpell()
    {
        Instantiate(orbOfLight, transform.position, transform.rotation);
    }

    void SpellListen()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
