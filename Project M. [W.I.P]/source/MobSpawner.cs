using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class MobSpawner : MonoBehaviour
{
    public int Difficulty;
    public List<GameObject> AvaibleEnemies = new List<GameObject>();
    public int Chance;
    void Start()
    {
        if(NetworkManager.Singleton.IsHost == false)
        {
            Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().enabled = false;
        


    }

    // Update is called once per frame
    void Update()
    {
         
    }
    public void Spawn(int InternalSeed)
    {
        if (NetworkManager.Singleton.IsHost == false)
        {
            return;
        }
        Difficulty = (int)(Vector3.Distance(transform.position, new Vector3(0, 0, 0)) / 100);
        System.Random rand = new System.Random(InternalSeed);
        for (int i = 0; i < SpriteHolder.spriteholder.Enemies.Length; i++)
        {
            if (SpriteHolder.spriteholder.Difficulties[i] <= Difficulty)
            {
                AvaibleEnemies.Add(SpriteHolder.spriteholder.Enemies[i]);
            }
        }
        if (rand.Next(0,Chance) == 0)
        {
            if(AvaibleEnemies.Count < 1)
            {
                return;
            }

            int EnemyIndex = rand.Next(0, AvaibleEnemies.Count);
            Vector3 pos = transform.position + new Vector3(Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2), 0, Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2));
            Controller.controller.SpawnEnemy(EnemyIndex,pos);
        }

    }
}
