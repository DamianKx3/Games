using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Ambient : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip[] clips;

    void Start()
    {
        if(Data.FinalMode == false)
        {
            StartCoroutine(loop());
        }
        else
        {

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator loop()
    {
        float R = Random.Range(45f, 120f);
        yield return new WaitForSeconds(R);

        audiosource.clip = clips[Random.Range(0,clips.Length)];
        audiosource.pitch = Random.Range(0.7f, 1f);
        audiosource.Play();
    }
    public void PlayAmb(int index)
    {
        audiosource.clip = clips[index];
        audiosource.pitch = Random.Range(0.7f, 1f);
        audiosource.Play();
    }
}
