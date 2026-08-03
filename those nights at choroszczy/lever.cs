using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    public bool canpull;
    public Animator animator;
    public TimeAndPower TimeAndPower;
    public ParticleSystem ParticleSystem1;
    public ParticleSystem ParticleSystem2;
    public major major;
    public jumpscareController jumpscareController;
    void Start()
    {
        canpull = true;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canpull == true && jumpscareController.caninteract == true)
        {
            RaycastHit hit;
            
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "lever")
                {
                    StartCoroutine(cooldown());
                    animator.SetTrigger("pull");
                    
                }
            }
        }
    }
    IEnumerator cooldown()
    {
        if (TimeAndPower.power > 0.1f)
        {

            major.gas = true;
            canpull = false;
            StartCoroutine(Wait());
            TimeAndPower.usage++;
            yield return new WaitForSeconds(2.5f);
            TimeAndPower.usage--;
            canpull = true;
            major.gas = false;
        }
        else
        {
            major.gas = false;

            canpull = true;
        }

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        ParticleSystem1.Play();
        ParticleSystem2.Play();
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3);
        ParticleSystem2.Stop();

    }

}
