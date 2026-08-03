using UnityEngine;

public class Graffiti : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer SpriteRenderer;
    public Color Color;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        SpriteRenderer.color =new Color(Color.r * Controller.GraffitiDarkness,Color.g * Controller.GraffitiDarkness, Color.b * Controller.GraffitiDarkness,1);
        
    }
    public void Spawn(System.Random rand)
    {
        Color = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
        SpriteRenderer.sprite = sprites[rand.Next(sprites.Length)];
    }
}
