using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelStats : MonoBehaviour
{
    public float MoneyOnStart;
    public Transform Spawn;
    public Controller Controller;
    public List<int> Forbitten;
    public int weather; //0-daytime 1-nighttime 2-maybe rain 3- maybe snow
    public Light2D Light;
    public float TimeLeft;

    public GameObject rain;
    public GameObject BaseObj;
    public GameObject Baseinside1;
    public GameObject Baseinside2;
    public BaseGround BG;
    public float waitsec;
    void Awake()
    {
        Work();
    }
    public void Start()
    {
        if(Data.LvlPlace == 0)
        {
            Spawn.transform.position = new Vector3(0,0, 0);

        }

    }

    // Update is called once per frame
    void Update()
    {
        if(waitsec < 1)
        {
            waitsec = waitsec + Time.unscaledDeltaTime;
            Camera.main.transform.position = new Vector3(Spawn.position.x, Spawn.position.y, Camera.main.transform.position.z);
        }
        if(BG.OnG == true)
        {
            Baseinside1.SetActive(true);
            Baseinside2.SetActive(false);
        }
        else
        {
            Baseinside2.SetActive(true);
            Baseinside1.SetActive(false);
        }

    }
    public void Work()
    {
        BaseObj.transform.position = Spawn.position + new Vector3(0,0,0);
        
        Controller = FindFirstObjectByType<Controller>();
        Controller.money = MoneyOnStart;
        Controller.spawnpoint = Spawn;
        if(TimeLeft == 0)
        {
            TimeLeft = -1;
        }
        Controller.TimeLeft= TimeLeft;
        //Debug.Log(Controller.TimeLeft);
        Light = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>();
        switch (weather)
        {
            case 0:
                Light.intensity = 0.9f;
                Camera.main.backgroundColor = new Color(0.1f, 0.2f, 0.2f, 1f);

                rain.SetActive(false);
                
                break;
            case 1:
                Light.intensity = 0.25f;
                Camera.main.backgroundColor = Color.black;
                
                
                rain.SetActive(false);
                
                break;
            case 2:
                Camera.main.backgroundColor = new Color(0.05f, 0.05f, 0.08f, 1f);
                Light.intensity = 0.5f;
                rain.SetActive(true);
                break;
            case 3:
                Camera.main.backgroundColor = new Color(0.2f, 0.15f, 0f, 1f);
                Light.intensity = 0.75f;
                rain.SetActive(false);
                break;
        }
    }
}
