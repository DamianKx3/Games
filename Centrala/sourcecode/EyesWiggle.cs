using UnityEngine;

public class EyesWiggle : MonoBehaviour
{
    public Vector3 start;
    float timer;
    bool pos;
    void Start()
    {
        start = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if(timer > 0.025f)
        {
            timer = 0;
            if(pos == false)
            {
                pos = true;
            }
            else
            {
                pos = false;
            }
        }
        if(pos == false)
        {
            transform.localPosition = start + new Vector3(0.02f,0,0);
        }
        else
        {
            transform.localPosition = start + new Vector3(-0.02f, 0, 0);

        }
    }
}
