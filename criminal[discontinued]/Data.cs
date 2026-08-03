using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public static int LvlPlace;

    public static bool Editor;
    public static bool Load;
    public static string SaveName;
    public static bool backtoeditor;

    public List<float> X;
    public List<float> Y;
    public List<float> Z;
    public List<int> BlockID;
    public float moneyonstart;
    public int weather;
    public List<int> layer;
    public List<int> BlockDir;
    public List<int> Forbitten;
    public float timeLeft;
    public float[] Spawnpoint = new float[2];
    public List<string> Bcolors;
    public List<string> colorpresets;
    public float rotation;

}
