using Unity.Netcode;
using UnityEngine;

public class item : NetworkBehaviour
{
    public NetworkVariable<ushort> ID = new NetworkVariable<ushort>(0,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public int count;
    public int durability;
    public SpriteRenderer spriteRenderer;
    public SpriteHolder spriteHolder;
    void Start()
    {
        spriteHolder = SpriteHolder.spriteholder;

    }

    // Update is called once per frame
    void Update()
    {
        if(count == 0 && IsHost == true)
        {
            Destroy(gameObject);
        }
        spriteRenderer.sprite = spriteHolder.sprites[ID.Value];

    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            count = controller.AddItem(ID.Value, count,durability);
            Debug.Log("c: " + count);

        }
    }
}
