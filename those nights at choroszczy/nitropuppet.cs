using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nitropuppet : MonoBehaviour
{
    public float pointsleft;
    public jumpscareController jumpscareController;
    public GameObject nitropupet;
    public float anger;
    public GameObject handle;
    public AudioSource audio;
    void Start()
    {
        
        pointsleft = 0;
        StartCoroutine(loop());
        StartCoroutine(NitroP());
    }

    // Update is called once per frame
    void Update()
    {
        if(pointsleft < 0)
        {
            pointsleft = 0;
        }
        nitropupet.transform.position = this.transform.position - new Vector3(0,0.2f,0) + new Vector3(0,pointsleft / 110,0);
        if (Input.GetMouseButton(0))
        {
            if (jumpscareController.caninteract == true)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.name == "box")
                    {
                        pointsleft = pointsleft - 20 * Time.deltaTime;
                        Debug.Log(pointsleft);
                        handle.transform.Rotate(1000 * Time.deltaTime, 0, 0);
                        audio.UnPause();
                    }
                }
            }
        }
        else
        {
            audio.Pause();
        }
    }
    IEnumerator NitroP()
    {
        yield return null;
    }
    IEnumerator loop()
    {
        while (pointsleft < 100)
        {
            yield return new WaitForSeconds(0.35f);
            pointsleft = pointsleft + 1 * anger;
        }
        jumpscareController.enemyID = 5;
        jumpscareController.StartJS();
        gameObject.SetActive(false);

    }
}
