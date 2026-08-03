using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{


    public Rigidbody2D Rigidbody2D;
    public int DirIDle;
    public float speed;
    public float hp;
    public int state;

    public GameObject[] emotions;

    public GameObject blood;
    public TriggerHuman TriggerRight;
    public TriggerHuman TriggerLeft;
    public TriggerHuman grounded;
    public bool panic;

    public bool Soilder;
    bool lock1;
    bool[] locks = new bool[3];
    public bool Died;
    public GameObject bigcorpse;
    public GameObject smallcorpse;
    public GameObject[] easyblood;
    public GameObject[] hardblood;
    public Sprite[] hugeblood;
    public AudioSource audioSource;

    [Header("bodypartsObj")]
    public GameObject head;
    public GameObject arm1;
    public GameObject arm2;
    public GameObject body;
    public GameObject lowbody;
    public GameObject leg1;
    public GameObject leg2;
    [Header("RandomizedLook")]
    public bool donotrand;
    public Sprite[] shirt;
    public Sprite[] hands;
    public Sprite[] lowbodypants;
    public Sprite[] pants;
    public GameObject[] haircuts;

    bool desthumanlock;
    void Start()
    {

        for (int i = 0; i < easyblood.Length; i++)
        {
            easyblood[i].SetActive(false);
        }
        for (int i = 0; i < hardblood.Length; i++)
        {
            hardblood[i].SetActive(false);
        }


        if (donotrand == false)
        {

            //nowe
            int r1 = Random.Range(0, shirt.Length);
            int r2 = Random.Range(0, pants.Length);
            body.GetComponent<SpriteRenderer>().sprite = shirt[r1];
            arm1.GetComponent<SpriteRenderer>().sprite = hands[r1];
            arm2.GetComponent<SpriteRenderer>().sprite = hands[r1];
            leg1.GetComponent<SpriteRenderer>().sprite = pants[r2];
            leg2.GetComponent<SpriteRenderer>().sprite = pants[r2];
            lowbody.GetComponent<SpriteRenderer>().sprite = lowbodypants[r2];
            foreach(GameObject obj in haircuts)
            {
                obj.SetActive(false);
            }
            haircuts[Random.Range(0,haircuts.Length)].SetActive(true);

        }


        GameObject[] hum = GameObject.FindGameObjectsWithTag("Human");
        for (int i = 0;i < hum.Length; i++)
        {
            Physics2D.IgnoreCollision(hum[i].GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }


        if(Soilder == false)
        {
            StartCoroutine(Cooldown());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        


        if (transform.position.y < -50)
        {
            hp = -1;
        }
        if(Rigidbody2D.velocity.y < -9 && grounded.OnTrigger == true && hp > 0)
        {
            hp = 0;
        }
        if(hp <= 0)
        {
            Die();

        }
        if(Soilder == false)
        {
            for (int i = 0; i < emotions.Length; i++)
            {
                emotions[i].SetActive(false);
            }
            if (state == 0)
            {
                emotions[0].SetActive(true);
            }
            if (state == 1)
            {
                emotions[1].SetActive(true);
            }
            if (state == 2)
            {
                emotions[2].SetActive(true);
                if (lock1 == false)
                {
                    lock1 = true;
                    speed = speed + 3;
                }
            }
        }
        TakeDmg();
        

    }
    //funkcja takedmg to tylko skrot aby nie bylo w update
    public void TakeDmg()
    {
        if (Died == true)
        {
            for (int i = 0; i < emotions.Length; i++)
            {
                emotions[i].SetActive(false);
            }
            emotions[3].SetActive(true);
            if (hp < -1)
            {
                for (int i = 0; i < easyblood.Length; i++)
                {
                    easyblood[i].SetActive(true);
                }

            }
            if (hp < -2f)
            {

                for (int i = 0; i < hardblood.Length; i++)
                {
                    hardblood[i].SetActive(true);
                }
            }
            if (hp < -4f)
            {

                for (int i = 0; i < hardblood.Length; i++)
                {
                    hardblood[i].SetActive(false);
                }
                for (int i = 0; i < easyblood.Length; i++)
                {
                    easyblood[i].SetActive(false);
                }
                for (int i = 0; i < haircuts.Length; i++)
                {
                    haircuts[i].SetActive(false);
                }
                head.GetComponent<SpriteRenderer>().sprite = hugeblood[0];
                body.GetComponent<SpriteRenderer>().sprite = hugeblood[1];
                arm1.GetComponent<SpriteRenderer>().sprite = hugeblood[2];
                arm2.GetComponent<SpriteRenderer>().sprite = hugeblood[2];
                leg1.GetComponent<SpriteRenderer>().sprite = hugeblood[2];
                leg2.GetComponent<SpriteRenderer>().sprite = hugeblood[2];
                lowbody.GetComponent<SpriteRenderer>().sprite = hugeblood[3];
                if (hp > -6.5f)
                {
                    //Instantiate(blood, transform.position, Quaternion.identity);
                }
            }
            if (hp < -6.5f && hp > -9)
            {
                if (locks[0] == false)
                {

                    audioSource.Play();
                    locks[0] = true;
                    Instantiate(bigcorpse, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
                    Instantiate(blood, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }

            }
            if (hp < -9 && hp > -11)
            {
                if (locks[1] == false)
                {

                    audioSource.Play();
                    locks[1] = true;
                    Instantiate(smallcorpse, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
                    Instantiate(blood, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }

            }
            if (hp < -11 && hp > -15)
            {
                if (locks[2] == false)
                {

                    audioSource.Play();
                    locks[2] = true;
                    Instantiate(blood, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }

            }
            if (hp < -15)
            {

                Destroy(gameObject);
            }
        }
    }
    public void GetPeople()
    {
        Human[] h = GameObject.FindObjectsOfType<Human>();
        foreach (Human human in h)
        {
            if (Vector2.Distance(transform.position, human.gameObject.transform.position) < 30 && human.Died == false)
            {
                if (human.state == 0 || human.state == 1)
                {
                    human.state = 2;
                }
            }
        }
    }
    //smierc od strony kodu
    public void Die()
    {
        if(Died == false)
        {
            Died = true;
            FindFirstObjectByType<Controller>().money = FindFirstObjectByType<Controller>().money + 200;
            GetPeople();

            audioSource.Play();
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            Rigidbody2D.freezeRotation = false;
            

        }

    }

    //Funkcje odpowiedzilane za gore + take damage
    public void DestroyHuman()
    {
        Die();
        TakeDmg();
        if (desthumanlock == true || hp >= -2)
        {
            return;
        }

        desthumanlock = true;
        GameObject[] temp = new GameObject[6];
        temp[0] = head;
        temp[1] = body;
        temp[2] = arm1;
        temp[3] = arm2;
        temp[4] = leg1;
        temp[5] = leg2;

        for (int i = 0; i < temp.Length; i++)
        {
            for (int j = 0; j < temp[i].transform.childCount; j++)
            {

                if (temp[i].transform.GetChild(j).GetComponent<ParticleSystem>())
                {
                    temp[i].transform.GetChild(j).GetComponent<ParticleSystem>().Play();
                }
            }

            temp[i].transform.parent = null;
            temp[i].tag = "otherdamage";
            temp[i].AddComponent<Rigidbody2D>();
            temp[i].AddComponent<BoxCollider2D>();
            temp[i].AddComponent<OtherDamage>();
            temp[i].GetComponent<OtherDamage>().hp = 1.5f;
            temp[i].GetComponent<OtherDamage>().Corpse = true;
            temp[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5,5), Random.Range(-5 ,5)),ForceMode2D.Impulse);
            


        }

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {

        if (Died == false)
        {
            if (Soilder == false)
            {
                if (DirIDle == 0)
                {
                    Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
                }
                if (DirIDle == 1)
                {
                    if (TriggerRight.OnTrigger == true)
                    {
                        Rigidbody2D.velocity = new Vector2(speed, Rigidbody2D.velocity.y);
                    }
                    else
                    {
                        DirIDle = 0;
                    }

                }
                if (DirIDle == 2)
                {
                    if (TriggerLeft.OnTrigger == true)
                    {
                        Rigidbody2D.velocity = new Vector2(-speed, Rigidbody2D.velocity.y);
                    }
                    else
                    {
                        DirIDle = 0;
                    }

                }
            }

        }


    }
    public IEnumerator Cooldown()
    {
        while (true)
        {
            if(panic == false)
            {
                yield return new WaitForSeconds(Random.Range(0f, 5f));
                DirIDle = Random.Range(0, 3);
            }
            else
            {
                if (DirIDle != 0)
                {
                    yield return new WaitForSeconds(Random.Range(1f, 3f));
                    DirIDle = Random.Range(0, 3);
                }
                else
                {
                    yield return new WaitForSeconds(Random.Range(0f, 1.5f));
                    DirIDle = Random.Range(1, 3);
                }

            }

        }

    }
    public IEnumerator corpse1()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.tag = "Human";

    }
    

}


