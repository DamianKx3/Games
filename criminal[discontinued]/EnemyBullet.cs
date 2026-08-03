using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Radius;
    public Rigidbody2D Rigidbody2D;
    public CircleCollider2D CircleCollider;
    bool exploded;

    public bool ATGM;
    public Transform Target;
    bool dest;
    public GameObject particles;
    void Start()
    {
        if (!ATGM)
        {
            //float X1 = Forward1.transform.position.x - transform.position.x;
            //float Y1 = Forward1.transform.position.y - transform.position.y;
            Rigidbody2D.AddForce(new Vector2( 300,  300) * -transform.right);
        }
        else
        {

            Rigidbody2D.AddForce(new Vector2(50, 50) * -transform.right);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ATGM == true)
        {
            if (dest == false)
            {

                if(Target != null)
                {
                    float angle = AngleBetweenPoints(transform.position, Target.position);

                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    if (Vector2.Distance(transform.position, Target.position) < 3f)
                    {
                        dest = true;
                    }
                }
                Rigidbody2D.AddForce(new Vector2(1400, 1400) * -transform.right * Time.fixedDeltaTime);



            }
            if (Rigidbody2D.velocity.x > 50)
            {
                Rigidbody2D.velocity = new Vector2(50, Rigidbody2D.velocity.y);
            }
            if (Rigidbody2D.velocity.y > 50)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 50);
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
        if (Vector2.Distance(transform.position, Camera.main.transform.position) > 150)
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
        
        if (exploded == false && collision.isTrigger == false && collision.GetComponent<Enemy>() == null)
        {
            if (collision.transform.parent != null)
            {
                if (collision.transform.parent.gameObject.GetComponent<Enemy>() == false)
                {
                    exploded = true;
                    StartCoroutine(Explode());
                }
            }
            else
            {
                exploded = true;
                StartCoroutine(Explode());
            }

        }
        if (exploded == false)
        {
            if (collision.transform.tag == "Spawnable")
            {

                exploded = true;
                StartCoroutine(Explode());
            }

        }
        if (exploded == true)
        {
            if (collision.transform.tag == "Spawnable")
            {
                if (Radius - Vector2.Distance(collision.transform.position, transform.position) > 0.2f)
                {
                    collision.transform.GetComponent<Tank>().hp = collision.transform.GetComponent<Tank>().hp - (Radius - Vector2.Distance(collision.transform.position, transform.position));
                }
                else
                {
                    collision.transform.GetComponent<Tank>().hp = collision.transform.GetComponent<Tank>().hp - 0.2f;
                }
                
            }

        }

    }
   
    public IEnumerator Explode()
    {
        Rigidbody2D.velocity = Vector3.zero;
        Rigidbody2D.gravityScale = 0;
        CircleCollider.radius = Radius;
        yield return new WaitForEndOfFrame();
        GameObject particle1 = Instantiate(particles, transform.position, Quaternion.identity);
        particle1.transform.localScale = new Vector3(Radius / 5 * 1, Radius / 5 * 1, Radius / 5 * 1);
        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}
