using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class GoldenKonon : MonoBehaviour
{
    public float anger;
    public GameObject[] Gonon = new GameObject[10];
    public Controllpanel Controllpanel;
    public jumpscareController jumpscareController;
    MeshRenderer MeshRenderer;
    public VideoPlayer VideoPlayer;
    public GameObject screen1;
    public int random;
    public bool done;
    public float time;
   

    private void Start()
    {
        time = 25;
        VideoPlayer.playbackSpeed = 1;
        done = false;
        for (int i = 0; i < 7; i++)
        {
            Gonon[i].SetActive(false);
        }
        StartCoroutine(StartGame());
        
    }


    private void Update()
    {


        if (time < 1)
        {
            time = 99;
            jumpscareController.enemyID = 4;
            jumpscareController.StartJS();
            gameObject.SetActive(false);
        }
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(Random.Range(20, 60) / anger);
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(20, 60) / anger);
            yield return null;
            for (int i = 0; i < 7; i++)
            {
                Gonon[i].SetActive(false);
            }
            Debug.Log("0");
            StartCoroutine(next());
            while (done == false)
            {
                yield return null;
            }
        }
        
    }
    public IEnumerator next()
    {

        done = false;
        VideoPlayer.Play();
        screen1.SetActive(true);
        time = 25;
        StartCoroutine(Timer());
        for (int y = 0; y < Random.Range(3, 8); y++)
        {
            random = Random.Range(0, 7);
            Gonon[random].SetActive(true);
            MeshRenderer = Gonon[random].GetComponent<MeshRenderer>();
            MeshRenderer.material.color = new Color(1, 1, 1, 1);
            yield return null;
            bool towhile = false;
            while (towhile == false)
            {
                yield return null;
                if (jumpscareController.caninteract == true)
                {
                    RaycastHit hit;

                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.name == "goldenknur")
                        {
                            towhile = true;

                        }
                    }
                }

            }
            MeshRenderer = Gonon[random].GetComponent<MeshRenderer>();
            MeshRenderer.material.color = new Color(1, 1, 1, 1);

            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(0.05f);
                MeshRenderer.material.color = MeshRenderer.material.color - new Color(0, 0, 0, 0.1f);


            }
            Gonon[random].SetActive(false);
            

        }
        VideoPlayer.Stop();
        screen1.SetActive(false);
        done = true;
    }
    public IEnumerator Timer()
    {
        VideoPlayer.playbackSpeed = 1;
        while (done == false)
        {
            yield return new WaitForSeconds(1);
            time = time - 1;
            VideoPlayer.playbackSpeed = VideoPlayer.playbackSpeed + 0.025f;
        }

        
    
    }


}
//