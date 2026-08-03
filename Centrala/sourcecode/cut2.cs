using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cut2 : MonoBehaviour
{
    public float timer;
    public string[] txtlines;
    public string[] txtlinesENG;
    public TextMeshProUGUI txt;
    void Start()
    {
        if(Settings.TranslateToEng == true)
        {
            txtlines = txtlinesENG;
        }
        StartCoroutine(enum1());
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }
        if(timer > 50)
        {
            SceneManager.LoadScene(2);
        }
    }
    IEnumerator enum1()
    {
        for (int i = 0; i < txtlines.Length; i++)
        {
            txt.text = txtlines[i];
            yield return new WaitForSeconds((float)txtlines[i].Length / 14);
        }

    }
}
