using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class Computer : MonoBehaviour
{
    public bool holding;
    public GameObject[] Windows;
    public int currentIndex;
    public Konon Konon;
    public GameObject heartalive;
    public GameObject heartdead;
    public TextMeshProUGUI sugardisplay;
    public TextMeshProUGUI moneydisplay;
    public Controller controller;
    public TextMeshProUGUI duration;
    public TextMeshProUGUI saturation;
    public TextMeshProUGUI brightness;
    public TextMeshProUGUI bloom;
    public Slider Durationslider;
    public Slider MaxDur;
    public Slider MinDur;
    public Slider Brightnessslider;
    public Slider saturationslider;
    public Slider bloomslider;
    public Slider renderslider;
    public float qualityofvid;
    public TMP_Dropdown qualitydropdown;
    float t1;
    int brightnesstarget;
    int saturationtarget;
    int bloomtarget;
    public bool startrendering;

    public float additionalX;
    public float additionalY;
    int maxheight;
    int maxwidth;
    public TextMeshProUGUI dateDetail;
    public TextMeshProUGUI moneyhistory;
    float t2;
    public RectTransform content;

    public Button YTButton;
    public Slider goalslider;
    public TextMeshProUGUI goal;
    public Police police;
    public RawImage movimg;
    void Start()
    {
        brightnesstarget = Random.Range(10, 90);
        saturationtarget = Random.Range(10, 90);
        bloomtarget = Random.Range(10, 90);

    }

    // Update is called once per frame
    void Update()
    {
        if (Data.Comments == 0)
        {
            YTButton.gameObject.SetActive(false);

        }
        if (police.State != 0)
        {
            heartalive.SetActive(false);
            heartdead.SetActive(true);

        }
        else
        {
            heartalive.SetActive(true);
            heartdead.SetActive(false);

        }
        maxheight = Screen.height;
        maxwidth = Screen.width;

        additionalX = (Input.mousePosition.x - maxwidth / 2) / maxwidth;
        additionalY = (Input.mousePosition.y - maxheight / 2) / maxheight;
        dateDetail.text = Data.Date;
        if(Data.Date == "")
        {
            //dateDetail.text = System.DateTime.Today.Day +"." + System.DateTime.Today.Month + "." +System.DateTime.Today.Year;
            dateDetail.text = System.DateTime.Today.ToString("dd.mm.yyyy");
        }
        if (holding == true)
        {
            //Vector3 pos1 = transform.position + new Vector3(0, additionalY, 0) + additionalX * transform.right;
            //Windows[currentIndex].transform.position = pos1;
            Windows[currentIndex].transform.localPosition = new Vector3(additionalX * Screen.width, additionalY * Screen.height - 300, 0);



        }

        goal.text = "goal: \n" + controller.MoneyGoal + "$";
        goalslider.value = controller.Money / controller.MoneyGoal;




        t1 = t1 + Time.deltaTime;
        if (t1 > 1)
        {
            t1 = 0;
            sugardisplay.text = "sugar: " + Mathf.Round(Konon.sugarLevel * 100) / 100;
            moneydisplay.text = Mathf.Round(controller.Money * 100) / 100 + "$";
        }
        if(MaxDur.value <= MinDur.value)
        {
            MinDur.value = MinDur.value - 0.05f;
            MaxDur.value = MaxDur.value + 0.05f;
        }
        if(startrendering == false) // nie ma zmieniania po renderowaniu
        {
            duration.text = "duration: " + Mathf.Round(controller.KononOnCamera) + "s";
            Durationslider.value = (controller.KononOnCamera - controller.KononOnCamera * MinDur.value - controller.KononOnCamera * (1 - MaxDur.value)) / 100;
        }
        saturation.text = "saturation: " + saturationslider.value + "/100";
        brightness.text = "brightness: " + Brightnessslider.value + "/100";
        bloom.text = "bloom: " + bloomslider.value + "/100";
        if (Mathf.Abs(saturationslider.value - saturationtarget) <= 3)
        {
            saturation.color = Color.green;
        }
        else
        {
            saturation.color = Color.white;
        }


        if (Mathf.Abs(Brightnessslider.value - brightnesstarget) <= 3)
        {
            brightness.color = Color.green;
        }
        else
        {
            brightness.color = Color.white;
        }


        if (Mathf.Abs(bloomslider.value - bloomtarget) <= 3)
        {
            bloom.color = Color.green;
        }
        else
        {
            bloom.color = Color.white;
        }
        if(startrendering == true)
        {
            if(renderslider.value == 1)
            {
                startrendering = false;
                int mon = 0;
                //0.55 0.65

                mon = mon + 10;
                if (Durationslider.value > 0.55f && Durationslider.value < 0.65f)
                {
                    mon = mon + 20;
                    if (Mathf.Abs(MinDur.value - (1 - MaxDur.value)) < 0.2f)
                    {
                        mon = mon + 5;
                    }
                }
                else
                {
                    if ((Durationslider.value > 0.32f && Durationslider.value < 0.55f) || ((Durationslider.value > 0.65f && Durationslider.value < 0.85f)))
                    {
                        mon = mon + 10;
                    }
                }

                if (Mathf.Abs(saturationslider.value - saturationtarget) <= 3)
                {
                    mon = mon + 5;
                }

                if (Mathf.Abs(Brightnessslider.value - brightnesstarget) <= 3)
                {
                    mon = mon + 5;
                }

                if (Mathf.Abs(bloomslider.value - bloomtarget) <= 3)
                {
                    mon = mon + 5;
                }
                if (qualityofvid == 0.2f)
                {
                    mon = mon + 5;
                }
                if (qualityofvid == 0.1f)
                {
                    mon = mon + 10;
                }
                brightnesstarget = Random.Range(10, 90);
                saturationtarget = Random.Range(10, 90);
                bloomtarget = Random.Range(10, 90);
                controller.KononOnCamera = 0;
                renderslider.value = 0;
                controller.videos.Add(mon);
            }
            else
            {
                renderslider.value = renderslider.value + Time.deltaTime * qualityofvid;//0.25 bylo
            }
        }
        content.sizeDelta = new Vector2(content.sizeDelta.x, 100 * controller.comments.Count + 160);

        movimg.texture = controller.screenshot;

    }
    public void StartHold(int index)
    {
        currentIndex = index;
        holding = true;
    }
    public void SetOnTop(int index)
    {

        Windows[index].transform.SetAsLastSibling();

    }
    public void ResetPos(int index)
    {
        Windows[index].transform.localPosition = new Vector3(0, 0, 0);
    }
    public void StopHold()
    {
        holding=false;
    }
    public void Render1()
    {
        if (startrendering == false && controller.KononOnCamera >= 30)
        {
            if(qualitydropdown.value == 0)
            {
                qualityofvid = 0.6f;
            }else if (qualitydropdown.value == 1)
            {
                qualityofvid = 0.22f;
            }
            else if (qualitydropdown.value == 2)
            {
                qualityofvid = 0.07f;
            }
            startrendering = true;
        }
    }
    public void pagemoney(int add)
    {
        if(moneyhistory.pageToDisplay > 1 || add > 0)
        {
            moneyhistory.pageToDisplay = moneyhistory.pageToDisplay + add;
        }
    }
}
