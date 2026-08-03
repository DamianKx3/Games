using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBlocks : MonoBehaviour
{
    public GameObject visible;
    public GameObject Structure;
    public bool Entity;
    public bool parachuteEnemy;
    public GameObject[] EntityPhases;
    void Awake()
    {
        if(parachuteEnemy == false)
        {
            Spawn();
        }
        else
        {
            if (Data.Editor == false)
            {
                visible.SetActive(false);
            }

        }

        
        
        
    }
    public void Spawn()
    {
        if (Entity == false)
        {

            if (Data.Editor == false)
            {
                visible.SetActive(false);
                GameObject obj = Instantiate(Structure, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (Data.Editor == false)
            {
                visible.SetActive(false);
                GameObject obj = Instantiate(EntityPhases[Random.Range(0, EntityPhases.Length)], transform.position, Quaternion.identity);
                if (parachuteEnemy == true)
                {
                    obj.GetComponent<Enemy>().Parachute = true;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
