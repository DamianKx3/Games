using UnityEngine;

public class FeatureSpawner : MonoBehaviour
{
    public GameObject[] Features;
    public int Chance;
    public void Spawn(System.Random rand,Chunk chunk)
    {
        if(rand.Next(0,Chance) == 0)
        {
           GameObject feature = Instantiate(Features[rand.Next(0,Features.Length)]);
            feature.transform.parent = transform;
            feature.transform.localPosition = Vector3.zero;
            feature.transform.parent = transform.parent;
            chunk.AddFeature(feature);
            Destroy(gameObject);
        }

    }

}
