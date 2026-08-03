using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public List<GameObject> Targets;
    public int AIType;
    public GameObject rot;
    public Transform Target;
    public CircleCollider2D CircleCollider2D;
    public GameObject inside;
    public float r2;

    public float cooldown;
    public GameObject bullet;
    float maxcooldown;
    public Transform BulletSpawn;
    public bool Parachute;
    public GameObject parachuteObj;
    float temppos;
    float temppostimer;
    public float rand;
    void Start()
    {
        maxcooldown = cooldown;
        rand = Random.Range(-10, 10);
        if(Parachute == true)
        {
            transform.position = new Vector3(transform.position.x,60,transform.position.z);
        }
    }

    //wszystko tu jest strasznie pomieszane ale AI0 to antitank AI1 to antiair AI2 to soldier AI3 to czolg
    void Update()
    {
        for (int i = 0; i < Targets.Count; i++)
        {
            if (Targets[i] == null)
            {
                Targets.Remove(Targets[i]);
            }
        }
        //nie zyje nie strzela
        if(GetComponent<Human>() && GetComponent<Human>().hp < 0)
        {
            return;
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            CircleCollider2D.enabled = false;
        }
        else
        {
            CircleCollider2D.enabled = true;
        }

        //obracanie
        if(Parachute == true)
        {
            parachuteObj.SetActive(true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -5.5f);
            if(temppostimer >= 0.5f)
            {
                if (Mathf.Abs(temppos - transform.position.y) < 0.01f)
                {
                    Parachute = false;
                }
                temppos = transform.position.y;
                temppostimer = 0;
            }
            else
            {
                temppostimer = temppostimer + 1 * Time.deltaTime;
            }
            

        }
        else
        {
            if(parachuteObj != null)
            {
                parachuteObj.SetActive(false);
            }
        }

        if (Targets.Count > 0)
        {
            Target = Targets[0].transform;
            float angle = AngleBetweenPoints(rot.transform.position, Target.position);


            if(AIType != 2)
            {
                if (Target.position.x > transform.position.x)
                {
                    inside.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    r2 = 180;
                }
                else
                {
                    inside.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    r2 = 0;
                }
            }

            
            if (AIType == 0 || AIType == 3)
            {
                if(r2 == 180)
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
                }
                else
                {
                    if (angle > 25)
                    {
                        angle = 25;

                    }

                    if (angle < -25)
                    {
                        angle = -25;
                    }
                }

                
            }
            if(AIType == 2)
            {
                angle = angle + -90;
            }
            rot.transform.localRotation = Quaternion.Euler(new Vector3(0f, r2, angle + rand));
        }
        if(cooldown <= 0)
        {
            if (Targets.Count > 0)
            {
                cooldown = maxcooldown;
                
                GameObject EB = GameObject.Instantiate(bullet, BulletSpawn.position + new Vector3(0,0.5f,0), rot.transform.rotation);
                rand = Random.Range(-10, 10);
                EB.GetComponent<EnemyBullet>().Target = Target;
            }

        }
        else
        {
            cooldown = cooldown - 1 * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Spawnable")
        {
            if(AIType == 0 || AIType == 3)
            {
                if (collision.GetComponent<Tank>().AITYPE == "Tank" || collision.GetComponent<Tank>().AITYPE == "ATGM" || collision.GetComponent<Tank>().AITYPE == "Transporter" || collision.GetComponent<Tank>().AITYPE == "Launcher" || collision.GetComponent<Tank>().AITYPE == "Killdozer")
                {
                    Targets.Add(collision.gameObject);
                }
            }
            if (AIType == 1)
            {
                if (collision.GetComponent<Tank>().AITYPE == "Bomber" || collision.GetComponent<Tank>().AITYPE == "Drone" || collision.GetComponent<Tank>().AITYPE == "Helicopter")
                {
                    Targets.Add(collision.gameObject);
                }
            }
            if (AIType == 2)
            {

                Targets.Add(collision.gameObject);
                
            }
            if (AIType == 4)
            {

                if (collision.GetComponent<Tank>().AITYPE == "Bomber" || collision.GetComponent<Tank>().AITYPE == "Helicopter")
                {
                    Targets.Remove(collision.gameObject);
                }

            }

        }
    }
    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Spawnable")
        {

            
             Targets.Remove(collision.gameObject);
            if (AIType == 4)
            {
                Targets.Add(collision.gameObject);
            }


        }
    }
}
