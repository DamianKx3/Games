using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{

    [Header("panele")]
    public GameObject upperpanel;
    public GameObject lowerpanel;
    public GameObject[] CpanelVer;
    public GameObject shoppanel;
    public GameObject bombpanel;
    [Header("pieniadze")]
    public float money;
    public TextMeshProUGUI textmoney;
    [Header("spawnowanie")]
    public Transform spawnpoint;
    public GameObject[] Spawnable;
    public int BoughtID { get; set; }
    [Header("wybrany pojazd")]
    public GameObject Selected;
    public bool selectedObj;
    [Header("niepotrzebny public")]
    public bool DestSet;
    public bool TargetSet;
    Vector2 mousepos;
    public bool BombsUnlocked;
    public List<GameObject> alive;
    [Header("UI itd")]
    public GameObject Menu;
    public Texture2D[] cursors;
    bool lock1;
    public GameObject bomber;
    public bool pause;
    public GameObject pauseMenu;
    public GameObject backtoEditorButton;
    public int bombselected { get; set; }
    public int TempPrice { get; set; }
    [Header("panning mode")]
    public bool planningMode;
    public TextMeshProUGUI buttontextslow;
    public GameObject slowUI;
    [Header("time")]
    public float TimeLeft;
    public TextMeshProUGUI TimeLeftText;
    [Header("Settings")]
    public TextMeshProUGUI fpstext;
    int fps;
    bool bombseltemp;
    void Start()
    {
        bombpanel.SetActive(false);
        shoppanel.SetActive(false);
        lowerpanel.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        StartCoroutine(count());
        Time.timeScale = 1;

        Application.targetFrameRate = 120;

        if(Data.Load == true)
        {
            backtoEditorButton.SetActive(true);
        }
        if(SettingsData._showFPS == true)
        {
            InvokeRepeating("GetFPS", 1, 1);
        }
        else
        {
            fpstext.transform.gameObject.SetActive(false);
        }
        AudioListener.volume = SettingsData._volume;

    }
    //licznik czasu prawdziwy
    public IEnumerator count()
    {
        yield return new WaitForSeconds(1);
        while (TimeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            TimeLeft--;
        }
        //pojawianie sie zolnierzy po uplywie czasu
        StructureBlocks[] sb = FindObjectsOfType<StructureBlocks>(); 
        for (int i = 0; i < sb.Length; i++)
        {
            if (sb[i].parachuteEnemy == true)
            {
                sb[i].Spawn();
            }
        }
        yield return new WaitForSeconds(3);
        TimeLeft = -1;
    }
    // Update is called once per frame
    void Update()
    {
        //licznik

        

        if (TimeLeft > 0)
        {
            if (TimeLeft % 60 > 9)
            {
                TimeLeftText.text = Mathf.Floor(TimeLeft / 60) + ":" + TimeLeft % 60;
            }
            else
            {
                TimeLeftText.text = Mathf.Floor(TimeLeft / 60) + ":0" + TimeLeft % 60;
            }

        }
        else
        {
            TimeLeftText.text = "0:00";
            if(TimeLeft == -1)
            {
                TimeLeftText.text = "";
            }
        }
        //pauza
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        //keybindy
        if(Selected != null && Selected.GetComponent<Tank>().AITYPE != "Bomber")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectedUIcancel();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Target();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                NoTarget1();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Destination();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Stop();
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Direction();
            }
        }
        
        //panele
        if (Selected != null)
        {
            for (int i = 0; i < CpanelVer.Length; i++)
            {
                CpanelVer[i].SetActive(false);
            }
            switch (Selected.GetComponent<Tank>().AITYPE)
            {
                default:
                    break;
                case "Tank":
                    CpanelVer[0].SetActive(true);
                    break;
                case "ATGM":
                    CpanelVer[0].SetActive(true);
                    break;
                case "Drone":
                    CpanelVer[1].SetActive(true);
                    break;
                case "Bomber":
                    CpanelVer[2].SetActive(true);
                    break;
                case "Helicopter":
                    CpanelVer[0].SetActive(true);
                    break;
                case "Transporter":
                    CpanelVer[0].SetActive(true);
                    break;
                case "Launcher":
                    CpanelVer[0].SetActive(true);
                    break;
                case "Killdozer":
                    CpanelVer[0].SetActive(true);
                    break;
            }
            if(Selected == bomber)
            {
                CpanelVer[2].SetActive(false);
            }
        }
        //tekst kasa
        textmoney.text = money + "$";

        //nacisniecie
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector3.forward);

        if (selectedObj == true && TargetSet == true)
        {

            Selected.GetComponent<Tank>().Target = mousepos;
            //Debug.Log("working");
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObj == true && DestSet == true)
            {
                Selected.GetComponent<Tank>().Destination = mousepos;
                SelectedUIcancel();

            }

            if (selectedObj == true && TargetSet == true)
            {


                Selected.GetComponent<Tank>().Target = mousepos;
                if(bomber != null)
                {
                    bomber.transform.position = mousepos;
                }
                Selected.GetComponent<Tank>().NoTarget = false;
                Selected.GetComponent<Tank>().Targetaccept = true;

                if (Selected == bomber)
                {
                    bombseltemp = true;
                    SpawnBomber();
                }
                SelectedUIcancel();
                

            }
            if (hit.collider != null && hit.collider.gameObject.tag == "Spawnable")
            {
                selectedObj = true;
                Selected = hit.collider.gameObject;

                SelectedUI();
            }
            else
            {
                //SelectedUIcancel();
            }

            //check if still alive
            if(Selected == null)
            {
                SelectedUIcancel();
            }
            
        }

        //EndLVL
        GameObject[] o = GameObject.FindGameObjectsWithTag("Human");
        alive.Clear();
        for (int i = 0; i < o.Length; i++)
        {
            if (o[i].GetComponent<Human>().Died == false)
            {
                alive.Add(o[i]);
            }
        }
        if (alive.Count < 1 && FindFirstObjectByType<PrefabCreator>() == null)
        {
            if (lock1 == false)
            {
                lock1 = true;
                StartCoroutine(EndLvl());
            }
        }


        //Bombpanel
        if (BombsUnlocked == true)
        {
            bombpanel.SetActive(true);
        }
        else
        {
            bombpanel.SetActive(false);
        }
    }
    public IEnumerator EndLvl()
    {
        if (FindFirstObjectByType<SaveSystem>() != null)
        {
            FindFirstObjectByType<SaveSystem>().SaveLevelComplated();
        }

        yield return new WaitForSecondsRealtime(3);
        lowerpanel.SetActive(false);
        upperpanel.SetActive(false);
        shoppanel.SetActive(false);
        Menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Shop()
    {
        if(lock1 == false)
        {
            if (shoppanel.activeSelf == true)
            {
                shoppanel.SetActive(false);
            }
            else
            {
                shoppanel.SetActive(true);
            }
            SelectedUIcancel();
        }
        
    }
    //pieprzenie sie z UI
    public void SelectedUI()
    {
        if(lock1 == false)
        {
            shoppanel.SetActive(false);
            lowerpanel.SetActive(true);
            selectedObj = true;
        }

    }
    public void SelectedUIcancel()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        lowerpanel.SetActive(false);
        selectedObj = false;
        Selected = null;
        DestSet = false;
        TargetSet = false;
    }


    //przetwarzanie transakcji kupna i spawnowanie pojazdu 
    public void Spawn()
    {
        if(TempPrice <= money)
        {
            if (BombsUnlocked == true && BoughtID == 3) return;
            money = money - TempPrice;
            Instantiate(Spawnable[BoughtID], spawnpoint.position + new Vector3(0,0,-1), Quaternion.identity);
            if(BoughtID == 3)
            {
                BombsUnlocked = true;
            }
        }
        else
        {
            StartCoroutine(redflashmoney());
        }

    }
    //to samo ale bombowiec jest zdymany i wymaga osobnego
    public void SpawnBomber()
    {
        if (TempPrice <= money)
        {
            Debug.Log(bombseltemp);
            if(bombseltemp == false)
            {
                foreach (Tank vechicle in FindObjectsOfType<Tank>())
                {
                    if(vechicle.AITYPE == "Bomber")
                    {
                        return;
                    }
                }
                money = money - TempPrice;
                BomberSel();
                Target();
            }

            


        }
        else
        {
            StartCoroutine(redflashmoney());
        }
        if(bombseltemp == true)
        {

            bombseltemp = false;
            GameObject bomber1 = Instantiate(Spawnable[4], new Vector3(spawnpoint.transform.position.x - 15, bomber.transform.position.y, 0), Quaternion.identity);
            bomber1.GetComponent<Tank>().Target = bomber.transform.position;
            bomber1.GetComponent<Tank>().bullet = bomber1.GetComponent<Tank>().Additionals[bombselected];
            if (bombselected == 1)
            {
                alarm[] a = GameObject.FindObjectsOfType<alarm>();
                foreach (alarm Alarm in a)
                {
                    Alarm.atom = true;
                }
            }
            else
            {
                alarm[] a = GameObject.FindObjectsOfType<alarm>();
                foreach (alarm Alarm in a)
                {
                    Alarm.active = true;
                }
            }
            switch (bombselected)
            {
                case 0:
                    bomber1.GetComponent<Tank>().ShootType = 0;
                    break;
                case 1:
                    bomber1.GetComponent<Tank>().ShootType = 0;
                    break;
                case 2:
                    bomber1.GetComponent<Tank>().ShootType = 1;
                    break;
                case 3:
                    bomber1.GetComponent<Tank>().ShootType = 0;
                    break;
                case 4:
                    bomber1.GetComponent<Tank>().ShootType = 0;
                    break;

            }

        }

    }
    public void BaseUpgrade()
    {
        if (TempPrice <= money)
        {
            
        }

    }
    //funkcje pojazdow z paneli stgerowania
    public void Destination()
    {
        Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
        DestSet = true;
        TargetSet = false;
        
    }
    public void Target()
    {
        Cursor.SetCursor(cursors[1], Vector2.zero, CursorMode.Auto);
        TargetSet = true;
        DestSet= false;
        Selected.GetComponent<Tank>().NoTarget = false;
        Selected.GetComponent<Tank>().Targetaccept = false;


    }
    public void Stop()
    {
        Selected.GetComponent<Tank>().Destination = Selected.transform.position;
    }
    public void Direction()
    {
        Selected.GetComponent<Tank>().ChangeDir();
    }
    public void NoTarget1()
    {
        Selected.GetComponent<Tank>().NoTarget = true;
        Selected.GetComponent<Tank>().Targetaccept = false;
    }
    public void BomberSel()
    {
        Selected = bomber;
        SelectedUI();
        Debug.Log(bomber.transform.position + " po UI");
    }
    //pauza
    public void Pause()
    {
        if (pause == true)
        {
            pause = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            if (planningMode == true)
            {
                SlowMode();
            }
        }
        else
        {
            pause = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void BackToMenu()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene(1);
    }
    public void BacktoEditor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Data.Load = true;
        Data.Editor = true;
        Data.backtoeditor= true;
        SceneManager.LoadScene(0);
    }
    //planning mode
    public void SlowMode()
    {
        if(planningMode == true)
        {
            planningMode = false;
            Time.timeScale = 1.0f;
            slowUI.SetActive(false);
            buttontextslow.text = "Normal Mode";
        }
        else
        {
            planningMode = true;
            Time.timeScale = 0.1f;
            slowUI.SetActive(true);
            buttontextslow.text = "Planning Mode";
        }
    }
    public void GetFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        fpstext.text = fps + " FPS";
    }
    public IEnumerator redflashmoney()
    {
        textmoney.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        textmoney.color = Color.green;
        yield return new WaitForSeconds(0.1f);
        textmoney.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        textmoney.color = Color.green;
    }
}
