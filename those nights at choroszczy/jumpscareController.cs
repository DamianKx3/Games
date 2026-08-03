using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class jumpscareController : MonoBehaviour
{
    public GameObject Maincamera;
    public GameObject jumpcamera;
    public GameObject black;
    public int enemyID;
    public GameObject[] jumpscares = new GameObject[4];
    public bool caninteract;
    AudioSource AudioSource;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        caninteract = true;
        Maincamera.SetActive(true);
        jumpscares[enemyID].SetActive(false);
        jumpcamera.SetActive(false);
    }
    public void StartJS()
    {
        if(caninteract == true)
        {
            caninteract = false;
            Maincamera.SetActive(false);
            PlayerPrefs.SetInt("JID", enemyID);
            jumpscares[enemyID].SetActive(true);
            jumpcamera.SetActive(true);
            StartCoroutine(jumplenght());
        }
        
    }
    IEnumerator jumplenght()
    {
        AudioSource.Play();
        yield return new WaitForSeconds(1.5f);
        black.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioSource.Stop();
        SceneManager.LoadScene(2);
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }
    }
}
