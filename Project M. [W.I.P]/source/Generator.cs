using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.AI.Navigation;
using Unity.Collections;
using Unity.Netcode;
using UnityEditor.AI;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class Generator : MonoBehaviour
{
    public int ChunkSize;
    public int RenderDistance;
    public GameObject Chunk;
    public GameObject Player;
    public GameObject SpawnChunk;
    public Controller Controller;
    public Dictionary<Vector3Int,GameObject> ChunkObjs = new Dictionary<Vector3Int, GameObject>();
    public Dictionary<Vector3Int,ChunkData> ChunkDatas = new Dictionary<Vector3Int, ChunkData>();


    public List<Vector3Int> Requested = new List<Vector3Int>();

    public List<Vector3Int> torem;
    public int Seed;
    public bool Started;
    public NavMeshSurface navmesh;
    float UpdateNav = 2;
    public TextMeshProUGUI debug;
    public static Generator generator;
    void Start()
    {
        
    }
    private void Awake()
    {
        generator = this;
    }
    public void StartWork()
    {
  
        
        Started = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(Started == false)
        {
            return;
        }
        if(Player == null)
        {
            return;
        }

        if(NetworkManager.Singleton.IsHost == true)
        {


            int CenterX = Mathf.RoundToInt(Controller.ClientPlayer.transform.position.x / ChunkSize);
            int CenterZ = Mathf.RoundToInt(Controller.ClientPlayer.transform.position.z / ChunkSize);
            debug.text = "chunkpos: " + CenterX + "  " + CenterZ + "\nplayer: " + Controller.ClientPlayer.transform.position.x + " " + Controller.ClientPlayer.transform.position.z + "\n" + (Controller.ClientPlayer.transform.position.x - CenterX * ChunkSize) + "\n" + (Controller.ClientPlayer.transform.position.z - CenterZ * ChunkSize);
            LoadClientChunk(new Vector3Int(Mathf.RoundToInt(CenterX), 0, Mathf.RoundToInt(CenterZ)));
            int addx = 0;
            int addz = 0;
            if (Controller.ClientPlayer.transform.position.x - CenterX * ChunkSize > 10)
            {
                LoadClientChunk(new Vector3Int(CenterX + 1, 0, CenterZ));
                addx = 1;
            }
            else if (Controller.ClientPlayer.transform.position.x - CenterX * ChunkSize < -10)
            {
                LoadClientChunk(new Vector3Int(CenterX - 1, 0, CenterZ));
                addx = -1;
            }

            if (Controller.ClientPlayer.transform.position.z - CenterZ * ChunkSize > 10)
            {
                LoadClientChunk(new Vector3Int(CenterX, 0, CenterZ + 1));
                addz = 1;
            }
            else if (Controller.ClientPlayer.transform.position.z - CenterZ * ChunkSize < -10)
            {
                LoadClientChunk(new Vector3Int(CenterX, 0, CenterZ - 1));
                addz = -1;
            }
            if (addx != 0 && addz != 0)
            {
                LoadClientChunk(new Vector3Int(CenterX + addx, 0, CenterZ + addz));
            }




        }
        else
        {
            int CenterX = Mathf.RoundToInt(Controller.ClientPlayer.transform.position.x / ChunkSize);
            int CenterZ = Mathf.RoundToInt(Controller.ClientPlayer.transform.position.z / ChunkSize);
            if(CheckRequestedChunk(new Vector3Int(CenterX,0,CenterZ)) == false)
            {
                Controller.ChunkRequestDataServerRPC(NetworkManager.Singleton.LocalClientId, CenterX, CenterZ);
            }

            int addx = 0;
            int addz = 0;
            if (Controller.ClientPlayer.transform.position.x - CenterX * ChunkSize > 10)
            {
                if (CheckRequestedChunk(new Vector3Int(CenterX+1, 0, CenterZ)) == false)
                {
                    Controller.ChunkRequestDataServerRPC(NetworkManager.Singleton.LocalClientId, CenterX + 1, CenterZ);

                }
                addx = 1;
            }
            else if (Controller.ClientPlayer.transform.position.x - CenterX * ChunkSize < -10)
            {
                if (CheckRequestedChunk(new Vector3Int(CenterX - 1, 0, CenterZ)) == false)
                {
                    Controller.ChunkRequestDataServerRPC(NetworkManager.Singleton.LocalClientId, CenterX - 1, CenterZ);
                }
                addx = -1;
            }

            if (Controller.ClientPlayer.transform.position.z - CenterZ * ChunkSize > 10)
            {
                if (CheckRequestedChunk(new Vector3Int(CenterX, 0, CenterZ +1)) == false)
                {
                    Controller.ChunkRequestDataServerRPC(NetworkManager.Singleton.LocalClientId, CenterX, CenterZ + 1);
                }
                addz = 1;
            }
            else if (Controller.ClientPlayer.transform.position.z - CenterZ * ChunkSize < -10)
            {
                if (CheckRequestedChunk(new Vector3Int(CenterX, 0, CenterZ - 1)) == false)
                {
                    Controller.ChunkRequestDataServerRPC(NetworkManager.Singleton.LocalClientId, CenterX, CenterZ - 1);
                }
                addz = -1;
            }
            if (addx != 0 && addz != 0)
            {
                if (CheckRequestedChunk(new Vector3Int(CenterX+addx, 0, CenterZ +addz)) == false)
                {
                    Controller.ChunkRequestDataServerRPC(NetworkManager.Singleton.LocalClientId, CenterX + addx, CenterZ +addz);
                }
            }
        }
        List<Vector3Int> KeysToDelete = new List<Vector3Int>();
        foreach (var item in ChunkObjs)
        {
            if(item.Value == null)
            {
                KeysToDelete.Add(item.Key);
            }
        }
        for (int i = 0; i < KeysToDelete.Count; i++)
        {
            ChunkObjs.Remove(KeysToDelete[i]);
        }
        KeysToDelete.Clear();



        if(UpdateNav > 0)
        {
            if (UpdateNav - Time.deltaTime < 0)
            {
                navmesh.UpdateNavMesh(navmesh.navMeshData);
            }
            UpdateNav = UpdateNav - Time.deltaTime;

        }
        
        
       
    }
    public void LoadClientChunk(Vector3Int pos)
    {
        if (ChunkDatas.ContainsKey(pos) == true)
        {
            if (ChunkObjs.ContainsKey(pos) == true)
            {
                Chunk ch = ChunkObjs[pos].GetComponent<Chunk>();
                ch.DissapearTimer = 5;


            }
            else
            {
                GameObject chunk;
                if (pos != new Vector3Int(0, 0, 0))
                {
                    chunk = Instantiate(Chunk, pos * ChunkSize, Quaternion.identity);
                }
                else
                {
                    chunk = Instantiate(SpawnChunk, pos * ChunkSize, Quaternion.identity);
                }
                chunk.transform.parent = transform;
                ChunkObjs.Add(pos, chunk);

                int hasz = Seed ^ (pos.x + pos.z) * 213769;
                hasz = hasz ^ (pos.x * 21376913);
                hasz = hasz ^ (pos.z * 69692137);
                Chunk tmpchunk = chunk.GetComponent<Chunk>();
                tmpchunk.MyData = ChunkDatas[pos];
                Debug.Log("dupa " + ChunkDatas[pos].saveableData.Count);
                tmpchunk.MyKey = pos;
                tmpchunk.chunkseed = hasz;
                tmpchunk.DissapearTimer = 5;
                tmpchunk.Generate(true);     
                UpdateNav = 2;

            }
        }
        else
        {
            GameObject chunk;
            if (pos != new Vector3Int(0, 0, 0))
            {
                chunk = Instantiate(Chunk, pos * ChunkSize, Quaternion.identity);
            }
            else
            {
                chunk = Instantiate(SpawnChunk, pos * ChunkSize, Quaternion.identity);
            }
            ChunkData a = new ChunkData();
            ChunkDatas.Add(pos, a);
            chunk.transform.parent = transform;
            ChunkObjs.Add(pos, chunk);
            int hasz = Seed ^ (pos.x + pos.z) * 213769;
            hasz = hasz ^ (pos.x * 21376913);
            hasz = hasz ^ (pos.z * 69692137);
            Chunk tmpchunk = chunk.GetComponent<Chunk>();
            tmpchunk.MyData = a;
            tmpchunk.MyKey= pos;
            tmpchunk.chunkseed = hasz;
            tmpchunk.DissapearTimer = 5;
            tmpchunk.Generate(false);
            UpdateNav = 2;
        }
        
        


    }
    
    public bool CheckRequestedChunk(Vector3Int key)
    {
        if (Requested.Contains(key))
        {
            if (ChunkObjs.ContainsKey(key))
            {
                Chunk ch = ChunkObjs[key].GetComponent<Chunk>();
                ch.DissapearTimer = 5;

            }


            return true;
        }
        else
        {
            Requested.Add(key);
            return false;
        }

    }

}


