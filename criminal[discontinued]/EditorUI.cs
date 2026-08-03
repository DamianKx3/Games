using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EditorUI : MonoBehaviour
{
    public bool pause;
    public int Mode { get;set; }
    public int SectionSelect { get; set; }

    public GameObject pauseMenu;
    public GameObject[] Sections;
    public GameObject Mode0Hide;
    public GameObject Mode2Hide;
    public GameObject Mode5Hide;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject section in Sections)
        {
            section.SetActive(false);
        }
        Sections[SectionSelect].SetActive(true);
        if(Mode == 0)
        {
            Mode0Hide.SetActive(true);
        }
        else
        {
            Mode0Hide.SetActive(false);
        }
        if (Mode == 2)
        {
            Mode2Hide.SetActive(true);
        }
        else
        {
            Mode2Hide.SetActive(false);
        }

        if (Mode == 5)
        {
            Mode5Hide.SetActive(true);
        }
        else
        {
            Mode5Hide.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        if(pause == true)
        {
            pause = false;
            pauseMenu.SetActive(false);
        }
        else
        {
            pause = true;
            pauseMenu.SetActive(true);
        }
    }
    public void SaveAndExit()
    {
        FindFirstObjectByType<SaveSystem>().Save();
        SceneManager.LoadScene(0);

    }
    public void Continue()
    {
        Pause();
    }
    public void ExitNoSave()
    {
        SceneManager.LoadScene(0);
    }
    public void SaveAndPlay()
    {
        FindFirstObjectByType<SaveSystem>().Save();
        Data.Load = true;
        Data.Editor = false;
        SceneManager.LoadScene(1);
    }
}
