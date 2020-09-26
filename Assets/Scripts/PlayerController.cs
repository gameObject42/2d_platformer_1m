using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    private float moveInput;

    public bool facingRigt = true;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRigt == false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRigt == true && moveInput < 0)
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingRigt = !facingRigt;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
