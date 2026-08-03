using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class SaveButtons : MonoBehaviour
{
    public string Name1;
    public string Path;
    public string info;
    public TextMeshProUGUI TextName;
    public TextMeshProUGUI Textinfo;
    void Start()
    {
        TextName.text = Name1;
        Textinfo.text = info;
    }

    public void Del()
    {
        File.Delete(Path);
        Destroy(gameObject);
    }
    public void Enter()
    {
        Data.SaveName = Name1;
        FindFirstObjectByType<MainMenu>().EditorEnterLoad();
    }
}
