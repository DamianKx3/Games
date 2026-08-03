using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class winsec : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerPrefs.GetInt("mode"))
        {
            case 0:
                PlayerPrefs.SetInt("beg", 1);
                break;
            case 1:
                PlayerPrefs.SetInt("norm", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("normhard", 1);
                break;
            case 3:
                PlayerPrefs.SetInt("nit", 1);
                break;
            case 4:
                PlayerPrefs.SetInt("nithard", 1);
                break;
        }
        StartCoroutine(enumerator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator enumerator()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(12);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
