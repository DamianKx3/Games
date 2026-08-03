using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization;
using System.IO.Pipes;

public class SaveSystem : MonoBehaviour
{
    public string Path;

    public string SaveName;
    public List<float> X;
    public List<float> Y;
    public List<float> Z;
    public List<int> BlockID;
    public List<int> Dir;
    public List<string> Bcolors;
    public float moneyonstart;
    public int weather;
    public List<int> layer;
    public TMP_InputField moneyInput;
    public TMP_Dropdown weatherDropdown;
    public TextAsset[] Lvls;
    public List<int> Forbitten;
    public Toggle[] ForbittenToggle;

    public float timeLeft;
    public TMP_InputField timeInput;
    public List<int> listemp;
    public float[] basepos = {0,0};
    public List<string> colorpresets;
    [Header("KREATOR POZIOMOW OFICJALNYCH")]
    public bool OfficialCreate; // zmienia rozszerzenie z .dupa na .txt 
    void Start()
    {
        if(basepos.Length == 0)
        {
            basepos[0] = 0;
            basepos[1] = 0;
        }
        Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        SaveName = Data.SaveName;
        
        Path = Application.persistentDataPath + "/saves/" + SaveName + ".dupa";

        if (OfficialCreate == true)
        {
            Path = Application.persistentDataPath + "/saves/" + SaveName + ".txt";
        }

        if (Data.Load == true)
        {
            Load();
        }
        else
        {
            if(Data.Editor == false)
            {
                OfficialLvlLoad();
            }

        }
        if(Data.Editor == true)
        {
            if(SettingsData._autosave == true)
            {
                StartCoroutine(autosave());
            }
        }

    }
    private void Update()
    {

        if (Data.Editor == false)
        {
            return;
        }
        if (moneyInput != null && moneyInput.text != "")
        {
            moneyonstart = float.Parse(moneyInput.text);
        }
        else
        {
            moneyonstart = 0;
        }
        if (timeInput != null && timeInput.text != "")
        {
            timeLeft = float.Parse(timeInput.text);
        }
        else
        {
            timeLeft = 0;
        }
        if(weatherDropdown != null)
        {
            weather = weatherDropdown.value;
        }


    }
    public void GetInfo()
    {
        X.Clear();
        Y.Clear();
        Z.Clear();
        BlockID.Clear();
        layer.Clear();
        Dir.Clear();
        Bcolors.Clear();
        colorpresets = FindFirstObjectByType<PrefabCreator>().colorpresets;
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            X.Add(block.transform.position.x);
            Y.Add(block.transform.position.y);
            Z.Add(block.transform.position.z);
            BlockID.Add(block.GetComponent<Blocks>().ID);
            layer.Add(block.GetComponentInChildren<SpriteRenderer>().sortingOrder);
            Dir.Add(block.GetComponent<Blocks>().dir);
            if (block.GetComponentInChildren<SpriteRenderer>().sortingOrder > -1)
            {
                Bcolors.Add(block.GetComponentInChildren<SpriteRenderer>().color.r + "." + block.GetComponentInChildren<SpriteRenderer>().color.g + "." + block.GetComponentInChildren<SpriteRenderer>().color.b);
            }
            else
            {
                Bcolors.Add((block.GetComponentInChildren<SpriteRenderer>().color.r + 0.4f) + "." + (block.GetComponentInChildren<SpriteRenderer>().color.g + 0.4f) + "." + (block.GetComponentInChildren<SpriteRenderer>().color.b + 0.4f));
            }
            
        }

    }


    public void Save()
    {
        GetInfo();
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        Data data = new Data();
        data.X = X;
        data.Y = Y;
        data.Z = Z;
        data.BlockID = BlockID;
        data.moneyonstart = moneyonstart;
        data.weather = weather;
        data.layer = layer;
        data.BlockDir = Dir;
        data.Bcolors = Bcolors;
        data.Spawnpoint = basepos;
        data.colorpresets = colorpresets;
        Forbitten.Clear();
        for (int i = 0; i < ForbittenToggle.Length; i++)
        {
            if (ForbittenToggle[i].isOn == true)
            {
                Forbitten.Add(i);
            }

        }
        data.Forbitten = Forbitten;
        data.timeLeft = timeLeft;
        FileStream fileStream = File.Create(Path);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }
    public void Load()
    {
        if (File.Exists(Path))
        {

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fileStream = File.Open(Path, FileMode.Open);

            Data data = (Data)formatter.Deserialize(fileStream);
            X = data.X;
            Y = data.Y;
            Z = data.Z;
            BlockID = data.BlockID;
            Forbitten = data.Forbitten;
            if (moneyInput != null)
            {
                moneyInput.text = data.moneyonstart.ToString();
                weatherDropdown.value = data.weather;
                timeInput.text = data.timeLeft.ToString();
                SetAllToggles();
            }
            moneyonstart = data.moneyonstart;
            weather = data.weather;
            timeLeft = data.timeLeft;

            layer = data.layer;
            basepos = data.Spawnpoint;
            colorpresets = data.colorpresets;
            if(data.BlockDir != null)
            {
                Dir = data.BlockDir;
            }
            if (data.Bcolors != null)
            {
                Bcolors = data.Bcolors;
            }
            FindFirstObjectByType<PrefabCreator>().LvlLoad();
            fileStream.Close();
            if(Data.Editor == true)
            {
                FindFirstObjectByType<PrefabCreator>().basepos.transform.position = new Vector3(basepos[0], basepos[1], 0);
                FindFirstObjectByType<PrefabCreator>().colorpresets = colorpresets;
            }
        }
        else
        {
            Debug.LogError("File not found: " + Path);
        }
        if (FindFirstObjectByType<LevelStats>() != null)
        {
            LevelStats ls = FindFirstObjectByType<LevelStats>();
            ls.MoneyOnStart = moneyonstart;
            ls.weather = weather;
            ls.TimeLeft = timeLeft;
            ls.Forbitten = Forbitten;
            ls.Spawn.position = new Vector2(basepos[0], basepos[1]);
            ls.Work();
        }
    }
    public void OfficialLvlLoad()
    {
        //Path = "levels/a";
        //Debug.Log(Path);
        //TextAsset textAsset = (TextAsset)Resources.Load(Path);
        Stream stream = new MemoryStream(Lvls[Data.LvlPlace].bytes);
        BinaryFormatter formatter = new BinaryFormatter();
        Data data = (Data)formatter.Deserialize(stream);
        X = data.X;
        Y = data.Y;
        Z = data.Z;
        BlockID = data.BlockID;
        if (moneyInput != null)
        {
            moneyInput.text = data.moneyonstart.ToString();
            weatherDropdown.value = data.weather;
        }
        moneyonstart = data.moneyonstart;
        weather = data.weather;
        timeLeft = data.timeLeft;
        Forbitten = data.Forbitten;
        layer = data.layer;
        basepos = data.Spawnpoint;
        colorpresets = data.colorpresets;
        if(data.Bcolors != null)
        {
            Bcolors = data.Bcolors;
        }

        if (data.BlockDir != null)
        {
            Dir = data.BlockDir;
        }
        FindFirstObjectByType<PrefabCreator>().LvlLoad();
        stream.Close();
        if (FindFirstObjectByType<LevelStats>() != null)
        {
            LevelStats ls = FindFirstObjectByType<LevelStats>();
            ls.MoneyOnStart = moneyonstart;
            ls.weather = weather;
            ls.TimeLeft = timeLeft;
            ls.Forbitten = Forbitten;
            if(basepos != null)
            {
                ls.Spawn.position = new Vector2(basepos[0], basepos[1]);
                FindFirstObjectByType<PrefabCreator>().colorpresets = colorpresets;
            }

            ls.Work();
        }
    }
    public void SetAllToggles()
    {
        for (int i = 0; i < ForbittenToggle.Length; i++)
        {

            if (Forbitten.Contains(i))
            {

                ForbittenToggle[i].isOn = true;
            }

        }
    }

    public void SaveLevelComplated()
    {
        if (Data.Editor == false)
        {
            LoadLevelComplated();
            Directory.CreateDirectory(Application.persistentDataPath + "/Main/");
            Path = Application.persistentDataPath + "/Main/camp.dupa";




            BinaryFormatter binaryFormatter = new BinaryFormatter();
            DataCamp data = new DataCamp();
            data.LvlComplated = listemp;
            FileStream fileStream = File.Create(Path);
            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
        }
    }
    public void LoadLevelComplated()
    {
        Path = Application.persistentDataPath + "/Main/camp.dupa";
        if (File.Exists(Path))
        {

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fileStream = File.Open(Path, FileMode.Open);

            DataCamp data = (DataCamp)formatter.Deserialize(fileStream);
           listemp = data.LvlComplated;
            
            fileStream.Close();
            if (listemp.Contains(Data.LvlPlace) == false)
            {
                listemp.Add(Data.LvlPlace);
            }
        }
        else
        {
            return;
        }

    }
    public IEnumerator autosave()
    {
        yield return new WaitForSecondsRealtime(300);
        Save();
    }
}