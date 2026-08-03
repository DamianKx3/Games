using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camformove : MonoBehaviour
{
    public Camera[] cameras;
    public IEnumerator move0()
    {
        cameras[0].farClipPlane = 1 ;
        yield return new WaitForSeconds(0.2f);
        cameras[0].farClipPlane = 150;
    }
    public IEnumerator move1()
    {
        cameras[1].farClipPlane = 1;
        yield return new WaitForSeconds(0.2f);
        cameras[1].farClipPlane = 150;
    }
    public IEnumerator move2()
    {
        cameras[2].farClipPlane = 1;
        yield return new WaitForSeconds(0.2f);
        cameras[2].farClipPlane = 150;
    }
    public IEnumerator move3()
    {
        cameras[3].farClipPlane = 1;
        yield return new WaitForSeconds(0.2f);
        cameras[3].farClipPlane = 150;
    }
    public IEnumerator move4()
    {
        cameras[4].farClipPlane = 1;
        yield return new WaitForSeconds(0.2f);
        cameras[4].farClipPlane = 150;

    }
    public IEnumerator move5()
    {
        cameras[5].farClipPlane = 1;
        yield return new WaitForSeconds(0.2f);
        cameras[5].farClipPlane = 150;
    }
    public IEnumerator move6()
    {
        cameras[6].farClipPlane = 1;
        yield return new WaitForSeconds(0.2f);
        cameras[6].farClipPlane = 150;
    }
}
