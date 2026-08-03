using UnityEngine;
using UnityEngine.UI;

public class noice : MonoBehaviour
{
    public Image noise;
    public SpriteRenderer sr;
    public Sprite[] sprites;
    float timer;
    int R;
    int R2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime + timer;
        if(timer > 0.02f)
        {
            timer = 0;
            R = Random.Range(0, sprites.Length);
            if(R == R2)
            {
                for (int i = 0; i < 10; i++)
                {
                    R = Random.Range(0, sprites.Length);
                    if(R != R2)
                    {
                        break;
                    }
                }

            }
            else
            {
                R2 = R;
            }
            if(noise != null)
            {
                noise.sprite = sprites[R];
            }
            if(sr != null)
            {
                sr.sprite = sprites[R];
            }
        }
    }
}
