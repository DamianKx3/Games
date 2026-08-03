using Unity.VisualScripting;
using UnityEngine;

public class Street : building
{
    public GameObject[] Lines;

    public override void Generate()
    {
        if (neighbors[0] == 1)
        {
            Lines[0].SetActive(true);
        }
        if (neighbors[1] == 1)
        {
            Lines[1].SetActive(true);

        }
        if (neighbors[2] == 1)
        {
            Lines[2].SetActive(true);

        }
        if (neighbors[3] == 1)
        {
            Lines[3].SetActive(true);
        }
    }

}
