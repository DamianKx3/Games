using UnityEngine;

public class CameraNoclip : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }


    void Update()
    {
        transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * speed;
    }
}
