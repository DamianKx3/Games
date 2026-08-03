using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 Before;
    public PlayerController playerController;
    public float Zoom = 37;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(playerController == null)
            {
                playerController = player.GetComponent<PlayerController>();
            }
            if (playerController.Dimension.Value == 0)
            {
                //GetComponent<Camera>().cullingMask = LayerMask.GetMask("Default", "UI");
                Zoom = 37;
            }
            else if (playerController.Dimension.Value == 1)
            {
                //GetComponent<Camera>().cullingMask = LayerMask.GetMask("Interior", "UI");
                Zoom = 25;
            }
        }

    }
    void FixedUpdate()
    {

        if (player != null)
        {
            if (Vector3.Distance(player.transform.position, Before) > 10)
            {
                transform.position = new Vector3(player.position.x, player.position.y + Zoom, player.position.z - Zoom);

            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y + Zoom, player.position.z - Zoom), Time.deltaTime * 5);
            Before = player.transform.position;
        }

    }
}
