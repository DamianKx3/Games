using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cutscene : MonoBehaviour
{
    public GameObject[] cutscenes;
    public List<GameObject> currentframes;
    public List<GameObject> currentframes2;
    public List<GameObject> currentframes3;
    int num;
    public AudioSource AudioSource;
    void Start()
    {
        num = Data.LvlPlace;

        for (int i = 0; i < cutscenes.Length; i++)
        {
            cutscenes[i].SetActive(false);
        }
        cutscenes[num].SetActive(true);
        AudioSource = cutscenes[num].GetComponent<AudioSource>();
        for (int i = 0; i < cutscenes[num].GetComponentInChildren<Frames>().frames.Length; i++)
        {
            currentframes.Add(cutscenes[num].GetComponentInChildren<Frames>().frames[i]);
            currentframes[i].SetActive(false);
        }

        for (int i = 0; i < cutscenes[num].GetComponentInChildren<Frames>().frames2.Length; i++)
        {
            currentframes.Add(cutscenes[num].GetComponentInChildren<Frames>().frames2[i]);
            currentframes2[i].SetActive(false);
        }

        for (int i = 0; i < cutscenes[num].GetComponentInChildren<Frames>().frames3.Length; i++)
        {
            currentframes.Add(cutscenes[num].GetComponentInChildren<Frames>().frames3[i]);
            currentframes3[i].SetActive(false);
        }
        StartCoroutine(Work());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            Exit();
        }
    }
    public IEnumerator Work()
    {
        yield return null;
        Debug.Log(num + " NUM");
        switch (num)
        {
            default:
                for (int i = 0; i < currentframes.Count; i++)
                {
                    currentframes[i].SetActive(false);
                }
                Exit();
                break;
            case 0:
                Exit();
                break;
            case 1:
                Exit();
                break;
            case 2:
                Exit();
                break;
            case 3:
                Exit();
                break;
            case 4:
                Exit();
                break;
            case 5:
                for (int i = 0; i < 60; i++)
                {
                    currentframes[0].SetActive(true);
                    currentframes[1].SetActive(false);
                    currentframes[2].SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                    currentframes[0].SetActive(false);
                    currentframes[1].SetActive(true);
                    currentframes[2].SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                    currentframes[0].SetActive(false);
                    currentframes[1].SetActive(false);
                    currentframes[2].SetActive(true);
                    yield return new WaitForSeconds(0.1f);
                }
                while (AudioSource.isPlaying == true)
                {
                    yield return null;
                }
                Exit();
                break;
            case 6:
                yield return new WaitForSeconds(1f);
                while (AudioSource.isPlaying == true)
                {
                    yield return null;
                }
                Exit();
                break;
            case 7:
                yield return new WaitForSeconds(0.5f);
                currentframes[0].SetActive(true);
                yield return new WaitForSeconds(1f);
                currentframes[1].SetActive(true);
                yield return new WaitForSeconds(1f);
                currentframes[2].SetActive(true);
                yield return new WaitForSeconds(1.5f);
                currentframes[3].SetActive(true);
                yield return new WaitForSeconds(2f);
                Exit();
                break;
            case 8:
                yield return new WaitForSeconds(1f);
                while (AudioSource.isPlaying == true)
                {
                    yield return null;
                }
                Exit();
                break;
            case 9:
                yield return new WaitForSeconds(1f);
                while (AudioSource.isPlaying == true)
                {
                    yield return null;
                }
                Exit();
                break;
        }
    }
    public void Exit()
    {

        SceneManager.LoadScene(1);
    }
}
