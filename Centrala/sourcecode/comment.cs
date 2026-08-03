using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class comment : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool isBad;
    public Controller Controller;
    public float addrating;
    public TextMeshProUGUI nickname;
    public Image pic;
    void Start()
    {
        Controller = FindFirstObjectByType<Controller>();
        pic.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        nickname.text = "user_" + Random.Range(0,10000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Delete()
    {
        if(isBad == true)
        {
            Controller.MoneyMultipler = Controller.MoneyMultipler + addrating;

        }
        else
        {
            Controller.punishment = Controller.punishment + 0.2f;
        }
        Destroy(gameObject);
    }
}
