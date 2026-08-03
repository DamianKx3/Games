using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour
{

    public GameObject loading;
    public GameObject check0;
    public GameObject check1;
    public GameObject check2;
    public GameObject check3;
    public GameObject check4;
    public Text survtext;
    void Start()
    {
        if(Application.loadedLevelName != "ded")
        {
            if (PlayerPrefs.GetInt("beg") == 1)
            {
                check0.SetActive(true);
            }
            if (PlayerPrefs.GetInt("norm") == 1)
            {
                check1.SetActive(true);
            }
            if (PlayerPrefs.GetInt("normhard") == 1)
            {
                check2.SetActive(true);
            }
            if (PlayerPrefs.GetInt("nit") == 1)
            {
                check3.SetActive(true);
            }
            if (PlayerPrefs.GetInt("nithard") == 1)
            {
                check4.SetActive(true);
            }
            if (PlayerPrefs.GetInt("sec1") > 0)
            {
                survtext.text = "najlepszy czas: " + PlayerPrefs.GetInt("sec1").ToString() + " sekund";
            }
        }
        

    }


    public void beggining()
    {

        loading.SetActive(true);
        PlayerPrefs.SetInt("mode", 0);
        SceneManager.LoadScene(7);

    }
    public void normal()
    {
       
        loading.SetActive(true);
        PlayerPrefs.SetInt("mode", 1);
        SceneManager.LoadScene(1);

    }
    public void normalhard()
    {
        
        loading.SetActive(true);
        PlayerPrefs.SetInt("mode", 2);
        SceneManager.LoadScene(4);
    }
    public void nitro()
    {
        
        loading.SetActive(true);
        PlayerPrefs.SetInt("mode", 3);
        SceneManager.LoadScene(5);
    }
    public void nitrohard()
    {
        
        loading.SetActive(true);
        PlayerPrefs.SetInt("mode", 4);
        SceneManager.LoadScene(6);
    }
    public void impo()
    {

        loading.SetActive(true);
        PlayerPrefs.SetInt("mode", 5);
        SceneManager.LoadScene(8);
    }
    public void back()
    {

        SceneManager.LoadScene(0);
    }
    public void quit()
    {
        Application.Quit();
    }
    public void yt()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCT-emeO73pXw3tWAHf2ZyTg");
    }
}
