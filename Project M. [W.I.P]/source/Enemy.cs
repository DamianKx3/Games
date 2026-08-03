using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Enemy : NetworkBehaviour
{
    public int EnemyID;
    //public PlayerController Player;
    public float hp;
    public float armor;
    public Generator Generator;
    public NavMeshAgent agent;
    public float speed;
    public SortingGroup sortingGroup;
    public int Dimension;
    public GameObject[] bodyparts;
    public GameObject target;
    public string[] TriggeredBy;
    public Animator animator;
    Controller controller;
    public float SightDistance;
    [Header("Dropy")]
    public int[] ItemIDs;
    public int[] ItemWeight;
    public int Tries;
    System.Random rand;
    public List<int> FinalList = new List<int>();
    void Start()
    {

        //Generator = FindFirstObjectByType<Generator>();
        controller = Controller.controller;
        //Player = controller.ClientPlayer.GetComponent<PlayerController>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
     void Update()
     {
        //if (Player == null)
        //{
            //Player = controller.ClientPlayer.GetComponent<PlayerController>();
        //}
        agent.speed = speed;
        Logic();
  

     }
    public virtual void Logic()
    {
        if (target == null)
        {
            agent.destination = transform.position;
            Collider[] hit = Physics.OverlapSphere(transform.position,SightDistance);
            bool b = false;


            for (int i = 0; i < hit.Length; i++)
            {
                for (int j = 0; j < TriggeredBy.Length; j++)
                {
                    if (hit[i].tag == TriggeredBy[j] && hit[i].gameObject.layer == Dimension)
                    {
                        target = hit[i].gameObject;
                        b = true;
                        break;
                    }
                    if (b == true)
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            agent.destination = target.transform.position;
            if (Vector3.Distance(transform.position, target.transform.position) > SightDistance)
            {
                target = null;
            }
        }

        if(hp <= 0)
        {
            Die();

        }
    }
    public virtual void Die()
    {
        for (int i = 0; i < Tries; i++)
        {
            int a = rand.Next(0, FinalList.Count);
            if (FinalList[a] >= 0)
            {
                if (FinalList[a] == 0)
                {
                    continue;
                }
                controller.SpawnItem((ushort)FinalList[a], 1, 1, transform.position + new Vector3(rand.Next(-5, 5), 1, rand.Next(-5, 5)));
            }
            else
            {
                controller.SpawnMoney((float)-FinalList[a] / 100, transform.position + new Vector3(rand.Next(-5, 5), 1, rand.Next(-5, 5)));
            }


        }
        Destroy(gameObject);
    }
    public void Damage(float Dmg)
    {
        hp = hp - Dmg * (1 - (armor / 100));
    }
}
