using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    public static SpriteHolder spriteholder;

    public Sprite[] sprites;
    public string[] ItemNames;
    public string[] ItemTags;
    public int[] DeafultDurability;
    [Header("crafting")]
    public string[] Ingridients;
    public string[] result;
    [Header("items")]
    public GameObject[] CustomUse;
    public int[] IdsUse;
    [Header("Enemies")]
    public GameObject[] Enemies;
    public int[] Difficulties;
    [Header("Blocks")]
    public GameObject[] Blocks;
    private void Awake()
    {
        spriteholder = this;
    }
    void Start()
    {
        for (int i = 0; i < CustomUse.Length; i++)
        {
            CustomUse[i].GetComponent<Useable>().AutoID = IdsUse[i];
        }
        for (int i = 0; i < Blocks.Length; i++)
        {
            Blocks[i].GetComponent<block>().ID = i;
        }
        for(int i = 0;i < Enemies.Length; i++)
        {
            Enemies[i].GetComponent<Enemy>().EnemyID = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
