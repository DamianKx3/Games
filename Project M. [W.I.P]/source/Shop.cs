using UnityEngine;

public class Shop : building
{
    public GameObject[] Parts;
    public GameObject floor;
    public GameObject[] RoofDeco;
    public Material Material;
    public Renderer[] ToColor;
    int colind;
    public Color[] Colors;
    int AdditionalFloors = 0;
    public FeatureSpawner roofSpawner;
    public FeatureSpawner roofSpawnerHouse;
    public bool IsHouse = false;
    public int ReplaceType = 0;
    public GameObject Graffiti;
    public GameObject[] Masks;
    public GameObject ShopInterior;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Generate()
    {
        colind = rand.Next(0, Colors.Length);
        for (int i = 0; i < ToColor.Length; i++)
        {
            ToColor[i].material.color = Colors[colind];
        }
        if ((neighbors[0] != 3 && neighbors[2] != 3) || (neighbors[1] != 3 && neighbors[3] != 3))
        {
            IsHouse = true;
        }
        if (neighbors[0] != 3)
        {
            Parts[0].SetActive(true);
            if (IsHouse == false)
            {
                Parts[6].SetActive(true);
            }
        }
        if (neighbors[1] != 3)
        {
            Parts[1].SetActive(true);
            if (IsHouse == false)
            {
                Parts[7].SetActive(true);
            }

        }
        if (neighbors[2] != 3)
        {
            Parts[2].SetActive(true);
            if (IsHouse == false)
            {
                Parts[8].SetActive(true);
            }

        }
        if (neighbors[3] != 3)
        {
            Parts[3].SetActive(true);
            if (IsHouse == false)
            {
                Parts[9].SetActive(true);
            }
        }

        if (IsHouse == false)
        {
            Parts[4].SetActive(true);
            int r = rand.Next(0, 4);
            if (r == 0)
            {
                AdditionalFloors = 1;
                for (int i = 0; i < AdditionalFloors; i++)
                {
                    GameObject floor1 = Instantiate(floor, transform);
                    floor1.transform.localPosition = new Vector3(0, (i + 2) * 10 + 5, 0);
                }
            }
            else
            {
                roofSpawner.Spawn(rand,Main);
            }

        }
        else
        {
            Parts[5].SetActive(true);
            //roofSpawnerHouse.Spawn(rand);

        }
        int r1 = rand.Next(0, 2);
        for (int i = 0; i < Masks.Length; i++)
        {
            for (int j = 0; j < r1; j++)
            {
                GameObject graffiti = Instantiate(Graffiti, Masks[i].transform);
                graffiti.transform.localPosition = new Vector3((float)rand.Next(-50, 50) / 100, (float)rand.Next(-40, 40) / 100, 0);
                float r2 = rand.Next(15, 30);
                graffiti.transform.localScale = new Vector3(r2 / 100, r2 / 100, 1);
                graffiti.transform.localEulerAngles = new Vector3(0, 0, rand.Next(-45, 45));
                graffiti.GetComponent<Graffiti>().Spawn(rand);
            }
        }


    }
    public void SetInterior(int i, int j)
    {
        int layer = (i & 1) + ((j & 1) << 1);
        ShopInterior.transform.position = new Vector3(transform.position.x,-50 * (layer + 1),transform.position.z);
    }
}

