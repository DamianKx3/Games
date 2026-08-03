using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    public GameObject lvlPanel;
    public GameObject settingsPanel;
    public GameObject EditorPanel;

    public GameObject changelogpanel;
    public TextMeshProUGUI text;
    public TextAsset changelog;
    public TextMeshProUGUI Ver;

    public Transform editorButtontransform;
    public GameObject editorLoadButton;

    public TMP_InputField NewName;
    public TextMeshProUGUI nameError;
    public int Lvl { get; set; }
    public int Chapter { get; set; }
    public GameObject[] chapters;
    public GameObject[] content;
    public List<int> lvls;
    [Header("CZYTAJ WSZYSTKIE PLIKI")]
    public bool ReadAllFiles;
    void Start()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Time.timeScale = 1f;
        string ver1 = changelog.text;
        Ver.text = ver1[0] + "" + ver1[1] + "" + ver1[2] + "" + ver1[3] + "Beta";

        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/saves");
        FileInfo[] info;
        if (ReadAllFiles == false)
        {
            info = dir.GetFiles("*.dupa");

        }
        else
        {
            info = dir.GetFiles("*.*");

        }
        for (int i = 0; i < info.Length; i++)
        {
            GameObject button2137 = GameObject.Instantiate(editorLoadButton);
            button2137.transform.SetParent(editorButtontransform);
            button2137.GetComponent<SaveButtons>().Name1 = info[i].Name.Split('.')[0];
            button2137.GetComponent<SaveButtons>().Path = info[i].ToString();
            button2137.GetComponent<SaveButtons>().info = (info[i].Length / 1024)+ "KB  created: " + info[i].CreationTime + "  used: " + info[i].LastAccessTime;
        }
        if(Data.backtoeditor == true)
        {
            EditorEnterLoad();
        }
        lvl();
        LoadSave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        Data.LvlPlace = Lvl;
        Data.Editor = false;
        Data.Load = false;
        SceneManager.LoadScene(3);
    }
    public void Settings()
    {
        settingsPanel.SetActive(true);
        lvlPanel.SetActive(false);
        EditorPanel.SetActive(false);
    }
    public void lvl()
    {
        settingsPanel.SetActive(false);
        lvlPanel.SetActive(true);
        EditorPanel.SetActive(false);
        ChapterBack();
    }
    public void Editor()
    {
        settingsPanel.SetActive(false);
        lvlPanel.SetActive(false);
        EditorPanel.SetActive(true);
        Data.LvlPlace = 0;
        nameError.text = "";
    }
    public void EditorEnter()
    {
        if(NewName.text == "")
        {
            nameError.text = "name cannot be empty";
            StartCoroutine(temperrdissapear());
            return;
        }
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/saves");
        FileInfo[] info;
        if (ReadAllFiles == false)
        {
            info = dir.GetFiles("*.dupa");

        }
        else
        {
            info = dir.GetFiles("*.*");

        }

        for (int i = 0; i < info.Length; i++)
        {
            if (info[i].Name.Split('.')[0] == NewName.text)
            {
                nameError.text = "name cannot be used twice";
                StartCoroutine(temperrdissapear());
                return;
            }
        }
        if (NewName.text.Contains('.') || NewName.text.Contains('<') || NewName.text.Contains('>') || NewName.text.Contains(':') || NewName.text.Contains('/') || NewName.text.Contains('\\') || NewName.text.Contains('|') || NewName.text.Contains('?') || NewName.text.Contains('*') || NewName.text.Contains('\'') || NewName.text.Contains('&') || NewName.text.Contains('@') || NewName.text.Contains('"'))
        {
            nameError.text = "name cannot contain chars like '. < > : / \\ | ? * ' '";
            StartCoroutine(temperrdissapear());
            return;
        }

        Data.backtoeditor= false;
        Data.Editor = true;
        Data.Load = false;
        Data.LvlPlace = 0;
        Data.SaveName = NewName.text;
        SceneManager.LoadScene(2);
    }
    public IEnumerator temperrdissapear()
    {
        yield return new WaitForSeconds(3);
        nameError.text = "";
    }
    public void EditorEnterLoad()
    {
        Data.backtoeditor = false;
        Data.Editor = true;
        Data.Load = true;
        SceneManager.LoadScene(2);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ChangelogToggle()
    {
        if(changelogpanel.activeSelf == false)
        {
            changelogpanel.SetActive(true);
            text.text = changelog.text;
        }
        else
        {
            changelogpanel.SetActive(false);
        }
    }

    public void OpenFolder()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        System.Diagnostics.Process.Start(Application.persistentDataPath + "/saves");
    }

    public void ChapterBack()
    {
        for (int i = 0; i < content.Length; i++)
        {
            content[i].SetActive(false);
        }
        for (int i = 0; i < chapters.Length; i++)
        {
            chapters[i].SetActive(true);
        }
    }
    public void Chapterselected()
    {
        for (int i = 0; i < chapters.Length; i++)
        {
            chapters[i].SetActive(false);
        }
        content[Chapter].SetActive(true);
    }
    

    public void LoadSave()
    {
        if (File.Exists(Application.persistentDataPath + "/Main/camp.dupa"))
        {

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fileStream = File.Open(Application.persistentDataPath + "/Main/camp.dupa", FileMode.Open);

            DataCamp data = (DataCamp)formatter.Deserialize(fileStream);
            lvls = data.LvlComplated;
            fileStream.Close();
        }
        else
        {
            //Debug.LogError("File not found: " + Application.persistentDataPath + "/Main/camp.dupa");
        }
    }
    public void DeleteSave()
    {
        File.Delete(Application.persistentDataPath + "/Main/camp.dupa");
    }
}
