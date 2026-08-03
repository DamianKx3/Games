using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool Triggered;
    public List<GameObject> players = new List<GameObject>();
    public ushort TargetDim;
    public Transform TeleportTo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Used(ulong ID)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<NetworkObject>().OwnerClientId == ID)
            {
                players[i].GetComponent<PlayerController>().Dimension.Value = TargetDim;
                if(TeleportTo != null)
                {
                    players[i].GetComponent<NetworkTransform>().Teleport(TeleportTo.position, players[i].transform.rotation, players[i].transform.localScale);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().portal = this;
            players.Add(collision.gameObject);
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().portal = null;
            players.Remove(collision.gameObject);
        }
    }
}
