using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phonecall : MonoBehaviour
{
    public AudioSource AudioSource;
    public GameObject GameObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioSource.Stop();
            GameObject.SetActive(false);

        }
        if(AudioSource.isPlaying == false)
        {
            GameObject.SetActive(false);
        }
    }
}
