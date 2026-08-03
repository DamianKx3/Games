using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class officelight : MonoBehaviour
{
    Light Light;
    public TimeAndPower TimeAndPower;
    void Start()
    {
        Light = GetComponent<Light>();

    }

    
    void Update()
    {
        Light.range = 8  + TimeAndPower.power / 10;

    }
}
