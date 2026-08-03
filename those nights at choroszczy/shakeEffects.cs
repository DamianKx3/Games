using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeEffects : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        float power = 0.2f;
        Vector3 deafult = transform.position;
        Quaternion deafultrot = transform.rotation;
        while (true)
        {
            float x = Random.Range(-0.5f, 0.5f) * power;
            float y = Random.Range(-0.5f, 0.5f) * power;
            float rx = Random.Range(-50f, 50f) * power;
            float ry = Random.Range(-50f, 50f) * power;
            float rz = Random.Range(-50f, 50f) * power;
            transform.position = new Vector3(deafult.x + x, deafult.y + y, deafult.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x + rx, transform.eulerAngles.y + ry, transform.eulerAngles.z + rz);
            yield return new WaitForSeconds(0.02f);
            transform.position = deafult;
            transform.rotation = deafultrot;

        }
    }
}
