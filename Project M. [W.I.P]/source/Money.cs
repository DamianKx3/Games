using Unity.Netcode;
using UnityEngine;

public class Money : NetworkBehaviour
{
    public NetworkVariable<float> Value = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public SpriteRenderer sr;
    public Sprite[] sprites;

    void Start()
    {
        if(Value.Value == 1f)
        {
            sr.sprite = sprites[0];
        }else if (Value.Value == 2f)
        {
            sr.sprite = sprites[1];
        }
        else if (Value.Value == 5f)
        {
            sr.sprite = sprites[2];
        }
        else if (Value.Value == 0.1f)
        {
            sr.sprite = sprites[3];
        }
        else if (Value.Value == 0.2f)
        {
            sr.sprite = sprites[4];
        }
        else if (Value.Value == 0.5f)
        {
            sr.sprite = sprites[5];
        }
        else
        {
            sr.sprite = sprites[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            controller.PickUpMoney(Value.Value);
            Destroy(gameObject);
        }
    }
}
