//using System.Collections.Generic;
//using TMPro;
//using Unity.Collections;
//using UnityEngine;


//public class ChunkBackup : MonoBehaviour
//{
//    public GameObject streetHori;
//    public GameObject streetVert;
//    public float Size;
//    public List<GameObject> Objs;
//    int[][] grid;
//    public GameObject[] spawners1x1;
//    public GameObject[] spawners2x2;
//    public GameObject[] spawners4x4;
//    public GameObject[] spawners8x8;
//    public GameObject num;

//    void Start()
//    {
//        grid = new int[(int)(Size / 10)][];
//        for (int i = 0; i < grid.Length; i++)
//        {
//            grid[i] = new int[(int)(Size / 10)];
//        }

//        GameObject borderstreet1 = Instantiate(streetVert);
//        borderstreet1.transform.parent = gameObject.transform;
//        borderstreet1.transform.localPosition = new Vector3(-0.5f, 0, 0);
//        borderstreet1.transform.localScale = new Vector3(1 / Size * 10, 1, 1);
//        for (int i = 0; i < (int)(Size / 10); i++)
//        {
//            grid[0][i] = 1;
//            grid[i][0] = 1;

//            grid[(int)(Size / 10) - 1][i] = 2;
//            grid[i][(int)(Size / 10) - 1] = 5;
//        }
//        GameObject borderstreet2 = Instantiate(streetHori);
//        borderstreet2.transform.parent = gameObject.transform;
//        borderstreet2.transform.localPosition = new Vector3(0, 0.5f, 0);
//        borderstreet2.transform.localScale = new Vector3(1, 1 / Size * 10, 1);


//        Generate();
//        for (int i = 0; i < (int)(Size / 10); i++)
//        {
//            for (int j = 0; j < (int)(Size / 10); j++)
//            {
//                num.GetComponent<TextMeshPro>().text += grid[i][j] + " ";
//            }
//            num.GetComponent<TextMeshPro>().text += "\n";
//        }

//    }
//    public void Generate()
//    {
//        //Debug.Log((int)(Size / 30));
//        for (int i = 1; i < (int)(Size / 30); i++)
//        {
//            if (Random.Range(0, 2) == 0)
//            {
//                GameObject street1 = Instantiate(streetHori);
//                street1.transform.parent = gameObject.transform;
//                street1.transform.localPosition = new Vector3(0, 0.5f - i / (Size / 30), 0);
//                street1.transform.localScale = new Vector3(1, 1 / Size * 10, 1);
//                for (int j = 0; j < (int)(Size / 10); j++)
//                {
//                    //Debug.Log(i + "   " + j);
//                    grid[i * 3][j] = 1;


//                }
//            }
//            if (Random.Range(0, 2) == 0)
//            {
//                GameObject street2 = Instantiate(streetVert);
//                street2.transform.parent = gameObject.transform;
//                street2.transform.localPosition = new Vector3(i / (Size / 30) - 0.5f, 0, 0);
//                street2.transform.localScale = new Vector3(1 / Size * 10, 1, 1);
//                for (int j = 0; j < (int)(Size / 10); j++)
//                {
//                    //Debug.Log(i + "   " + j);
//                    grid[j][i * 3] = 1;


//                }
//            }
//        }
//        for (int i = 0; i < Size / 10; i++)
//        {
//            for (int j = 0; j < Size / 10; j++)
//            {
//                if (grid[i][j] == 1)
//                {
//                    if (i + 1 < Size / 10 - 1 && grid[i + 1][j] == 0)
//                    {
//                        grid[i + 1][j] = 2;
//                    }
//                    if (i - 1 >= 0 && grid[i - 1][j] == 0)
//                    {
//                        grid[i - 1][j] = 3;
//                    }
//                    if (j + 1 < Size / 10 - 1 && grid[i][j + 1] == 0)
//                    {
//                        grid[i][j + 1] = 4;
//                    }
//                    if (j - 1 >= 0 && grid[i][j - 1] == 0)
//                    {
//                        grid[i][j - 1] = 5;
//                    }

//                }

//            }
//        }
//        for (int i = (int)Size / 10 - 1; i >= 0; i--)
//        {
//            for (int j = (int)Size / 10 - 1; j >= 0; j--)
//            {
//                int dir = -1;
//                switch (grid[i][j])
//                {
//                    default:
//                        break;
//                    case 2:
//                        dir = 0;
//                        break;
//                    case 3:
//                        dir = 1;
//                        break;
//                    case 4:
//                        dir = 0;
//                        break;
//                    case 5:
//                        dir = 1;
//                        break;
//                }
//                if (dir != -1)
//                {

//                    if (CanGenerate(i, j, grid, 8, dir) == true)
//                    {
//                        PlaceBuilding(8, i, j);
//                    }
//                    else
//                    {

//                        if (Random.Range(0, 3) != 0)
//                        {
//                            int a = 4;
//                            while (a > 0)
//                            {
//                                if (CanGenerate(i, j, grid, a, dir) == true)
//                                {
//                                    PlaceBuilding(a, i, j);
//                                    break;
//                                }
//                                else
//                                {
//                                    a--;
//                                    if (a == 3)
//                                    {
//                                        a--;
//                                    }
//                                }
//                            }
//                        }
//                        else
//                        {
//                            PlaceBuilding(1, i, j);

//                        }

//                    }


//                }



//            }
//        }

//    }
//    public void PlaceBuilding(int size, int i, int j)
//    {

//        if (size == 1)
//        {
//            GameObject building = Instantiate(spawners1x1[0]);
//            building.transform.parent = transform;
//            building.transform.position = new Vector3(transform.position.x - Size / 2 + j * 10, transform.position.y + Size / 2 - i * 10, 0);
//            foreach (ObjectSpawner spawn in building.GetComponentsInChildren<ObjectSpawner>())
//            {
//                //spawn.parent = this;
//            }
//        }
//        else if (size == 2)
//        {
//            GameObject building = Instantiate(spawners2x2[0]);
//            building.transform.parent = transform;
//            building.transform.position = new Vector3(transform.position.x - Size / 2 + j * 10, transform.position.y + Size / 2 - i * 10, 0);
//            building.transform.position += new Vector3(-5, 5, 0);
//            foreach (ObjectSpawner spawn in building.GetComponentsInChildren<ObjectSpawner>())
//            {
//                //spawn.parent = this;
//            }
//        }
//        else if (size == 4)
//        {
//            GameObject building = Instantiate(spawners4x4[0]);
//            building.transform.parent = transform;
//            building.transform.position = new Vector3(transform.position.x - Size / 2 + j * 10, transform.position.y + Size / 2 - i * 10, 0);
//            building.transform.position += new Vector3(-15, 15, 0);
//            foreach (ObjectSpawner spawn in building.GetComponentsInChildren<ObjectSpawner>())
//            {
//                //spawn.parent = this;
//            }
//        }
//        else if (size == 8)
//        {
//            GameObject building = Instantiate(spawners8x8[0]);
//            building.transform.parent = transform;
//            building.transform.position = new Vector3(transform.position.x - Size / 2 + j * 10, transform.position.y + Size / 2 - i * 10, 0);
//            building.transform.position += new Vector3(-35, 35, 0);
//            foreach (ObjectSpawner spawn in building.GetComponentsInChildren<ObjectSpawner>())
//            {
//               // spawn.parent = this;
//            }
//        }

//    }

//    public bool CanGenerate(int x, int y, int[][] grid, int size, int dir)
//    {
//        if (dir == 0)
//        {
//            if (x - size + 1 >= 0 && y - size + 1 >= 0)
//            {
//                for (int i = 0; i < size; i++)
//                {
//                    for (int j = 0; j < size; j++)
//                    {
//                        if (grid[x - i][y - j] == 1 || grid[x - i][y - j] == -1 || grid[x - i][y - j] > 5)
//                        {
//                            return false;
//                        }

//                    }
//                }
//            }
//            else
//            {
//                return false;
//            }
//            for (int i = 0; i < size; i++)
//            {
//                for (int j = 0; j < size; j++)
//                {
//                    grid[x - i][y - j] = -1;
//                }
//            }
//        }
//        else if (dir == 1)
//        {
//            if (x - size + 1 >= 0 && y + size - 1 < grid[x].Length)
//            {
//                for (int i = 0; i < size; i++)
//                {
//                    for (int j = 0; j < size; j++)
//                    {
//                        if (grid[x + i][y - j] == 1 || grid[x + i][y - j] == -1 || grid[x + i][y - j] > 5)
//                        {
//                            return false;
//                        }

//                    }
//                }
//            }
//            else
//            {

//                return false;
//            }
//            for (int i = 0; i < size; i++)
//            {
//                for (int j = 0; j < size; j++)
//                {
//                    grid[x + i][y - j] = -1;
//                }
//            }
//        }
//        return true;
//    }
//    void Update()
//    {

//    }
//}
