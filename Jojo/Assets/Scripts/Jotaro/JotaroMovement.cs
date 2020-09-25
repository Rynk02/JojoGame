using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JotaroMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;
    float mx;
    public Animator jotaroAnim;

    public float jumpForce;
    public Transform feet;
    public LayerMask groundLayers;

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Jump();
        }

        if(Mathf.Abs(mx) > 0.05)
        {
            jotaroAnim.SetBool("isRunning", true);
        }
        else
        {
            jotaroAnim.SetBool("isRunning", false);
        }

        if (mx > 0)
        {
            transform.localScale = new Vector3(3f, 3f, 3f);
        }
        else if(mx < 0 )
        {
            transform.localScale = new Vector3(-3f, 3f, 3f);
        }

        jotaroAnim.SetBool("isGrounded", isGrounded());
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);

        rb.velocity = movement;
    }

    void Jump()
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }

    public bool isGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        if (groundCheck != null)
        {
            return true;
        }
        return false;
    }
}
