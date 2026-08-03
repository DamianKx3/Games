using UnityEngine;

public class Zeasteregg : MonoBehaviour
{
    public int type;
    public GameObject mjr1;
    public CameraController controller;
    public bool lock1;
    void Start()
    {
        mjr1.SetActive(false);
        if(type == 1)
        {
            if(Random.Range(0,50) == 25)
            {
                mjr1.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(type == 1)
        {
            if (controller.LookState == 2)
            {
                lock1 = true;
            }
            if(lock1 == true)
            {
                mjr1.transform.position = mjr1.transform.position - new Vector3(0,3,0) * Time.deltaTime;
                if(mjr1.transform.position.y < -20)
                {
                    mjr1.SetActive(false);
                    type = 0;
                }
            }
        }
    }
}
