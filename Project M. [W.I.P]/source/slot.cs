using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    public int Index;
    public int Link;
    public string Name;
    public int ID;
    public int count;
    public int durability;
    public TextMeshProUGUI text;
    public PlayerController playerController;
    public SpriteHolder spriteHolder;
    public Image Image;
    public Image bg;
    public Slider DurabilitySlider;
    void Start()
    {
        playerController = Controller.controller.ClientPlayer.GetComponent<PlayerController>();
        spriteHolder = SpriteHolder.spriteholder;
        Link = Index;
    }

    // Update is called once per frame
    void Update()
    {

        ID = playerController.inventory[Link].ID;
        count = playerController.inventory[Link].Count;
        durability = playerController.inventory[Link].Durability;


        Image.sprite = spriteHolder.sprites[ID];
        Name = spriteHolder.ItemNames[ID];
        if (spriteHolder.DeafultDurability[ID] > 1)
        {
            DurabilitySlider.value = (float)durability / (float)spriteHolder.DeafultDurability[ID];
            DurabilitySlider.gameObject.SetActive(true);
        }
        else
        {
            DurabilitySlider.gameObject.SetActive(false);
        }

        if (ID != 0)
        {
            text.text = Name + " " + count;
        }
        else
        {
            text.text = "";
        }
        if(playerController.SelectedNow == Index)
        {
            transform.localScale = new Vector3(0.95f, 0.95f, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.85f, 0.85f,1);
        }
        if (playerController.inventory[Link].InUse == false)
        {
            bg.color = new Color(1, 1, 1, 0.2f);
        }
        else
        {
            bg.color = new Color(0, 1, 0, 0.2f);
        }

    }
    public void Clicked()
    {
        if (Input.GetMouseButton(1))
        {
            if (playerController.inventory[Link].InUse == false)
            {
                string[] tags = spriteHolder.ItemTags[playerController.inventory[Link].ID].Split(' ');
                if ( tags.Contains("head"))
                {
                    playerController.inventory[Link].InUse = true;
                }else if (tags.Contains("chest"))
                {
                    playerController.inventory[Link].InUse = true;
                }
                else if (tags.Contains("legs"))
                {
                    playerController.inventory[Link].InUse = true;
                }
 
            }
            else
            {
                playerController.inventory[Link].InUse = false;
            }
        }
        else
        {
            if (playerController.eq.gameObject.activeSelf == true)
            {
                if (playerController.swap1 == null)
                {
                    playerController.swap1 = this;
                }
                else
                {
                    playerController.swap2 = this;
                }
            }
        }

    }
}
