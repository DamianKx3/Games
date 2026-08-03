using UnityEngine;

public class SkipDpx : MonoBehaviour
{
    public GameObject Canv;
    public GameObject animCanv;

    void Start()
    {

        if(Data.showDPXstudios == false)
        {
            Data.showDPXstudios = true;
        }
        else
        {
            GetComponent<Animator>().enabled = false;
            Canv.SetActive(true);
            animCanv.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
