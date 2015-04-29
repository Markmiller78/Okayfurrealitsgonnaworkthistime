using UnityEngine;
using System.Collections;



public class GenerateLoot : MonoBehaviour {


    public GameObject EmberFire;
    public GameObject EmberLife;
    public GameObject EmberWind;
    public GameObject EmberFrost;

    public GameObject AccesOrb;
    public GameObject AccesBolt;
    public GameObject AccesSnare;
    public GameObject AccesBlast;
    public GameObject AccesMine;
    public GameObject AccesSing;
    public GameObject AccesChain;

    public GameObject Decoy;
    public GameObject Blink;
    public GameObject WhirlWind;
    public GameObject Trailblazer;
    public GameObject Charge;

    public enum Loots { none = 0, emberfire , emberlife, emberwind, emberfrost, accesOrb, accesbolt, accessnare,
    accesblast, accesmine,accessing, acceschain}
    Loots lootToDrop;
	// Use this for initialization
	void Start () 
    {
	
	}
	

    public void Generateloot()
    {

        if(gameObject.tag == "Chest")
        {
            int RandNum = Random.Range(1,11);
            GameObject Loot = DetermineType(RandNum);
            string SendName = DetermineName(RandNum, 1, 1);
            GameObject temp = (GameObject)Instantiate(Loot, transform.position, transform.rotation);
            temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);                     
            int MoreLoots = Random.Range(0, 100);
            if(MoreLoots > 50)
            {
                transform.position += new Vector3(.5f, 0, 0);
                RandNum = Random.Range(1, 11);
                Loot = DetermineType(RandNum);
                SendName = DetermineName(RandNum, 1, 1);
                temp = (GameObject)Instantiate(Loot, transform.position, transform.rotation);
                temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);            
                transform.position -= new Vector3(.5f, 0, 0);
            }
            int MoarLoots = Random.Range(0, 100);
            if (MoarLoots > 70)
            {
                transform.position -= new Vector3(.7f, 0, 0);
                RandNum = Random.Range(1, 11);
                Loot = DetermineType(RandNum);
                SendName = DetermineName(RandNum, 1, 1);
                temp = (GameObject)Instantiate(Loot, transform.position, transform.rotation);
                temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);        
                transform.position += new Vector3(.7f, 0, 0);
            }
            int MoarLootz = Random.Range(0, 100);
            if (MoarLootz > 80)
            {
                transform.position += new Vector3(0, .6f, 0);
                RandNum = Random.Range(1, 11);
                Loot = DetermineType(RandNum);
                SendName = DetermineName(RandNum, 1, 1);
                temp = (GameObject)Instantiate(Loot, transform.position, transform.rotation);
                temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);        
                transform.position -= new Vector3(0, .6f, 0);
            }
        }
        if(gameObject.tag == "Enemy")
        {
            int MoarLootz = Random.Range(0, 100);
            if (MoarLootz > 90)
            {
                transform.position += new Vector3(0, .6f, 0);
                int RandNum = Random.Range(1, 11);
                GameObject Loot = DetermineType(RandNum);
                string SendName = DetermineName(RandNum, 1, 1);
                GameObject temp = (GameObject)Instantiate(Loot, transform.position, transform.rotation);
                temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);        
                transform.position -= new Vector3(0, .6f, 0);
            }
        }

    }

    GameObject DetermineType(int randNum)
    {
        if(randNum == 1)
        {
            return EmberFire;
        }
        else if (randNum == 2)
        {
            return EmberLife;
        }
        else if (randNum == 3)
        {
            return EmberWind;
        }
        else if (randNum == 4)
        {
            return EmberFrost;
        }
        else if (randNum == 5)
        {
            return AccesOrb;
        }
        else if (randNum == 6)
        {
            return AccesBolt;
        }
        else if (randNum == 7)
        {
            return AccesSnare;
        }
        else if (randNum == 8)
        {
            return AccesBlast;
        }
        else if (randNum == 9)
        {
            return AccesMine;
        }
        else if (randNum == 10)
        {
            return AccesSing;
        }
        else if (randNum == 11)
        {
            return AccesChain;
        }
        print("DETERMINE LOOT ERROR: Returned Chain");
        return AccesChain;
    }

    string DetermineName(int randNum, int Attribute1, int Attribute2)
    {
        // WORK HERE FRANSWEA
        return "Heyo";
    }
}

