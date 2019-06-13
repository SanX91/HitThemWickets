using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMeterUI : MonoBehaviour
{
    public float speed = 50;
    public float maxAngle = 30;
    public RectTransform arrow;

    float angle;

    private void Start()
    {
        StartCoroutine(MeterCycle());
    }

    IEnumerator MeterCycle()
    {
        float dir = Random.Range(0, 1) == 0?-1:1;
        while(true)
        {
            angle = Vector3.SignedAngle(Vector3.up, arrow.up, Vector3.forward);
            arrow.Rotate(Vector3.forward, speed * Time.deltaTime * dir);
            if(Mathf.Abs(angle)>=maxAngle
                &&Mathf.Sign(angle)==dir)
            {
                arrow.rotation = Quaternion.Euler(0, 0, maxAngle * dir);
                dir *= -1;
            }

            yield return null;
        }
    }

    public void ToggleMeter(bool isActive)
    {
        if(isActive)
        {
            StartCoroutine(MeterCycle());
        }

        gameObject.SetActive(isActive);
    }
}
