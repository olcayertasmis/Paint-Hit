using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Rigidbody rb;

    public void Thrown(float ballSpeed)
    {
        rb.AddForce(Vector3.forward * ballSpeed, ForceMode.Impulse);
    }

    public void ChangeColor(Color color)
    {
        meshRenderer.material.color = color;
    }
}
