using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Make a player move around inside a cube like area (walk on walls) </summary>
public class PlayerController2 : MonoBehaviour
{
    public LayerMask toHitGround;
    public float moveSpeed = 7000;
    public float rotationSpeed = 100;
    public float gravityRotationSmoothing = 0.1f;
    public bool isActive = false;

    private Rigidbody rb = null;
    private float gravity = -20;
    private float moveRot = 0;
    private float movePos = 0;
    private Quaternion oldRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldRotation = transform.rotation;
    }

    private void Update()
    {
        moveRot = Input.GetAxisRaw("Horizontal");
        movePos = Input.GetAxisRaw("Vertical");
        oldRotation = transform.rotation;
        UpdatePlayerNormal();
    }
    
    private void FixedUpdate() {
        if(isActive)
        {
            // Update position and rotation of player
            rb.AddRelativeTorque(Vector3.up * moveRot * rotationSpeed, ForceMode.Force);
            rb.AddRelativeForce(Vector3.forward * movePos * moveSpeed, ForceMode.Force);
        }
        rb.AddRelativeForce(Vector3.down * -gravity, ForceMode.Acceleration);
    }

    /// <summary> Determine if the user is about to fall off a edge and update thier normal to match
    /// surrounding faces </summary>
    private void UpdatePlayerNormal()
    {
        
        RaycastHit hit;
        if(Physics.Raycast((transform.position + transform.up * 0.25f) - transform.forward * 0.1f, transform.forward, out hit, 0.6f, toHitGround))
        {
            // Plane is under user
            CalculateNewRotation(hit.normal);
        }
        else if(Physics.Raycast(transform.position + new Vector3(0,0.1f,0), -transform.up, out hit, 1f, toHitGround))
        {
            CalculateNewRotation(hit.normal);
        }
        
    }

    /// <summary> Rotate the player so that the player's up vector matches with the normal given
    /// also mostly keeping the original forward direction </summary>
    private void CalculateNewRotation(Vector3 hitNormal)
    {
        Vector3 myForward = Vector3.Cross(transform.right,hitNormal);
        Quaternion targetRot = Quaternion.LookRotation(myForward, hitNormal);
        transform.rotation = Quaternion.Lerp(targetRot, oldRotation, gravityRotationSmoothing);
    }
}
