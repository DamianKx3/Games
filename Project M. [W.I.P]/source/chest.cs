using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public int[] ItemIDs;
    public int[] ItemWeight;
    public int Tries;
    public bool AlreadyOpened = false;
    public List<int> FinalList = new List<int>();
    public bool Triggered;
    public List<GameObject> players = new List<GameObject>();
    public Controller Controller;
    public System.Random rand;
    void Start()
    {
        Controller = Controller.controller;
        rand = new System.Random();
        for (int i = 0; i < ItemIDs.Length; i++)
        {
            for (int j = 0; j < ItemWeight[i]; j++)
            {
                FinalList.Add(ItemIDs[i]);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().chest = this;
            players.Add(collision.gameObject);
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().chest = null;
            players.Remove(collision.gameObject);
        }
    }
    public void Open()
    {
        Debug.Log("open");
        if(AlreadyOpened == true)
        {
            return;
        }
        AlreadyOpened = true;
        for (int i = 0; i < Tries; i++)
        {
            int a = rand.Next(0, FinalList.Count);
            if (FinalList[a] >= 0)
            {
                if (FinalList[a] == 0)
                {
                    continue;
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
