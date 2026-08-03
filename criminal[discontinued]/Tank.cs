using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [Header("obowiazkowe")]
    public string AITYPE;
    public float hp;
    public float speed;
    public int ammo;
    int MaxAmmo;
    public GameObject Gun;
    public GameObject bullet;
    public Rigidbody2D Rigidbody2D;
    public Transform Inside;
    public GameObject TextAbove;
    [Header("dodatkowe")]
    public GameObject bullet2;
    public GameObject[] Additionals;
    public int ShootType;
    public float TextHeight;
    public TriggerHuman GroundTracksTest;
    public float deflection;
    public AudioSource EngineAudio;
    public DozerTest Dozer;
    [Header("niepotrzebne public")]
    public Vector2 Destination;
    public Vector2 Target;
    public int Dir;
    public bool NoTarget;
    public bool Targetaccept;
    bool lock1;
    bool lock2;
    bool deflectionbool;



    void Start()
    {

        MaxAmmo = ammo;
        //unikanie kolizji maszyn sojuszniczych
        GameObject[] spawnable= GameObject.FindGameObjectsWithTag("Spawnable");
        if(AITYPE != "BPH")
        {
            for (int i = 0; i < spawnable.Length; i++)
            {
                Physics2D.IgnoreCollision(spawnable[i].GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
            }
        }
        //ustanwianie braku celu
        NoTarget = true;
        Destination= transform.position;

        //zaczyna technicznie mechanike strzelania
        if(AITYPE == "Tank")
        {
            ChangeDir();
            StartCoroutine(shoot());
        }
        if (AITYPE == "ATGM" || AITYPE == "Helicopter")
        {
            ChangeDir();
            StartCoroutine(ATGMshoot());

        }
        if(AITYPE == "BPH")
        {

            FindFirstObjectByType<Controller>().bomber = gameObject;
        }
        if (AITYPE == "Transporter")
        {

            ChangeDir();
            StartCoroutine(Machinegun()); 
        }
        if (AITYPE == "Launcher")
        {

            ChangeDir();
            StartCoroutine(shoot());
        }
        if(AITYPE == "Killdozer")
        {
            ChangeDir();
            StartCoroutine(Dozer1());
        }
    }


    void Update()
    {


        //text nad pojazdem
        if (TextAbove != null)
        {
            TextAbove.transform.position = transform.position + new Vector3(0, TextHeight, -1);
            TextAbove.transform.rotation = Quaternion.identity;
            if(AITYPE != "Drone")
            {
                TextAbove.GetComponent<TextMeshPro>().text = AITYPE + "\n" + "ammo: " + ammo + "/" + MaxAmmo;
            }
            else
            {
                TextAbove.GetComponent<TextMeshPro>().text = AITYPE + "\n" + ammo;
            }

        }


        //smierc
        if (hp <= 0)
        {
            Destroy(gameObject);
            if(AITYPE == "Bomber")
            {
                FindFirstObjectByType<Controller>().BombsUnlocked = false;
                Destroy(FindFirstObjectByType<Controller>().bomber);
            }

        }

        //TANK
        if (AITYPE == "Tank")
        {
            if (Mathf.Abs(Destination.x - transform.position.x) > 0.5f && GroundTracksTest.OnTrigger == true)
            {
                if (Destination.x < transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(-speed * transform.right.x, Rigidbody2D.velocity.y);
                }

                if (Destination.x > transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(speed * transform.right.x, Rigidbody2D.velocity.y);
                }
                EngineAudio.volume = 0.15f;
            }
            else
            {
                EngineAudio.volume = 0.05f;
                Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
            }

            float angle = AngleBetweenPoints(Gun.transform.position, Target);
            //Debug.Log(angle);
            if (Dir == 0)
            {
                if (NoTarget == false)
                {
                    if (angle > 25)
                    {
                        angle = 25;

                    }

                    if (angle < -25)
                    {
                        angle = -25;
                    }
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }

            }
            if (Dir == 1)
            {
                if (NoTarget == false)
                {
                    if (angle < 155 && angle > 0)
                    {
                        angle = 155;
                        //angle 180--155

                    }

                    if (angle > -155 && angle < 0)
                    {
                        angle = -155;
                        //angle -180----155
                    }
                    //dziala dookoka 
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 180f));
                }

            }

        }

        //ATGM
        if (AITYPE == "ATGM")
        {
            if (Mathf.Abs(Destination.x - transform.position.x) > 0.5f && GroundTracksTest.OnTrigger == true)
            {
                if (Destination.x < transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(-speed * transform.right.x, Rigidbody2D.velocity.y);
                }

                if (Destination.x > transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(speed * transform.right.x, Rigidbody2D.velocity.y);
                }
                EngineAudio.volume = 0.15f;
            }
            else
            {
                EngineAudio.volume = 0.05f;
                Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
            }

            float angle = AngleBetweenPoints(Gun.transform.position, Target);
            //Debug.Log(angle);
            if (Dir == 0)
            {
                if (NoTarget == false)
                {
                    if (angle > 5)
                    {
                        angle = 5;

                    }

                    if (angle < -60)
                    {
                        angle = -60;
                    }
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }

            }
            if (Dir == 1)
            {
                if (NoTarget == false)
                {
                    if (angle < 175 && angle > 0)
                    {
                        angle = 175;
                        //angle 180--155

                    }

                    if (angle > -120 && angle < 0)
                    {
                        angle = -120;
                        //angle -180---- -155
                    }
                    //dziala dookoka 
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 180f));
                }

            }
        }
        //IRAN DRONE
        if (AITYPE == "Drone")
        {
            //animacja unoszenia
            if (deflectionbool == true)
            {
                deflection = deflection - 0.08f * Time.deltaTime;
                if (deflection < -0.08f)
                {
                    deflectionbool = false;
                }
            }
            else
            {
                deflection = deflection + 0.08f * Time.deltaTime;
                if (deflection > 0.08f)
                {
                    deflectionbool = true;
                }
            }
            if (Mathf.Abs(Destination.x - transform.position.x) > 0.5f)
            {
                if (Destination.x < transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(-speed * transform.right.x, Rigidbody2D.velocity.y);
                }

                if (Destination.x > transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(speed * transform.right.x, Rigidbody2D.velocity.y);
                }
            }
            else
            {
                Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
            }

            if (Mathf.Abs(Destination.y - transform.position.y) > 0.5f)
            {
                if (Destination.y < transform.position.y)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, -speed * transform.up.y);
                }



                if (Destination.y > transform.position.y)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, speed * transform.up.y);
                }

            }
            else
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
            }
            Inside.transform.localPosition = new Vector3(0, deflection, 0);
        }

        //BOMBER      do reworku

        if (AITYPE == "BPH")
        {
            transform.position = Target;
        }
        if (AITYPE == "Bomber")
        {
            //animacja unoszenia
            if (deflectionbool == true)
            {
                deflection = deflection - 0.08f * Time.deltaTime;
                if (deflection < -0.15f)
                {
                    deflectionbool = false;
                }
            }
            else
            {
                deflection = deflection + 0.08f * Time.deltaTime;
                if (deflection > 0.15f)
                {
                    deflectionbool = true;
                }
            }
            Rigidbody2D.velocity = new Vector2(speed * transform.right.x, Rigidbody2D.velocity.y);

            if (Target.x <= transform.position.x)
            {
                if (lock1 == false)
                {
                    switch (ShootType)
                    {
                        case 0:
                            lock1 = true;
                            StartCoroutine(BombDrop());
                            break;
                        case 1:
                            lock1 = true;
                            StartCoroutine(BombDropSmall());
                            break;
                    }

                }

            }
            if (transform.position.x > Target.x + 100 && lock1 == true)
            {
                Destroy(gameObject);
            }
            Inside.transform.localPosition = new Vector3(0, deflection, 0);

        }
        if (AITYPE == "Helicopter")
        {
            //animacja unoszenia
            if (deflectionbool == true)
            {
                deflection = deflection - 0.06f * Time.deltaTime;
                if (deflection < -0.03f)
                {
                    deflectionbool = false;
                }
            }
            else
            {
                deflection = deflection + 0.06f * Time.deltaTime;
                if (deflection > 0.03f)
                {
                    deflectionbool = true;
                }
            }
            if (Mathf.Abs(Destination.x - transform.position.x) > 0.5f)
            {
                if (Destination.x < transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(-speed * transform.up.y, Rigidbody2D.velocity.y);
                }

                if (Destination.x > transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(speed * transform.up.y, Rigidbody2D.velocity.y);
                }
            }
            else
            {
                Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
            }

            if (Mathf.Abs(Destination.y - transform.position.y) > 0.5f)
            {
                if (Destination.y < transform.position.y)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, -speed * transform.up.y);
                }



                if (Destination.y > transform.position.y)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, speed * transform.up.y);
                }

            }
            else
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
            }
            Inside.transform.localPosition = new Vector3(0, deflection, 0);
            float angle = AngleBetweenPoints(Gun.transform.position, Target);
            //Debug.Log(angle);
            if (Dir == 0)
            {
                Gun.GetComponent<SpriteRenderer>().flipY = false;
                if (NoTarget == false)
                {
                    if (angle > 25)
                    {
                        angle = 25;

                    }

                    if (angle < 0)
                    {
                        angle = 0;
                    }
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }

            }
            if (Dir == 1)
            {
                Gun.GetComponent<SpriteRenderer>().flipY = true;
                if (NoTarget == false)
                {
                    if (angle < 155 && angle > 0)
                    {
                        angle = 155;
                        //angle 180--155

                    }

                    if (angle > -180 && angle < 0)
                    {
                        angle = -180;
                        //angle -180----155
                    }
                    //dziala dookoka 
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 180f));
                }

            }
        }
        //TANK
        if (AITYPE == "Transporter")
        {
            if (Mathf.Abs(Destination.x - transform.position.x) > 0.5f && GroundTracksTest.OnTrigger == true)
            {
                if (Destination.x < transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(-speed * transform.right.x, Rigidbody2D.velocity.y);
                }

                if (Destination.x > transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(speed * transform.right.x, Rigidbody2D.velocity.y); ;
                }
                EngineAudio.volume = 0.15f;
            }
            else
            {
                EngineAudio.volume = 0.05f;
                Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
            }

            float angle = AngleBetweenPoints(Gun.transform.position, Target);
            //Debug.Log(angle);
            if (Dir == 0)
            {
                if (NoTarget == false)
                {
                    if (angle > 25)
                    {
                        angle = 25;

                    }

                    if (angle < -25)
                    {
                        angle = -25;
                    }
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }

            }
            if (Dir == 1)
            {
                if (NoTarget == false)
                {
                    if (angle < 155 && angle > 0)
                    {
                        angle = 155;
                        //angle 180--155

                    }

                    if (angle > -155 && angle < 0)
                    {
                        angle = -155;
                        //angle -180----155
                    }
                    //dziala dookoka 
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 180f));
                }

            }

        }
        //Launcher
        if (AITYPE == "Launcher")
        {
            if (Mathf.Abs(Destination.x - transform.position.x) > 0.5f && GroundTracksTest.OnTrigger == true)
            {
                if (Destination.x < transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(-speed * transform.right.x, Rigidbody2D.velocity.y);
                }

                if (Destination.x > transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(speed * transform.right.x, Rigidbody2D.velocity.y); ;
                }
                EngineAudio.volume = 0.15f;
            }
            else
            {
                EngineAudio.volume = 0.05f;
                Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
            }

            float angle = AngleBetweenPoints(Gun.transform.position, Target);
            //Debug.Log(angle);
            if (Dir == 0)
            {
                if (NoTarget == false)
                {
                    if (angle > -55)
                    {
                        angle = -55;

                    }

                    if (angle < -85)
                    {
                        angle = -85;
                    }

                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }

            }
            if (Dir == 1)
            {
                if (NoTarget == false)
                {

                    if (angle < -130)
                    {
                        angle = -130;
                        //angle 180--155

                    }
                    if (angle < 180 && angle > 0)
                    {
                        angle = -130;
                        //angle 180--155


                    }
                    if (angle > -95 && angle < 0)
                    {
                        angle = -95;
                        //angle -180-- -155
                    }
                    //dziala dookoka 

                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 180f));
                }

            }

        }
        if (AITYPE == "Killdozer")
        {
            if (Mathf.Abs(Destination.x - transform.position.x) > 0.5f && GroundTracksTest.OnTrigger == true)
            {
                if (Destination.x < transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(-speed * transform.right.x, Rigidbody2D.velocity.y);
                }

                if (Destination.x > transform.position.x)
                {
                    Rigidbody2D.velocity = new Vector2(speed * transform.right.x, Rigidbody2D.velocity.y); ;
                }
                EngineAudio.volume = 0.2f;
            }
            else
            {
                EngineAudio.volume = 0.05f;
                Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
            }

            float angle = AngleBetweenPoints(Gun.transform.position, Target);
            //Debug.Log(angle);
            if (Dir == 0)
            {
                if (NoTarget == false)
                {
                    if (angle > 10)
                    {
                        angle = 10;

                    }

                    if (angle < -10)
                    {
                        angle = -10;
                    }
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }

            }
            if (Dir == 1)
            {
                if (NoTarget == false)
                {
                    if (angle < 170 && angle > 0)
                    {
                        angle = 170;
                        //angle 180--155

                    }

                    if (angle > -170 && angle < 0)
                    {
                        angle = -170;
                        //angle -180----155
                    }
                    //dziala dookoka 
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, angle));
                }
                else
                {
                    Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 180f));
                }

            }


        }
    }
    //nie ruszac to do obliczania kata bo nie wiem do konca czym jest kurwa atan2
    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    //funkcja do obrotu
    public void ChangeDir()
    {
        if(AITYPE == "Tank"|| AITYPE == "ATGM" || AITYPE == "Helicopter" || AITYPE == "Transporter" || AITYPE == "Launcher" || AITYPE == "Killdozer")
        {
            Destination = transform.position;
            if (Dir == 0)
            {

                Inside.transform.localRotation = Quaternion.Euler(0, 180, 0);
                Dir = 1;
                return;
            }
            if (Dir == 1)
            {
                Inside.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Dir = 0;
                return;
            }
        }
        if(AITYPE == "Drone")
        {
            if (lock1 == false)
            {
                lock1 = true;
                StartCoroutine(Explode());
            }
        }
        
    }
    //po wejsciu na trigger (zazwyczaj chodzi o rozbicie)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(AITYPE == "Drone" && collision.gameObject.GetComponent<Enemy>() == null && collision.isTrigger ==false)
        {
            if(lock1 == false)
            {
                lock1 = true;
                StartCoroutine(Explode());
            }

        }
        if(AITYPE == "Bomber" && collision.gameObject.GetComponent<Bomb>() == null && collision.gameObject.GetComponent<Enemy>() == null && collision.gameObject.GetComponent<EnemyBullet>() == null && collision.isTrigger == false)
        {
            if(lock2 == false)
            {
                lock2 = true;
                Instantiate(bullet2, transform.position + new Vector3(7,0,0), Quaternion.identity);
                FindFirstObjectByType<Controller>().BombsUnlocked = false;
                Destroy(FindFirstObjectByType<Controller>().bomber);
                Destroy(gameObject);
            }
            

        }

        if (AITYPE == "Helicopter" && collision.gameObject.GetComponent<bullet>() == null && collision.gameObject.GetComponent<Enemy>() == null && collision.isTrigger == false)
        {
            if (lock1 == false)
            {
                lock1 = true;
                Instantiate(bullet2, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                Destroy(gameObject);
            }

        }
    }
    //strzelanie
    public IEnumerator shoot()
    {
        while (ammo > 0)
        {
            yield return null;
            if (NoTarget == false)
            {
                yield return new WaitForSeconds(3);
                if (NoTarget == false && Targetaccept == true)
                {
                    ammo--;
                    GameObject bullet1 = Instantiate(bullet, Gun.transform.position, Gun.transform.rotation);
                    if(AITYPE == "Launcher")
                    {
                        bullet1.GetComponent<bullet>().Target = Target;
                    }
                }

            }


        }
    }
    public IEnumerator Machinegun()
    {
        while (ammo > 0)
        {
            yield return null;
            if (NoTarget == false)
            {
                yield return new WaitForSeconds(0.1f);
                if (NoTarget == false && Targetaccept == true)
                {
                    ammo--;
                    GameObject bullet1 = Instantiate(bullet, Gun.transform.position, Gun.transform.rotation);
                }

            }
        }
    }
    public IEnumerator Dozer1()
    {
        while (ammo > 0)
        {
            yield return null;
            if (NoTarget == false)
            {
                if (NoTarget == false && Targetaccept == true && Dozer.OnTrigger == true)
                {
                    Dozer.blockNow.GetComponent<Blocks>().durability = Dozer.blockNow.GetComponent<Blocks>().durability - 5;
                    if(Dozer.blockNow.GetComponent<Blocks>().durability <= 0)
                    {
                        Destroy(Dozer.blockNow);
                    }
                    ammo--;
                    yield return new WaitForSeconds(0.1f);
                }

            }
        }
    }
    public IEnumerator ATGMshoot()
    {
        while (ammo > 0)
        {
            yield return null;
            if (NoTarget == false)
            {
                yield return new WaitForSeconds(3);
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(1);

                    if (NoTarget == false && Targetaccept == true)
                    {

                        ammo--;
                        Debug.Log(transform.rotation.eulerAngles.z);
                        GameObject bullet1 = null;
                        if (Dir == 1)
                        {
                            bullet1 = Instantiate(bullet, Gun.transform.position, Gun.transform.rotation * Quaternion.Euler(0, 0, 25));

                        }
                        else
                        {
                            bullet1 = Instantiate(bullet, Gun.transform.position, Gun.transform.rotation * Quaternion.Euler(0, 0, -25));

                        }
                        bullet1.GetComponent<bullet>().Target = Target;
                        if(AITYPE == "Helicopter")
                        {
                            //bullet1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200) * transform.right);
                        }
                        if (AITYPE == "ATGM")
                        {
                            //bullet1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200) * -transform.right);
                        }
                        if (ammo < 1)
                        {
                            break;

                        }
                    }

                }



            }


        }
    }
    public IEnumerator BombDrop()
    {
        yield return null;
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
    public IEnumerator BombDropSmall()
    {
        for (int i = 0; i < 5; i++)
        {

            Instantiate(bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }

    }
    public IEnumerator Explode()
    {
        ammo = 3;
        yield return new WaitForSeconds(1);
        ammo = 2;
        yield return new WaitForSeconds(1);
        ammo = 1;
        yield return new WaitForSeconds(1);
        ammo = 0;
        Instantiate(bullet, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }



    //przejezdzanie ludzi
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (AITYPE == "Tank" || AITYPE == "ATGM" || AITYPE == "Transporter" || AITYPE == "Launcher" || AITYPE == "Killdozer")
        if(collision.gameObject.tag == "Human" )
        {
            if (Mathf.Abs(0-Rigidbody2D.velocity.x) > 2 || Mathf.Abs(0 - Rigidbody2D.velocity.y) > 2)
            {
                    if (collision.gameObject.GetComponent<Human>().hp > -7)
                    {
                        collision.gameObject.GetComponent<Human>().hp = -7;
                    }
                
            }
            
        }
        if (collision.gameObject.transform.parent && collision.gameObject.transform.parent.tag == "Block")
        {
            
            if (collision.transform.parent.GetComponent<Blocks>().durability <= 1.1f)
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "Block")
        {

            if (collision.transform.GetComponent<Blocks>().durability <= 1.1f)
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "otherdamage")
        {

            if (collision.transform.GetComponent<OtherDamage>().Corpse == true)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
