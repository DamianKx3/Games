using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class winanim : MonoBehaviour
{
    public GameObject[] kasa;
    public List<GameObject> queue;
    public Controller Controller;
    public TextMeshProUGUI text;
    public Image bg;
    float t;
    int goal;
    int tick;
    public AudioSource as1;
    void Start()
    {
        Controller = FindFirstObjectByType<Controller>();
        goal = Controller.MoneyGoal;
        if(Controller.MoneyGoal > 3000)
        {
            for (int i = 0; i < Mathf.FloorToInt(goal / 1000); i++)
            {
                GameObject obj = Instantiate(kasa[5], transform);
                obj.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                obj.transform.localPosition = obj.transform.localPosition + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
                obj.SetActive(false);
                queue.Add(obj);
            }
            goal = goal % 1000;
        } 
        for (int i = 0; i < Mathf.FloorToInt(goal / 100); i++)
        {
            GameObject obj = Instantiate(kasa[0], transform);
            obj.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            obj.transform.localPosition = obj.transform.localPosition + new Vector3(Random.Range(-5,5), Random.Range(-5, 5), 0);
            obj.SetActive(false);
            queue.Add(obj);
        }
        goal = goal % 100;
        for (int i = 0; i < Mathf.FloorToInt(goal / 50); i++)
        {
            GameObject obj = Instantiate(kasa[1], transform);
            obj.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            obj.transform.localPosition = obj.transform.localPosition + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            obj.SetActive(false);
            queue.Add(obj);
        }
        goal = goal % 50;
        for (int i = 0; i < Mathf.FloorToInt(goal / 10); i++)
        {
            GameObject obj = Instantiate(kasa[2], transform);
            obj.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            obj.transform.localPosition = obj.transform.localPosition + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            obj.SetActive(false);
            queue.Add(obj);
        }
        goal = goal % 10;
        for (int i = 0; i < Mathf.FloorToInt(goal / 5); i++)
        {
            GameObject obj = Instantiate(kasa[3], transform);
            obj.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            obj.transform.localPosition = obj.transform.localPosition + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            obj.SetActive(false);
            queue.Add(obj);
        }
        goal = goal % 5;
        for (int i = 0; i < Mathf.FloorToInt(goal); i++)
        {
            GameObject obj = Instantiate(kasa[4], transform);
            obj.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            obj.transform.localPosition = obj.transform.localPosition + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            obj.SetActive(false);
            queue.Add(obj);
        }

        text.text = "Wypłata!!!\n" + Controller.Money + "/" + Controller.MoneyGoal + "$";
    }

    // Update is called once per frame
    void Update()
    {
        if(bg.color.a < 1)
        {
            bg.color = bg.color + new Color(0,0,0,3) * Time.unscaledDeltaTime;
        }
        t = t + Time.unscaledDeltaTime;
        if(tick < queue.Count && t > 0.2f)
        {
            t = 0;
            queue[tick].SetActive(true);
            as1.Play();
            tick++;
            
        }
    }
}
