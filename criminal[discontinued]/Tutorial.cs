using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public LevelStats levelStats;
    public GameObject[] levels;
    void Start()
    {
        if (Data.Editor == true || Data.Load == true)
        {
            gameObject.SetActive(false);
            return;
        }
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }
        switch (Data.LvlPlace)
        {
            default:
                gameObject.SetActive(false);
                break;
            case 0:
                levels[0].SetActive(true); 
                break;
            case 1:
                levels[1].SetActive(true);
                break;
            case 2:
                levels[2].SetActive(true);
                break;
            case 3:
                levels[3].SetActive(true);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
