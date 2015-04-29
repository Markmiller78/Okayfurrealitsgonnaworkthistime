using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayEnmiesRemaining : MonoBehaviour
{

    float timer;
    public Text theText;
    // Use this for initialization
    void Start()
    {
        timer = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 1;
            int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
            theText.text = count.ToString();
        }
    }
}
