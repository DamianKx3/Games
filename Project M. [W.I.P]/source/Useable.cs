using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Useable : MonoBehaviour
{
    public int Type;

    public float maxcooldown;
    public float Damage;
    public float cooldown;
    public Controller Controller;
    byte angle = 0;
    public int PlaceID;
    public int AutoID;
    public PlayerController User;
    public float HealValue;
    public float AlkoValue;
    public int GiveID;
    void Start()
    {
        Controller = Controller.controller;
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0)
        {
            cooldown = cooldown - Time.deltaTime;
        }
        if(Type == 1)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                angle++;
                if (angle > 3)
                {
                    angle = 0;
                }
            }
        }
    }
    public void Used(float X, float Y, float Z)
    {
        if(Type == 0)
        {
            Punch(X,Y,Z);
        }
        else if(Type == 1)
        {
            int x = Mathf.RoundToInt(X / 4) * 4;
            int y = Mathf.RoundToInt(Y / 4) * 4;
            int z = Mathf.RoundToInt(Z / 4) * 4;
            if (Controller.controller.BlockRegister.ContainsKey((new Vector3Int(x, y, z), SpriteHolder.spriteholder.Blocks[PlaceID].GetComponent<block>().Type))==false)
            {
                PlaceObject(x, y, z, angle);
                User.RemoveItem(AutoID, 1);
            }

        }else if (Type == 2)
        {
            int dur = User.LowerDurability(AutoID, 1);
            HealPlayer(HealValue);
            Alko(AlkoValue);
            if(dur <= 0)
            {
                User.RemoveItem(AutoID,1);
                if(GiveID != 0)
                {
                    User.AddItem(GiveID, 1, SpriteHolder.spriteholder.DeafultDurability[GiveID]);
                }
            }
        }

       
    }
    public void Punch(float X, float Y, float Z)
    {
        //Debug.Log("a");
        if (cooldown <= 0)
        {
            cooldown = maxcooldown;
            Collider[] hits = Physics.OverlapSphere(new Vector3(X, Y, Z), 2);
            Debug.Log("Useable Punch " + X + " " + Y + " " + Z);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].GetComponent<Enemy>())
                {
                    hits[i].GetComponent<Enemy>().Damage(Damage);
                    return;
                }
                if (hits[i].GetComponent<PlayerController>())
                {
                    hits[i].GetComponent<PlayerController>().Damage(Damage);
                    return;


                }
            }

           


        }
    }
    public void PlaceObject(int X,int Y, int Z, byte angle)
    {
        //GameObject block = Instantiate(PlaceAble, new Vector3(Mathf.Round(X / 4)*4, Mathf.Round(Y/4) * 4, Mathf.Round(Z/4)*4),Quaternion.Euler(0,90 * angle,0));
        //block.GetComponent<block>().rot = angle;
        Controller.controller.PlaceBlockClientRPC(X, Y, Z, angle, (ushort)PlaceID);


    }
    public void HealPlayer(float val) {

        User.hp = User.hp + val;
    }
    public void Alko(float val)
    {

        User.alko = User.alko + val;
    }

}
