using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO.Pipes;
using UnityEngine.SceneManagement;
using TMPro;
public class Settings : MonoBehaviour
{
    public string Path;
    public Toggle showfps;
    public Toggle autosave;
    public Slider volumeslider;
    public TMP_Dropdown dropdownlang;
    int dropdowntest;
    void Start()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/Main/");
        Path = Application.persistentDataPath + "/Main/settings.dupa";
        Load();
        dropdowntest = dropdownlang.value;
    }
    private void Update()
    {
        if(dropdownlang.value - dropdowntest != 0)
        {
            dropdowntest = dropdownlang.value;
            Langchange();
        }
    }

    // Update is called once per frame
    public void DeleteSettings()
    {
        File.Delete(Path);
        SceneManager.LoadScene(0);
    }
    public void Langchange()
    {
        Save();
        SceneManager.LoadScene(0);
    }
    public void Save()
    {

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        SettingsData settingsdata = new SettingsData();

        //

        settingsdata.showFPS= showfps.isOn;
        settingsdata.autosave= autosave.isOn;
        settingsdata.volume = volumeslider.value;
        settingsdata.Lang = dropdownlang.value;
        FileStream fileStream = File.Create(Path);
        binaryFormatter.Serialize(fileStream, settingsdata);
        fileStream.Close();
        SettingsData._showFPS = showfps.isOn;
        SettingsData._autosave= autosave.isOn;
        SettingsData._volume = volumeslider.value;
        AudioListener.volume = volumeslider.value;
        SettingsData._Lang = dropdownlang.value;
        Debug.Log(dropdownlang.value);
    }
    public void Load()
    {
        if (File.Exists(Path))
        {

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fileStream = File.Open(Path, FileMode.Open);

            SettingsData settingsdata = (SettingsData)formatter.Deserialize(fileStream);

            //
            showfps.isOn = settingsdata.showFPS;
            autosave.isOn = settingsdata.autosave;
            volumeslider.value = settingsdata.volume;
            dropdownlang.value = settingsdata.Lang;
            fileStream.Close();

            SettingsData._showFPS = showfps.isOn;
            SettingsData._autosave = autosave.isOn;
            SettingsData._volume = volumeslider.value;
            AudioListener.volume = volumeslider.value;
            SettingsData._Lang = dropdownlang.value;
        }
        else
        {
            Debug.Log(":c");
        }
       
    }
}
