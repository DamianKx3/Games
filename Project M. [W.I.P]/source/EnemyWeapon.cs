using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int Type;
    public float maxcooldown;
    public float cooldown;
    public float Damage;
    public List<Collider> colliders;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            foreach (Collider col in colliders)
            {
                if (col.gameObject.GetComponent<PlayerController>())
                {
                    col.gameObject.GetComponent<PlayerController>().Damage(Damage);
                    cooldown = maxcooldown;
                    break;
                }
            }


        }
        else
        {
            cooldown = cooldown - Time.deltaTime;
        }
    }
    public void OnTriggerEnter(Collider collision)
    {
        colliders.Add(collision);
    }
    public void OnTriggerExit(Collider collision)
    {
        colliders.Remove(collision);
    }

}
