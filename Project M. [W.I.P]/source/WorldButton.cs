using System.IO;
using TMPro;
using UnityEngine;

public class WorldButton : MonoBehaviour
{
    public TextMeshProUGUI Nametxt;
    public string Name;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Nametxt.text = Name;
    }
    public void Load()
    {
        FindFirstObjectByType<MainMenu>().HostLoad(Name);
    }
    public void Delete()
    {
        Directory.Delete(MenuTransfer.path + "/" + Name,true);
        Destroy(gameObject);
    }
}
