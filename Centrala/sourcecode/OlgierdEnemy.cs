using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class OlgierdEnemy : MonoBehaviour
{
    public Controller Controller;
    public int State;
    public float speed;
    public float timer;
    public float LevelOfDanger;
    public GameObject OlgierdSprite;
    public Fiodor Fiodor;
    public Animator Animator;
    public Transform[] startpointPH1;
    public Transform[] endpointPH1;
    public Transform[] startpointPH2;
    public Transform[] endpointPH2;
    public Transform targetpoint;
    public CameraController maincam;
    public AudioSource breakInAudio;
    public BoxCollider collider1;
    public float scareTimer;
    public bool flashing;
    public bool lockedtokill;
    public Animator CamAnim;
    public Animator walk;
    public AudioSource jumpscaresound;
    public Ambient amb;
    public bool ambtry;
    void Start()
    {
        if(speed == 0)
        {
            speed = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (State == 0)
        {

            scareTimer = 0;
            Animator.enabled = false;
            walk.enabled = true;
            OlgierdSprite.SetActive(false);
            timer = timer + Time.deltaTime;
            if (timer > 6 && Data.invincibility <= 0)
            {
                timer = 0;
                if (Random.Range(0, 100) < LevelOfDanger)
                {
                    State = 1;
                    OlgierdSprite.SetActive(true);
                    OlgierdSprite.transform.position = startpointPH1[Random.Range(0, startpointPH1.Length)].position;
                    targetpoint = endpointPH1[Random.Range(0, endpointPH1.Length)];
                }
            }
            
        }else if (State == 1)
        {
            walk.speed = speed;
            OlgierdSprite.transform.position = Vector3.MoveTowards(OlgierdSprite.transform.position, targetpoint.position, Time.deltaTime * speed);
            if (Vector3.Distance(OlgierdSprite.transform.position,targetpoint.position) < 0.5f)
            {
                State = 2;
            }
        }else if(State == 2)
        {
            walk.speed = 0;
            timer = timer + Time.deltaTime;
            if (timer > 1) 
            {
                timer = 0;
                if (Random.Range(0, 100) < LevelOfDanger && Controller.WindowState == 0 && maincam.LookState != 2)
                {
                    State = 3;
                    OlgierdSprite.transform.position = startpointPH2[Random.Range(0, startpointPH2.Length)].position;
                    targetpoint = endpointPH2[Random.Range(0, endpointPH2.Length)];
                }
            }
            
        }else if (State == 3)
        {
            walk.speed = speed;
            OlgierdSprite.transform.position = Vector3.MoveTowards(OlgierdSprite.transform.position, targetpoint.position, Time.deltaTime * speed);
            if (ambtry == false)
            {
                ambtry = true;
                if (Random.Range(0, 3) == 0 && Vector3.Distance(OlgierdSprite.transform.position, targetpoint.position) < 3f)
                {
                    amb.PlayAmb(1);
                }
            }
            if (Vector3.Distance(OlgierdSprite.transform.position, targetpoint.position) < 0.5f)
            {
                breakInAudio.Play();
                lockedtokill = true;
                State = 4;
            }
        }else if(State == 4 && maincam.LookState != 2)
        {
            walk.enabled = false;
            Animator.enabled = true;
            Controller.tip = "patrz przez okno, świeć na niego aż sobie nie pójdzie. Nie pozwól aby dotarł do twojego okna";
            if (Settings.TranslateToEng == true) Controller.tip = "look through the window, flash him(using the flashlight of course) till he dissapears. Don't let him come close to the window.";

            Controller.DIED = true;
            if (Controller.DoorState == 0)
            {
                Animator.SetInteger("State",1);

            }
            else
            {
                Animator.SetInteger("State", 2);
            }
            CamAnim.SetBool("Windowscare", true);
            jumpscaresound.Play();
            State = 10;
        }
        else
        {
            OlgierdSprite.SetActive(true);
            Animator.speed = 1;
        }
        
        if (Controller.WindowState == 2 && lockedtokill == false)
        {
            if (flashing == true)
            {
                scareTimer = scareTimer + Time.deltaTime;
                speed = 0;
                if (scareTimer >= 3.5f)
                {
                    State = 0;
                    Controller.flashlightmal = 0.2f;
                }
            }
            else
            {
                scareTimer = 0;
                speed = 1;
            }
            collider1.enabled = true;
            collider1.center = OlgierdSprite.transform.position;
        }
        else
        {
            collider1.enabled = false;
            flashing = false;
            scareTimer = 0;
            speed = 1;
        }
        
    }
    public void SetState(int a)
    {
        State = a;
    }
    public void Ded()
    {
        Controller.ShowDeathScreen();
    }


}
