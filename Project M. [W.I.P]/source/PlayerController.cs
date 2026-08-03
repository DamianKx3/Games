using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    [Header("ownersend")]
    public NetworkVariable<ushort> itemHeld = new NetworkVariable<ushort>(0,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [Header("recieve")]
    public float speed;
    public float money;
    public float hp;
    public float armor;
    public float alko;
    public string nick;
    public List<inventoryslot> inventory = new List<inventoryslot>();
    public List<GameObject> slots1 = new List<GameObject>();
    public NetworkVariable<ushort> Dimension = new NetworkVariable<ushort>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public int SelectedNow;
    [Header("links")]
    public Controller Controller;
    public Animator animator;
    public Rigidbody rb;
    public SpriteHolder sh;
    [Header("UI")]
    public GameObject slot;
    public Transform hotbar;
    public Transform armorbar;
    public Transform eq;
    public Slider hpslider;
    public Slider alkoslider;
    public TextMeshProUGUI moneytxt;
    public slot swap1;
    public slot swap2;
    public TextMeshPro Nicktxt;
    [Header("other")]
    public GameObject[] bodyparts;
    public GameObject MainHand;
    public GameObject item;
    Vector3 lastpos;
    [Header("useableHand")]
    public Useable Main;
    public GameObject Weapon;
    float punchcooldown;
    [Header("useable")]
    public Portal portal;
    public chest chest;
    [Header("kastomizacja")]
     public GameObject[] hair;
    public int SelectedHair;
      public GameObject[] beard;
    public int SelectedBeard;
    public Color[] haircol;
    public int SelectedhairCol;
    public SpriteRenderer[] pants;
    public Color[] pantscol;
    public int SelectedpantsCol;
    public SpriteRenderer[] shirt;
    public int SelectedshirtCol;
    public Color[] shirtcol;
    public SpriteRenderer[] skin;
    public int SelectedskinCol;
    public Color[] skincol;
    
    public void Start1()
    {

        sh = SpriteHolder.spriteholder;
        RefreshLook();

        for (int i = 0; i < 10; i++)
        {
            inventoryslot slot = new inventoryslot();
            slot.ID = 0;
            slot.Durability = 0;
            slot.Count = 0;
            inventory.Add(slot);
        }
        Controller = Controller.controller;
        if (IsOwner == true)
        {

            RefreshEQ();
            Controller.ClientPlayer = gameObject;
            Controller.OnCreate(gameObject);
        }
    }
    private void Start()
    {
        if(IsHost == false)
        {
            Start1();
        }
        transform.eulerAngles = new Vector3(45, 0, transform.eulerAngles.z);
    }
    public void RefreshEQ()
    {
        foreach (var slot in slots1)
        {
            Destroy(slot);
        }
        slots1.Clear();
        for (int i = 0; i < 5; i++)
        {
            GameObject slot1 = Instantiate(slot, hotbar);
            slot1.GetComponent<slot>().Index = i;
            slots1.Add(slot1);
        }
        for (int i = 5; i < inventory.Count; i++)
        {
            GameObject slot1 = Instantiate(slot, eq);
            slot1.GetComponent<slot>().Index = i;
            slots1.Add(slot1);
        }
    }
    public void RefreshLook()
    {
        for(int i = 0;  i < hair.Length; i++)
        {
            hair[i].gameObject.SetActive(false);
        }
        hair[SelectedHair].SetActive(true);
        for (int i = 0; i < beard.Length; i++)
        {
            beard[i].gameObject.SetActive(false);
        }
        beard[SelectedBeard].SetActive(true);
        hair[SelectedHair].GetComponent<SpriteRenderer>().color = haircol[SelectedhairCol];
        beard[SelectedBeard].GetComponent<SpriteRenderer>().color = haircol[SelectedhairCol];
        for (int i = 0; i < pants.Length; i++)
        {
            pants[i].color = pantscol[SelectedpantsCol];

        }
        for (int i = 0; i < shirt.Length; i++)
        {
            shirt[i].color = shirtcol[SelectedshirtCol];
        }
        for (int i = 0; i < skin.Length; i++)
        {
            skin[i].color = skincol[SelectedskinCol];
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (IsHost == true)
        {
            if(hp <= 0)
            {
                Die();
            }
        }

        if (Dimension.Value == 0)
        {
            //gameObject.layer = 0;
        }
        else if (Dimension.Value == 1)
        {
            //gameObject.layer = 6;
        }
        for (int i = 0; i < bodyparts.Length; i++)
        {
            if (Dimension.Value == 0)
            {
                //bodyparts[i].layer = 0;
            }
            else if (Dimension.Value == 1)
            {
                //bodyparts[i].layer = 6;
            }

        }
        if (transform.position.x > lastpos.x)
        {
            transform.localScale = new Vector3(1,1,1);
            Nicktxt.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }
        if (transform.position.x < lastpos.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Nicktxt.gameObject.GetComponent<RectTransform>().localScale = new Vector3(-0.15f, 0.15f, 0.15f);
        }
        Nicktxt.text = nick;
        Nicktxt.gameObject.transform.eulerAngles = new Vector3(45, 0, 0);
        
        item.GetComponent<SpriteRenderer>().sprite = sh.sprites[itemHeld.Value];
        if (sh.IdsUse.Contains(itemHeld.Value))
        {
            Main.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (Weapon == null)
            {
                Weapon = Instantiate(sh.CustomUse[Array.IndexOf(sh.IdsUse, itemHeld.Value)], Main.transform.position, MainHand.transform.rotation);
                Weapon.transform.parent = Main.transform;
                Weapon.GetComponent<Useable>().User = this;
            }
            if (Weapon.GetComponent<Useable>().AutoID != sh.CustomUse[Array.IndexOf(sh.IdsUse, itemHeld.Value)].GetComponent<Useable>().AutoID)
            {
                Destroy(Weapon.gameObject);
                Weapon = Instantiate(sh.CustomUse[Array.IndexOf(sh.IdsUse, itemHeld.Value)], Main.transform.position, MainHand.transform.rotation);
                Weapon.transform.parent = Main.transform;
                Weapon.GetComponent<Useable>().User = this;
            }

        }
        else
        {
            Destroy(Weapon);
            Main.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (IsOwner == false)
        {

            return;
        }

        itemHeld.Value = (ushort)inventory[slots1[SelectedNow].GetComponent<slot>().Link].ID;

        if (punchcooldown > 0)
        {
            punchcooldown = punchcooldown - Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0) && punchcooldown <= 0)
        {
            punchcooldown = 0.1f;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(transform.position, hit.point) < 10)
                {
                    PunchServerRPC(hit.point.x, hit.point.y, hit.point.z);

                }
            }

        }
        if (Input.mouseScrollDelta.y > 0)
        {
            SelectedNow--;
            if(SelectedNow < 0)
            {
                SelectedNow = 4;
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            SelectedNow++;
            if (SelectedNow > 4)
            {
                SelectedNow = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropServerRPC(slots1[SelectedNow].GetComponent<slot>().Link,NetworkObjectId);
        }
       
        moneytxt.text = Mathf.Round(money * 100) / 100 + "zl";
        hpslider.value = hp / 100;
        alkoslider.value = alko / 100;
        if(swap2 != null)
        {
            (swap1.Link, swap2.Link) = (swap2.Link, swap1.Link);
            swap1 = null;
            swap2 = null;

        }
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Count <= 0)
            {
                inventory[i].ID = 0;
                inventory[i].Count = 0;
                inventory[i].Durability = 1;
            }

        }

    }

    private void FixedUpdate()
    {
        if(IsOwner == true)
        {
            rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical")) * speed;
        }
        if (Mathf.Abs(lastpos.x - transform.position.x) > 0 || Mathf.Abs(lastpos.z - transform.position.z) > 0)
        {
            animator.SetInteger("State", 1);
        }
        else
        {

            animator.SetInteger("State", 0);
        }
        lastpos = transform.position;
    }
    public void Die()
    {
        money = 0;
        alko = 0;
        transform.position = new Vector3(0,1, 0);
        hp = 100;
    }
    public void RemoveItem(int ID, int count)
    {
        int index = SearchIndexRemove(ID);
        for (int i = 0; i < count; i++)
        {

            if (index == -1)
            {
                Debug.Log(ID + "   " + count);
                break;
            }
            if (inventory[index].Count > 0)
            {
                inventory[index].Count--;
                inventory[index].Durability = SpriteHolder.spriteholder.DeafultDurability[ID];
  
            }
            else
            {
                index = SearchIndexRemove(ID);
                i--;
            }
        }
        if (IsHost == true)
        {
            List<ushort> id1 = new List<ushort>();
            List<byte> c = new List<byte>();
            List<ushort> dur = new List<ushort>();
            for (int i = 0; i < inventory.Count; i++)
            {
                inventoryslot slot = inventory[i];
                id1.Add((ushort)slot.ID);
                c.Add((byte)slot.Count);
                dur.Add((ushort)slot.Durability);

            }
            Controller.SendEqClientRpc(GetComponent<NetworkObject>().OwnerClientId, id1.ToArray(), c.ToArray(), dur.ToArray());
        }

    }
    public int LowerDurability(int ID,int val)
    { 
        int Index = SearchIndex(ID);
        inventory[Index].Durability = inventory[Index].Durability - val;

        if (IsHost == true)
        {
            List<ushort> id1 = new List<ushort>();
            List<byte> c = new List<byte>();
            List<ushort> dur = new List<ushort>();
            for (int i = 0; i < inventory.Count; i++)
            {
                inventoryslot slot = inventory[i];
                id1.Add((ushort)slot.ID);
                c.Add((byte)slot.Count);
                dur.Add((ushort)slot.Durability);

            }
            Controller.SendEqClientRpc(GetComponent<NetworkObject>().OwnerClientId, id1.ToArray(), c.ToArray(), dur.ToArray());
        }
        return inventory[Index].Durability;
    }
    
    public int AddItem(int ID, int count, int durability)
    {

        int a = 0;
        int index = SearchIndex(ID);
        bool durTemp = false;
        for (int i = 0; i < count; i++)
        {
            if (index == -1)
            {
                break;
            }
            if (inventory[index].Count < 20)
            {
                if(durability == SpriteHolder.spriteholder.DeafultDurability[ID] || durTemp == true)
                {
                    inventory[index].ID = ID;
                    inventory[index].Count++;
                    inventory[index].Durability = durability;
                }
                else
                {
                    if (inventory[index].Durability + durability > SpriteHolder.spriteholder.DeafultDurability[ID])
                    {
                        inventory[index].Count++;
                        inventory[index].Durability = inventory[index].Durability = durability - SpriteHolder.spriteholder.DeafultDurability[ID];
                    }
                    else
                    {
                        inventory[index].Durability = inventory[index].Durability + durability;

                    }
                    durTemp = true;
                    continue;
                }
                



            }
            else
            {
                index = SearchIndex(ID);
                i--;
            }
            a = i+1;
        }
        if (a != 0 && IsHost == true)
        {
            List<ushort> id1 = new List<ushort>();
            List<byte> c = new List<byte>();
            List<ushort> dur = new List<ushort>();
            for (int i = 0; i < inventory.Count; i++)
            {
                inventoryslot slot = inventory[i];
                id1.Add((ushort)slot.ID);
                c.Add((byte)slot.Count);
                dur.Add((ushort)slot.Durability);

            }
            Controller.SendEqClientRpc(GetComponent<NetworkObject>().OwnerClientId,id1.ToArray(),c.ToArray(), dur.ToArray());
        }
        return count - a;

    }
    int SearchIndex(int ID)
    {
        int Index = -1;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ID == 0 && Index == -1)
            {
                Index = i;
            }
            if(inventory[i].ID == ID && inventory[i].Count < 20)
            {
                Index = i;
            }
        }
        return Index;
    }
    int SearchIndexRemove(int ID)
    {
        int Index = -1;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ID == ID)
            {
                Index = i;
                break;
            }
        }

        return Index;
    }
    public bool CheckAvaibility(string recipe)
    {
        List<int> IDlist= new List<int>();
        List<int> Countlist = new List<int>();
        for (int i = 0; i < inventory.Count; i++)
        {
            if (!IDlist.Contains(inventory[i].ID))
            {
                IDlist.Add(inventory[i].ID);
                Countlist.Add(inventory[i].Count);
            }
            else
            {
                for (int j = 0; j < Countlist.Count; j++)
                {
                    if (IDlist[j] == inventory[i].ID)
                    {
                        Countlist[j] = Countlist[j] + inventory[i].Count;
                        break;
                    }
                }
            }
        }
        string[] a = recipe.Split('#');
        bool craftable = true;
        for (int i = 0; i < a.Length / 2; i++)
        {
            if(IDlist.Contains(int.Parse(a[2 * i])))
            {
                if (Countlist[IDlist.IndexOf(int.Parse( a[2 * i]))] >= int.Parse(a[2 * i + 1]))
                {

                }
                else
                {
                    craftable = false;
                }
            }
            else
            {
                craftable = false;
            }

        }
        return craftable;
    }

    [ServerRpc(RequireOwnership =false)]
    public void PunchServerRPC(float X,float Y,float Z)
    {
        Debug.Log("punchserverrpc " + X + " " + Y + " " + Z);
        foreach (var item in inventory)
        {
            if (item.ID == itemHeld.Value)
            {
                if (sh.IdsUse.Contains(itemHeld.Value))
                {

                    Weapon.GetComponent<Useable>().Used(X,Y,Z);

                }
                else
                {
                    Main.Used(X, Y, Z);
                }
                return;
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void UseServerRPC(ulong ID)
    {
        Debug.Log("use");
        if(chest != null)
        {
           chest.Open();
        }
        else if(portal != null)
        {
            portal.Used(ID);

        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void DropServerRPC(int Index, ulong ID)
    {


        if (inventory[Index].Count > 0)
        {
            RemoveItem((ushort)inventory[Index].ID,1);
            Controller.SpawnItem((ushort)inventory[Index].ID, 1,inventory[Index].Durability, transform.position + new Vector3(2, 0, 0));
        }

    }
    public void Damage(float Dmg)
    {
        hp = hp - Dmg * (1 - (armor / 100));
        Controller.SendStatsClientRPC(GetComponent<NetworkObject>().OwnerClientId, hp,alko,money);
    }
    [ClientRpc]
    public void SendNickClientRPC(FixedString32Bytes nick)
    {
        this.nick = nick.ToString();
    }
    public void PickUpMoney(float Money)
    {
        money = money + Money;
        Controller.SendStatsClientRPC(GetComponent<NetworkObject>().OwnerClientId, hp, alko, money);
    }
}
public class inventoryslot
{
    public int ID;
    public int Count;
    public int Durability;
    public bool InUse;
}
