using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveLock : MonoBehaviour
{
    public MainMenu menu;
    public Button button;
    public Button buttonhard;
    public string Wanted;
    public string ORwanted;
    public GameObject lockobj;
    
    void Start()
    {

        menu = FindFirstObjectByType<MainMenu>();
        button.interactable = false;
        if(buttonhard != null)
        {
            buttonhard.interactable = false;
            buttonhard.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
        }
        lockobj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
     if(menu.save.Split(' ').Contains(Wanted) || (menu.save.Split(' ').Contains(ORwanted) && ORwanted != ""))
        {
            button.gameObject.SetActive(true);

            button.interactable = true;
            if (buttonhard != null)
            {
                buttonhard.gameObject.SetActive(true);
                buttonhard.interactable = true;
            }

            lockobj.SetActive(false);
        }
    }
}
