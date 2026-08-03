using UnityEngine;
using System.Collections;
public class diody : MonoBehaviour
{
    public Transform[] pos;
    void Start()
    {
        StartCoroutine(Work());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Work()
    {
        yield return new WaitForSeconds(Random.Range(0f,1f));
        while (true)
        {
            yield return new WaitForSeconds(1);
            GetComponent<Light>().enabled = true;
            transform.position = pos[Random.Range(0, pos.Length)].position;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z-0.13f);
            if (Random.Range(0,2) == 0)
            {
                yield return new WaitForSeconds(1);
                GetComponent<Light>().enabled = false;
            }

            
        }
    }
}
