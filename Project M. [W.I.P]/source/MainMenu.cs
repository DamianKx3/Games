using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField ipfield;
    public TMP_InputField seedfield;
    public TMP_InputField namefield;
    public GameObject WorldSlot;
    public Transform holder;
    void Start()
    {
        Directory.CreateDirectory(MenuTransfer.path);
        DirectoryInfo di = new DirectoryInfo(MenuTransfer.path);
        DirectoryInfo[] dir = di.GetDirectories();

        for (int i = 0; i < dir.Length; i++)
        {
            GameObject wb = Instantiate(WorldSlot,holder);
            wb.GetComponent<WorldButton>().Name = dir[i].Name;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Client()
    {
        MenuTransfer.JoinMode = 1;
        MenuTransfer.Ip = ipfield.text;
        SceneManager.LoadScene(1);
    }
    public void Host()
    {
        MenuTransfer.JoinMode = 0;
        MenuTransfer.Ip = "127.0.0.1";
        MenuTransfer.Seed = seedfield.text;
        MenuTransfer.WorldName = namefield.text;
        SceneManager.LoadScene(1);
    }
    public void HostLoad(string Name)
    {
        MenuTransfer.JoinMode = 0;
        MenuTransfer.Ip = "127.0.0.1";
        MenuTransfer.WorldName = Name;
        MenuTransfer.Seed = "";
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit(); 
    }
}
public class MenuTransfer
{
    public static string path = Application.persistentDataPath + "/saves";
    public static string Seed;
    public static string WorldName;
    public static int JoinMode = -1;
    public static string Ip;
}
