using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class Controller : MonoBehaviour
{
    [Header("Ogolne")]
    public int MonitorLook;
    public Vector3 CamPos;
    public Transform MainCam;
    int lookstate;
    public float Money;
    public float MoneyMultipler;
    public List<int> videos;
    public int MoneyGoal;
    public float KononOnCamera;
    public float RecordMultipler;
    public string LevelNow;
    public GameObject LightMain;
    float lmt;
    string save;
    bool saveexists;
    bool lock1;
    public TextMeshProUGUI Nightxt;
    public AudioSource footstep;
    [Header("Dzialanie kamer")]
    public int CameraNow;
    CameraController camcontroller;
    public RenderTexture Currentrenderer;
    public RenderTexture[] camsRend;
    public RawImage CameraScreen;
    public Monitoring[] Monitorings;
    public GameObject noRespondScreen;
    public float noRespond;
    public int Cam1Crash;
    public int Cam2Crash;
    public GameObject BlockedScreen;
    public TextMeshProUGUI camtxt;
    public Texture2D screenshot;
    [Header("przejscie do komputerow")]
    public Transform Screen1;
    public Transform Screen2;
    public GameObject clickedAt;
    public GameObject camTrigger;
    public GameObject compTrigger;
    public GameObject CameraCanvas;
    public GameObject CompCanvas;
    [Header("Przeciwnicy")]
    public Konon Konon;
    public Fiodor Fiodor;
    public OlgierdEnemy Olgierd;
    int KononPos;
    [Header("Drzwi")]
    public GameObject Doors;
    public int DoorState;
    public Animator animator;
    public bool HoldingDoors;
    public Camera CameraDoors;
    public bool DIED;
    [Header("DrzwiNaKamerze")]
    public GameObject buttondoors;
    public GameObject outsideDoors;
    public Button wiatrolapcam;
    Vector3 outstartpos;
    public bool CdoorsClosed;
    public bool holddoorbutton { get; set; }
    [Header("Okno")]
    public GameObject Oknotrigger;
    public int WindowState;
    public GameObject windowlight;
    public AudioSource flashclick;
    public float flashlightmal;
    [Header("EndScreens")]
    public GameObject DeathScreen;
    public GameObject WinScreen;
    public TextMeshProUGUI backtomain;
    public TextMeshProUGUI tiptxt;
    public string tip;
    public TextMeshProUGUI wintxt;
    public string wincondition;
    float btmt;
    [Header("postProcessing")]
    public VolumeProfile[] VolumeProfiles;
    public Volume PostProcessing;
    public bool SkipJumpScareAnim;
    [Header("Komentarze")]
    public Transform content;
    public GameObject comment;
    public List<GameObject> comments;
    public string[] goodcom;
    public string[] badcom;
    public string[] goodcomENG;
    public string[] badcomENG;
    public TextMeshProUGUI ratingtext;
    public float punishment;
    float t2;
    [Header("outage")]
    public Light[] Lights;
    public AudioSource camAudio;
    public AudioClip poweroutcl;
    public GameObject Playaudiobutton;
    public float audiocooldown;
    public AudioSource playaudio;
    public AudioClip[] clips;
    public outrage outrage;
    public GameObject outagelight;
    [Header("major")]
    public GameObject major;
    [Header("Final")]
    public GameObject comptriggerFinal;
    public GameObject leftbutton;
    public GameObject rightbutton;
    public GameObject leftdoors;
    public GameObject rightdoors;
    public Transform FinalCam;
    public int FinalDoorState;
    public GameObject kuferTrigger;
    public int kuferstate;
    public Animator CamAnimFinal;
    public GameObject leftlamp;
    public GameObject rightlamp;
    public bool[] burnedOutRooms;
    public float burndealy;
    public TextMeshProUGUI burntxt;
    public Animator kufer;
    public FinalJan Jan;
    public AudioSource doorsFinalsound;
    public AudioSource zolnierzesong;
    public GameObject finalofficeanim;
    void Start()
    {
        burndealy = 40;
        MoneyMultipler = 1;
        PostProcessing.profile = VolumeProfiles[0];

        if(Data.FinalMode == false)
        {
            camcontroller = MainCam.gameObject.GetComponent<CameraController>();

        }
        else
        {
            camcontroller = FinalCam.gameObject.GetComponent<CameraController>();
            MainCam = FinalCam;
            camTrigger = comptriggerFinal;
            Screen1 = comptriggerFinal.transform;
            CameraCanvas.GetComponentInParent<Canvas>().worldCamera = FinalCam.GetComponent<Camera>();
            CamAnimFinal.enabled = false;
            animator = FinalCam.GetComponent<Animator>();
        }
        if(RecordMultipler <= 0)
        {
            RecordMultipler = 1;
        }
        ChangeCam(0);
        //drzwi z kamery
        outstartpos = outsideDoors.transform.position;
        outsideDoors.transform.position = outstartpos + new Vector3(0,7,0);
        CamPos = MainCam.position;


        //save
        LevelNow = Data.CurrentLvl;
        Fiodor.LevelOfDanger = Data.Fiodor;
        Olgierd.LevelOfDanger = Data.Olgierd;
        if(Data.MoneyGoal != 0)
        {
            MoneyGoal = Data.MoneyGoal;
        }
        Konon.LevelOfDanger = Data.Konon;
        Money = Data.MoneyOnStart;

        if(File.Exists(Application.persistentDataPath + "/Save/s.txt"))
        {
            saveexists = true;
            StreamReader sr = new StreamReader(Application.persistentDataPath + "/Save/s.txt");
            save = sr.ReadToEnd();
            sr.Close();
        }
        Nightxt.text = Data.Napis;
        outagelight.SetActive(false);
        if (Data.powerOut == true && Data.FinalMode == false)
        {
            camAudio.loop = false;
            camAudio.clip = poweroutcl;
            camAudio.Play();
            StartCoroutine(outbreak());
            Playaudiobutton.SetActive(true);
            outagelight.SetActive(true);
        }
        if(Data.CurrentLvl == "4" || Data.CurrentLvl == "4H")
        {
            finalofficeanim.SetActive(true);
        }
        if(Settings.TranslateToEng == true)
        {
            goodcom = goodcomENG;
            badcom = badcomENG;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Data.invincibility > 0)
        {
            Data.invincibility = Data.invincibility - Time.deltaTime;
            Debug.Log(Data.invincibility);
        }
        else
        {
            Data.invincibility = 0;
        }
        
        if (DIED == true)
        {
            tiptxt.text = "wskazówka: " + tip;
            if (Settings.TranslateToEng == true) tiptxt.text = "tip: " + tip;
            PostProcessing.profile = VolumeProfiles[0];
            if(lmt == 0)
            {
                outrage.toflash = 0;
            }
            lmt = lmt + Time.deltaTime;
            if(lmt > 0.1f)
            {
                lmt = 0;
                if(LightMain.activeSelf == true)
                {
                    LightMain.SetActive(false);
                }
                else
                {
                    LightMain.SetActive(true);
                }
            }
            CameraCanvas.SetActive(false);
            CompCanvas.SetActive(false);
            animator.enabled = true;
            camcontroller.Distable = true;
            MainCam.GetComponent<Camera>().enabled = true;
            CameraDoors.enabled = false;  
            return;
        }
        if (Input.GetKey(KeyCode.Tab) && Time.timeScale != 0)
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
        }
        CompCanvas.GetComponent<Computer>().moneyhistory.text = "";
        if (MoneyMultipler - punishment > 0)
        {
            for (int i = 0; i < videos.Count; i++)
            {
                Money = Money + (float)videos[i] / 50 * Time.deltaTime * (MoneyMultipler - punishment);
                CompCanvas.GetComponent<Computer>().moneyhistory.text = CompCanvas.GetComponent<Computer>().moneyhistory.text + Mathf.Round(((float)videos[i] / 50 * MoneyMultipler) * 1000) / 1000 + "$ -jotube adsense\n";
            }
        }
        else
        {
            for (int i = 0; i < videos.Count; i++)
            {
                CompCanvas.GetComponent<Computer>().moneyhistory.text = CompCanvas.GetComponent<Computer>().moneyhistory.text + "0$ -jotube adsense (too low rating)\n";
            }
        }


        if (Input.GetKey(KeyCode.Escape))
        {
            backtomain.gameObject.SetActive(true);
            btmt = btmt + Time.deltaTime;
            backtomain.text = $"powrót do menu w: {Mathf.Round(3 - btmt)}s...";
            if (Settings.TranslateToEng == true) backtomain.text = $"back to main menu in: {Mathf.Round(3 -btmt)}s...";
            if(btmt > 2)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            btmt = 0;
            backtomain.gameObject.SetActive(false);
        }
        if (Money >= MoneyGoal)
        {
            if(lock1 == true)
            {
                return;
            }
            lock1 = true;
            if (Data.FinalMode ==false)
            {
                if(Data.powerOut == false)
                {
                    wincondition = "zarobiono: " + MoneyGoal + "$";
                    if (Settings.TranslateToEng == true) wincondition = "Earns: " + MoneyGoal + "$";
                }
                else
                {
                    wincondition = "dotrwano do 6:00";
                    if (Settings.TranslateToEng == true) wincondition = "survived to 6:00";
                }
            }
            else
            {
                wincondition = "zniszczono dowody";
                if (Settings.TranslateToEng == true) wincondition = "evidence has been destroyed";
            }
            wintxt.text = "Misja zaliczona: \n" + wincondition;
            Time.timeScale = 0;
            WinScreen.SetActive(true);
            if(saveexists == true)
            {

                string[] a = save.Split(' ');
                if (a.Contains(LevelNow) == false)
                {
                    StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Save/s.txt", true);
                    sw.Write(LevelNow + " ");
                    sw.Close();
                }
            }
            else
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Save/");
                StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Save/s.txt", true);
                sw.Write(LevelNow + " ");
                sw.Close();
            }
            if(Data.FinalMode == true && (Data.CurrentLvl == "4"|| Data.CurrentLvl == "4H"))
            {
                SceneManager.LoadScene(3);
            }

        }

        //poruszanie sie gracza
        lookstate = camcontroller.LookState;
        RaycastHit hit;
        if (Physics.Raycast(MainCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
        {

            if (Input.GetMouseButtonDown(0) && hit.collider != null)
            {
                clickedAt = hit.collider.gameObject;
                Debug.Log(hit.collider);

                //ruch do kamer
                if(clickedAt == camTrigger && MonitorLook == 0)
                {
                    camcontroller.Distable = true;
                    MonitorLook = 1;
                }
                //ruch do kompa
                if (clickedAt == compTrigger && MonitorLook == 0)
                {
                    camcontroller.Distable = true;
                    MonitorLook = 2;
                }
                //ruch do drzwi
                if(camcontroller.LookState == 3 && clickedAt == Doors && DoorState == 0)
                {
                    camcontroller.Distable = true;
                    DoorState = 1;
                }
                //ruch do okna
                
                if (camcontroller.LookState == 2 && clickedAt == Oknotrigger && WindowState == 0)
                {
                    camcontroller.Distable = true;
                    WindowState = 1;
                }
                //finaldrzwi
                if (clickedAt == leftbutton)
                {
                    FinalDoorState = 0;
                }
                if (clickedAt == rightbutton)
                {
                    FinalDoorState = 1;
                }
                if (clickedAt == kuferTrigger && kuferstate == 0)
                {
                    kuferstate = 1;


                }
            }
        }
        //monitor
        if (MonitorLook == 3)
        {
            MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, CamPos, Time.deltaTime * 10);
            if (Vector3.Distance(MainCam.transform.position, CamPos) < 0.4f)
            {
                MonitorLook = 0;
                camcontroller.Distable = false;
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(1) && MonitorLook != 0 && MonitorLook != 3)
            {
                major.GetComponent<Major>().Off();
                if (MonitorLook == 1)
                {
                    screenshot = ScreenCapture.CaptureScreenshotAsTexture();
                    
                }

                MonitorLook = 3;
            }
        }
        if (MonitorLook == 1)
        {
            MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, Screen1.position, Time.deltaTime * 10);
            if(Vector3.Distance(MainCam.transform.position,Screen1.position) < 0.4f)
            {
                PostProcessing.profile = VolumeProfiles[1];
                bool jortrigger = false;
                if(CameraCanvas.activeSelf == false)
                {
                    jortrigger = true;
                }
                CameraCanvas.SetActive(true);
                if(jortrigger == true)
                {
                    major.GetComponent<Major>().Trigger();
                }
                Currentrenderer = camsRend[CameraNow];
                CameraScreen.texture = Currentrenderer;
                if(Data.FinalMode == false)
                {
                    if (Data.powerOut == false)
                    {
                        if (CameraNow != 6 && Konon.Pos == CameraNow && Monitorings[CameraNow].Trigger.Triggered && KononOnCamera < 90)
                        {
                            if (major.GetComponent<Major>().State != 2)
                            {
                                KononOnCamera = KononOnCamera + Time.deltaTime * RecordMultipler;

                            }
                            if (Mathf.Round(KononOnCamera % 59) > 9)
                            {
                                camtxt.text = "00:0" + Mathf.Floor(KononOnCamera / 59) + ":" + Mathf.Round(KononOnCamera % 59);
                            }
                            else
                            {
                                camtxt.text = "00:0" + Mathf.Floor(KononOnCamera / 59) + ":0" + Mathf.Round(KononOnCamera % 59);
                            }
                        }
                    }
                    else
                    {
                        switch (Mathf.FloorToInt(KononOnCamera / 60))
                        {
                            default:
                                break;
                            case 0:
                                camtxt.text = "12 AM";
                                break;
                            case 1:
                                camtxt.text = "1 AM";
                                break;
                            case 2:
                                camtxt.text = "2 AM";
                                break;
                            case 3:
                                camtxt.text = "3 AM";
                                break;
                            case 4:
                                camtxt.text = "4 AM";
                                break;
                            case 5:
                                camtxt.text = "5 AM";
                                break;
                        }
                    }
                }
                
                

            }
            else
            {
                PostProcessing.profile = VolumeProfiles[0];
                CameraCanvas.SetActive(false);
            }
        }
        else
        {
            CameraCanvas.SetActive(false);

        }
        if (Data.powerOut == true && Data.FinalMode == false)
        {
            KononOnCamera = KononOnCamera + Time.deltaTime;
            if (Mathf.FloorToInt(KononOnCamera / 60) >= 6)
            {
                Money = MoneyGoal;
            }
        }
        if(Data.FinalMode == true)
        {

            int a = 0;
            for (int i = 0; i < burnedOutRooms.Length; i++)
            {
                if (burnedOutRooms[i] == false)
                {
                    a++;
                }
            }
            if(a == 2)
            {
                if ((Data.CurrentLvl == "4"|| Data.CurrentLvl == "4H") && zolnierzesong.isPlaying == false)
                {
                    zolnierzesong.Play();
                }
                if(burndealy  <= 0)
                {
                    Money = MoneyGoal;
                }
            }
            if(a == 1)
            {
                Money = MoneyGoal;
            }
        }
        if (MonitorLook == 2)
        {
            MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, Screen2.position, Time.deltaTime * 10);
            if (Vector3.Distance(MainCam.transform.position, Screen2.position) < 0.4f)
            {
                CompCanvas.SetActive(true);
                PostProcessing.profile = VolumeProfiles[1];
            }
            else
            {
                CompCanvas.SetActive(false);
                PostProcessing.profile = VolumeProfiles[0];
            }
        }
        else
        {
            CompCanvas.SetActive(false);
        }


        //drzwi
        if(DoorState == 1)
        {
            animator.enabled = true;
            animator.SetBool("GoDoors",true);
            if(footstep.isPlaying == false)
            {
                footstep.pitch = Random.Range(0.3f, 0.4f);
                footstep.Play();
            }
        }
        if(DoorState == 0)
        {
            animator.enabled = false;
            if(MonitorLook == 0)
            {
                PostProcessing.profile = VolumeProfiles[0];
            }
        }
        if(DoorState == 2)
        {
            PostProcessing.profile = VolumeProfiles[0];
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetBool("Look", false);
                animator.SetBool("Block", true);

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetBool("Block", false);
                animator.SetBool("Look", true);

            }
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetBool("GoDoors", false);


            }
            if (footstep.isPlaying == false && animator.GetBool("GoDoors") == false)
            {
                footstep.pitch = Random.Range(0.3f, 0.4f);
                footstep.Play();
            }
        }
        if (DoorState == 3)
        {

            MainCam.GetComponent<Camera>().enabled = false;
            CameraDoors.enabled = true;
            PostProcessing.profile = VolumeProfiles[2];
            Debug.Log("a");
            if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(1))
            {
                //blok
                animator.SetBool("Look", false);
                animator.SetBool("Block", false);
                PostProcessing.profile = VolumeProfiles[0];

            }

        }
        else
        {
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            MainCam.GetComponent<Camera>().enabled = true;
            CameraDoors.enabled = false;
        }

        if (DoorState == 4)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetMouseButtonDown(1))
            {
                //patrz
                animator.SetBool("Look", false);
                animator.SetBool("Block", false);

            }
            HoldingDoors = true;
            //if (Input.GetMouseButton(0))
            //{

            //}
            //else
            //{
                //HoldingDoors = false;
            //}


        }
        else
        {
            HoldingDoors = false;
        }
        if (DoorState == 5)
        {
            camcontroller.Distable = false;
            DoorState = 0;
        }
        //wylaczenie kamer na ktorych jest ruch
        if (noRespond > 0)
        {
            noRespond = noRespond - Time.deltaTime;
            if (CameraNow == Cam1Crash || CameraNow == Cam2Crash)
            {
                noRespondScreen.SetActive(true);
                Monitorings[CameraNow].gameObject.SetActive(false);
            }
        }
        else
        {
            noRespondScreen.SetActive(false);
            Monitorings[CameraNow].gameObject.SetActive(true);
        }
        //przycisk drzwi z kamery
        if (Data.FinalMode == false)
        {
            if (Data.ActiveDoors == true)
            {
                if (Data.powerOut == false)
                {
                    if (CameraNow != 6)
                    {
                        buttondoors.SetActive(false);
                    }
                    else
                    {
                        buttondoors.SetActive(true);
                    }
                    if (holddoorbutton == false)
                    {
                        if (outsideDoors.transform.position.y < outstartpos.y + 7f)
                        {
                            outsideDoors.transform.position = outsideDoors.transform.position + new Vector3(0, 1, 0) * Time.deltaTime;
                        }
                        else
                        {
                            outsideDoors.transform.position = new Vector3(outsideDoors.transform.position.x, outstartpos.y + 7f, outsideDoors.transform.position.z);
                        }
                    }
                    else
                    {
                        if (outsideDoors.transform.position.y > outstartpos.y)
                        {
                            outsideDoors.transform.position = outsideDoors.transform.position - new Vector3(0, 1, 0) * Time.deltaTime;
                        }
                        else
                        {
                            outsideDoors.transform.position = new Vector3(outsideDoors.transform.position.x, outstartpos.y, outsideDoors.transform.position.z);
                        }
                    }
                    if (outsideDoors.transform.position.y > outstartpos.y + 1)
                    {
                        CdoorsClosed = false;
                    }
                    else
                    {
                        CdoorsClosed = true;
                    }
                }
                else
                {
                    wiatrolapcam.interactable = true;
                    CdoorsClosed = false;
                    buttondoors.SetActive(false);
                }

            }
            else
            {
                wiatrolapcam.interactable = false;
                CdoorsClosed = true;
                buttondoors.SetActive(false);
            }

        }
        else
        {
            wiatrolapcam.interactable = false;
            buttondoors.SetActive(false);
        }


        //okno
        if (WindowState == 1)
        {
            animator.enabled = true;
            animator.SetBool("Window", true);
            if (footstep.isPlaying == false)
            {
                footstep.pitch = Random.Range(0.3f, 0.4f);
                footstep.Play();
            }
        }
        if (WindowState == 2)
        {
            if(windowlight.activeSelf == false)
            {
                flashclick.Play();
            }
            if (flashlightmal > 0)
            {
                flashlightmal = flashlightmal - Time.deltaTime;
                windowlight.SetActive(false);
            }
            else
            {
                windowlight.SetActive(true);
            }

            if (Physics.Raycast(MainCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 500.0f))
            {
                //windowlight.transform.eulerAngles = Vector3.RotateTowards(windowlight.transform.eulerAngles,hit.point,Time.deltaTime * 10, Time.deltaTime * 10);
                Vector3 dir = (hit.point - windowlight.transform.position);
                windowlight.transform.rotation = Quaternion.LookRotation(dir);
            }
            Ray ray = MainCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
            bool testo = false;
            foreach (RaycastHit hit1 in hits)
            {
                if (hit1.collider.gameObject.tag == "Olo")
                {
                    testo = true;
                    break;
                }
            }
            if(testo == true)
            {
                Olgierd.flashing = true;
            }
            else
            {
                Olgierd.flashing = false;
            }

            //windowlight.transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Lerp(transform.eulerAngles.y, targetRot, Time.deltaTime * 10), transform.eulerAngles.z);

            if (Input.GetMouseButtonDown(1))
            {
                WindowState = 3;
            }
        }
        else
        {
            if (windowlight.activeSelf == true)
            {
                flashclick.Play();
            }
            windowlight.SetActive(false);
        }
        if(WindowState == 3)
        {
            animator.enabled = true;
            animator.SetBool("Window", false);
            if (footstep.isPlaying == false)
            {
                footstep.pitch = Random.Range(0.3f, 0.4f);
                footstep.Play();
            }
        }
        if(WindowState == 2)
        {
            Oknotrigger.GetComponent<Collider>().enabled = false;
        }
        else
        {
            Oknotrigger.GetComponent<Collider>().enabled = true;

        }

        //komentarze
        if (Data.Comments > 0 && Data.FinalMode == false)
        {
            if (punishment > 0)
            {
                punishment = punishment - Time.deltaTime * 0.005f;
            }
            ratingtext.text = Mathf.Round((MoneyMultipler - punishment) * 1000) / 100 + "/10*";
            if (MoneyMultipler < 0.1f)
            {
                MoneyMultipler = 0.1f;
            }
            else if (MoneyMultipler > 1)
            {
                MoneyMultipler = 1;
            }
            if (t2 > 5 / Data.Comments)
            {

                t2 = 0;
                int r = Random.Range(0, 10);
                if (r >= 3 && videos.Count > 0)
                {

                    for (int i = 0; i < comments.Count; i++)
                    {
                        if (comments[i] == null)
                        {
                            Debug.Log(comments[i]);
                            comments.RemoveAt(i);
                        }
                    }
                    if (r >= 9)
                    {
                        //comments.Add("-"+badcom[Random.Range(0,badcom.Length)]);
                        GameObject a = Instantiate(comment, content);
                        a.GetComponent<comment>().text.text = badcom[Random.Range(0, badcom.Length)];
                        a.GetComponent<comment>().isBad = true;
                        a.GetComponent<comment>().addrating = MoneyMultipler - MoneyMultipler * 0.9f;
                        MoneyMultipler = MoneyMultipler * 0.9f;
                        comments.Add(a);
                    }
                    else
                    {
                        //comments.Add(goodcom[Random.Range(0, goodcom.Length)]);
                        GameObject a = Instantiate(comment, content);
                        a.GetComponent<comment>().text.text = goodcom[Random.Range(0, goodcom.Length)];
                        comments.Add(a);
                    }
                }
            }
            else
            {
                t2 = t2 + Time.deltaTime;
            }
        }
        if(Data.powerOut == true && Data.FinalMode == false)
        {
            if(audiocooldown > 0)
            {
                audiocooldown = audiocooldown - Time.deltaTime;
            }
        }
        //FiNAL
        if(Data.FinalMode == true)
        {


            if (burndealy > 0)
            {
                burndealy = burndealy - Time.deltaTime;
                burntxt.text = "burn\n(" + Mathf.Round(burndealy) + "s.)";
            }
            else
            {
                burntxt.text = "burn\n(ready)";
            }
            if (FinalDoorState == 0)
            {
                if(rightlamp.activeSelf == false)
                {
                    doorsFinalsound.Play();
                }
                rightlamp.SetActive(true);
                leftlamp.SetActive(false);
                if (rightdoors.transform.localPosition.x < 0.74f)
                {
                    rightdoors.transform.localPosition = rightdoors.transform.localPosition + new Vector3(5, 0, 0) * Time.deltaTime;
                }
                else
                {
                    rightdoors.transform.localPosition = new Vector3(0.74f, rightdoors.transform.localPosition.y, rightdoors.transform.localPosition.z);

                }
                if (leftdoors.transform.localPosition.x > -0.22f)
                {
                    leftdoors.transform.localPosition = leftdoors.transform.localPosition + new Vector3(-5, 0, 0) * Time.deltaTime;
                }
                else
                {
                    leftdoors.transform.localPosition = new Vector3(-0.22f, leftdoors.transform.localPosition.y, leftdoors.transform.localPosition.z);

                }
            }
            else
            {
                if (rightlamp.activeSelf == true)
                {
                    doorsFinalsound.Play();
                }
                rightlamp.SetActive(false);
                leftlamp.SetActive(true);
                if (leftdoors.transform.localPosition.x < 0.74f)
                {
                    leftdoors.transform.localPosition = leftdoors.transform.localPosition + new Vector3(5, 0, 0) * Time.deltaTime;
                }
                else
                {
                    leftdoors.transform.localPosition = new Vector3(0.74f, leftdoors.transform.localPosition.y, leftdoors.transform.localPosition.z);
                }

                if (rightdoors.transform.localPosition.x > -0.22f)
                {
                    rightdoors.transform.localPosition = rightdoors.transform.localPosition + new Vector3(-5, 0, 0) * Time.deltaTime;
                }
                else
                {
                    rightdoors.transform.localPosition = new Vector3( -0.22f, rightdoors.transform.localPosition.y, rightdoors.transform.localPosition.z);
                }
            }
            if(kuferstate == 1)
            {
                CamAnimFinal.enabled = true;
                CamAnimFinal.SetInteger("kufer",1);
                kufer.SetBool("open", true);
                if (Input.GetMouseButtonDown(1))
                {
                    CamAnimFinal.SetInteger("kufer", 2);
                    kufer.SetBool("open", false);
                    kuferstate = 2;

                }
            }
            if(kuferstate == 2)
            {
                CamAnimFinal.enabled = true;
            }
            if(kuferstate == 0)
            {
                CamAnimFinal.enabled = false;
            }
            if (lookstate == 0)
            {
                if (Physics.Raycast(MainCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 500.0f))
                {
                    //windowlight.transform.eulerAngles = Vector3.RotateTowards(windowlight.transform.eulerAngles,hit.point,Time.deltaTime * 10, Time.deltaTime * 10);
                    Vector3 dir = (hit.point - windowlight.transform.position);
                    windowlight.transform.rotation = Quaternion.LookRotation(dir);
                }
                Ray ray = MainCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
                bool testo = false;
                foreach (RaycastHit hit1 in hits)
                {
                    if (hit1.collider.gameObject.tag == "Jan")
                    {
                        testo = true;
                        break;
                    }
                }
                Jan.flashed = testo;
            }
        }

    }
    public void ChangeCam(int num)
    {
        if(CameraNow != num)
        {
            major.GetComponent<Major>().Trigger();
        }
        CameraNow = num;
        foreach (var item in Monitorings)
        {
            item.gameObject.SetActive(false);
        }

        //Monitorings[num].gameObject.SetActive(true);
    }
    public bool pay(int price)
    {
        if (price <= Money)
        {
            Money = Money - price;
            return true;
        }
        return false;
    }
    public void DoorsMove(int ID)
    {
        DoorState = ID;

    }
    public void ShowDeathScreen()
    {
        DeathScreen.SetActive(true);
    }
    
    public void OpenWar(bool open)
    {
        //Wardrobe.GetComponent<Animator>().SetBool("open", open);
    }
    public void SetWindowState(int state)
    {
        WindowState = state;
        if(state == 0)
        {
            camcontroller.Distable = false;
            animator.enabled = false;
        }
    }
    public IEnumerator outbreak()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            bool tmp = false;
            for (int i = 0; i < Lights.Length; i++)
            {
                if (Lights[i].intensity > 0)
                {
                    Lights[i].intensity = Lights[i].intensity - 10 * Time.deltaTime;
                    tmp = true;
                    yield return null;


                }
            }
            if(tmp == false)
            {
                break;
            }

        }
    }
    public void Playaudio()
    {
        if(audiocooldown <= 0)
        {
            audiocooldown = 2;
            Konon.PlayAudio(CameraNow);
            Debug.Log("Play audio " + CameraNow);
            playaudio.clip = clips[Random.Range(0,clips.Length)];
            playaudio.Play();
        }
    }
    public void Burn()
    {
        if(burndealy <= 0 && burnedOutRooms[CameraNow] == false)
        {
            if(CameraNow == 1 && burnedOutRooms[0] == false)
            {
                return;
            }
            if (CameraNow == 4)
            {
                return;
            }
            burnedOutRooms[CameraNow] = true;
            burndealy = 50;
        }

    }
    
}
