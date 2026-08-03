using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUse : MonoBehaviour
{
    public bool InMainMenu;
    public string settings;
    float timer;
    public Slider volume;
    public bool Listener;
    [Header("TRANSLATE:")]
    public string Translate;
    public void Awake()
    {
        if (InMainMenu == true)
        {
            LoadSettings();

        }
    }
    void Start()
    {

        if (InMainMenu == false)
        {
            if(Settings.TranslateToEng == true)
            {
                if (Translate != "")
                {
                    GetComponent<TextMeshProUGUI>().text = Translate;
                }
            }

            if(Listener == true)
            {
                AudioListener.volume = Settings.volume;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (InMainMenu == true)
        {
            if(timer < 0.5f)
            {
                timer += Time.unscaledDeltaTime;
            }
            else
            {
                timer = 0;
                SaveSets();
            }
            Settings.volume = volume.value;
            AudioListener.volume = Settings.volume;
        }
    }
    public void LoadSettings()
    {
        if (Directory.Exists(Application.persistentDataPath + "/Save/") == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Save/");
        }
        if (File.Exists(Application.persistentDataPath + "/Save/settings.txt"))
        {
            StreamReader sr = new StreamReader(Application.persistentDataPath + "/Save/settings.txt");
            settings = sr.ReadToEnd();
            sr.Close();
            Settings.volume = float.Parse(settings.Split(' ')[0]);
            volume.value = Settings.volume;
            if (settings.Split(' ')[1] == "T")
            {
                Settings.TranslateToEng = true;
            }
            else
            {
                Settings.TranslateToEng = false;

            }
        }
        else
        {
            StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Save/settings.txt");
            sw.Write("1 F");
            sw.Close();
            LoadSettings();
        }

    }
    public void SaveSets()
    {
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Save/settings.txt");
        string a = "F";
        if (Settings.TranslateToEng == true)
        {
            a = "T";
        }
        sw.Write(Settings.volume + " " + a);
        sw.Close();
        LoadSettings();
    }
    public void Lang(bool Eng)
    {
        if (Settings.TranslateToEng != Eng)
        {
            Settings.TranslateToEng = Eng;
            SaveSets();
            SceneManager.LoadScene(0);
        }


    }
}

public class Settings
{
    public static float volume;
    public static bool TranslateToEng;
}
