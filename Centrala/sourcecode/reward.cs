using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class reward : MonoBehaviour
{
    public Image reward1;
    public GameObject trophyEz;
    public GameObject trophyH;
    public string WantedEz;
    public string WantedH;
    public MainMenu menu;
    void Start()
    {
        menu = FindFirstObjectByType<MainMenu>();
        reward1.enabled = false;
        trophyEz.SetActive(false);
        trophyH.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (menu.save.Split(' ').Contains(WantedH))
        {
            reward1.enabled = true;
            trophyH.SetActive(true);
        }
        else
        {
            if (menu.save.Split(' ').Contains(WantedEz))
            {
                reward1.enabled = true;
                trophyEz.SetActive(true);
            }
        }

    }
}
