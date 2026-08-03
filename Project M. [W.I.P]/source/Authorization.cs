using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;

public class Authorization : NetworkBehaviour
{
    public TMP_InputField Name;
    public TMP_InputField Pass;

    public static Dictionary<string,string> Passwords = new Dictionary<string, string>();
    public bool Closed;
    public Controller Controller;
    public GameObject AuthoScreen;
    public bool Started;
    public TextMeshProUGUI openstatustxt;
    void Start()
    {

        AuthoScreen.SetActive(true);
        if (Directory.Exists(MenuTransfer.path + "/" + MenuTransfer.WorldName + "/") && File.Exists(MenuTransfer.path + "/" + MenuTransfer.WorldName + "/" + "VulnerableData.json"))
        {
 
            AuthoSaving ass = new AuthoSaving();
            StreamReader sr = new StreamReader(MenuTransfer.path + "/" + MenuTransfer.WorldName + "/" + "VulnerableData.json");
            string read = sr.ReadToEnd();
            sr.Close();
            ass = JsonUtility.FromJson<AuthoSaving>(read);
            for (int i = 0; i < ass.Passes.Count; i++)
            {
                Passwords.Add(ass.NickName[i], ass.Passes[i]);
            }

        }

        if (MenuTransfer.JoinMode == 0)
        {
            NetworkManager.Singleton.StartHost();
        }
        if (MenuTransfer.JoinMode == 1)
        {
            NetworkManager.Singleton.StartClient();


        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Closed == false)
        {
            openstatustxt.text = "world status: open";
        }
        else
        {
            openstatustxt.text = "world status: closed";
        }
    }
    public void ToggleClose()
    {
        if(Closed == false)
        {
            Closed = true;
        }
        else
        {
            Closed = false;
        }
    }
    public void Join()
    {
        if (IsHost == true)
        {
            SendInfoServerRPC((FixedString32Bytes)Name.text, (FixedString32Bytes)Pass.text, NetworkManager.Singleton.LocalClientId);
            Started = true;
            return;
        }
        SendInfoServerRPC((FixedString32Bytes)Name.text, (FixedString32Bytes)Pass.text, NetworkManager.Singleton.LocalClientId);

    }
    public void Goback()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene(0);
    }
    [ServerRpc(RequireOwnership =false)]
    public void SendInfoServerRPC(FixedString32Bytes login,FixedString32Bytes password, ulong ID)
    {

        if(IsHost == false && Started == false)
        {
            return;
        }

        if (Passwords.ContainsKey(login.ToString()))
        {
            if (Passwords[login.ToString()] == password.ToString())
            {
                AuthorizeClientRPC(ID,MenuTransfer.WorldName, MenuTransfer.Seed,login);

            }
        }else if (Closed == false)
        {
            //Debug.Log(ID);
            Passwords.Add(login.ToString(), password.ToString());
            AuthorizeClientRPC(ID, MenuTransfer.WorldName, MenuTransfer.Seed, login);

        }

    }
    [ClientRpc]
    public void AuthorizeClientRPC(ulong ID, FixedString32Bytes name, FixedString32Bytes Seed,FixedString32Bytes nick)
    {

        if (ID == NetworkManager.Singleton.LocalClientId)
        {   if(IsHost ==false)
            {
                MenuTransfer.Seed = Seed.ToString();
                MenuTransfer.WorldName = name.ToString();

            }
            Controller.LoadLevel(MenuTransfer.WorldName);
            AuthoScreen.SetActive(false);
        }
        if(IsHost == true)
        {
            Controller.CreatePlayer(ID, nick);
        }
    }
}
