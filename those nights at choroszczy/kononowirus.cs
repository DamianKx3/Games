using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kononowirus : MonoBehaviour
{
    public Controllpanel Controllpanel;
    public TimeAndPower TimeAndPower;
    void Start()
    {

    }

    
    void Update()
    {
        TimeAndPower.virus = true;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        { 
            if(hit.collider.name == this.gameObject.name && Input.GetMouseButtonDown(0) || Controllpanel.laptopbatery < 0.1f)
            {
                TimeAndPower.virus = false;
                gameObject.SetActive(false);
            }
        
        
        }
        TimeAndPower.power = TimeAndPower.power - 1 * Time.deltaTime;
        if(Application.loadedLevelName == "wprowadzenie")
        {
            TimeAndPower.virus = false;
            gameObject.SetActive(false);
        }
    }
}
