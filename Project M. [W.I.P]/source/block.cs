using UnityEngine;

public class block : MonoBehaviour
{
    public int ID;
    public int rot;
    public float hp;
    public byte Type;
    void Start()
    {
        Controller.controller.BlockRegister.Add((new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z)),Type),gameObject);
    }

    // Update is called once per frame
    void Update()
    {
     if(hp <= 0)
        {
            Destroy(gameObject);
        }   
    }
    private void OnDestroy()
    {
        Controller.controller.BlockRegister.Remove((new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z)),Type));

    }
}
