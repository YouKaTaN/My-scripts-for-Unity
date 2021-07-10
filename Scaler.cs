using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    private Coroutine ScaleCorotine;
    void Start()
    {
        ScaleCorotine = StartCoroutine(ScalerIE());
    }
    private IEnumerator ScalerIE()
    {
        for (int i = 0; i < 40; i++)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0);
            yield return null;
        }
        for (int i = 0; i < 40; i++)
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            yield return null;
        }
        StartCoroutine(ScalerIE());
    }
}
