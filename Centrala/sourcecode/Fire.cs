using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    public bool Main;
    public SpriteRenderer renderer1;
    public Sprite[] sprites;
    float a;
    int i;
    public Controller controller;
    public GameObject[] rooms;
    public Button[] Buttons;
    public float[] timers;
    public AudioSource firesound;

    void Start()
    {
        if(Main == false)
        {
            renderer1 = GetComponent<SpriteRenderer>();
            i = Random.Range(0, sprites.Length);
        }
        else
        {
            controller = FindFirstObjectByType<Controller>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Main == false)
        {
            a = a + Time.deltaTime;
            if (a > 0.1f)
            {
                a = 0;
                i++;
                if (i > sprites.Length - 1)
                {
                    i = 0;
                }
            }
            renderer1.sprite = sprites[i];
        }
        else
        {
            float c = 0;
            for(int i = 0;  i < rooms.Length; i++)
            {
                if (controller.burnedOutRooms[i] == true)
                {
                    rooms[i].SetActive(true);
                    c = c + 1f;
                }
            }
            if (Data.FinalMode == true)
            {
                firesound.enabled = true;
                firesound.volume = c / (float)controller.burnedOutRooms.Length;
            }
            else
            {
                firesound.enabled = false;
            }
            for (int i = 0; i < controller.burnedOutRooms.Length; i++)
            {
                if (controller.burnedOutRooms[i] == true)
                {
                    timers[i] = timers[i] + Time.deltaTime;
                    if (timers[i] > 7)
                    {
                        Buttons[i].interactable = false;
                    }
                }
            }
        }
        
    }
}
