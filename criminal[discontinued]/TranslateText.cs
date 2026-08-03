using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TranslateText : MonoBehaviour
{
    public string PolishTranslaction;
    void Start()
    {
        if (SettingsData._Lang == 1)
        {
            if (GetComponent<TextMeshProUGUI>())
            {
                GetComponent<TextMeshProUGUI>().text = PolishTranslaction;
            }
            if (GetComponent<TextMeshPro>())
            {
                GetComponent<TextMeshPro>().text = PolishTranslaction;
            }
        }
    }


}
