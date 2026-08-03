using UnityEngine;

public class ToggleMode : MonoBehaviour
{
    public bool FinalMode;

    void Start()
    {
        if(FinalMode == true)
        {
            if(Data.FinalMode == true)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (Data.FinalMode == true)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }


}
