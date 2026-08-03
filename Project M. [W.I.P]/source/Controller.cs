using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.Netcode;

using UnityEngine;

using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;
using static UnityEngine.Rendering.DebugUI.Table;
public class Controller : NetworkBehaviour
{
    [Header("server")]
    public float time;
    public List<GameObject> Players;
    public Dictionary<FixedString32Bytes, PlayerSaving> PlayersData = new Dictionary<FixedString32Bytes, PlayerSaving>();
    public GameObject ClientPlayer;
    public GameObject ItemPrefab;
    public GameObject MoneyPrefab;
    [Header("links")]
    public Generator Generator;
    public CameraController CameraController;
    public static Controller controller;
    public SpriteHolder SpriteHolder;
    [Header("UI")]
    public GameObject GameCanvas;
    public GameObject PreCanvas;
    public TMP_InputField SaveNameIF;
    public TMP_InputField SeedIF;
    public TextMeshProUGUI PlayerList;
    public TextMeshProUGUI Chat;
    public List<string> ChatHistory;
    public TMP_InputField typemessage;
    [Header("Console")]
    public GameObject Console;
    public TMP_InputField Consoletext;
    public TextMeshProUGUI ConsoleOutput;
    [Header("Player UI")]
    public Slider AlkoSlider;
    public Slider HpSlider;
    public Transform hotbar;
    public Transform eq;
    public Transform armor;
    public Transform crafting;
    public Transform craftingholder;
    public GameObject craftingSlot;
    public TextMeshProUGUI moneytxt;
    public TextMeshProUGUI Namesign;
    public GameObject buttonsw;
    
    [Header("other")]
    public Light globalLight;
    public Light2D globalLight2D;
    public GameObject PlayerPrefab;
    Color day = new Color(1, 1, 1, 1);
    Color Night = new Color(0.3f, 0.3f, 1, 1);
    Color afternoon = new Color(1, 0.8f, 0.8f, 1);
    public static float GraffitiDarkness;
    public List<Saveable> saveableholder = new List<Saveable>();
    public List<SaveableData> saveableData = new List<SaveableData>();
    public Dictionary<(Vector3Int,byte),GameObject> BlockRegister = new Dictionary<(Vector3Int, byte), GameObject>();
    private void Awake()
    {
        controller = this;
    }
    void Start()
    {

        SpriteHolder = SpriteHolder.spriteholder;
        GameCanvas.SetActive(false);
        PreCanvas.SetActive(true);
        for (int i = 0; i < SpriteHolder.Ingridients.Length; i++)
        {
            GameObject slot = Instantiate(craftingSlot, craftingholder);
            slot.GetComponent<craftingslot>().CraftingID = i;
        }
        eq.gameObject.SetActive(false);
        crafting.gameObject.SetActive(false);
    }
    
    public void LoadLevel(string Name)
    {
        if(NetworkManager.IsHost == true)
        {
            if (File.Exists(MenuTransfer.path + "/" + Name + "/" + "save.json"))
            {

                Wrapper w = new Wrapper();
                StreamReader sr = new StreamReader(MenuTransfer.path + "/" + Name + "/" + "save.json");
                string read = sr.ReadToEnd();
                sr.Close();
                w = JsonUtility.FromJson<Wrapper>(read);
                Generator.Seed = w.seed;
                time = w.time;
                MenuTransfer.Seed = w.seed.ToString();
                MenuTransfer.WorldName = w.Name;

                for (int i = 0; i < w.Chunks.Count; i++)
                {
 
                    ChunkData d = w.Chunks[i];
                    Vector3Int k = w.ChunksKeys[i];
                    Generator.ChunkDatas.Add(k, d);



                }
                for (int i = 0; i < w.Players.Count; i++)
                {

                    PlayerSaving ps = w.Players[i];
                    PlayersData.Add(ps.Nick, ps);
                }
            }
            else
            {
                time = 1.5f;
                if (string.IsNullOrEmpty(MenuTransfer.Seed) == false)
                {
                    Generator.Seed = int.Parse(MenuTransfer.Seed);
                }
                else
                {
                    Generator.Seed = new System.Random().Next();
                    MenuTransfer.Seed = Generator.Seed.ToString();
                }

            }
        }
        else
        {
            Generator.Seed = int.Parse(MenuTransfer.Seed);
            Debug.Log(Generator.Seed);
        }
        

    }
    public void ss()
    {
        Save(MenuTransfer.WorldName,int.Parse(MenuTransfer.Seed));
    }
    public void Save(string Name,int seed)
    {
        Wrapper w = new Wrapper();
        w.seed = seed;
        w.Name = Name;
        w.time = time;
  
        for (int i = 0; i < Players.Count; i++)
        {
            PlayerController pc = Players[i].GetComponent<PlayerController>();
            PlayerSaving ps = new PlayerSaving();
            ps.alko = pc.alko;
            ps.hp = pc.hp;
            ps.Money = pc.money;
            ps.Nick = pc.nick;
            for (int j = 0; j < pc.inventory.Count; j++)
            {
                inventoryslot s = pc.inventory[j];
                ps.itemId.Add((ushort)s.ID);
                ps.itemcount.Add((byte)s.Count);
                ps.itemdurability.Add((ushort)s.Durability);
            }
            w.Players.Add(ps);
        }
        foreach (KeyValuePair<Vector3Int,ChunkData> item in Generator.ChunkDatas)
        {
            SaveableSort(item.Key,true);
            w.ChunksKeys.Add(item.Key);
            w.Chunks.Add(item.Value);
            Debug.Log(item.Key + " " + item.Value.saveableData.Count);
        }
        string json = JsonUtility.ToJson(w);
        Directory.CreateDirectory(MenuTransfer.path + "/" + Name);
        StreamWriter sw = new StreamWriter(MenuTransfer.path + "/" + Name + "/save.json");
        sw.Write(json);
        sw.Close();
        Debug.Log(w.Chunks.Count);
        AuthoSaving ass = new AuthoSaving();
        foreach (var item in Authorization.Passwords)
        {
            ass.NickName.Add(item.Key.ToString());
            ass.Passes.Add(item.Value.ToString());
        }
        json = JsonUtility.ToJson(ass);
        sw = new StreamWriter(MenuTransfer.path + "/" + Name + "/VulnerableData.json");
        sw.Write(json);
        sw.Close();

    }
    // Update is called once per frame
    void Update()
    {

        Namesign.text = MenuTransfer.WorldName;
        time = time + Time.deltaTime * 0.01f;
        if (Mathf.Sin(time) > 0.1f)
        {
            globalLight.intensity = Mathf.Sin(time);
            GraffitiDarkness = Mathf.Sin(time);
        }
        else
        {
            globalLight.intensity = 0.1f;
            GraffitiDarkness = 0.1f;
        }
        if (Mathf.Sin(time) > 0f && Mathf.Sin(time) < 0.2f)
        {
            globalLight.color = Color.Lerp(globalLight.color,afternoon,Time.deltaTime);
        } else if (Mathf.Sin(time) > 0.2f)
        {
            globalLight.color = Color.Lerp(globalLight.color, day, Time.deltaTime);
        }
        else
        {
            globalLight.color = Color.Lerp(globalLight.color, Night, Time.deltaTime);
        }
        globalLight2D.intensity = globalLight.intensity;
        globalLight2D.color = globalLight.color;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (eq.gameObject.activeSelf == true)
            {
                eq.gameObject.SetActive(false);
            }
            else
            {
                if(ClientPlayer.GetComponent<PlayerController>().portal != null || ClientPlayer.GetComponent<PlayerController>().chest != null)
                {
                    ClientPlayer.GetComponent<PlayerController>().UseServerRPC(NetworkManager.LocalClientId);
                }
                else
                {
                    eq.gameObject.SetActive(true);
                    if (crafting.gameObject.activeSelf == true)
                    {
                        crafting.gameObject.SetActive(false);
                    }

                }

            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (crafting.gameObject.activeSelf == true)
            {
                crafting.gameObject.SetActive(false);

            }
            else
            {
                crafting.gameObject.SetActive(true);
                if(eq.gameObject.activeSelf == true)
                {
                    eq.gameObject.SetActive(false);
                }

            }
        }
        if (eq.gameObject.activeSelf == true || crafting.gameObject.activeSelf == true)
        {
            buttonsw.SetActive(true);
        }
        else
        {
            buttonsw.SetActive(false);
        }
        Chat.text = "";
        if(ChatHistory.Count > 10)
        {
            for (int i = ChatHistory.Count - 10; i < ChatHistory.Count; i++)
            {

                Chat.text = Chat.text + ChatHistory[i] + "\n";
            }
        }
        else
        {
            for (int i = 0; i < ChatHistory.Count; i++)
            {

                Chat.text = Chat.text + ChatHistory[i] + "\n";
            }
        }
        if(NetworkManager.Singleton.IsHost == true)
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                if(Console.activeSelf == false)
                {
                    Console.SetActive(true);
                }
                else
                {
                    Console.SetActive(false);
                }
            }
        }


    }
    public void ConsoleEnter()
    {
        if (NetworkManager.Singleton.IsHost == true)
        {
            string[] split = Consoletext.text.Split(' ');
            if (split[0] == "spawn")
            {
                
                try
                {
                    if (split[1] == "item")
                    {
                        SpawnItem(ushort.Parse(split[2]), int.Parse(split[3]), int.Parse(split[4]), new Vector3(float.Parse(split[5]), float.Parse(split[6]), float.Parse(split[7])));


                    }else if (split[1] == "money")
                    {
                        SpawnMoney(float.Parse(split[2]), new Vector3(float.Parse(split[3]), float.Parse(split[4]), float.Parse(split[5])));

                    }else if (split[1] == "enemy")
                    {
                        SpawnEnemy(int.Parse(split[2]), new Vector3(float.Parse(split[3]), float.Parse(split[4]), float.Parse(split[5])));
                    }
                }
                catch
                {
                    ConsoleOutput.text = "spawn item id count durability x y z/ spawn money value x y z";
                }
            }
            else if (split[0] == "tp")
            {
                try
                {

                }
                catch
                {

                }
            }else if (split[0] == "stats")
            {

            }
            else
            {
                ConsoleOutput.text = "commands: spawn, tp, stats";
            }

        }
    }
    public void SaveableSort(Vector3Int ChunkKey,bool ForceSave = false)
    {
        if(ForceSave == true)
        {
            if(Generator.ChunkObjs.ContainsKey(ChunkKey) == false)
            {
                return;
            }
        }
        //czyszczenie obiektow ktore nie istnieja
        List<Saveable> ToDelete = new List<Saveable>();
        for (int i = 0; i < saveableholder.Count; i++)
        {
            if (saveableholder[i] == null)
            {
                ToDelete.Add(saveableholder[i]);
            }
        }
        for (int i = 0; i < ToDelete.Count; i++)
        {
            saveableholder.Remove(ToDelete[i]);
        }
        ToDelete.Clear();
        Generator.ChunkDatas[ChunkKey].saveableData.Clear();
        for (int i = 0; i < saveableholder.Count; i++)
        {
            saveableholder[i].Save();
            SaveableData Data = saveableholder[i].SaveableData;
            Vector3Int key = new Vector3Int(Mathf.RoundToInt(Data.PosX/ Generator.ChunkSize), 0, Mathf.RoundToInt(Data.PosZ / Generator.ChunkSize));
            if (key == ChunkKey)
            {
                Generator.ChunkDatas[key].saveableData.Add(Data);
                if(ForceSave == false)
                {
                    if (NetworkManager.Singleton.IsHost == true || saveableholder[i].GetComponent<NetworkObject>() == false)
                    {
                        Destroy(saveableholder[i].gameObject);
                    }

                }

            }

        }

    }
    public void SwichEq()
    {
        if (crafting.gameObject.activeSelf == true)
        {
            crafting.gameObject.SetActive(false);
            eq.gameObject.SetActive(true);

        }
        else
        {
            crafting.gameObject.SetActive(true);
            eq.gameObject.SetActive(false);

        }
    }
    public void OnCreate(GameObject Player)
    {
        PlayerController p = Player.GetComponent<PlayerController>();
        p.alkoslider = AlkoSlider;
        p.hpslider = HpSlider;
        p.moneytxt = moneytxt;
        p.hotbar = hotbar;
        p.eq = eq;
        p.armorbar = armor;
        p.RefreshEQ();
        PreCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        Generator.Player = Player;
        CameraController.player = Player.transform;
        Generator.StartWork();
    }
    public void CreatePlayer(ulong ID,FixedString32Bytes nick)
    {
        GameObject Player = Instantiate(PlayerPrefab,new Vector3(0,1,0),Quaternion.identity);
        Player.GetComponent<NetworkObject>().Spawn();
        Player.GetComponent<NetworkObject>().ChangeOwnership(ID);
        Player.GetComponent<PlayerController>().nick = nick.ToString();
        Player.GetComponent<PlayerController>().Start1();
        Players.Add(Player);
        SendWorldInfoClientRpc(time);
        UpdatePlayerList();
        if (PlayersData.ContainsKey(nick))
        {
            PlayerSaving ps = PlayersData[nick];
            SendStatsClientRPC(ID, ps.hp, ps.alko, ps.Money);
            SendEqClientRpc(ID, ps.itemId.ToArray(), ps.itemcount.ToArray(), ps.itemdurability.ToArray());
        }
        for (int i = 0; i < Players.Count; i++)
        {
            PlayerController p = Players[i].GetComponent<PlayerController>();
            p.SendNickClientRPC(p.nick);
        }



    }
    public void UpdatePlayerList()
    {
        PlayerList.text = "";
        for (int i = 0; i < Players.Count; i++)
        {
            PlayerList.text = PlayerList.text + i + ". "+Players[i].GetComponent<PlayerController>().nick + "\n";
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void CraftServerRPC(ulong ID,ushort CraftingID)
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].GetComponent<NetworkObject>().OwnerClientId == ID)
            {
                PlayerController playerController = Players[i].GetComponent<PlayerController>();
                if (playerController.CheckAvaibility(SpriteHolder.Ingridients[CraftingID]))
                {
                    for (int j = 0; j < SpriteHolder.Ingridients[CraftingID].Split('#').Length / 2; j++)
                    {
                        playerController.RemoveItem(ushort.Parse(SpriteHolder.Ingridients[CraftingID].Split('#')[j * 2]), byte.Parse(SpriteHolder.Ingridients[CraftingID].Split('#')[j * 2 + 1]));

                    }
                    for (int j = 0; j < SpriteHolder.result[CraftingID].Split('#').Length / 2; j++)
                    {
                        playerController.AddItem(ushort.Parse(SpriteHolder.result[CraftingID].Split('#')[j * 2]), byte.Parse(SpriteHolder.result[CraftingID].Split('#')[j * 2 + 1]), SpriteHolder.DeafultDurability[ushort.Parse(SpriteHolder.result[CraftingID].Split('#')[j * 2])]);
                    }
                }
                break;
            }
        }
    }
    [ClientRpc]
    public void SendEqClientRpc(ulong ID, ushort[] items, byte[] count,ushort[] durability)
    {
        if (ID == NetworkManager.Singleton.LocalClientId)
        {
            PlayerController p = ClientPlayer.GetComponent<PlayerController>();
            for (int i = 0; i < items.Length; i++)
            {
                inventoryslot s = p.inventory[i];
                s.ID = items[i];
                s.Count = count[i];
                s.Durability = durability[i];

            }
        }

        
    }
    [ServerRpc]
    public void BuyItemServerRpc(ulong ID, ushort additem,byte count, float value)
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].GetComponent<NetworkObject>().OwnerClientId == ID)
            {
                PlayerController playerController = Players[i].GetComponent<PlayerController>();
                if(playerController.money >= value)
                {
                    playerController.money = playerController.money - value;
                    playerController.AddItem(additem, count, SpriteHolder.spriteholder.DeafultDurability[additem]);
                }
                break;
            }
        }
    }
    [ClientRpc]
    public void SendStatsClientRPC(ulong ID, float hp, float alko, float money)
    {
        if (ID == NetworkManager.Singleton.LocalClientId)
        {
            PlayerController p = ClientPlayer.GetComponent<PlayerController>();
            p.hp = hp;
            p.alko = alko;
            p.money = money;

        }

    }
    [ClientRpc]
    public void SendWorldInfoClientRpc(float time)
    {
        if(NetworkManager.IsHost == false)
        {
            this.time = time;
        }

    }
    [ServerRpc(RequireOwnership =false)]
    public void SendChatServerRPC(FixedString32Bytes nick, FixedString32Bytes text)
    {
        SendChatClientRPC(nick, text);
    }
    
    [ClientRpc]
    public void SendChatClientRPC(FixedString32Bytes nick, FixedString32Bytes text)
    {
        ChatHistory.Add(nick+": "+text);
    }
    public void SendChatButton()
    {
        if (string.IsNullOrEmpty(typemessage.text))
        {
            return;
        }
        SendChatServerRPC(ClientPlayer.GetComponent<PlayerController>().nick, typemessage.text);
        typemessage.text = "";
    }
    public void SpawnItem(ushort ID,int count,int durability,Vector3 pos,int Dimension = 0)
    {
        
        GameObject item = Instantiate(ItemPrefab, pos, Quaternion.identity);
        item.GetComponent<item>().ID.Value = ID;
        item.GetComponent<item>().count = count;
        item.GetComponent<item>().durability = durability;
        item.GetComponent<NetworkObject>().Spawn();

    }
    public void SpawnMoney(float value, Vector3 pos, int Dimension = 0)
    {

        GameObject item = Instantiate(MoneyPrefab, pos, Quaternion.identity);
        item.GetComponent<Money>().Value.Value = value;
        item.GetComponent<NetworkObject>().Spawn();

    }
    public void SpawnEnemy(int ID, Vector3 pos,int Dimension = 0)
    {

        GameObject Enemy = Instantiate(SpriteHolder.spriteholder.Enemies[ID], pos, Quaternion.identity);
        //Enemy.transform.parent = transform.parent;
        Enemy.GetComponent<NetworkObject>().Spawn();
    }

    [ClientRpc]
    public void SendChunkDataClientRPC(int x, int z, float[] blockX, float[] blockY, float[] BlockZ, ushort[] BlockID, byte[] rot, ushort[] ChunkDelta, ClientRpcParams rpcParams = default)
    {

        if (NetworkManager.Singleton.IsHost == false)
        {
            Vector3Int key = new Vector3Int(x, 0, z);
            ChunkData data = new ChunkData();
            data.ObjectDelta = ChunkDelta.ToList<ushort>();
            for (int i = 0; i < BlockID.Length; i++)
            {
                SaveableData s = new SaveableData();
                s.PosX = blockX[i];
                s.PosY = blockY[i];
                s.PosZ = BlockZ[i];
                s.ints = new int[2];
                s.ints[0] = BlockID[i];
                s.ints[1] = rot[i];
                s.Type = 4;
                data.saveableData.Add(s);
            }
            Debug.Log("kurwa " + BlockID.Length);
            Generator.ChunkDatas[key] = data;
            Generator.LoadClientChunk(key);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void ChunkRequestDataServerRPC(ulong PlayerID, int x, int z)
    {
        ClientRpcParams rpc = new ClientRpcParams();
        rpc.Send = new ClientRpcSendParams();
        rpc.Send.TargetClientIds = new ulong[] { PlayerID };
        Generator.LoadClientChunk(new Vector3Int(x, 0, z));
        SaveableSort(new Vector3Int(x,0,z), true);
        ChunkData chunk = Generator.ChunkDatas[new Vector3Int(x, 0, z)];
        List<float> bX = new List<float>();
        List<float> bY = new List<float>();
        List<float> bZ = new List<float>();
        List<ushort> bID = new List<ushort>();
        List<byte> rot = new List<byte>();

        for (int i = 0; i < chunk.saveableData.Count; i++)
        {
            if (chunk.saveableData[i].Type == 4)
            {
                bX.Add(chunk.saveableData[i].PosX);
                bY.Add(chunk.saveableData[i].PosY);
                bZ.Add(chunk.saveableData[i].PosZ);
                bID.Add((ushort)chunk.saveableData[i].ints[0]);
                rot.Add((byte)chunk.saveableData[i].ints[1]);
            }

        }

        SendChunkDataClientRPC(x, z, bX.ToArray(), bY.ToArray(), bZ.ToArray(), bID.ToArray(), rot.ToArray(), chunk.ObjectDelta.ToArray(), rpc);
    }
    [ClientRpc]
    public void PlaceBlockClientRPC(int X,int Y,int Z,byte rot,ushort BlockID)
    {
        if (Generator.ChunkDatas.ContainsKey(new Vector3Int(X / Generator.ChunkSize,0,Z / Generator.ChunkSize)))
        {
            GameObject block = Instantiate(SpriteHolder.spriteholder.Blocks[BlockID], new Vector3(X, Y, Z), Quaternion.Euler(0, 90 * rot, 0));
            block.GetComponent<block>().rot = rot;
        }

    }
    [ClientRpc]
    public void DestroyBlockClientRPC(int X, int Y,int Z, byte Type)
    {
        if (Generator.ChunkDatas.ContainsKey(new Vector3Int(X / Generator.ChunkSize, 0, Z / Generator.ChunkSize)))
        {
            if(BlockRegister.ContainsKey((new Vector3Int(X, Y, Z),Type)))
            {
                Destroy(BlockRegister[(new Vector3Int(X, Y, Z), Type)]);
            }
        }


    }

}



[System.Serializable]
class Wrapper
{
    public int seed;
    public string Name;
    public float time;
    public List<ChunkData> Chunks = new List<ChunkData>();
    public List<Vector3Int> ChunksKeys = new List<Vector3Int>();
    public List<PlayerSaving> Players = new List<PlayerSaving>();
}
[System.Serializable]
public class ChunkData
{
    //ToSend
    public List<ushort> ObjectDelta = new List<ushort>();

    public List<SaveableData> saveableData = new List<SaveableData>();




    //tosave(host)

}
[System.Serializable]
public class PlayerSaving
{
    public List<ushort> itemId = new List<ushort>();
    public List<byte> itemcount = new List<byte>();
    public List<ushort> itemdurability = new List<ushort>();
    public float Money;
    public float hp;
    public float alko;
    public string Nick;
}
[System.Serializable]
public class AuthoSaving
{
    public List<string> NickName = new List<string>();
    public List<string> Passes = new List<string>();

}