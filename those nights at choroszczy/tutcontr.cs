using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutcontr : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip[] audioClips;
    public GameObject Qskip;
    public GameObject otherskip;
    public TimeAndPower TimeAndPower;
    public int state;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        StartCoroutine(enumerator());
    }

   
    IEnumerator enumerator()
    {
        state = 0;
        yield return new WaitForSeconds(5f);
        while (state == 0)
        {
            Qskip.SetActive(true);
            otherskip.SetActive(false);
            AudioSource.clip = audioClips[state];
            AudioSource.Play();
            while (AudioSource.isPlaying)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    AudioSource.Stop();
                    break;
                }
            }
            Qskip.SetActive(false);
            otherskip.SetActive(true);
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    state = 1;
                    break;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    state = 0;
                    break;
                }
            }
            
        }
        while (state == 1)
        {
            Qskip.SetActive(true);
            otherskip.SetActive(false);
            AudioSource.clip = audioClips[state];
            AudioSource.Play();
            while (AudioSource.isPlaying)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    AudioSource.Stop();
                    break;
                }
            }
            Qskip.SetActive(false);
            otherskip.SetActive(true);
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    state = 2;
                    break;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    state = 1;
                    break;
                }
 
            }

        }
        while (state == 2)
        {
            Qskip.SetActive(true);
            otherskip.SetActive(false);
            AudioSource.clip = audioClips[state];
            AudioSource.Play();
            while (AudioSource.isPlaying)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    AudioSource.Stop();
                    break;
                }
            }
            Qskip.SetActive(false);
            otherskip.SetActive(true);
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    state = 3;
                    break;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    state = 2;
                    break;
                }

            }

        }
        while (state == 3)
        {
            Qskip.SetActive(true);
            otherskip.SetActive(false);
            AudioSource.clip = audioClips[state];
            AudioSource.Play();
            while (AudioSource.isPlaying)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    AudioSource.Stop();
                    break;
                }
            }
            Qskip.SetActive(false);
            otherskip.SetActive(true);
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    state = 4;
                    break;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    state = 3;
                    break;
                }
            }

        }
        while (state == 4)
        {
            Qskip.SetActive(true);
            otherskip.SetActive(false);
            AudioSource.clip = audioClips[state];
            AudioSource.Play();
            while (AudioSource.isPlaying)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    AudioSource.Stop();
                    break;
                }
            }
            Qskip.SetActive(false);
            otherskip.SetActive(true);
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    state = 5;
                    break;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    state = 4;
                    break;
                }
            }

        }
        TimeAndPower.AM = 2137;

    }
}
