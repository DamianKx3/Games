using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class building : MonoBehaviour
{
    public int[] neighbors = new int[4];
    public System.Random rand;
    public Chunk Main;
    public int InternalSeed;
    public bool Load = false;
    public building()
    {

    }
    void Start()
    {

    
    }
    public void Set(Chunk chunk, System.Random rand, int[] neighbors,int seed,bool load)
    {

        this.neighbors = neighbors;
        Main = chunk;
        this.rand = rand;
        InternalSeed = seed;
        Load = load;
        Generate();

    }
    public virtual void Generate()
    {
        //Generacja
    }
    
    void Update()
    {
        
    }
}
