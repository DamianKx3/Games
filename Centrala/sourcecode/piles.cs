using UnityEngine;

public class piles : MonoBehaviour
{
    public GameObject[] objs;
    public int Mode;
    public int Max;
    public int Min;
    public float distance;
    float dist;
    int R;
    int temp;
    void Start()
    {
        R = Random.Range(Min, Max);
        if(Mode == 1)
        {
            while(temp < R)
            {
                GameObject obj = Instantiate(objs[Random.Range(0, objs.Length)],transform.position + new Vector3(Random.Range(-0.1f,0.1f),dist,Random.Range(-0.1f, 0.1f)),Quaternion.identity);
                obj.transform.parent = transform;
                obj.transform.eulerAngles = new Vector3 (90,0,Random.Range(0f,360f));
                dist = dist + distance;
                temp++;
            }

        }else if(Mode == 2)
        {
            while (temp < R)
            {
                GameObject obj = Instantiate(objs[Random.Range(0, objs.Length)], transform.position + new Vector3(dist, 0, Random.Range(-0.1f, 0.1f)), Quaternion.identity);
                obj.transform.parent = transform;
                obj.transform.eulerAngles = new Vector3(90, 0, Random.Range(0f, 360f));
                obj.transform.eulerAngles = new Vector3(0, 0, 0);
                dist = dist + distance;
                temp++;
            }


        }
        else if (Mode == 3)
        {
            while (temp < R)
            {
                GameObject obj = Instantiate(objs[Random.Range(0, objs.Length)], transform.position + new Vector3(0,0,dist), Quaternion.identity);
                obj.transform.parent = transform;
                obj.transform.eulerAngles = new Vector3(90, 0, Random.Range(0f, 360f));
                obj.transform.eulerAngles = new Vector3(0, -90, 0);
                dist = dist + distance;
                temp++;
            }


        }
    }


}
