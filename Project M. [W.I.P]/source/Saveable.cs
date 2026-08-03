using System.Collections.Generic;
using UnityEngine;


public class Saveable : MonoBehaviour
{
    public SaveableData SaveableData = new SaveableData();
    Controller controller = Controller.controller;
    public void Start()
    {
        if (controller.saveableholder.Contains(this) == false)
        {
            controller.saveableholder.Add(this);
        }
    }
    public void Save()
    {
        SaveableData.PosX = transform.position.x;
        SaveableData.PosY = transform.position.y;
        SaveableData.PosZ = transform.position.z;
        if (gameObject.GetComponent<item>())
        {
            SaveableData.Type = 1;
            SaveableData.ints = new int[3];
            SaveableData.ints[0] = gameObject.GetComponent<item>().ID.Value;
            SaveableData.ints[1] = gameObject.GetComponent<item>().count;
            SaveableData.ints[2] = gameObject.GetComponent<item>().durability;
        }else if (gameObject.GetComponent<Money>())
        {
            SaveableData.Type = 2;
            SaveableData.floats = new float[1];
            SaveableData.floats[0] = gameObject.GetComponent<Money>().Value.Value;
        }else if (gameObject.GetComponent<Enemy>())
        {
            SaveableData.Type = 3;
            SaveableData.floats = new float[1];
            SaveableData.floats[0] = gameObject.GetComponent<Enemy>().hp;

        }
        else if (gameObject.GetComponent<block>())
        {
            SaveableData.Type = 4;
            SaveableData.floats = new float[1];
            SaveableData.floats[0] = gameObject.GetComponent<block>().hp;
            SaveableData.ints = new int[2];
            SaveableData.ints[0] = gameObject.GetComponent<block>().ID;
            SaveableData.ints[1] = gameObject.GetComponent<block>().rot;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class SaveableData
{
    public int Type;
    public float PosX;
    public float PosY;
    public float PosZ;
    //additional data
    public float[] floats;
    public int[] ints;
    public string[] strings;
}
