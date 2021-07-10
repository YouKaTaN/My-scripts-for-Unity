using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public float upForce;
    public float flowForce;

    private float waterSpeed = 0.2f;
    private Renderer rend;
    [SerializeField] private bool river = true;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        float offset = Time.time * waterSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WheelCollider>() != null)
        {
            other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<CarController>() != null)
        {
            other.attachedRigidbody.AddForce(Vector3.up * upForce);
            if(river == true)
            {
                other.attachedRigidbody.AddForce(Vector3.right * flowForce);
            }
        }
    }
}
