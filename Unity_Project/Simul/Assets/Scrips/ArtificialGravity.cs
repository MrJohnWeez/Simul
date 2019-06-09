using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Allows a cube to have super gravity </summary>
public class ArtificialGravity : MonoBehaviour
{
    private float gravity = -16;
    private Rigidbody rb = null;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Add super heavy gravity
        rb.AddRelativeForce(Vector3.down * -gravity, ForceMode.Acceleration);
    }
}
