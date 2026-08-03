using UnityEngine;
using UnityEngine.UI;


public class glitch : MonoBehaviour
{
    public Sprite[] sprites;
    float timer;
    public float maxtimer;
    public Image spriteRenderer;
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.localScale = new Vector3(Random.Range(0.1f, 1.2f), Random.Range(0.1f, 1.1f), Random.Range(0.1f, 1.1f));
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if(timer > 0.1f)
        {
            timer = 0;
            spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
            if (spriteRenderer.enabled == false)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                if (Random.Range(0, 3) == 0)
                {
                    spriteRenderer.enabled = false;
                }

            }
            transform.localScale = new Vector3(Random.Range(0.1f,1.2f), Random.Range(0.1f, 1.1f), Random.Range(0.1f, 1.1f));
            GetComponent<RectTransform>().localPosition = new Vector3 (Random.Range(-100,100), Random.Range(-100, 100), 0);

            
        }
    }
}
