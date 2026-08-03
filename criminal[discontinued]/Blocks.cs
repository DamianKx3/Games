using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public float durability;
    //public bool grounded;
    //public bool isBedrock;
    public int ID;
    public int dir;
    public bool isBg;
    public bool CantleaveBlood;
    void Start()
    {
        if (durability == 0) durability = 10;
        //if(isBedrock==true)grounded= true;

        if(Data.Editor == false)
        {
            if(ID != 0 && ID != 1 && ID != 2 && ID != 3)
            {
                gameObject.AddComponent<BlockGravity>();
                GetComponent<BlockGravity>().Dir = dir;
                GetComponent<BlockGravity>().isbg = isBg;
            }

            

        }
    }
}
