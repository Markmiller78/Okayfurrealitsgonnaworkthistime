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
        string theName="";

        string[] embers = new string[10];
        embers[0] = "Scorching Ember of ";
        embers[1] = "Quake Ember of ";
        embers[2] = "Frost Ember of ";
        embers[3] = "Gale Ember of ";
        embers[4] = "Rejuvenating Ember of";
        embers[5] = "Doom Ember of ";

       
        string[] accessories = new string[3];
        accessories[0] = "Ring of ";
        accessories[1] = "Bracelet of ";
        accessories[2] = "Necklace of ";

        string[] attribute1 = new string[2];
        attribute1[0] = "Awesome";
        attribute1[1] = "Stengthening";

        string[] attribute2 = new string[2];
        attribute2[0] = "Destruction";
        attribute2[1] = "Syphoning";

        if (randNum > 4)
        {
            theName += accessories[Random.Range(0, 2)];

        }
        else
            theName += embers[randNum];
        if(Random.value>0.5f)
        {

            theName += attribute1[Random.Range(0, 1)];
        }
        else
            theName += attribute2[Random.Range(0, 1)];


        return theName;
    }
}

