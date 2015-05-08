//using UnityEngine;
//using System.Collections;

//public class EarthSpin : MonoBehaviour {

//    public Vector3 forward;
//    // Use this for initialization
//    void Start () {
//        forward = transform.up;
	
//    }
	
//    // Update is called once per frame
//    void Update () 
//    {
//        float tempangle = Random.value * Mathf.Rad2Deg;
//        tempangle += 90.0f;

//        Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
//      //  this.gameObject.GetComponent<SpriteRenderer>().transform.rotation = Quaternion.Slerp(this.gameObject.GetComponent<SpriteRenderer>().transform.rotation, rotation, Time.deltaTime * 2.5f);
// transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, rotation, Time.deltaTime * 2.5f);
//        this.gameObject.transform.Translate(forward.normalized);
//     //  this.gameObject.transform.Rotate(rotation.eulerAngles * Time.deltaTime * 10, Space.Self);
//     //  this.gameObject.transform.Rotate(Vector3.up,tempangle*Time.deltaTime*3.0f);
          
//    }
//}
