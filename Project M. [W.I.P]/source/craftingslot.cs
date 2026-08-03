using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class craftingslot : MonoBehaviour
{
    public int CraftingID;
    public SpriteHolder holder;
    public Transform ingridientHolder;
    public Transform resultHolder;
    public GameObject ingridientPrefab;
    public PlayerController playerController;
    public Controller controller;
    void Start()
    {
        holder = SpriteHolder.spriteholder;
        controller = Controller.controller;
        string[] ingr = holder.Ingridients[CraftingID].Split('#');
        string[] res = holder.result[CraftingID].Split('#');
        for (int i = 0; i < ingr.Length / 2; i++)
        {
            GameObject img = Instantiate(ingridientPrefab, ingridientHolder.transform);
            img.GetComponent<Image>().sprite = holder.sprites[int.Parse(ingr[i * 2])];
            img.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = ingr[i * 2 + 1];
        }
        for (int i = 0; i < res.Length / 2; i++)
        {
            GameObject img = Instantiate(ingridientPrefab, resultHolder.transform);
            img.GetComponent<Image>().sprite = holder.sprites[int.Parse(res[i * 2])];
            img.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = res[i * 2 + 1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Craft()
    {

        controller.CraftServerRPC(NetworkManager.Singleton.LocalClientId,(ushort)CraftingID);
    }
}
