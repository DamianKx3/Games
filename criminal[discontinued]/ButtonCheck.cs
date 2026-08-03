using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonCheck : MonoBehaviour
{
    public int ID;
    public GameObject lockObj;
    public LevelStats levelStats;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(levelStats == null)
        {
            levelStats = FindFirstObjectByType<LevelStats>();
        }

        if (levelStats.Forbitten != null && levelStats.Forbitten.Contains(ID))
        {
            lockObj.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
    }
}
