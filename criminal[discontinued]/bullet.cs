using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float Radius;
    public Rigidbody2D Rigidbody2D;
    public CircleCollider2D CircleCollider;
    bool exploded;
    public Transform Forward1;
    public bool ATGM;
    public Vector3 Target;
    int dest;
    bool dest2;
    public GameObject particles;
    float cooldown;
    public bool ballistic;
    public Transform inside;
    bool oncam;

    void Start()
    {
        if(!ATGM)
        {


            if(ballistic == false)
            {
                Rigidbody2D.AddForce(new Vector2(150, 150) * -transform.right);
            }
            else
            {

                Rigidbody2D.AddForce(new Vector2(250, 250) * -transform.right);
            }
        }
        else
        {
            Rigidbody2D.AddForce(new Vector2(200, 200) * -transform.right);
        }


    }
    private void Update()
    {
        cooldown = cooldown + Time.deltaTime;
        if(ATGM == false)
        {
            float angle = AngleBetweenPoints(new Vector2(transform.position.x,transform.position.y) + Rigidbody2D.velocity, transform.position);
            
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
        }
        range();


    }
    public void range()
    {
        oncam = true;
        if (transform.position.x > Camera.main.transform.position.x + Camera.main.orthographicSize * 2)
        {
            //Debug.Log("POZA ZASIEGIEM");
            oncam = false;
        }
        if (transform.position.x < Camera.main.transform.position.x - Camera.main.orthographicSize * 2)
        {
            //Debug.Log("POZA ZASIEGIEM");
            oncam = false;
        }
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            //Debug.Log("POZA ZASIEGIEM");
            oncam = false;
        }
        if (transform.position.y > Camera.main.transform.position.y + Camera.main.orthographicSize)
        {
            //Debug.Log("POZA ZASIEGIEM");
            oncam = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(ATGM == true && cooldown > 0.5f)
       {
            if(cooldown > 4)
            {
                dest = 3;
            }
            Rigidbody2D.gravityScale = 0;
            if(dest < 2)
            {
                float angle = AngleBetweenPoints(transform.position, Target);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                if (Vector2.Distance(transform.position,Target) < 4f && dest2 == false)
                {
                    dest2 = true;
                    Debug.Log("deststop");
                    dest++;

                }
                else
                {
                    dest2 = false;
                }
            }
            Rigidbody2D.AddForce(new Vector2(700, 700) * -transform.right * Time.fixedDeltaTime);

            if(Rigidbody2D.velocity.x > 50)
            {
                Rigidbody2D.velocity = new Vector2(50, Rigidbody2D.velocity.y);
            }
            if (Rigidbody2D.velocity.y > 50)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x,50);
            }
            if (Rigidbody2D.velocity.x < -50)
            {
                Rigidbody2D.velocity = new Vector2(-50, Rigidbody2D.velocity.y);
            }
            if (Rigidbody2D.velocity.y < -50)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, -50);
            }
        }

       if(Vector2.Distance(transform.position,Camera.main.transform.position) > 100 && ballistic == false)
       {
            Destroy(gameObject);
       }
        
    }
    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag != "Spawnable" && exploded == false && collision.isTrigger == false)
        {
            exploded = true;
            StartCoroutine(Explode());
        }
        if(exploded == true && collision.tag != "Spawnable")
        {
            if(collision.transform.parent != null && collision.transform.parent.tag == "Block")
            {
                if (Radius - Vector2.Distance(collision.transform.position, transform.position) >= 0.2f)
                {
                    collision.transform.parent.GetComponent<Blocks>().durability = collision.transform.parent.GetComponent<Blocks>().durability - (Radius - Vector2.Distance(collision.transform.position, transform.position));
                    if (collision.transform.parent.GetComponent<Blocks>().durability <= 0)
                    {
                        Destroy(collision.transform.parent.gameObject);
                    }
                }
                else
                {
                    collision.transform.parent.GetComponent<Blocks>().durability = collision.transform.parent.GetComponent<Blocks>().durability - 0.2f;
                    if (collision.transform.parent.GetComponent<Blocks>().durability <= 0)
                    {
                        Destroy(collision.transform.parent.gameObject);
                    }
                }
               
            }
            if(collision.tag == "Human")
            {
                if (Radius - Vector2.Distance(collision.transform.position, transform.position) >= 0.2f)
                {
                    collision.gameObject.GetComponent<Human>().hp = collision.gameObject.GetComponent<Human>().hp - (Radius - Vector2.Distance(collision.transform.position, transform.position));
                    if(Radius >= 4 && Vector2.Distance(collision.transform.position, transform.position) <= Radius / 2 && Random.Range(0,2) > 0)
                    {
                        collision.gameObject.GetComponent<Human>().DestroyHuman();
                    }
                }
                else
                {
                    collision.gameObject.GetComponent<Human>().hp = collision.gameObject.GetComponent<Human>().hp - 0.2f;
                }
                
            }
            if (collision.tag == "otherdamage")
            {
                if (Radius - Vector2.Distance(collision.transform.position, transform.position) >= 0.2f)
                {
                    collision.gameObject.GetComponent<OtherDamage>().hp = collision.gameObject.GetComponent<OtherDamage>().hp - (Radius - Vector2.Distance(collision.transform.position, transform.position));
                }
                else
                {
                    collision.gameObject.GetComponent<OtherDamage>().hp = collision.gameObject.GetComponent<OtherDamage>().hp - 0.2f;
                }

            }
            if (collision.GetComponent<Rigidbody2D>())
            {
                Debug.Log(collision.gameObject);
                if (transform.position.x > collision.transform.position.x)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-collision.transform.position.x - transform.position.x,0) * Radius);
                }
                if (transform.position.x < collision.transform.position.x)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.transform.position.x - transform.position.x, 0) * Radius);
                }
                if (transform.position.y < collision.transform.position.y)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,collision.transform.position.x - transform.position.x) * Radius);
                }
                if (transform.position.y > collision.transform.position.y)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -collision.transform.position.x - transform.position.x) * Radius);
                }

            }

        }

    }
    public void GetPeople()
    {
        Human[] h = GameObject.FindObjectsOfType<Human>();
        foreach (Human human in h)
        {
            if(Vector2.Distance(transform.position,human.gameObject.transform.position) < Radius * 5)
            {
                if(human.state == 0)
                {
                    human.state = 1;
                }
            }
        }
        alarm[] a = GameObject.FindObjectsOfType<alarm>();
        foreach (alarm Alarm in a)
        {
            Alarm.active = true;
        }
    } 
    public IEnumerator Explode()
    {
        Rigidbody2D.velocity = Vector3.zero;
        Rigidbody2D.gravityScale = 0;
        CircleCollider.radius = Radius;
        GetPeople();
        yield return new WaitForEndOfFrame();
        if(oncam == true && Camera.main.gameObject.GetComponent<CameraController>().shakestrenght < Radius / 1.5f)
        {
            Camera.main.gameObject.GetComponent<CameraController>().shakestrenght = Radius / 1.5f;
        }
        GameObject particle1 = Instantiate(particles, transform.position, Quaternion.identity);
        particle1.transform.localScale = new Vector3(Radius / 5 * 1, Radius / 5 * 1, Radius / 5 * 1);
        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }





    
}
