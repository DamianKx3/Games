using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalController : MonoBehaviour
{
    public TextMeshProUGUI Nightxt;
    public TextMeshProUGUI backtomain;
    float btmt;
    public GameObject clickedAt;
    public CameraController camcontroller;
    public Vector3 CamPos;
    public Transform MainCam;
    public int lookstate;
    public bool DIED;
    public GameObject Camcollider;
    public GameObject RightButton;
    public GameObject LeftButton;
    public GameObject RightDoors;
    public GameObject LeftDoors;
    public int MonitorLook;
    public GameObject CameraCanvas;

    public RenderTexture Currentrenderer;
    public RenderTexture[] camsRend;
    public RawImage CameraScreen;
    public int CameraNow;

    void Start()
    {
        Nightxt.text = Data.Napis;
        CamPos = MainCam.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            backtomain.gameObject.SetActive(true);
            btmt = btmt + Time.deltaTime;
            backtomain.text = $"back to main menu in: {Mathf.Round(3 - btmt)}s...";
            if (btmt > 2)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            btmt = 0;
            backtomain.gameObject.SetActive(false);
        }
        lookstate = camcontroller.LookState;
        RaycastHit hit;
        if (Physics.Raycast(MainCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
        {

            if (Input.GetMouseButtonDown(0) && hit.collider != null)
            {
                clickedAt = hit.collider.gameObject;
                Debug.Log(hit.collider);
                //ruch do kamer
                if(clickedAt == Camcollider)
                {
                    MonitorLook = 1;
                }
                
            }
        }
        if (MonitorLook == 1)
        {
            MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, camcontroller.transform.position, Time.deltaTime * 10);
            if (Vector3.Distance(MainCam.transform.position, camcontroller.transform.position) < 0.4f)
            {
                CameraCanvas.SetActive(true);
                CameraCanvas.GetComponent<Canvas>().worldCamera = MainCam.GetComponent<Camera>();
                Currentrenderer = camsRend[CameraNow];
                CameraScreen.texture = Currentrenderer;
            }
            else
            {
                CameraCanvas.SetActive(false);
            }
        }
    }
}
