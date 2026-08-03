using UnityEngine;

public class SideWalk : building
{
    public GameObject[] parts;
    public FeatureSpawner[] Spawner;
    public MobSpawner[] MobSpawner;
    public ItemSpawner[] ItemSpawner;

    public override void Generate()
    {
        if (neighbors[0] == 1|| neighbors[0] == -1)
        {
            parts[0].SetActive(true);
            parts[3].SetActive(true);
        }
        if (neighbors[1] == 1|| neighbors[1] == -1)
        {
            parts[0].SetActive(true);
            parts[1].SetActive(true);
        }
        if (neighbors[2] == 1 || neighbors[2] == -1)
        {
            parts[1].SetActive(true);
            parts[2].SetActive(true);
        }
        if (neighbors[3] == 1 || neighbors[3] == -1)
        {
            parts[2].SetActive(true);
            parts[3].SetActive(true);
        }
        foreach (var item in Spawner)
        {
            item.Spawn(rand,Main);
        }
        if(Load == false)
        {
            for (int i = 0; i < ItemSpawner.Length; i++)
            {
                ItemSpawner[i].Spawn(InternalSeed + ((int)(transform.position.x * transform.position.z) + (int)transform.position.x + (int)transform.position.z + i) * 2137);
            }
            for (int i = 0; i < MobSpawner.Length; i++)
            {
                MobSpawner[i].Spawn(InternalSeed + ((int)(transform.position.x * transform.position.z) + (int)transform.position.x + (int)transform.position.z + i) * 6969);
            }
        }


    }
}
