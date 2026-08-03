using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public int Chance;
    public int[] ItemIDs;
    public int[] ItemWeight;
    public List<int> FinalList = new List<int>();
    public Controller Controller;
    void Start()
    {
        if (NetworkManager.Singleton.IsHost == false)
        {
            Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn(int InternalSeed)
    {
        if (NetworkManager.Singleton.IsHost == false)
        {
            return;
        }
        System.Random rand = new System.Random(InternalSeed);
        Controller = Controller.controller;
        for (int i = 0; i < ItemIDs.Length; i++)
        {
            for (int j = 0; j < ItemWeight[i]; j++)
            {
                FinalList.Add(ItemIDs[i]);
            }

        }
        if (rand.Next(0, Chance) == 0)
        {
            int a = rand.Next(0, FinalList.Count);
            if (FinalList[a] >= 0)
            {
                if(FinalList[a] == 0)
                {
                    return;
                }
                Controller.SpawnItem((ushort)FinalList[a], 1, 1, transform.position + new Vector3(rand.Next(-5, 5), 1, rand.Next(-5, 5)));
            }
            else
            {
                Controller.SpawnMoney((float)-FinalList[a] / 100, transform.position + new Vector3(rand.Next(-5, 5), 1, rand.Next(-5, 5)));
            }


            

        }

    }
}
