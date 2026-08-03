using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public int CurrentLevel;
    public int ContinueLvl;
    public int Mode;
    public string save = "";
    public bool OnlyInMain;
    private float t1;
    private float t2;
    bool tmp;
    public AudioSource source;
    public bool isCustom { get; set; }
    public Slider Fiodorslider;
    public TextMeshProUGUI fiodortxt;
    public Slider OlgierdSlider;
    public TextMeshProUGUI olotxt;
    public Slider KononSlider;
    public TextMeshProUGUI konontxt;
    public Slider Money;
    public TextMeshProUGUI Moneytxt;
    public Slider comments;
    public TextMeshProUGUI commenttxt;
    public Toggle wiatrolap;
    public TMP_Dropdown moneydropdown;
    public Toggle outbreak;
    public Slider MoneyOnStart;
    public GameObject loadingscr;
    public TextMeshProUGUI moneyyonstart;
    public Slider Joorexe;
    public TextMeshProUGUI joortxt;
    public bool Final;

    public Slider majorfinal;
    public TextMeshProUGUI majorfinaltxt;

    public Slider kononfinal;
    public TextMeshProUGUI kononfinaltxt;

    public Slider janfinal;
    public TextMeshProUGUI janfinaltxt;

    public bool Hardmode {  get; set; }
    void Start()
    {

        Time.timeScale = 1.0f;
        if(OnlyInMain == true)
        {
            if (File.Exists(Application.persistentDataPath + "/Save/s.txt"))
            {
                StreamReader sr = new StreamReader(Application.persistentDataPath + "/Save/s.txt");
                save = sr.ReadToEnd();
                sr.Close();
            }
            t1 = Random.Range(1f, 3f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OnlyInMain == true)
        {
            if(t1 > 0)
            {
                t1 = t1 - Time.deltaTime;

            }
            else
            {
                if (source.volume < 0.4f && tmp == false)
                {
                    source.volume = source.volume + Time.deltaTime * 0.2f;
                    t2 = Random.Range(1f, 3f);

                }
                else
                {
                    tmp = true;
                    if (t2 > 0)
                    {
                        t2 = t2 - Time.deltaTime;
     
                    }
                    else
                    {
                        if (source.volume > 0.1f)
                        {

                            source.volume = source.volume - Time.deltaTime * 0.2f;
                        }
                        else
                        {
                            t1 = Random.Range(6f, 12f);
                            tmp = false;
                        }
                    }
                }
            }

            olotxt.text = OlgierdSlider.value + "/50";
            konontxt.text = KononSlider.value + "/50";
            fiodortxt.text = Fiodorslider.value + "/50";
            Moneytxt.text = "Cel: " + Money.value + "$";
            if (Settings.TranslateToEng == true) Moneytxt.text = "Goal: " + Money.value + "$";
            commenttxt.text = "intensywnosc komentarzy: " + comments.value;
            if (Settings.TranslateToEng == true) commenttxt.text = "comments: " + comments.value;
            moneyyonstart.text = "kasa na start: " + MoneyOnStart.value + "$";
            if (Settings.TranslateToEng == true) moneyyonstart.text = "Oney on start: " + MoneyOnStart.value + "$";
            joortxt.text = "j00r.exe "+Joorexe.value + "/50";
            kononfinaltxt.text = (int)kononfinal.value + "/50";
            majorfinaltxt.text = (int)majorfinal.value + "/50";
            janfinaltxt.text = (int)janfinal.value + "/50";

        }

    }
    public void Play(string lvl)
    {
        Data.Konon = 0;
        Data.Fiodor = 0;
        Data.Olgierd = 0;
        Data.MoneyGoal = 100;
        Data.Comments = 0;
        Data.ActiveDoors = true;
        Data.Napis = "Noc ?";
        Data.Date = "";
        Data.powerOut = false;
        Data.FinalMode = false;
        Data.Major = 0;
        Data.FinalKonon = 0;
        Data.FinalMajor = 0;
        Data.Finaljano = 0;
        Data.invincibility = 0;

        Data.CurrentLvl = lvl;
        switch (lvl)
        {
            default:
                break;
            case "T":
                Data.Konon = 10;
                Data.Fiodor = 10;
                Data.Olgierd = 10;
                Data.MoneyGoal = 50;
                Data.Comments = 0;
                Data.ActiveDoors = false;
                Data.Napis = "samouczek";
                if (Settings.TranslateToEng == true) Data.Napis = "Tutorial";
                Data.Date = System.DateTime.Today.ToString("dd.MM.yyyy").ToString();
                Data.MoneyOnStart = 25;
                Data.powerOut = false;
                Data.FinalMode = false;
                Data.Major = 0;
                Data.invincibility = 213769;
                break;
            case "0":
                Data.Konon = 10;
                Data.Fiodor = 5;
                Data.Olgierd = 5;
                Data.MoneyGoal = 100;
                Data.Comments = 0;
                Data.ActiveDoors = false;
                Data.Napis = "Noc 1";
                if (Settings.TranslateToEng == true) Data.Napis = "Night 1";
                Data.Date = "02.03.2025";
                Data.MoneyOnStart = 25;
                Data.powerOut = false;
                Data.FinalMode = false;
                Data.Major = 0;
                Data.invincibility = 30;
                break;
            case "1":
                Data.Konon = 15;
                Data.Fiodor = 5;
                Data.Olgierd = 5;
                Data.MoneyGoal = 250;
                Data.Comments = 1;
                Data.ActiveDoors = true;
                Data.Napis = "Noc 2";
                if (Settings.TranslateToEng == true) Data.Napis = "Night 2";
                Data.Date = "03.03.2025";
                Data.MoneyOnStart = 15;
                Data.powerOut = false;
                Data.FinalMode = false;
                Data.Major = 0;
                break;
            case "2":
                Data.Konon = 12;
                Data.Fiodor = 13;
                Data.Olgierd = 13;
                Data.MoneyGoal = 1;
                Data.Comments = 0;
                Data.ActiveDoors = true;
                Data.Napis = "Noc 3";
                if (Settings.TranslateToEng == true) Data.Napis = "Night 3";
                Data.Date = "04.03.2025";
                Data.MoneyOnStart = 0;
                Data.powerOut = true;
                Data.FinalMode = false;
                Data.Major = 15;
                break;
            case "3":
                Data.Konon = 18;
                Data.Fiodor = 10;
                Data.Olgierd = 10;
                Data.MoneyGoal = 300;
                Data.Comments = 2;
                Data.ActiveDoors = true;
                Data.Napis = "Noc 4";
                if (Settings.TranslateToEng == true) Data.Napis = "Night 4";
                Data.Date = "05.03.2025";
                Data.MoneyOnStart = 0;
                Data.powerOut = false;
                Data.FinalMode = false;
                Data.Major = 12;
                break;
            case "4":
                Data.MoneyGoal = 10;
                Data.MoneyOnStart = 0;
                Data.FinalMode = true;
                Data.FinalKonon = 23;
                Data.FinalMajor = 30;
                Data.Finaljano = 15;
                Data.Major = 5;
                Data.Napis = "Finale";
                Data.Date = "06.03.2025";
                break;
            case "0H":
                Data.Konon = 11;
                Data.Fiodor = 5;
                Data.Olgierd = 5;
                Data.MoneyGoal = 100;
                Data.Comments = 0;
                Data.ActiveDoors = false;
                Data.Napis = "Noc 1";
                if (Settings.TranslateToEng == true) Data.Napis = "Night 1";
                Data.Date = "02.03.2025";
                Data.MoneyOnStart = 25;
                Data.powerOut = false;
                Data.FinalMode = false;
                Data.Major = 0;
                break;
            case "1H":
                Data.Konon = 15;
                Data.Fiodor = 6;
                Data.Olgierd = 6;
                Data.MoneyGoal = 280;
                Data.Comments = 1;
                Data.ActiveDoors = true;
                Data.Napis = "Noc 2";
                if (Settings.TranslateToEng == true) Data.Napis = "Night 2";
                Data.Date = "03.03.2025";
                Data.MoneyOnStart = 15;
                Data.powerOut = false;
                Data.FinalMode = false;
                Data.Major = 0;
                break;
            case "2H":
                Data.Konon = 15;
                Data.Fiodor = 14;
                Data.Olgierd = 14;
                Data.MoneyGoal = 1;
                Data.Comments = 0;
                Data.ActiveDoors = true;
                Data.Napis = "Noc 3";
                if (Settings.TranslateToEng == true) Data.Napis = "Night 3";
                Data.Date = "04.03.2025";
                Data.MoneyOnStart = 0;
                Data.powerOut = true;
                Data.FinalMode = false;
                Data.Major = 22;
                break;
            case "3H":
                Data.Konon = 20;
                Data.Fiodor = 10;
                Data.Olgierd = 10;
                Data.MoneyGoal = 333;
                Data.Comments = 2;
                Data.ActiveDoors = true;
                Data.Napis = "Noc 4";
                if (Settings.TranslateToEng == true) Data.Napis = "Night 4";
                Data.Date = "05.03.2025";
                Data.MoneyOnStart = 0;
                Data.powerOut = false;
                Data.FinalMode = false;
                Data.Major = 15;
                break;
            case "4H":
                Data.MoneyGoal = 10;
                Data.MoneyOnStart = 0;
                Data.FinalMode = true;
                Data.FinalKonon = 25;
                Data.FinalMajor = 30;
                Data.Finaljano = 15;
                Data.Major = 15;
                Data.Napis = "Finale";
                Data.Date = "06.03.2025";
                break;

        }
        if (isCustom == true)
        {
            Data.Konon = (int)KononSlider.value;
            Data.Fiodor = (int)Fiodorslider.value;
            Data.Olgierd = (int)OlgierdSlider.value;
            Data.MoneyGoal = (int)Money.value;
            Data.ActiveDoors = !wiatrolap.isOn;
            Data.Comments = (int)comments.value;
            Data.Napis = "c:";
            Data.Date = System.DateTime.Today.ToString("dd.MM.yyyy").ToString();
            Data.MoneyOnStart = (int)MoneyOnStart.value;
            Data.powerOut = outbreak.isOn;
            Data.FinalMode = Final;
            Data.Major = (int)Joorexe.value;
            Data.FinalKonon = (int)kononfinal.value;
            Data.FinalKonon = (int)kononfinal.value;
            Data.FinalMajor = (int)majorfinal.value;
            Data.Finaljano = (int)janfinal.value;
            Data.CurrentLvl = "c";

        }
        loadingscr.SetActive(true);
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void credits()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);
    }
    public void ending()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(3);
    }
    public void Erase()
    {
        if (File.Exists(Application.persistentDataPath + "/Save/s.txt"))
        {
            File.Delete(Application.persistentDataPath + "/Save/s.txt");
            loadingscr.SetActive(true);
            SceneManager.LoadScene(0);
        }
    }
    public void valdropdown()
    {
        switch (moneydropdown.value)
        {
            default:
                break;
            case 0:
                Money.value = 100;
                break;
            case 1:
                Money.value = 200;
                break;
            case 2:
                Money.value = 300;
                break;
            case 3:
                Money.value = 400;
                break;
            case 4:
                Money.value = 500;
                break;
            case 5:
                Money.value = 800;
                break;
            case 6:
                Money.value = 1000;
                break;
            case 7:
                Money.value = 2000;
                break;
            case 8:
                Money.value = 5000;
                break;
        }

    }
    public void Finaltoggle(bool final)
    {
        Final = final;
    }

}
