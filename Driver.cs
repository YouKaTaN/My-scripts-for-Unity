using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private float driving;
    private void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        StartCoroutine(DrivingIE());
    }

    private IEnumerator DrivingIE()
    {
        for (int i = 0; i < 20; i++)
        {
            driving += 5f;
            skinnedMeshRenderer.SetBlendShapeWeight(0, driving);
            yield return null;  
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 20; i++)
        {
            driving -= 5f;
            skinnedMeshRenderer.SetBlendShapeWeight(0, driving);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DrivingIE());
    }
}
