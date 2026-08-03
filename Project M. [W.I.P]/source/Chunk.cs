using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using Unity.Collections;
using Unity.Netcode;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.Rendering;


public class Chunk : MonoBehaviour
{
    public int[][] buildingIndex = new int[12][];
    public GameObject[] Buildings;
    public bool SpawnChunk = false;
    public Sprite[] grassdecor;
    public Sprite[] grassdecor2;

    public GameObject[] ReplaceBuilding;

    public GameObject sr;
    public Transform Player;
    public Controller Controller;


    public Dictionary<ushort,GameObject> features = new Dictionary<ushort, GameObject>();
    ushort featureID = 0;
    public int chunkseed;
    public System.Random rand;

    public float DissapearTimer;
    public ChunkData MyData;
    public Vector3Int MyKey;
    public void Generate(bool load)
    {
        rand = new System.Random(chunkseed);
        Controller = Controller.controller;
        Player = Controller.ClientPlayer.transform;
        PlaceLayout();
        if(SpawnChunk == false)
        {
            Replace(0,3);
        }

        PlaceDeco();

        //buildingIndex[1][1] = 0;
        //stawianie widzialnych
        for (int i = 1; i < buildingIndex.Length - 1; i++)
        {
            for (int j = 1; j < buildingIndex[i].Length - 1; j++)
            {
                if(buildingIndex[i][j] == 0)
                {
                    continue;
                }

                int[] a = new int[4];
                //a[0] = lewo
                //a[1] = gora
                //a[2] = prawo
                //a[3] = dol
                a[0] = buildingIndex[i - 1][j];
                a[1] = buildingIndex[i][j + 1];
                a[2] = buildingIndex[i + 1][j];
                a[3] = buildingIndex[i][j - 1];
                
                if(rand.Next(0,25) == 0 && buildingIndex[i][j] == 3)
                {
                    GameObject obj1 = Instantiate(ReplaceBuilding[0], transform.position, Quaternion.identity);
                    obj1.transform.parent = transform;
                    obj1.transform.localPosition = new Vector3(-0.45f + (i -1) * 0.1f, 0.001f, -0.45f + (j-1) * 0.1f);
                    if (obj1.GetComponent<building>())
                    {
                        obj1.GetComponent<building>().Set(this, rand, a, chunkseed,load);
                    }
                    if (obj1.GetComponent<Shop>())
                    {
                        obj1.GetComponent<Shop>().SetInterior(i,j);
                    }
                    continue;
                }
                if (rand.Next(0, 25) == 0 && buildingIndex[i][j] == 3)
                {
                    GameObject obj1 = Instantiate(ReplaceBuilding[1], transform.position, Quaternion.identity);
                    obj1.transform.parent = transform;
                    obj1.transform.localPosition = new Vector3(-0.45f + (i - 1) * 0.1f, 0.001f, -0.45f + (j - 1) * 0.1f);
                    if (obj1.GetComponent<building>())
                    {
                        obj1.GetComponent<building>().Set(this, rand, a,chunkseed,load);
                    }
                    if (obj1.GetComponent<Shop>())
                    {
                        obj1.GetComponent<Shop>().SetInterior(i, j);
                    }
                    continue;
                }
                GameObject obj = Instantiate(Buildings[buildingIndex[i][j]], transform.position, Quaternion.identity);
                obj.transform.parent = transform;
                obj.transform.localPosition = new Vector3(-0.45f + (i - 1) * 0.1f, 0.001f, -0.45f + (j - 1) * 0.1f);
                if (obj.GetComponent<building>())
                {
                    obj.GetComponent<building>().Set(this, rand, a, chunkseed,load);
                }


            }
        }
        if(load == true)
        {
            LoadChunk();
            
            
        }

    }
    public void AddFeature(GameObject feat)
    {
        features.Add(featureID, feat);
        featureID++;
    }
    public void LoadChunk()
    {
        for (int i = 0; i < MyData.ObjectDelta.Count; i++)
        {
            Destroy(features[MyData.ObjectDelta[i]]);
        }

        for (int i = 0; i < MyData.saveableData.Count; i++)
        {
            SaveableData asset = MyData.saveableData[i];
            Debug.Log("sraka " + MyData.saveableData[i].Type);
            if (asset.Type == 1)
            {
                if(NetworkManager.Singleton.IsHost == false)
                {
                    continue;
                }
                Controller.SpawnItem((ushort)asset.ints[0], asset.ints[1], asset.ints[2], new Vector3(asset.PosX, asset.PosY, asset.PosZ));
            }
            else if (asset.Type == 2)
            {
                if (NetworkManager.Singleton.IsHost == false)
                {
                    continue;
                }
                Controller.SpawnMoney(asset.floats[0], new Vector3(asset.PosX, asset.PosY, asset.PosZ));
            }
            else if (asset.Type == 3)
            {
                if (NetworkManager.Singleton.IsHost == false)
                {
                    continue;
                }
                Controller.SpawnEnemy(asset.ints[0], new Vector3(asset.PosX, asset.PosY, asset.PosZ));
            }
            else if (asset.Type == 4)
            {

                GameObject obj = Instantiate(SpriteHolder.spriteholder.Blocks[asset.ints[0]], new Vector3(asset.PosX, asset.PosY, asset.PosZ), Quaternion.Euler(0, 90 * asset.ints[1], 0));
                obj.GetComponent<block>().rot = asset.ints[1];
            }
        }
        MyData.saveableData.Clear();
    }
    public void PlaceLayout()
    {
        //Setup
        for (int i = 0; i < buildingIndex.Length; i++)
        {
            buildingIndex[i] = new int[12];
        }
        for (int i = 0; i < buildingIndex.Length; i++)
        {
            buildingIndex[0][i] = -1;
            buildingIndex[buildingIndex.Length - 1][i] = -1;

            buildingIndex[i][0] = -1;
            buildingIndex[i][buildingIndex.Length - 1] = -1;
        }

        if (SpawnChunk == true)
        {
            for (int i = 1; i < buildingIndex.Length -1; i++)
            {
                buildingIndex[1][i] = 1;
                buildingIndex[i][1] = 1;
            }
            FillNextTo(1,2,true);
            for (int i = 1; i < buildingIndex.Length - 1; i++)
            {
                if (buildingIndex[10][i] == 0)
                {
                    buildingIndex[10][i] = 2;
                }
                if (buildingIndex[i][10] == 0)
                {
                    buildingIndex[i][10] = 2;
                }


            }
            return;
        }

        //X
        int r = rand.Next(0, 3);
        if(r == 0)
        {
            for (int i = 1; i < buildingIndex.Length - 1; i++)
            {
                buildingIndex[i][1] = 1;

            }

        }
        else if(r == 1)
        {
            int add = rand.Next(-1, 2);
            for (int i = 1; i < buildingIndex.Length - 1; i++)
            {
                buildingIndex[i][6 + add] = 1;

            }
        }
        else
        {
            int add = rand.Next(-1, 2);
            for (int i = 1; i < buildingIndex.Length - 1; i++)
            {
                buildingIndex[i][6 + add] = 1;
                buildingIndex[i][1] = 1;

            }

        }

        //Y
        r = rand.Next(0, 3);
        if (r == 0)
        {
            for (int i = 1; i < buildingIndex.Length - 1; i++)
            {
                buildingIndex[1][i] = 1;

            }

        }
        else if (r == 1)
        {
            int add = rand.Next(-1, 2);
            for (int i = 1; i < buildingIndex.Length - 1; i++)
            {
                buildingIndex[6 + add][i] = 1;

            }
        }
        else
        {
            int add = rand.Next(-1, 2);
            for (int i = 1; i < buildingIndex.Length - 1; i++)
            {
                buildingIndex[6 + add][i] = 1;
                buildingIndex[1][i] = 1;

            }

        }
        FillNextTo(1, 2, true);
        for (int i = 1; i < buildingIndex.Length - 1; i++)
        {
            if (buildingIndex[10][i] == 0)
            {
                buildingIndex[10][i] = 2;
            }
            if(buildingIndex[i][10] == 0)
            {
                buildingIndex[i][10] = 2;
            }


        }

    }

    public void FillNextTo(int SearchFor,int Place,bool ReplaceOnlyZero = true)
    {
        for (int i = 1; i < buildingIndex.Length -1; i++)
        {
            for (int j = 1; j < buildingIndex.Length - 1; j++)
            {
                if (buildingIndex[i][j] == 0 || ReplaceOnlyZero == false)
                {
                    if (buildingIndex[i][j+1]== SearchFor || buildingIndex[i][j - 1] == SearchFor || buildingIndex[i - 1][j] == SearchFor || buildingIndex[i + 1][j] == SearchFor)
                    {
                        buildingIndex[i][j] = Place;
                    }
                }
            }
        }
    }
    public void PlaceDeco()
    {
        //Decorations
        for (int i = 0; i < rand.Next(25, 50); i++)
        {
            GameObject deco = Instantiate(sr, transform.position + new Vector3((float)(rand.NextDouble() * 100 - 50), 0.4f, (float)(rand.NextDouble() * 100 - 50)), Quaternion.identity);
            deco.GetComponent<SpriteRenderer>().sprite = grassdecor[rand.Next(0, grassdecor.Length)];
            if (rand.Next(0, 2) == 0)
            {
                deco.GetComponent<SpriteRenderer>().flipX = true;
            }
            deco.transform.parent = transform;
            deco.isStatic = true;
        }
        for (int i = 0; i < rand.Next(5, 20); i++)
        {
            GameObject deco = Instantiate(sr, transform.position + new Vector3((float)(rand.NextDouble() * 100 - 50), 0.4f, (float)(rand.NextDouble() * 100 - 50)), Quaternion.identity);
            deco.GetComponent<SpriteRenderer>().sprite = grassdecor2[rand.Next(0, grassdecor2.Length)];
            if (rand.Next(0, 2) == 0)
            {
                deco.GetComponent<SpriteRenderer>().flipX = true;
            }
            deco.transform.parent = transform;
            deco.isStatic = true;
        }
    }
    public void Replace(int Target,int Replacement)
    {
        for (int i = 1; i < buildingIndex.Length-1; i++)
        {
            for (int j = 1; j < buildingIndex[i].Length-1; j++)
            {
                if (buildingIndex[i][j] == Target)
                {
                    buildingIndex[i][j] = Replacement;
                }
            }
        }

    }


     
    void Update()
    {
        DissapearTimer = DissapearTimer - Time.deltaTime;
        if(DissapearTimer <= 0)
        {
            Controller.controller.SaveableSort(MyKey);
            Generator.generator.Requested.Remove(MyKey);
            Destroy(gameObject);
        }
    }



}

