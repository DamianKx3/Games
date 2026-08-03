using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlacer : MonoBehaviour
{
    public List<GameObject> levels;
    public int leveltospawn;
    public bool DoWork;
    public bool DevSpawn;
    bool lock1;
    void Start()
    {
        //Debug.Log(gameObject.name);
        if(DevSpawn == false)
        {
            leveltospawn = Data.LvlPlace;
        }

        if(DoWork == true)
        {
            Instantiate(levels[leveltospawn], transform.position, Quaternion.identity);
        }


    }


    void Update()
    {
        
    }
   
}
