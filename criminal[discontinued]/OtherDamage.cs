using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherDamage : MonoBehaviour
{
    public float hp;

    public GameObject[] SpawnOnDeath;
    public AudioSource audioSource;
    public Rigidbody2D Rigidbody2D;
    public float counter;
    float temphp;
    public bool skipTooLowHpRule;
    public bool Corpse;
    public bool IgnoreCollision;
    void Start()
    {
        temphp = hp;
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(counter < 0.5f)
        {
            hp = temphp;
            counter = counter + Time.deltaTime;
            return;
        }
        if (hp <= 0)
        {
            if(hp > -hp * 2 || skipTooLowHpRule == true)
            {
                foreach (GameObject A in SpawnOnDeath)
                {

                    Instantiate(A, transform.position, Quaternion.identity);
                }
            }
            
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Human")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),collision.gameObject.GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "otherdamage" && IgnoreCollision == true)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
}
