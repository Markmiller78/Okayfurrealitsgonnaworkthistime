using UnityEngine;
using System.Collections;

public class WhirlWind : MonoBehaviour
{


    //If you're looking for the whirlwind behavior, see "WhirlSpin.cs"


    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1200 * Time.deltaTime));

    }
}
