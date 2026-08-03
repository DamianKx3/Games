using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public float camspeed;
    public Camera Camera;
    float exithold;

    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;

    public GameObject rain;

    public float TopLimit;
    public float BottomLimit;
    public float LeftLimit;
    public float RightLimit;

    public float shakestrenght;
    float RandX;
    float RandY;
    float waitsec;
    void Start()
    {
        Camera = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        //exit
        rain.transform.position = new Vector3(transform.position.x, 35, transform.position.z);
        if(waitsec < 1 )
        {
            waitsec = waitsec + Time.unscaledDeltaTime;
            return;
        }
        if(shakestrenght > 0 )
        {
            shakestrenght = shakestrenght - Time.deltaTime * 5;
        }
        else
        {
            shakestrenght= 0;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + new Vector3(camspeed * Time.unscaledDeltaTime,0,0);

            if (Mathf.Abs(transform.position.x - cloud1.transform.position.x) > 260)
            {
                cloud1.transform.position = cloud1.transform.position + new Vector3(440,0,0);
            }

            if (Mathf.Abs(transform.position.x - cloud2.transform.position.x) > 260)
            {
                cloud2.transform.position = cloud2.transform.position + new Vector3(440, 0, 0);
            }

            if (Mathf.Abs(transform.position.x - cloud3.transform.position.x) > 260)
            {
                cloud3.transform.position = cloud3.transform.position + new Vector3(440, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position - new Vector3(camspeed * Time.unscaledDeltaTime, 0, 0);
            if (Mathf.Abs(transform.position.x - cloud1.transform.position.x) > 260)
            {
                cloud1.transform.position = cloud1.transform.position - new Vector3(440, 0, 0);
            }

            if (Mathf.Abs(transform.position.x - cloud2.transform.position.x) > 260)
            {
                cloud2.transform.position = cloud2.transform.position - new Vector3(440, 0, 0);
            }

            if (Mathf.Abs(transform.position.x - cloud3.transform.position.x) > 260)
            {
                cloud3.transform.position = cloud3.transform.position - new Vector3(440, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + new Vector3(0, camspeed * Time.unscaledDeltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position - new Vector3(0, camspeed * Time.unscaledDeltaTime, 0);
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            if(Camera.orthographicSize < 30)
            {
                Camera.orthographicSize = Camera.orthographicSize + 0.5f;
            }

        }
        if (Input.mouseScrollDelta.y > 0)
        {
            if (Camera.orthographicSize > 5f)
            {
                Camera.orthographicSize = Camera.orthographicSize - 0.5f;
            }


        }
        //speed = Limit - transform.position.y + Camera.orthographicSize
        //limits
        if (transform.position.y + Camera.orthographicSize > TopLimit)
        {
            transform.position = transform.position - new Vector3(0, camspeed * Time.unscaledDeltaTime, 0);

            if (transform.position.y + Camera.orthographicSize > TopLimit + 0.5f)
            {
                transform.position = transform.position - new Vector3(0,5 * camspeed * Time.unscaledDeltaTime, 0);
            }
        }
        if (transform.position.y - Camera.orthographicSize < BottomLimit)
        {
            transform.position = transform.position + new Vector3(0, camspeed * Time.unscaledDeltaTime, 0);
            if(transform.position.y - Camera.orthographicSize < BottomLimit - 0.5f)
            {
                transform.position = transform.position + new Vector3(0, 5 * camspeed * Time.unscaledDeltaTime, 0);
            }
        }
        if(transform.position.x - Camera.orthographicSize * 2 < RightLimit)
        {
            transform.position = transform.position + new Vector3(camspeed * Time.unscaledDeltaTime, 0, 0);
            if (transform.position.x - Camera.orthographicSize * 2 < RightLimit - 0.5f)
            {
                transform.position = transform.position + new Vector3(5 * camspeed * Time.unscaledDeltaTime, 0, 0);
            }
        }
        if (transform.position.x + Camera.orthographicSize * 2 > LeftLimit - 3)
        {
            transform.position = transform.position - new Vector3(camspeed * Time.unscaledDeltaTime, 0, 0);
            if(transform.position.x + Camera.orthographicSize * 2 > LeftLimit - 2.5f)
            {
                transform.position = transform.position - new Vector3(5 * camspeed * Time.unscaledDeltaTime, 0, 0);
            }
        }
        transform.position = transform.position - new Vector3(RandX, RandY, 0);
        RandX = Random.Range(-0.1f * shakestrenght, 0.1f * shakestrenght);
        RandY = Random.Range(-0.1f * shakestrenght, 0.1f * shakestrenght);
        transform.position = transform.position + new Vector3(RandX, RandY, 0);
    }
    
    
}
