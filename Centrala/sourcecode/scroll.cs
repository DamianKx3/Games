using UnityEngine;
using UnityEngine.SceneManagement;

public class scroll : MonoBehaviour
{
    public float speed;
    public bool isURL;
    public Vector3 Startpos;
    public float timer;
    void Start()
    {
        Startpos = transform.position;
    }


    void Update()
    {
        if(isURL == false)
        {
            transform.localPosition = transform.localPosition + new Vector3(0, speed, 0) * Time.unscaledDeltaTime;
            timer = timer + Time.deltaTime;
            if(timer > 65)
            {
                timer = 0;
                transform.position = Startpos;
            }
        }
    }
    public void OpenURL1(string url)
    {
        Application.OpenURL(url);
    }
    public void Goback()
    {
        SceneManager.LoadScene(0);
    }
}
