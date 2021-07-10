using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Mashroom : MonoBehaviour
{
    public static event Action MashroomDestroyed;
    public int id;
 
    [SerializeField] private GameObject SwapBrokenObject;
    [SerializeField] private float timeRemaining = 5;

    private void Start()
    {
        SwapBrokenObject.GetComponent<Rigidbody>();
    }
    private void SwapBrokenObj()
    {
        StartCoroutine(SwapBrokenObjIE());
    }
    private IEnumerator SwapBrokenObjIE()
    {
        yield return new WaitForSeconds(10);
        Destroy(obj: SwapBrokenObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<CarController>() || other.gameObject.GetComponent<TankController>())
        {
            GameObject Oskolki = Instantiate(SwapBrokenObject, transform.position, transform.rotation) as GameObject;
            Destroy(Oskolki, timeRemaining);
            SwapBrokenObj();
            MashroomDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
