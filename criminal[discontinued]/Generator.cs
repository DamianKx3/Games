using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject grass;
    float distance;
    public float width;
    public int chunkscount;
    public float GenY;
    void Start()
    {
        distance = 0;
        GenY = Random.Range(0f, 1f);
        for (int i = 0; i < chunkscount; i++)
        {
            Nextchunk();
        }

    }

   
    void Update()
    {
        
    }
    public void Nextchunk()
    {

        int mode = Random.Range(0,3);
        for (int i = 0; i < width; i++)
        {
            if(Random.Range(1,10) >= 3)
            {
                mode = Random.Range(0, 3);
            }
            if (GenY < 0)
            {
                mode = 0;
            }
            if(GenY > 1 )
            {
                mode = 1;
            }
            switch (mode)
            {
                default:
                    break;
                    case 0:
                    GenY = GenY + Random.Range(0.05f,0.2f);
                    break;
                    case 1:
                    GenY = GenY - Random.Range(0.05f, 0.2f);
                    break;
                    case 2:
                    GenY = GenY + Random.Range(-0.2f, 0.2f);
                    break;
            }
            Instantiate(grass,new Vector3(distance + 0.25f * i,GenY,0), Quaternion.identity);
        }
        distance = distance + width/4;
    }
}
