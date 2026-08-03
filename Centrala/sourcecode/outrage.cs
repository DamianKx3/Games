using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class outrage : MonoBehaviour
{
    float timer;
    public float toflash;
    public Light flash;
    float l;
    public GameObject comp2;
    public AudioSource thunder;
    void Start()
    {
        l = flash.intensity;
        flash.intensity = 0;
        if (Data.powerOut == false)
        {
            gameObject.SetActive(false);
            this.enabled = false;
            return;
        }
        comp2.SetActive(false);
        //oprocz wizualnych zmian zrobic tez aby drugi komputer nie dizalal a pierwszy zastapic laptopem
        //zrobic tak podczas tego poziomu kononowicz idzie do wyjscia, obserwujac go zostaje spowolniony
        //trzeba przetrwac na czas jak we fnafie
    }

    void Update()
    {
        timer = timer + Time.deltaTime;
        if(timer > toflash)
        {
            timer = 0;
            flash.intensity = l;
            toflash = Random.Range(8f, 20f);
            thunder.Play();
            StartCoroutine(secstrike());

        }
        if (flash.intensity > 0)
        {
            flash.intensity = flash.intensity - Time.deltaTime * 100;
        }
    }
    IEnumerator secstrike()
    {
        yield return new WaitForSeconds(0.8f);
        flash.intensity = l;
    }
}
