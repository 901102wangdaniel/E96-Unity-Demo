using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    private Rigidbody rb;
    bool isGrounded = false;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpheight = 5f;
    Vector2 direction = Vector2.zero;

    private bool isDashing;
    [SerializeField] private float dashingSpeed = 100f;
    private float dashingTime = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue value)
    {
        Vector2 direction = value.Get<Vector2>();
        Debug.Log(direction);
        this.direction = direction;

        // float movementX = this.direction.x;
        // float movementY = this.direction.y;

    }
    void Update()
    {
            Move(direction.x, direction.y);
    }

    private void Move(float x, float z)
    {
        if (isDashing) {
            rb.velocity = new Vector3(x * dashingSpeed, rb.velocity.y, z * dashingSpeed);
        } else {
            rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
        }
        
    }



    void OnJump()
    {
        if (isGrounded) 
        {
            Jump();
        }
        
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpheight, rb.velocity.z);
    }
    void OnCollisionExit(Collision collision) 
    {
        isGrounded = false;
    }
    void OnCollisionStay(Collision collision)
    {
        if (Vector3.Angle(collision.GetContact(0).normal, Vector3.up) < 45f)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    IEnumerator OnDash() 
    {
        isDashing = true;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
    }
    

}
