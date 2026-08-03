using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class losowycytat : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip[] audioClips;
    void Start()
    {
        StartCoroutine(random());
    }

    
    IEnumerator random()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(40f, 100f));
            if(AudioSource.isPlaying == false)
            {
                AudioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
                AudioSource.Play();
            }

        }
        
    }
}
