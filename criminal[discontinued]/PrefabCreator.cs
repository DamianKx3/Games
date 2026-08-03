using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class PrefabCreator : MonoBehaviour
{
    public Vector2 mousepos;
    public GameObject[] Blocks;
    public int selectedblock { get; set; }
    public GameObject cursor;
    public Sprite DelCur;
    public Sprite fillcur;
    public Sprite DelfillCur;
    public int layer;
    public int Dir { get; set; }
    public int mode { get; set; }
    public GameObject StructTemp;
    public GameObject EntityGroup;

    public TextMeshProUGUI layerText;
    public TextMeshProUGUI DirText;

    public SaveSystem SaveSystem;

    int filltemp;
    public Vector3 marker1;
    public Vector3 marker2;
    public bool hollow;
    public Toggle hollowset;
    public GameObject Deleter;
    public GameObject basepos;

    public TextMeshProUGUI Debugtext;
    int blockcount;
    public GameObject blockEditedNow;
    public int Action { get; set; }
    public Slider R;
    public Slider G;
    public Slider B;
    string colorString;
    public Image Colorimage;
    public TextMeshProUGUI DirtextEdit;
    public TextMeshProUGUI layertextEdit;
    public Toggle dontreplace;
    public bool dontreplacebool;

    public List<string> colorpresets;
    public int selectedpreset;
    public TextMeshProUGUI presetnum;
    public Image presetcolor;
    public TextMeshProUGUI ISblockevenfuckingselected;
    void Start()
    {

        layer= 0;
        SaveSystem = FindFirstObjectByType<SaveSystem>();
        if(Data.Editor != true)
        {
            gameObject.SetActive(false);
        }
        colorString = "1.1.1";
    }

    // Update is called once per frame
    void Update()
    {
        //edit
        if(mode == 5)
        {
            colorString = R.value + "." + G.value + "." + B.value;
            if (colorpresets.Count > 0 && selectedpreset >= 0)
            {
                presetnum.text = "color preset: " + selectedpreset;
                presetcolor.color = new Color(float.Parse(colorpresets[selectedpreset].Split('.')[0]), float.Parse(colorpresets[selectedpreset].Split('.')[1]), float.Parse(colorpresets[selectedpreset].Split('.')[2]), 1);
            }
            else
            {
                presetnum.text = "color preset: " + "empty :c";
                presetcolor.color = new Color(0, 0, 0, 0);
            }

            if(blockEditedNow == null)
            {
                ISblockevenfuckingselected.text = "click to select block";
            }
            else
            {
                ISblockevenfuckingselected.text = "Selected Block  id: " + blockEditedNow.GetComponent<Blocks>().ID + " pos:  " + blockEditedNow.transform.position;
            }
        }

        Colorimage.color = new Color(float.Parse(colorString.Split('.')[0]), float.Parse(colorString.Split('.')[1]), float.Parse(colorString.Split('.')[2]));
        if (blockEditedNow != null) 
        {
            switch (blockEditedNow.GetComponent<Blocks>().dir)
            {
                case 0:
                    DirtextEdit.text = "gravity joint: \n down";
                    break;
                case 1:
                    DirtextEdit.text = "gravity joint: \n right";
                    break;
                case 2:
                    DirtextEdit.text = "gravity joint: \n left";
                    break;
            }
            layertextEdit.text = "layer: " + blockEditedNow.GetComponentInChildren<SpriteRenderer>().sortingOrder;
            
            if(blockEditedNow.GetComponentInChildren<SpriteRenderer>().sortingOrder > -1)
            {
                blockEditedNow.GetComponentInChildren<SpriteRenderer>().color = new Color(float.Parse(colorString.Split('.')[0]), float.Parse(colorString.Split('.')[1]), float.Parse(colorString.Split('.')[2]));
            }
            else
            {
                blockEditedNow.GetComponentInChildren<SpriteRenderer>().color = new Color(float.Parse(colorString.Split('.')[0]) - 0.4f, float.Parse(colorString.Split('.')[1]) - 0.4f, float.Parse(colorString.Split('.')[2]) - 0.4f);
            }
            


        }
        else
        {
            switch (Dir)
            {
                case 0:
                    DirtextEdit.text = "gravity joint: \n down";
                    break;
                case 1:
                    DirtextEdit.text = "gravity joint: \n right";
                    break;
                case 2:
                    DirtextEdit.text = "gravity joint: \n left";
                    break;
            }
            layertextEdit.text = "layer: " + layer;
        }
        if (dontreplace.isOn)
        {
            dontreplacebool = true;
        }
        else
        {
            dontreplacebool = false;
        }
        //wazne gowno
        hollow = hollowset.isOn;
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //gravity hjoint tylko napis
        switch (Dir)
        {
            case 0:
                DirText.text = "gravity joint: \n down";
                break;
            case 1:
                DirText.text = "gravity joint: \n right";
                break;
            case 2:
                DirText.text = "gravity joint: \n left";
                break;
        }
      
        //selecting
        if(Input.GetKeyDown(KeyCode.Q))
        {
            selectedblock--;
            if(selectedblock < 0)
            {
                selectedblock = Blocks.Length - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedblock++;
            if(selectedblock > Blocks.Length -1)
            {
                selectedblock = 0;
            }
        }
        //layer
        if(layer > -1)
        {
            layerText.text = "layer " + layer;
        }
        else
        {
            
            layerText.text = "background " + Mathf.Abs(layer);
        }

        //raycast
        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector3.forward);
        
        //offplace
        if(!Input.GetKey(KeyCode.LeftControl))
        {
            mousepos = new Vector2(Mathf.Round(mousepos.x/1) * 1, Mathf.Round(mousepos.y / 1) * 1);
        }

        //cursor
        cursor.transform.position = mousepos;
        if(mode == 0)
        {
            blockEditedNow = null;
            if (dontreplacebool == true)
            {
                colorString = "1.1.1";
                Debug.Log(colorString);
            }
            else
            {
                Debug.Log(colorString + "d");
            }
            cursor.GetComponent<SpriteRenderer>().sprite = Blocks[selectedblock].GetComponentInChildren<SpriteRenderer>().sprite;
            if (Blocks[selectedblock].GetComponentInChildren<SpriteRenderer>().flipX == true)
            {
                cursor.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                cursor.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (Blocks[selectedblock].GetComponentInChildren<SpriteRenderer>().flipY == true)
            {
                cursor.GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                cursor.GetComponent<SpriteRenderer>().flipY = false;
            }
        }
        //Debugtext
        Debugtext.text = "BlockID: " + selectedblock + "\n" + "blocksCount: " + FindObjectsOfType<Blocks>().Length + " (" + FindObjectsOfType<StructureBlocks>().Length + " entity blocks)" + "\n" + "mousepos: " + mousepos;



        //place
        if (hit.collider != null && hit.collider.tag == "UI")
        {
            //konczy klatke gdy dotyka UI
            return;
        }
        if(mode == 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetMouseButton(0) && hit.collider == null)
                {
                    EditorBlock();
                }
            }
            else
            {
               

                //STAWIANIE
                if (Input.GetMouseButtonDown(0))
                {
                    EditorBlock();
                }


            }
        }

        if(mode == 1)
        {
            //usuwanie
            cursor.GetComponent<SpriteRenderer>().sprite = DelCur;
            if (hit.collider != null)
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetMouseButton(0) && hit.collider.transform.parent != null && hit.collider.transform.parent.tag == "Block")
                    {
                        Destroy(hit.collider.transform.parent.gameObject);
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0) && hit.collider.transform.parent != null && hit.collider.transform.parent.tag == "Block")
                    {
                        Destroy(hit.collider.transform.parent.gameObject);
                    }
                }


            }
        }

        if(mode == 2)
        {
            cursor.GetComponent<SpriteRenderer>().sprite = null;
        }
        //Fill
        if(mode == 3)
        {
            cursor.GetComponent<SpriteRenderer>().sprite = fillcur;
            if (filltemp == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    marker1 = mousepos;
                    filltemp++;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    marker2 = mousepos;
                    filltemp = 0;
                    for (int i = (int)Mathf.Min(marker1.x,marker2.x); i < Mathf.Abs(marker1.x-marker2.x) + (int)Mathf.Min(marker1.x, marker2.x) + 1; i++)
                    {
                        for (int j = (int)Mathf.Min(marker1.y, marker2.y); j < Mathf.Abs(marker1.y - marker2.y) + (int)Mathf.Min(marker1.y, marker2.y) + 1; j++)
                        {
                            if (hollow == false)
                            {
                                FillBlock(i, j);
                            }
                            else
                            {
                                if (i == (int)Mathf.Min(marker1.x, marker2.x))
                                {
                                    FillBlock(i, j);
                                }
                                if(j == (int)Mathf.Min(marker1.y, marker2.y))
                                {
                                    FillBlock(i, j);

                                }
                                if (i == Mathf.Abs(marker1.x - marker2.x) + (int)Mathf.Min(marker1.x, marker2.x))
                                {
                                    FillBlock(i, j);
                                }
                                if (j == Mathf.Abs(marker1.y - marker2.y) + (int)Mathf.Min(marker1.y, marker2.y))
                                {
                                    FillBlock(i, j);
                                }
                            }
                            
                        }
                    }
                    mode = 0;
                }
            }
            

        }
        //deletefill
        if (mode == 4)
        {
            cursor.GetComponent<SpriteRenderer>().sprite = DelfillCur;
            if (filltemp == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    marker1 = mousepos;
                    filltemp++;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    marker2 = mousepos;
                    filltemp = 0;
                    //Do zrobienia
                    GameObject del = GameObject.Instantiate(Deleter,new Vector3((Mathf.Min(marker1.x, marker2.x) + Mathf.Max(marker1.x, marker2.x)) / 2, (Mathf.Min(marker1.y, marker2.y)  + Mathf.Max(marker1.y, marker2.y)) / 2, 0),Quaternion.identity);
                    del.transform.localScale = new Vector3(Mathf.Abs(marker1.x - marker2.x), Mathf.Abs(marker1.y - marker2.y), 1);
                    
                    mode = 1;
                }
            }


        }
        if (mode == 5)
        {
            cursor.GetComponent<SpriteRenderer>().sprite = null;
            RaycastHit2D hitEdit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            if(Input.GetMouseButton(0) && hitEdit.collider != null && hitEdit.collider.transform.parent.gameObject.GetComponent<Blocks>())
            {
                //Debug.Log("Blok");
                blockEditedNow = hitEdit.collider.transform.parent.gameObject;
            }
            if (Input.GetMouseButton(0) && hitEdit.collider == null)
            {
                //Debug.Log("Blok");
                blockEditedNow = null;
            }
            if (Input.GetMouseButton(1))
            {
                //Debug.Log("Blok");
                blockEditedNow = null;
            }
        }
        else
        {
            blockEditedNow = null;
        }
        if (mode == 3 && Input.GetMouseButton(1))
        {
            mode = 0;
        }
        if(mode == 4 && Input.GetMouseButton(1))
        {
            mode = 1;
        }
        
    }
   
    public void nextlayer()
    {
        layer++;
        if(layer > 5)
        {
            layer = 5;
        }
    }
    public void prevlayer()
    {
        layer--;
        if(layer < -3)
        {
            layer = -3;
        }
    }
    //zakladka edit
    public void EditA()
    {
        if(mode == 5)
        {
            if (blockEditedNow != null)
            {
                switch (Action)
                {
                    case 0:
                        blockEditedNow.transform.position = blockEditedNow.transform.position + new Vector3(0, 1, 0);
                        break;
                    case 1:
                        blockEditedNow.transform.position = blockEditedNow.transform.position + new Vector3(0, -1, 0);
                        break;
                    case 2:
                        blockEditedNow.transform.position = blockEditedNow.transform.position + new Vector3(1, 0, 0);
                        break;
                    case 3:
                        blockEditedNow.transform.position = blockEditedNow.transform.position + new Vector3(-1, 0, 0);
                        break;

                    case 4:
                        blockEditedNow.transform.position = blockEditedNow.transform.position + new Vector3(0, 0.1f, 0);
                        break;
                    case 5:
                        blockEditedNow.transform.position = blockEditedNow.transform.position + new Vector3(0, -0.1f, 0);
                        break;
                    case 6:
                        blockEditedNow.transform.position = blockEditedNow.transform.position + new Vector3(0.1f, 0, 0);
                        break;
                    case 7:
                        blockEditedNow.transform.position = blockEditedNow.transform.position + new Vector3(-0.1f, 0, 0);
                        break;
                    case 8:
                        blockEditedNow.GetComponent<Blocks>().dir = 0;
                        Dir = 0;
                        break;
                    case 9:
                        blockEditedNow.GetComponent<Blocks>().dir = 2;
                        Dir = 2;
                        break;
                    case 10:
                        blockEditedNow.GetComponent<Blocks>().dir = 1;
                        Dir = 1;
                        break;
                    case 11:
                        blockEditedNow.GetComponentInChildren<SpriteRenderer>().sortingOrder--;
                        layer--;
                        if (layer < -3)
                        {
                            layer = -3;
                        }
                        if (blockEditedNow.GetComponentInChildren<SpriteRenderer>().sortingOrder < -3)
                        {
                            blockEditedNow.GetComponentInChildren<SpriteRenderer>().sortingOrder = -3;
                        }
                        if (blockEditedNow.GetComponentInChildren<SpriteRenderer>().sortingOrder < 0)
                        {
                            blockEditedNow.GetComponentInChildren<SpriteRenderer>().color = new Color(float.Parse(colorString.Split('.')[0]), float.Parse(colorString.Split('.')[1]), float.Parse(colorString.Split('.')[2])) - new Color(0.4f, 0.4f, 0.4f, 1);
                            blockEditedNow.GetComponent<Blocks>().isBg = true;


                        }
                        break;
                    case 12:
                        blockEditedNow.GetComponentInChildren<SpriteRenderer>().sortingOrder++;
                        layer++;
                        if (layer > 5)
                        {
                            layer = 5;
                        }
                        if (blockEditedNow.GetComponentInChildren<SpriteRenderer>().sortingOrder > 5)
                        {
                            blockEditedNow.GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;
                        }
                        if (blockEditedNow.GetComponentInChildren<SpriteRenderer>().sortingOrder > -1)
                        {

                            blockEditedNow.GetComponentInChildren<SpriteRenderer>().color = new Color(float.Parse(colorString.Split('.')[0]), float.Parse(colorString.Split('.')[1]), float.Parse(colorString.Split('.')[2]));
                            blockEditedNow.GetComponent<Blocks>().isBg = true;


                        }
                        break;
                    case 13:
                        selectedpreset++;
                        if (selectedpreset > colorpresets.Count - 1)
                        {
                            selectedpreset = 0;
                        }
                        break;
                    case 14:
                        selectedpreset--;
                        if (selectedpreset < 0)
                        {
                            selectedpreset = colorpresets.Count - 1;
                        }
                        break;
                    case 15:
                        if (colorpresets.Count > 0)
                        {
                            colorString = colorpresets[selectedpreset];
                            R.value = float.Parse(colorpresets[selectedpreset].Split('.')[0]);
                            G.value = float.Parse(colorpresets[selectedpreset].Split('.')[1]);
                            B.value = float.Parse(colorpresets[selectedpreset].Split('.')[2]);
                        }

                        break;
                    case 16:
                        colorpresets.Add(colorString);
                        break;
                    case 17:
                        if (colorpresets.Count > 0)
                        {
                            colorpresets.RemoveAt(selectedpreset);
                            selectedpreset--;
                        }
                        break;
                    case 18:
                        blockEditedNow = null;
                        break;
                }
            }
            else
            {
                switch (Action)
                {


                    case 9:
                        Dir = 2;
                        break;
                    case 10:
                        Dir = 1;
                        break;
                    case 11:
                        layer--;
                        if (layer < -3)
                        {
                            layer = -3;
                        }

                        break;
                    case 12:

                        layer++;
                        if (layer > 5)
                        {
                            layer = 5;
                        }
                        break;
                    case 13:
                        selectedpreset++;
                        if (selectedpreset > colorpresets.Count - 1)
                        {
                            selectedpreset = 0;
                        }
                        break;
                    case 14:
                        selectedpreset--;
                        if (selectedpreset < 0)
                        {
                            selectedpreset = colorpresets.Count - 1;
                        }
                        break;
                    case 15:
                        if (colorpresets.Count > 0)
                        {
                            colorString = colorpresets[selectedpreset];
                            R.value = float.Parse(colorpresets[selectedpreset].Split('.')[0]);
                            G.value = float.Parse(colorpresets[selectedpreset].Split('.')[1]);
                            B.value = float.Parse(colorpresets[selectedpreset].Split('.')[2]);
                        }

                        break;
                    case 16:
                        colorpresets.Add(colorString);
                        break;
                    case 17:
                        if (colorpresets.Count > 0)
                        {
                            colorpresets.RemoveAt(selectedpreset);
                            selectedpreset--;
                        }
                        break;
                    case 18:
                        blockEditedNow = null;
                        break;
                }
            }
        }
    }
    //place Functions
    public void EditorBlock()
    {
        GameObject block = Instantiate(Blocks[selectedblock], mousepos, Quaternion.identity);
        blockcount++;
        block.GetComponentInChildren<SpriteRenderer>().sortingOrder = layer;
        block.GetComponent<Blocks>().ID = selectedblock;
        block.GetComponent<Blocks>().dir = Dir;
        block.GetComponentInChildren<SpriteRenderer>().color = new Color(float.Parse(colorString.Split('.')[0]), float.Parse(colorString.Split('.')[1]), float.Parse(colorString.Split('.')[2]));
        if (layer < 0)
        {
            block.GetComponentInChildren<BoxCollider2D>().isTrigger = true;
            block.GetComponentInChildren<SpriteRenderer>().color = new Color(float.Parse(colorString.Split('.')[0]) - 0.4f, float.Parse(colorString.Split('.')[1]) - 0.4f, float.Parse(colorString.Split('.')[2]) - 0.4f);
            block.GetComponent<Blocks>().isBg = true;


        }
    }
    public void FillBlock(int i, int j)
    {
        GameObject block = Instantiate(Blocks[selectedblock], new Vector3(i, j, 0), Quaternion.identity);
        blockcount++;
        block.GetComponentInChildren<SpriteRenderer>().sortingOrder = layer;
        block.GetComponent<Blocks>().ID = selectedblock;
        block.GetComponentInChildren<SpriteRenderer>().color = new Color(float.Parse(colorString.Split('.')[0]), float.Parse(colorString.Split('.')[1]), float.Parse(colorString.Split('.')[2]));
        if (layer < 0)
        {
            block.GetComponentInChildren<BoxCollider2D>().isTrigger = true;
            block.GetComponentInChildren<SpriteRenderer>().color = new Color(float.Parse(colorString.Split('.')[0]) -0.4f, float.Parse(colorString.Split('.')[1]) - 0.4f, float.Parse(colorString.Split('.')[2]) - 0.4f);
            block.GetComponent<Blocks>().isBg = true;


        }
    }
    public void LvlLoad()
    {
        SaveSystem = FindFirstObjectByType<SaveSystem>();
        for (int i = 0; i < SaveSystem.BlockID.Count; i++)
        {
            GameObject block = GameObject.Instantiate(Blocks[SaveSystem.BlockID[i]],new Vector3(SaveSystem.X[i], SaveSystem.Y[i], SaveSystem.Z[i]),Quaternion.identity );
            blockcount++;
            block.GetComponent<Blocks>().ID = SaveSystem.BlockID[i];
            if (SaveSystem.Dir.Count > 0)
            {
                block.GetComponent<Blocks>().dir = SaveSystem.Dir[i];
            }
            if (SaveSystem.BlockID[i] != 21)
            {
                //entityblock
                SpriteRenderer Sp = block.GetComponentInChildren<SpriteRenderer>();
                if(Sp != null)
                {
                    Sp.sortingOrder = SaveSystem.layer[i];
                    if(SaveSystem.Bcolors.Count > 0)
                    {
                        Sp.color = new Color(float.Parse(SaveSystem.Bcolors[i].Split('.')[0]), float.Parse(SaveSystem.Bcolors[i].Split('.')[1]), float.Parse(SaveSystem.Bcolors[i].Split('.')[2]));
                    }
                    else
                    {
                        Sp.color = new Color(1,1,1);
                    }
                    
                    if (SaveSystem.layer[i] < 0)
                    {
                        block.GetComponentInChildren<BoxCollider2D>().isTrigger = true;
                        block.GetComponentInChildren<SpriteRenderer>().color = block.GetComponentInChildren<SpriteRenderer>().color - new Color(0.4f, 0.4f, 0.4f, 0);
                        block.GetComponent<Blocks>().isBg = true;



                    }
                }

                
            }
            

        }
    }
    public void FillButton()
    {
        if(mode == 0)
        {
            mode = 3;
        }
        if(mode == 1)
        {
            mode = 4;
        }

    } 
    public void Basepos()
    {
        if(mode == 2)
        {
            StartCoroutine(Basepos2());
        }
    }
    public IEnumerator Basepos2()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        basepos.transform.position = mousepos;
        FindFirstObjectByType<SaveSystem>().basepos[0] = mousepos.x;
        FindFirstObjectByType<SaveSystem>().basepos[1] = mousepos.y;
    }
}
