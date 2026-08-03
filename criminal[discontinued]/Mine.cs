using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Mine : MonoBehaviour
{
    public bool activated;
    public GameObject particles;
    public float Radius;
    bool lock1;
    public BoxCollider2D BoxCollider2D;
    public AudioSource AudioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activated == true && lock1 == false)
        {
            lock1 = true;
            StartCoroutine(Explode());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Spawnable")
        {
            activated = true;
        }
        if(activated == true)
        {
            if (collision.transform.tag == "Spawnable")
            {

                collision.transform.GetComponent<Tank>().hp = collision.transform.GetComponent<Tank>().hp - Radius * 15;
            }
        }
    }
    public IEnumerator Explode() 
    {
        yield return null;
        yield return new WaitForEndOfFrame();
        AudioSource.Play();
        BoxCollider2D.size = new Vector2(Radius, Radius);
        GameObject particle1 = Instantiate(particles, transform.position, Quaternion.identity);
        particle1.transform.localScale = new Vector3(Radius / 5 * 2, Radius / 5 * 2, Radius / 5 * 2);
        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}
