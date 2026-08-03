using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float Radius;
    public CircleCollider2D CircleCollider2D;
    public GameObject particles;
    bool oncam;
    void Start()
    {

        StartCoroutine(Explode());
        

    }

    // Update is called once per frame
    void Update()
    {
        range();
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
        CircleCollider2D.radius = Radius;
        GetPeople();
        yield return new WaitForEndOfFrame();
        range();
        if (oncam == true && Camera.main.gameObject.GetComponent<CameraController>().shakestrenght < Radius / 1.5f)
        {
            Debug.Log(2137);
            Camera.main.gameObject.GetComponent<CameraController>().shakestrenght = Radius / 1.5f;
        }
        GameObject particle1 = Instantiate(particles, transform.position, Quaternion.identity);
        particle1.transform.localScale = new Vector3(Radius / 5 * 1, Radius / 5 * 1, Radius / 5 * 1);
        yield return new WaitForSeconds(0.1f);
        
        Destroy(gameObject);
    }
}
