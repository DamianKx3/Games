using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float Radius;
    public Rigidbody2D Rigidbody2D;
    public CircleCollider2D CircleCollider;
    bool exploded;
    public GameObject particles;
    public bool isCluster;
    public GameObject ClusterBomb;
    float invi;
    public bool isdrill;
    bool oncam;
    void Start()
    {
        invi = 0.5f;
    }
    private void Update()
    {
        if(invi > 0)
        {
            invi = invi - Time.deltaTime;
        }
        range();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, Camera.main.transform.position) > 100)
        {
            Destroy(gameObject);
        }
        if(transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(invi <= 0)
        {
            if (collision.tag != "Spawnable" && exploded == false && collision.isTrigger == false)
            {
                exploded = true;
                StartCoroutine(Explode());
            }
            if (exploded == true && collision.tag != "Spawnable")
            {
                if (collision.transform.parent != null && collision.transform.parent.tag == "Block")
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
                if (collision.tag == "Human")
                {
                    if (Radius - Vector2.Distance(collision.transform.position, transform.position) >= 0.2f)
                    {
                        collision.gameObject.GetComponent<Human>().hp = collision.gameObject.GetComponent<Human>().hp - (Radius - Vector2.Distance(collision.transform.position, transform.position));
                        if (Radius >= 4 && Vector2.Distance(collision.transform.position, transform.position) <= Radius / 2 && Random.Range(0, 2) > 0)
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
            }
        }
        

    }
    public void range()
    {
        oncam = true;
        if (transform.position.x > Camera.main.transform.position.x + Camera.main.orthographicSize * 2)
        {
            Debug.Log("POZA ZASIEGIEM");
            oncam = false;
        }
        if (transform.position.x < Camera.main.transform.position.x - Camera.main.orthographicSize * 2)
        {
            Debug.Log("POZA ZASIEGIEM");
            oncam = false;
        }
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            Debug.Log("POZA ZASIEGIEM");
            oncam = false;
        }
        if (transform.position.y > Camera.main.transform.position.y + Camera.main.orthographicSize)
        {
            Debug.Log("POZA ZASIEGIEM");
            oncam = false;
        }
    }
    public void GetPeople()
    {
        Human[] h = GameObject.FindObjectsOfType<Human>();
        foreach (Human human in h)
        {
            if (Vector2.Distance(transform.position, human.gameObject.transform.position) < Radius * 5)
            {
                if (human.state == 0)
                {
                    human.state = 1;
                }
            }
        }
    }
    public IEnumerator Explode()
    {
        while (invi > 0)
        {
            yield return null;
        }
        Rigidbody2D.velocity = Vector3.zero;
        if(isdrill == false)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(1.2f);
        }

        Rigidbody2D.gravityScale = 0;
        CircleCollider.radius = Radius;
        GetPeople();
        yield return new WaitForEndOfFrame();
        if (oncam == true)
        {
            if (Camera.main.gameObject.GetComponent<CameraController>().shakestrenght < Radius / 1.5f)
            {
                Camera.main.gameObject.GetComponent<CameraController>().shakestrenght = Radius / 1.5f;
            }

        }
        if (isCluster == true)
        {
            GameObject clusterbomb = GameObject.Instantiate(ClusterBomb, transform.position + new Vector3(0.5f,0,0), transform.rotation);
            clusterbomb.GetComponent<Rigidbody2D>().velocity = new Vector2(8, 5);
            GameObject clusterbomb2 = GameObject.Instantiate(ClusterBomb, transform.position + new Vector3(-0.5f, 0, 0), transform.rotation);
            clusterbomb2.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, 5);
        }
        GameObject particle1 = Instantiate(particles, transform.position, Quaternion.identity);
        particle1.transform.localScale = new Vector3(Radius/ 5 * 1, Radius / 5 * 1, Radius / 5 * 1);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
