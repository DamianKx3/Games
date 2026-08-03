using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGravity : MonoBehaviour
{
    //grawitacja blokow bedzie tylko i wylacznie na blokach stalych
    public bool gravitated;
    Rigidbody2D rb;
    public string nam;
    public int Dir;
    float timer;
    public bool isbg;
    void Start()
    {
        if(isbg == false)
        {
            gameObject.AddComponent<Rigidbody2D>();
            rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
            rb.mass = 2;
            rb.gravityScale = 4;
        }
        else
        {
            
        }

        gravitated= true;

        timer = Random.Range(0.01f,0.35f);

    }

    // Update is called once per frame
    void Update()
    {
        //if(Vector2.Distance(transform.position, Camera.main.transform.position) > 60)
        //{
            //return;
        //}
        if(timer < 0.5f)
        {
            timer = timer + Time.deltaTime;
            return;
        }
        else
        {
            timer = 0;
            UpdatePhysics();
        }
        
    }
    public void UpdatePhysics()
    {
        RaycastHit2D[] hitDown = null;
        switch (Dir)
        {
            case 0:
                hitDown = Physics2D.RaycastAll(transform.position + new Vector3(0, -0.6f, 0), Vector2.down, 0.05f);
                break;
            case 1:
                hitDown = Physics2D.RaycastAll(transform.position + new Vector3(0.6f, 0, 0), Vector2.right, 0.05f);
                break;
            case 2:
                hitDown = Physics2D.RaycastAll(transform.position + new Vector3(-0.6f, 0, 0), Vector2.left, 0.05f);
                break;
            case 3:
                hitDown = Physics2D.RaycastAll(transform.position + new Vector3(0, 0.6f, 0), Vector2.down, 0.05f);
                break;
        }



        gravitated = false;
        if (isbg == true)
        {
            for (int i = 0; i < hitDown.Length; i++)
            {
                if (hitDown[i].collider != null)
                {


                    if (hitDown[i].collider.transform.parent && hitDown[i].collider.transform.parent.tag == "Block" || hitDown[i].collider.tag == "ground")
                    {

                        gravitated = true;

                    }

                }
            }
            if (gravitated == false)
            {
                Destroy(gameObject);
            }
            return;

        }

        //grawitated = false --- jest nad isbg
        for (int i = 0; i < hitDown.Length; i++)
        {
            if (hitDown[i].collider != null)
            {


                if (hitDown[i].collider.transform.parent && hitDown[i].collider.transform.parent.tag == "Block" || hitDown[i].collider.tag == "ground")
                {
                    if (rb.bodyType == RigidbodyType2D.Static)
                    {
                        if (hitDown[i].collider.transform.parent.gameObject != gameObject)
                        {
                            gravitated = true;
                            nam = hitDown[i].collider.transform.parent.name;

                        }
                    }
                    else
                    {
                        if (hitDown[i].collider.transform.parent.gameObject != gameObject && hitDown[i].collider.isTrigger == false)
                        {
                            gravitated = true;
                            nam = hitDown[i].collider.transform.parent.name;
                            Dir = 0;
                        }
                    }


                }
            }
        }

        if (gravitated == false)
        {

            rb.bodyType = RigidbodyType2D.Dynamic;

        }
        if (gravitated == true)
        {
            rb.bodyType = RigidbodyType2D.Static;

        }
    }
    

}
